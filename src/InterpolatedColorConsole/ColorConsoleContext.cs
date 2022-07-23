using System;
using System.Collections.Generic;
using System.Text;

namespace InterpolatedColorConsole
{    
    partial class ColoredConsole
    {
        /// <summary>
        /// Context that will "save" current console colors, can change the default color, and when disposed it will restore previous colors (it's IDisposable - so use it with "using" block)
        /// </summary>
        protected class ColorConsoleContext : IColorConsoleContext, IDisposable
        {
            private ConsoleColor _previousForegroundColor;
            private ConsoleColor _previousBackgroundColor;

            internal ColorConsoleContext()
            {
                _previousForegroundColor = Console.ForegroundColor;
                _previousBackgroundColor = Console.BackgroundColor;
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
        }
    }
}
