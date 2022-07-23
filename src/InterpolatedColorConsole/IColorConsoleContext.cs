using System;

namespace InterpolatedColorConsole
{
    /// <see cref="ColoredConsole.ColorConsoleContext"/>
    public interface IColorConsoleContext : IDisposable
    {
        /// <see cref="ColoredConsole.ColorConsoleContext.RestorePreviousColor"/>
        void RestorePreviousColor();
    }
}
