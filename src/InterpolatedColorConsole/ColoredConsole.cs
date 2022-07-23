using InterpolatedColorConsole.SpecialSymbols;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace InterpolatedColorConsole
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ColoredConsole
    {
        #region Tracking of Previous Colors (only for a SINGLE .Write() call
        private static Stack<ConsoleColor> _previousColors = new Stack<ConsoleColor>();
        private static Stack<ConsoleColor> _previousBackgroundColors = new Stack<ConsoleColor>();
        #endregion

        #region Static Write Methods that don't specify a color but write a FormattableString and will dynamically change colors if the interpolated string has embedded colors
        /// <summary>
        /// Writes to console (stdout) using the specified color(s). The interpolated string may have embedded colors. After writing it restores previous colors.
        /// </summary>
        public static void Write(FormattableString value) { InnerWrite(Console.Out, value); }
        /// <see cref="Write(ConsoleColor, FormattableString)"/>
        public static void WriteLine(FormattableString value) { InnerWrite(Console.Out, value); Console.Out.WriteLine(); }

        /// <summary>
        /// Writes to console error (stderr). The interpolated string may have embedded colors. After writing it restores previous colors.
        /// </summary>
        public static void WriteError(FormattableString value) { InnerWrite(Console.Error, value); }
        /// <see cref="WriteError(ConsoleColor, FormattableString)"/>
        public static void WriteLineError(FormattableString value) { InnerWrite(Console.Error, value); Console.Error.WriteLine(); }
        /// <see cref="WriteError(ConsoleColor, FormattableString)"/>
        #endregion


        #region Static Write Methods that specify a color and write FormattableString
        /// <summary>
        /// Writes to console (stdout) using the specified color(s). The interpolated string may have embedded colors. After writing it restores previous colors.
        /// </summary>
        public static void Write(ConsoleColor foregroundColor, FormattableString value) { InnerWrite(foregroundColor, Console.Out, value); }
        /// <see cref="Write(ConsoleColor, FormattableString)"/>
        public static void WriteLine(ConsoleColor foregroundColor, FormattableString value) { InnerWrite(foregroundColor, Console.Out, value); Console.Out.WriteLine(); }
        /// <see cref="Write(ConsoleColor, FormattableString)"/>
        public static void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, FormattableString value) { InnerWrite(foregroundColor, backgroundColor, Console.Out, value); }
        /// <see cref="Write(ConsoleColor, FormattableString)"/>
        public static void WriteLine(ConsoleColor foregroundColor, ConsoleColor backgroundColor, FormattableString value) { InnerWrite(foregroundColor, backgroundColor, Console.Out, value); Console.Out.WriteLine(); }
        /// <summary>
        /// Writes to console error (stderr) using the specified color(s). The interpolated string may have embedded colors. After writing it restores previous colors.
        /// </summary>
        public static void WriteError(ConsoleColor foregroundColor, FormattableString value) { InnerWrite(foregroundColor, Console.Error, value); }
        /// <see cref="WriteError(ConsoleColor, FormattableString)"/>
        public static void WriteLineError(ConsoleColor foregroundColor, FormattableString value) { InnerWrite(foregroundColor, Console.Error, value); Console.Error.WriteLine(); }
        /// <see cref="WriteError(ConsoleColor, FormattableString)"/>
        public static void WriteError(ConsoleColor foregroundColor, ConsoleColor backgroundColor, FormattableString value) { InnerWrite(foregroundColor, backgroundColor, Console.Error, value); }
        /// <see cref="WriteError(ConsoleColor, FormattableString)"/>
        public static void WriteLineError(ConsoleColor foregroundColor, ConsoleColor backgroundColor, FormattableString value) { InnerWrite(foregroundColor, backgroundColor, Console.Error, value); Console.Error.WriteLine(); }
        #endregion


        #region Static Write Methods that specify a color and write a plain string (through the RawString class which does implicit conversion)
        /// <summary>
        /// Writes to console (stdout) using the specified color(s). After writing it restores previous colors.
        /// </summary>
        public static void Write(ConsoleColor foregroundColor, RawString value) { InnerWrite(foregroundColor, Console.Out, value); }
        /// <see cref="Write(ConsoleColor, RawString)"/>
        public static void WriteLine(ConsoleColor foregroundColor, RawString value) { InnerWrite(foregroundColor, Console.Out, value); Console.Out.WriteLine(); }
        /// <see cref="Write(ConsoleColor, RawString)"/>
        public static void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, RawString value) { InnerWrite(foregroundColor, backgroundColor, Console.Out, value); }
        /// <see cref="Write(ConsoleColor, RawString)"/>
        public static void WriteLine(ConsoleColor foregroundColor, ConsoleColor backgroundColor, RawString value) { InnerWrite(foregroundColor, backgroundColor, Console.Out, value); Console.Out.WriteLine(); }

        /// <summary>
        /// Writes to console error (stderr) using the specified color(s). After writing it restores previous colors.
        /// </summary>
        public static void WriteError(ConsoleColor foregroundColor, RawString value) { InnerWrite(foregroundColor, Console.Error, value); }
        /// <see cref="WriteError(ConsoleColor, RawString)"/>
        public static void WriteLineError(ConsoleColor foregroundColor, RawString value) { InnerWrite(foregroundColor, Console.Error, value); Console.Error.WriteLine(); }
        /// <see cref="WriteError(ConsoleColor, RawString)"/>
        public static void WriteError(ConsoleColor foregroundColor, ConsoleColor backgroundColor, RawString value) { InnerWrite(foregroundColor, backgroundColor, Console.Error, value); }
        /// <see cref="WriteError(ConsoleColor, RawString)"/>
        public static void WriteLineError(ConsoleColor foregroundColor, ConsoleColor backgroundColor, RawString value) { InnerWrite(foregroundColor, backgroundColor, Console.Error, value); Console.Error.WriteLine(); }

        #endregion

        #region Static Write Methods that don't specify a color and write a plain string (through the RawString class which does implicit conversion)
        /// <summary>
        /// Writes to console (stdout) using the specified color(s). After writing it restores previous colors.
        /// </summary>
        public static void Write(RawString value) { InnerWrite(Console.Out, value); }
        /// <see cref="Write(RawString)"/>
        public static void WriteLine(RawString value) { InnerWrite(Console.Out, value); Console.Out.WriteLine(); }

        /// <summary>
        /// Writes to console error (stderr) using the specified color(s). After writing it restores previous colors.
        /// </summary>
        public static void WriteError(RawString value) { InnerWrite(Console.Error, value); }
        /// <see cref="WriteError(RawString)"/>
        public static void WriteLineError(RawString value) { InnerWrite(Console.Error, value); Console.Error.WriteLine(); }
        #endregion

        #region Other non-color-related Console Facades
        /// <inheritdoc cref="Console.WriteLine()"/>
        public static void WriteLine() { Console.Out.WriteLine(); }

        /// <inheritdoc cref="Console.ReadLine()"/>
        public static string ReadLine() { return Console.ReadLine(); }
        #endregion



        #region InnerWrites
        private static void InnerWrite(TextWriter writer, FormattableString value)
        {
            using (new ColorConsoleContext())
            {
                WriteInterpolatedWithColors(writer, value);
            }
        }
        private static void InnerWrite(ConsoleColor foregroundColor, TextWriter writer, FormattableString value)
        {
            using (ColoredConsole.WithColor(foregroundColor))
            {
                WriteInterpolatedWithColors(writer, value);
            }
        }
        private static void InnerWrite(ConsoleColor foregroundColor, ConsoleColor backgroundColor, TextWriter writer, FormattableString value)
        {
            using (ColoredConsole.WithColor(foregroundColor, backgroundColor))
            {
                WriteInterpolatedWithColors(writer, value);
            }
        }
        private static void InnerWrite(TextWriter writer, string value)
        {
            using (new ColorConsoleContext())
            {
                writer.Write(value);
            }
        }
        private static void InnerWrite(ConsoleColor foregroundColor, TextWriter writer, string value)
        {
            using (ColoredConsole.WithColor(foregroundColor))
            {
                writer.Write(value);
            }
        }
        private static void InnerWrite(ConsoleColor foregroundColor, ConsoleColor backgroundColor, TextWriter writer, string value)
        {
            using (ColoredConsole.WithColor(foregroundColor, backgroundColor))
            {
                writer.Write(value);
            }
        }

        #endregion

        #region Interpolated Strings Parsing and Embedded colors
        private static void WriteInterpolatedWithColors(TextWriter writer, FormattableString value)
        {
            _previousColors.Clear();
            _previousBackgroundColors.Clear();
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
        private static void InnerWriteFormattableArgument(TextWriter writer, object arg, string format)
        {
            if (arg is ConsoleColor && (format == "background" || format == "bg"))
            {
                _previousBackgroundColors.Push(Console.BackgroundColor);
                Console.BackgroundColor = (ConsoleColor)arg;
            }
            else if (arg is ConsoleColor)
            {
                _previousColors.Push(Console.ForegroundColor);
                Console.ForegroundColor = (ConsoleColor)arg;
            }
            else if (arg is RestorePreviousBackgroundColor)
            {
                if (_previousBackgroundColors.Count > 0)
                    Console.BackgroundColor = _previousBackgroundColors.Pop();
            }
            else if (arg is RestorePreviousColor)
            {
                if (_previousColors.Count > 0)
                    Console.ForegroundColor = _previousColors.Pop();
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

        #region Factories for ColorConsoleContext (contexts that will save current color, and restore it when disposed)
        /// <summary>
        /// Creates a new context that will "save" current console colors, can change the default color, and when disposed it will restore previous colors (it's IDisposable - so use it with "using" block)
        /// </summary>
        /// <param name="newForegroundColor"></param>
        /// <returns></returns>
        public static IColorConsoleContext WithColor(ConsoleColor newForegroundColor)
        {
            var context = new ColorConsoleContext();
            Console.ForegroundColor = newForegroundColor;
            return context;
        }

        /// <summary>
        /// Creates a new context that will "save" current console colors, can change the default color, and when disposed it will restore previous colors (it's IDisposable - so use it with "using" block)
        /// </summary>
        /// <param name="newForegroundColor"></param>
        /// <param name="newBackgroundColor"></param>
        /// <returns></returns>
        public static IColorConsoleContext WithColor(ConsoleColor newForegroundColor, ConsoleColor newBackgroundColor)
        {
            var context = new ColorConsoleContext();
            Console.ForegroundColor = newForegroundColor;
            Console.BackgroundColor = newBackgroundColor;
            return context;
        }

        /// <summary>
        /// Creates a new context that will "save" current console colors, can change the default color, and when disposed it will restore previous colors (it's IDisposable - so use it with "using" block)
        /// </summary>
        /// <param name="newBackgroundColor"></param>
        /// <returns></returns>
        public static IColorConsoleContext WithBackgroundColor(ConsoleColor newBackgroundColor)
        {
            var context = new ColorConsoleContext();
            Console.BackgroundColor = newBackgroundColor;
            return context;
        }
        #endregion
    }
}
