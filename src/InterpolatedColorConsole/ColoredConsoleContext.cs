using InterpolatedColorConsole.SpecialSymbols;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace InterpolatedColorConsole
{
    /// <summary>
    /// Context that will "save" current console colors, can change the default color, and when disposed it will restore previous colors (it's IDisposable - so use it with "using" block)
    /// </summary>
    internal class ColoredConsoleContext : IColoredConsoleContext, IDisposable
    {
        private ConsoleColor _previousForegroundColor;
        private ConsoleColor _previousBackgroundColor;

        #region Tracking of Previous Colors written inline
        internal Stack<ConsoleColor> _previousForegroundColors = new Stack<ConsoleColor>();
        internal Stack<ConsoleColor> _previousBackgroundColors = new Stack<ConsoleColor>();
        #endregion

        internal ColoredConsoleContext(ConsoleColor? newForegroundColor = null, ConsoleColor? newBackgroundColor = null)
        {
            _previousForegroundColor = Console.ForegroundColor;
            _previousBackgroundColor = Console.BackgroundColor;
            if (newForegroundColor.HasValue)
            {
                Console.ForegroundColor = newForegroundColor.Value;
            }
            if (newBackgroundColor.HasValue)
            {
                Console.BackgroundColor = newBackgroundColor.Value;
            }
        }

        /// <summary>
        /// When disposes it will automatically <see cref="RestorePreviousColor"/>
        /// </summary>
        public void Dispose()
        {
            RestorePreviousColor();
        }

        /// <summary>
        /// Restore previous color that was defined before the context was created and changed colors.
        /// </summary>
        public void RestorePreviousColor()
        {
            Console.ForegroundColor = _previousForegroundColor;
            Console.BackgroundColor = _previousBackgroundColor;
        }

        #region Interpolated Strings Parsing and Embedded colors
        internal void WriteInterpolatedWithColors(TextWriter writer, FormattableString value)
        {
            var arguments = value.GetArguments();
            var matches = _formattableArgumentRegex.Matches(value.Format);
            int lastPos = 0;
            for (int i = 0; i < matches.Count; i++)
            {
                // unescape escaped curly braces
                string literal = value.Format.Substring(lastPos, matches[i].Index - lastPos).Replace("{{", "{").Replace("}}", "}");
                lastPos = matches[i].Index + matches[i].Length;
                writer.Write(literal);
                // arguments[i] may not work because same argument can be used multiple times
                int argPos = int.Parse(matches[i].Groups["ArgPos"].Value);
                string argFormat = matches[i].Groups["Format"].Value;
                object arg = arguments[argPos];

                InnerWriteFormattableArgument(writer, arg, argFormat);
            }
            string lastPart = value.Format.Substring(lastPos).Replace("{{", "{").Replace("}}", "}");
            writer.Write(lastPart);
        }
        private void InnerWriteFormattableArgument(TextWriter writer, object arg, string format)
        {
            if (arg == null)
                return;
            if (arg is ConsoleColor && (format == "background" || format == "bg"))
            {
                _previousBackgroundColors.Push(Console.BackgroundColor);
                Console.BackgroundColor = (ConsoleColor)arg;
            }
            else if (arg is ConsoleColor)
            {
                _previousForegroundColors.Push(Console.ForegroundColor);
                Console.ForegroundColor = (ConsoleColor)arg;
            }
            else if (arg is RestorePreviousBackgroundColor)
            {
                if (_previousBackgroundColors.Count > 0)
                    Console.BackgroundColor = _previousBackgroundColors.Pop();
            }
            else if (arg is RestorePreviousColor)
            {
                if (_previousForegroundColors.Count > 0)
                    Console.ForegroundColor = _previousForegroundColors.Pop();
            }
            else if (arg is IFormattable)
                writer.Write(((IFormattable)arg).ToString(format, System.Globalization.CultureInfo.InvariantCulture));
            else
                writer.Write(arg.ToString());
        }

        private static Regex _formattableArgumentRegex = new Regex(
              "{(?<ArgPos>\\d*)(:(?<Format>[^}]*))?}",
            RegexOptions.IgnoreCase
            | RegexOptions.Singleline
            | RegexOptions.CultureInvariant
            | RegexOptions.IgnorePatternWhitespace
            | RegexOptions.Compiled
            );

        #endregion

    }
}
