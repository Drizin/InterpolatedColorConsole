using System;
using System.Collections.Generic;
using System.Text;

namespace InterpolatedColorConsole
{
    /// <summary>
    /// ColoredConsole basically has static facades to invoke the real methods on <see cref="FluentColoredConsole"/> (which is a fluent-api class)
    /// </summary>
    public static partial class ColoredConsole
    {
        #region Static Write Methods that don't specify a color but write a FormattableString and will dynamically change colors if the interpolated string has embedded colors
        /// <summary>
        /// Writes to console (stdout) using the specified color(s). The interpolated string may have embedded colors. After writing it DOES NOT restore previous colors.
        /// </summary>
        public static FluentColoredConsole Write(FormattableString value) => new FluentColoredConsole().Write(value);
        /// <see cref="Write(ConsoleColor, FormattableString)"/>
        public static FluentColoredConsole WriteLine(FormattableString value) => new FluentColoredConsole().WriteLine(value);

        /// <summary>
        /// Writes to console error (stderr). The interpolated string may have embedded colors. After writing it DOES NOT restore previous colors.
        /// </summary>
        public static FluentColoredConsole WriteError(FormattableString value) => new FluentColoredConsole().WriteError(value);
        /// <see cref="WriteError(ConsoleColor, FormattableString)"/>
        public static FluentColoredConsole WriteLineError(FormattableString value) => new FluentColoredConsole().WriteLineError(value);
        #endregion

        #region Static Write Methods that specify a color and write FormattableString
        /// <summary>
        /// Writes to console (stdout) using the specified color(s). The interpolated string may have embedded colors. After writing it restores previous colors.
        /// </summary>
        public static FluentColoredConsole Write(ConsoleColor foregroundColor, FormattableString value) => new FluentColoredConsole().Write(foregroundColor, value);
        /// <see cref="Write(ConsoleColor, FormattableString)"/>
        public static FluentColoredConsole WriteLine(ConsoleColor foregroundColor, FormattableString value) => new FluentColoredConsole().WriteLine(foregroundColor, value);
        /// <see cref="Write(ConsoleColor, FormattableString)"/>
        public static FluentColoredConsole Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, FormattableString value) => new FluentColoredConsole().Write(foregroundColor, backgroundColor, value);
        /// <see cref="Write(ConsoleColor, FormattableString)"/>
        public static FluentColoredConsole WriteLine(ConsoleColor foregroundColor, ConsoleColor backgroundColor, FormattableString value) => new FluentColoredConsole().WriteLine(foregroundColor, backgroundColor, value);
        /// <summary>
        /// Writes to console error (stderr) using the specified color(s). The interpolated string may have embedded colors. After writing it restores previous colors.
        /// </summary>
        public static FluentColoredConsole WriteError(ConsoleColor foregroundColor, FormattableString value) => new FluentColoredConsole().WriteError(foregroundColor, value);
        /// <see cref="WriteError(ConsoleColor, FormattableString)"/>
        public static FluentColoredConsole WriteLineError(ConsoleColor foregroundColor, FormattableString value) => new FluentColoredConsole().WriteLineError(foregroundColor,  value);
        /// <see cref="WriteError(ConsoleColor, FormattableString)"/>
        public static FluentColoredConsole WriteError(ConsoleColor foregroundColor, ConsoleColor backgroundColor, FormattableString value) => new FluentColoredConsole().WriteError(foregroundColor, backgroundColor, value);
        /// <see cref="WriteError(ConsoleColor, FormattableString)"/>
        public static FluentColoredConsole WriteLineError(ConsoleColor foregroundColor, ConsoleColor backgroundColor, FormattableString value) => new FluentColoredConsole().WriteLineError(foregroundColor, backgroundColor, value);
        #endregion

        #region Static Write Methods that specify a color and write a plain string (through the RawString class which does implicit conversion)
        /// <summary>
        /// Writes to console (stdout) using the specified color(s). After writing it restores previous colors.
        /// </summary>
        public static FluentColoredConsole Write(ConsoleColor foregroundColor, RawString value) => new FluentColoredConsole().Write(foregroundColor, value);
        /// <see cref="Write(ConsoleColor, RawString)"/>
        public static FluentColoredConsole WriteLine(ConsoleColor foregroundColor, RawString value) => new FluentColoredConsole().WriteLine(foregroundColor, value);
        /// <see cref="Write(ConsoleColor, RawString)"/>
        public static FluentColoredConsole Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, RawString value) => new FluentColoredConsole().Write(foregroundColor, backgroundColor, value);
        /// <see cref="Write(ConsoleColor, RawString)"/>
        public static FluentColoredConsole WriteLine(ConsoleColor foregroundColor, ConsoleColor backgroundColor, RawString value) => new FluentColoredConsole().WriteLine(foregroundColor, backgroundColor, value);

        /// <summary>
        /// Writes to console error (stderr) using the specified color(s). After writing it restores previous colors.
        /// </summary>
        public static FluentColoredConsole WriteError(ConsoleColor foregroundColor, RawString value) => new FluentColoredConsole().WriteError(foregroundColor, value);
        /// <see cref="WriteError(ConsoleColor, RawString)"/>
        public static FluentColoredConsole WriteLineError(ConsoleColor foregroundColor, RawString value) => new FluentColoredConsole().WriteLineError(foregroundColor, value);
        /// <see cref="WriteError(ConsoleColor, RawString)"/>
        public static FluentColoredConsole WriteError(ConsoleColor foregroundColor, ConsoleColor backgroundColor, RawString value) => new FluentColoredConsole().WriteError(foregroundColor, backgroundColor, value);
        /// <see cref="WriteError(ConsoleColor, RawString)"/>
        public static FluentColoredConsole WriteLineError(ConsoleColor foregroundColor, ConsoleColor backgroundColor, RawString value) => new FluentColoredConsole().WriteLineError(foregroundColor, backgroundColor, value);

        #endregion

        #region Static Write Methods that don't specify a color and write a plain string (through the RawString class which does implicit conversion)
        /// <summary>
        /// Writes to console (stdout) using the specified color(s). After writing it DOES NOT restore previous colors.
        /// </summary>
        public static FluentColoredConsole Write(RawString value) => new FluentColoredConsole().Write(value);
        /// <see cref="Write(RawString)"/>
        public static FluentColoredConsole WriteLine(RawString value) => new FluentColoredConsole().WriteLine(value);

        /// <summary>
        /// Writes to console error (stderr) using the specified color(s). After writing it DOES NOT restore previous colors.
        /// </summary>
        public static FluentColoredConsole WriteError(RawString value) => new FluentColoredConsole().WriteError(value);
        /// <see cref="WriteError(RawString)"/>
        public static FluentColoredConsole WriteLineError(RawString value) => new FluentColoredConsole().WriteLineError(value);
        #endregion

        #region Other non-color-related Console Facades
        /// <inheritdoc cref="Console.WriteLine()"/>
        public static FluentColoredConsole WriteLine() => new FluentColoredConsole().WriteLine();

        /// <inheritdoc cref="Console.ResetColor()"/>
        public static FluentColoredConsole ResetColor() => new FluentColoredConsole().ResetColor();

        /// <inheritdoc cref="Console.ReadLine()"/>
        public static string ReadLine() { return Console.ReadLine(); }
        #endregion


        #region Factories for ColoredConsoleContext (contexts that will save current color, and restore it when disposed)
        /// <summary>
        /// Creates a new context that will "save" current console colors, can change the default color, and when disposed it will restore previous colors (it's IDisposable - so use it with "using" block)
        /// </summary>
        /// <param name="newForegroundColor"></param>
        /// <returns></returns>
        public static IColoredConsoleContext WithColor(ConsoleColor newForegroundColor) => new FluentColoredConsole().WithColor(newForegroundColor);

        /// <summary>
        /// Creates a new context that will "save" current console colors, can change the default color, and when disposed it will restore previous colors (it's IDisposable - so use it with "using" block)
        /// </summary>
        /// <param name="newForegroundColor"></param>
        /// <param name="newBackgroundColor"></param>
        /// <returns></returns>
        public static IColoredConsoleContext WithColor(ConsoleColor newForegroundColor, ConsoleColor newBackgroundColor) => new FluentColoredConsole().WithColor(newForegroundColor, newBackgroundColor);

        /// <summary>
        /// Creates a new context that will "save" current console colors, can change the default color, and when disposed it will restore previous colors (it's IDisposable - so use it with "using" block)
        /// </summary>
        /// <param name="newBackgroundColor"></param>
        /// <returns></returns>
        public static IColoredConsoleContext WithBackgroundColor(ConsoleColor newBackgroundColor) => new FluentColoredConsole().WithBackgroundColor(newBackgroundColor);
        #endregion

        #region Fluent Methods for Changing/Restoring colors
        /// <summary>
        /// Changes current foreground color. Restore it using <see cref="FluentColoredConsole.RestorePreviousColor"/>
        /// </summary>
        public static FluentColoredConsole SetColor(ConsoleColor newForegroundColor) => new FluentColoredConsole().SetColor(newForegroundColor);
        /// <summary>
        /// Changes current background color. Restore it using <see cref="FluentColoredConsole.RestorePreviousBackgroundColor"/>
        /// </summary>
        public static FluentColoredConsole SetBackgroundColor(ConsoleColor newBackgroundColor) => new FluentColoredConsole().SetBackgroundColor(newBackgroundColor);
        #endregion


    }
}
