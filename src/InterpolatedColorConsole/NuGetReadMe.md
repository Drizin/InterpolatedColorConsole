# Interpolated Color Console

Yes, it's yet another Colored Console. But where colors can just be interpolated within strings, making it easier to use multiple colors.

## Examples

```cs
ColoredConsole.SetColor(ConsoleColor.Blue).WriteLine(@"
  _____       _                        _       _           _    _____      _            
 |_   _|     | |                      | |     | |         | |  / ____|    | |           
   | |  _ __ | |_ ___ _ __ _ __   ___ | | __ _| |_ ___  __| | | |     ___ | | ___  _ __ 
   | | | '_ \| __/ _ \ '__| '_ \ / _ \| |/ _` | __/ _ \/ _` | | |    / _ \| |/ _ \| '__|
  _| |_| | | | ||  __/ |  | |_) | (_) | | (_| | ||  __/ (_| | | |___| (_) | | (_) | |   
 |_____|_| |_|\__\___|_|  | .__/ \___/|_|\__,_|\__\___|\__,_|  \_____\___/|_|\___/|_|   
                          | |                                                           
                          |_|
");
using (ColoredConsole.WithColor(ConsoleColor.Cyan))
{
    ColoredConsole.WriteLine($"The default color was changed by the context");
    Console.WriteLine("It doesn't matter if I write with ColoredConsole or regular Console");
    ColoredConsole.WriteLine($"Even if we {ConsoleColor.Red}change the color inline and forget to restore it back...");
}
ColoredConsole.WriteLine($"...when contexts are disposed the previous colors are restored.").WriteLine();

ColoredConsole.SetColor(ConsoleColor.Green)
    .Write("One cool thing about ").Write(ConsoleColor.Yellow, "ColoredConsole ").WriteLine("is that it allows us to use chained-methods (Fluent API).")
    .Write("One way for ").Write(ConsoleColor.Yellow, "setting colors ").WriteLine("is using the Write() overloads that specify colors.")
    .Write("Another way is using ").SetColor(ConsoleColor.Blue).Write("SetColor() ").RestorePreviousColor()
    .SetBackgroundColor(ConsoleColor.Red).WriteLine("or SetBackgroundColor()").RestorePreviousBackgroundColor()
    .WriteLine($"It's also possible to use {ConsoleColor.Red}string interpolation{ConsoleColor.Green} to change foreground or {ConsoleColor.Red:bg}background colors")
    .WriteLine($"and there are symbols for {Symbols.PREVIOUS_BACKGROUND_COLOR}restoring previous colors ")
    .Write("or we can also use ")
    .Write(ConsoleColor.Yellow, "ResetColor()").ResetColor().Write(" which brings everything back to normal.");



ColoredConsole.WriteLine().WriteLine();

ColoredConsole.WriteLine(ConsoleColor.Green, $"Did you like it? Please give me a {ConsoleColor.Yellow}star{Symbols.PREVIOUS_COLOR} at {ConsoleColor.Blue}https://github.com/Drizin/InterpolatedColorConsole{Symbols.PREVIOUS_COLOR}");

ColoredConsole.ReadLine();
```




## License
MIT License


See full documentation [here](https://github.com/Drizin/InterpolatedColorConsole/)
