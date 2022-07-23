[![Nuget](https://img.shields.io/nuget/v/InterpolatedColorConsole?label=InterpolatedColorConsole)](https://www.nuget.org/packages/InterpolatedColorConsole)
[![Downloads](https://img.shields.io/nuget/dt/InterpolatedColorConsole.svg)](https://www.nuget.org/packages/InterpolatedColorConsole)

# Interpolated Color Console

Yes, it's yet another Colored Console. But where colors can just be interpolated within strings, making it easier to use multiple colors.

## Examples

```cs
using (ColoredConsole.WithColor(ConsoleColor.Blue))
{
    Console.WriteLine(@"
  _____       _                        _       _           _    _____      _            
 |_   _|     | |                      | |     | |         | |  / ____|    | |           
   | |  _ __ | |_ ___ _ __ _ __   ___ | | __ _| |_ ___  __| | | |     ___ | | ___  _ __ 
   | | | '_ \| __/ _ \ '__| '_ \ / _ \| |/ _` | __/ _ \/ _` | | |    / _ \| |/ _ \| '__|
  _| |_| | | | ||  __/ |  | |_) | (_) | | (_| | ||  __/ (_| | | |___| (_) | | (_) | |   
 |_____|_| |_|\__\___|_|  | .__/ \___/|_|\__,_|\__\___|\__,_|  \_____\___/|_|\___/|_|   
                          | |                                                           
                          |_|
".TrimStart('\n'));


    ColoredConsole.WriteLine("The default color was changed by the context");
    Console.WriteLine("It doesn't matter if I write with ColoredConsole or regular Console");
    ColoredConsole.WriteLine($"But the {ConsoleColor.Yellow}ColoredConsole{ConsoleColor.Red} allows us to change colors directly using embedded colors in interpolated strings");
}
Console.WriteLine($"After the context ({ConsoleColor.White}WithColor(){ConsoleColor.Blue}) is disposed, the previous color is restored.");


ColoredConsole.WriteLine();


using (ColoredConsole.WithColor(ConsoleColor.Yellow, ConsoleColor.Blue))
{
    ColoredConsole.WriteLine("We can set algo background colors");
    ColoredConsole.WriteLine($"And {ConsoleColor.Black}we can also {ConsoleColor.White:background}change background color within interpolated strings");
}

ColoredConsole.WriteLine("All back to normal! Enjoy!");

ColoredConsole.WriteLine(ConsoleColor.Green, $"Did you like it? Please give me a {ConsoleColor.Yellow}star{Symbols.PREVIOUS_COLOR} at {ConsoleColor.Blue}https://github.com/Drizin/InterpolatedColorConsole{Symbols.PREVIOUS_COLOR}");
```

Results:

![Alt text](./Screenshot.png?raw=true "Result colored console")

## License
MIT License
