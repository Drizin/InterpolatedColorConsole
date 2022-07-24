using System;

namespace InterpolatedColorConsole
{
    /// <see cref="ColoredConsoleContext"/>
    public interface IColoredConsoleContext : IDisposable
    {
        /// <see cref="ColoredConsoleContext.RestorePreviousColor"/>
        void RestorePreviousColor();
    }
}
