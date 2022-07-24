using InterpolatedColorConsole.SpecialSymbols;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace InterpolatedColorConsole
{
    /// <summary>
    /// This is the main ColoredConsoleClass, where most public methods return the class itself allowing Chained Methods (Fluent-API).
    /// Usually this is invoked through the static <see cref="ColoredConsole"/> class.
    /// </summary>
    public class FluentColoredConsole
    {

        /// <summary>
        /// The Write/WriteLine overloads that do NOT take a default color for foreground/background will NOT save and restore previous color,
        /// and in case (when using FormattableString overloads) it's possible to dynamically change foreground/background colors using string interpolation.
        /// This context will keep a stack of previous used colors and will restore when using symbols like <see cref="RestorePreviousColor"/> or <see cref="RestorePreviousBackgroundColor"/>
        /// </summary>
        private ColoredConsoleContext _currentContext = new ColoredConsoleContext();

        #region Write Methods that don't specify a color but write a FormattableString and will dynamically change colors if the interpolated string has embedded colors
        /// <summary>
        /// Writes to console (stdout) using the specified color(s). The interpolated string may have embedded colors. After writing it DOES NOT restore previous colors.
        /// </summary>
        public FluentColoredConsole Write(FormattableString value) { InnerWrite(Console.Out, value); return this; }
        /// <see cref="Write(ConsoleColor, FormattableString)"/>
        public FluentColoredConsole WriteLine(FormattableString value) { InnerWrite(Console.Out, value); Console.Out.WriteLine(); return this; }

        /// <summary>
        /// Writes to console error (stderr). The interpolated string may have embedded colors. After writing it DOES NOT restore previous colors.
        /// </summary>
        public FluentColoredConsole WriteError(FormattableString value) { InnerWrite(Console.Error, value); return this; }
        /// <see cref="WriteError(ConsoleColor, FormattableString)"/>
        public FluentColoredConsole WriteLineError(FormattableString value) { InnerWrite(Console.Error, value); Console.Error.WriteLine(); return this; }
        #endregion

        #region Write Methods that specify a color and write FormattableString
        /// <summary>
        /// Writes to console (stdout) using the specified color(s). The interpolated string may have embedded colors. After writing it restores previous colors.
        /// </summary>
        public FluentColoredConsole Write(ConsoleColor foregroundColor, FormattableString value) { InnerWrite(foregroundColor, Console.Out, value); return this; }
        /// <see cref="Write(ConsoleColor, FormattableString)"/>
        public FluentColoredConsole WriteLine(ConsoleColor foregroundColor, FormattableString value) { InnerWrite(foregroundColor, Console.Out, value); Console.Out.WriteLine(); return this; }
        /// <see cref="Write(ConsoleColor, FormattableString)"/>
        public FluentColoredConsole Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, FormattableString value) { InnerWrite(foregroundColor, backgroundColor, Console.Out, value); return this; }
        /// <see cref="Write(ConsoleColor, FormattableString)"/>
        public FluentColoredConsole WriteLine(ConsoleColor foregroundColor, ConsoleColor backgroundColor, FormattableString value) { InnerWrite(foregroundColor, backgroundColor, Console.Out, value); Console.Out.WriteLine(); return this; }
        /// <summary>
        /// Writes to console error (stderr) using the specified color(s). The interpolated string may have embedded colors. After writing it restores previous colors.
        /// </summary>
        public FluentColoredConsole WriteError(ConsoleColor foregroundColor, FormattableString value) { InnerWrite(foregroundColor, Console.Error, value); return this; }
        /// <see cref="WriteError(ConsoleColor, FormattableString)"/>
        public FluentColoredConsole WriteLineError(ConsoleColor foregroundColor, FormattableString value) { InnerWrite(foregroundColor, Console.Error, value); Console.Error.WriteLine(); return this; }
        /// <see cref="WriteError(ConsoleColor, FormattableString)"/>
        public FluentColoredConsole WriteError(ConsoleColor foregroundColor, ConsoleColor backgroundColor, FormattableString value) { InnerWrite(foregroundColor, backgroundColor, Console.Error, value); return this; }
        /// <see cref="WriteError(ConsoleColor, FormattableString)"/>
        public FluentColoredConsole WriteLineError(ConsoleColor foregroundColor, ConsoleColor backgroundColor, FormattableString value) { InnerWrite(foregroundColor, backgroundColor, Console.Error, value); Console.Error.WriteLine(); return this; }
        #endregion

        #region Write Methods that specify a color and write a plain string (through the RawString class which does implicit conversion)
        /// <summary>
        /// Writes to console (stdout) using the specified color(s). After writing it restores previous colors.
        /// </summary>
        public FluentColoredConsole Write(ConsoleColor foregroundColor, RawString value) { InnerWrite(foregroundColor, Console.Out, value); return this; }
        /// <see cref="Write(ConsoleColor, RawString)"/>
        public FluentColoredConsole WriteLine(ConsoleColor foregroundColor, RawString value) { InnerWrite(foregroundColor, Console.Out, value); Console.Out.WriteLine(); return this; }
        /// <see cref="Write(ConsoleColor, RawString)"/>
        public FluentColoredConsole Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, RawString value) { InnerWrite(foregroundColor, backgroundColor, Console.Out, value); return this; }
        /// <see cref="Write(ConsoleColor, RawString)"/>
        public FluentColoredConsole WriteLine(ConsoleColor foregroundColor, ConsoleColor backgroundColor, RawString value) { InnerWrite(foregroundColor, backgroundColor, Console.Out, value); Console.Out.WriteLine(); return this; }

        /// <summary>
        /// Writes to console error (stderr) using the specified color(s). After writing it restores previous colors.
        /// </summary>
        public FluentColoredConsole WriteError(ConsoleColor foregroundColor, RawString value) { InnerWrite(foregroundColor, Console.Error, value); return this; }
        /// <see cref="WriteError(ConsoleColor, RawString)"/>
        public FluentColoredConsole WriteLineError(ConsoleColor foregroundColor, RawString value) { InnerWrite(foregroundColor, Console.Error, value); Console.Error.WriteLine(); return this; }
        /// <see cref="WriteError(ConsoleColor, RawString)"/>
        public FluentColoredConsole WriteError(ConsoleColor foregroundColor, ConsoleColor backgroundColor, RawString value) { InnerWrite(foregroundColor, backgroundColor, Console.Error, value); return this; }
        /// <see cref="WriteError(ConsoleColor, RawString)"/>
        public FluentColoredConsole WriteLineError(ConsoleColor foregroundColor, ConsoleColor backgroundColor, RawString value) { InnerWrite(foregroundColor, backgroundColor, Console.Error, value); Console.Error.WriteLine(); return this; }

        #endregion

        #region Write Methods that don't specify a color and write a plain string (through the RawString class which does implicit conversion)
        /// <summary>
        /// Writes to console (stdout) using the specified color(s). After writing it DOES NOT restore previous colors.
        /// </summary>
        public FluentColoredConsole Write(RawString value) { InnerWrite(Console.Out, value); return this; }
        /// <see cref="Write(RawString)"/>
        public FluentColoredConsole WriteLine(RawString value) { InnerWrite(Console.Out, value); Console.Out.WriteLine(); return this; }

        /// <summary>
        /// Writes to console error (stderr) using the specified color(s). After writing it DOES NOT restore previous colors.
        /// </summary>
        public FluentColoredConsole WriteError(RawString value) { InnerWrite(Console.Error, value); return this; }
        /// <see cref="WriteError(RawString)"/>
        public FluentColoredConsole WriteLineError(RawString value) { InnerWrite(Console.Error, value); Console.Error.WriteLine(); return this; }
        #endregion

        #region Other non-color-related Console Facades
        /// <inheritdoc cref="Console.WriteLine()"/>
        public FluentColoredConsole WriteLine() { Console.Out.WriteLine(); return this; }

        /// <inheritdoc cref="Console.ReadLine()"/>
        public string ReadLine() { return Console.ReadLine(); }

        /// <inheritdoc cref="Console.ResetColor()"/>
        public FluentColoredConsole ResetColor() { Console.ResetColor(); return this; }
        #endregion

        #region Factories for ColoredConsoleContext (contexts that will save current color, and restore it when disposed)
        /// <summary>
        /// Creates a new context that will "save" current console colors, can change the default color, and when disposed it will restore previous colors (it's IDisposable - so use it with "using" block)
        /// </summary>
        public IColoredConsoleContext WithColor(ConsoleColor newForegroundColor)
        {
            return new ColoredConsoleContext(newForegroundColor);
        }

        /// <summary>
        /// Creates a new context that will "save" current console colors, can change the default color, and when disposed it will restore previous colors (it's IDisposable - so use it with "using" block)
        /// </summary>
        public IColoredConsoleContext WithColor(ConsoleColor newForegroundColor, ConsoleColor newBackgroundColor)
        {
            return new ColoredConsoleContext(newForegroundColor, newBackgroundColor);
        }

        /// <summary>
        /// Creates a new context that will "save" current console colors, can change the default color, and when disposed it will restore previous colors (it's IDisposable - so use it with "using" block)
        /// </summary>
        /// <param name="newBackgroundColor"></param>
        /// <returns></returns>
        public IColoredConsoleContext WithBackgroundColor(ConsoleColor newBackgroundColor)
        {
            return new ColoredConsoleContext(newBackgroundColor: newBackgroundColor);
        }
        #endregion

        #region Fluent Methods for Changing/Restoring colors
        /// <summary>
        /// Changes current foreground color. Restore it using <see cref="RestorePreviousColor"/>
        /// </summary>
        public FluentColoredConsole SetColor(ConsoleColor newForegroundColor)
        {
            Write($"{newForegroundColor}");
            return this;
        }
        /// <summary>
        /// Restores previous foreground color, after it was changed using <see cref="SetColor(ConsoleColor)"/>
        /// </summary>
        public FluentColoredConsole RestorePreviousColor()
        {
            Write($"{new RestorePreviousColor()}");
            return this;
        }
        /// <summary>
        /// Changes current background color. Restore it using <see cref="RestorePreviousBackgroundColor"/>
        /// </summary>
        public FluentColoredConsole SetBackgroundColor(ConsoleColor newBackgroundColor)
        {
            Write($"{newBackgroundColor:background}");
            return this;
        }
        /// <summary>
        /// Restores previous background color, after it was changed using <see cref="SetBackgroundColor(ConsoleColor)"/>
        /// </summary>
        public FluentColoredConsole RestorePreviousBackgroundColor()
        {
            Write($"{new RestorePreviousBackgroundColor()}");
            return this;
        }
        #endregion


        #region InnerWrites
        /// <summary>
        /// Writes the specified text which may dynamically change foreground/background colors using string interpolation.
        /// This method does NOT restore back the previous colors after writing.
        /// </summary>
        private void InnerWrite(TextWriter writer, FormattableString value)
        {
            _currentContext.WriteInterpolatedWithColors(writer, value);
        }
        /// <summary>
        /// Writes the specified text using current color settings.
        /// </summary>
        private void InnerWrite(TextWriter writer, string value)
        {
            writer.Write(value);
        }

        /// <summary>
        /// Changes default colors, writes the specified text, and restores back the previous colors.
        /// </summary>
        private void InnerWrite(ConsoleColor foregroundColor, TextWriter writer, FormattableString value)
        {
            using (var context = new ColoredConsoleContext(foregroundColor))
            {
                context.WriteInterpolatedWithColors(writer, value);
            }
        }
        /// <summary>
        /// Changes default colors, writes the specified text, and restores back the previous colors.
        /// </summary>
        private void InnerWrite(ConsoleColor foregroundColor, ConsoleColor backgroundColor, TextWriter writer, FormattableString value)
        {
            using (var context = new ColoredConsoleContext(foregroundColor, backgroundColor))
            {
                context.WriteInterpolatedWithColors(writer, value);
            }
        }
        /// <summary>
        /// Changes default colors, writes the specified text, and restores back the previous colors.
        /// </summary>
        private void InnerWrite(ConsoleColor foregroundColor, TextWriter writer, string value)
        {
            using (new ColoredConsoleContext(foregroundColor))
            {
                writer.Write(value);
            }
        }
        /// <summary>
        /// Changes default colors, writes the specified text, and restores back the previous colors.
        /// </summary>
        private void InnerWrite(ConsoleColor foregroundColor, ConsoleColor backgroundColor, TextWriter writer, string value)
        {
            using (new ColoredConsoleContext(foregroundColor, backgroundColor))
            {
                writer.Write(value);
            }
        }

        #endregion

    }
}
