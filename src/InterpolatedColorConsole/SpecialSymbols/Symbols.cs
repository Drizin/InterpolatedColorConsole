using InterpolatedColorConsole.SpecialSymbols;

namespace InterpolatedColorConsole
{
    /// <summary>
    /// Some helper symbols that can be used for special functions like restoring previous colors
    /// </summary>
    public class Symbols
    {
        /// <summary>
        /// Restores the previous Foreground color
        /// </summary>
        public static RestorePreviousColor PREVIOUS_COLOR => new RestorePreviousColor();

        /// <summary>
        /// Restores the previous Foreground color
        /// </summary>
        public static RestorePreviousBackgroundColor PREVIOUS_BACKGROUND_COLOR => new RestorePreviousBackgroundColor();
    }
}
