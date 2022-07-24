[![Nuget](https://img.shields.io/nuget/v/InterpolatedColorConsole?label=InterpolatedColorConsole)](https://www.nuget.org/packages/InterpolatedColorConsole)
[![Downloads](https://img.shields.io/nuget/dt/InterpolatedColorConsole.svg)](https://www.nuget.org/packages/InterpolatedColorConsole)

# Interpolated Color Console

Yes, it's yet another Colored Console. But with a Fluent API, nice utilities like contexts (which automatically restore previous colors), and where colors can just be interpolated within strings.

# Examples

## Mixed Examples

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

Results:

![Alt text](./Screenshot.png?raw=true "Result colored console")

## WriteLine 

```cs
// WriteLine with a foreground color:
ColoredConsole.WriteLine(ConsoleColor.Yellow, "Hello, World!");

// WriteLine with foreground and background colors:
ColoredConsole.WriteLine(ConsoleColor.Yellow, ConsoleColor.Blue, "Hello, World!");

// WriteLine only setting the Background Color (keep current Foreground)
ColoredConsole.WriteLine(Console.ForegroundColor, ConsoleColor.Blue, "Hello, World!");

```

There's also `Write()`

## Fluent API (Chained methods)
```cs
ColoredConsole
    .WriteLine(ConsoleColor.Cyan, "Hello")
    .Write(ConsoleColor.Yellow, "Fluent API")
    .Write(ConsoleColor.Blue, " makes concise code")
    .WriteLine();
```

## Contexts

```cs
// Set foreground color (and optional background color) of context.
using (ColoredConsole.WithColor(ConsoleColor.Yellow, ConsoleColor.Blue))
{
    //ColoredConsole.WriteLine(...);
    // When context is disposed the previous colores are automatically restored
}
```

## Modifying colors (inline) using string interpolation
```cs
ColoredConsole.WriteLine($"We can change {ConsoleColor.Yellow}foreground color");
ColoredConsole.WriteLine($"We can also change {ConsoleColor.White:background}background color");
```

## Modifying colors (inline) using string interpolation
```cs
ColoredConsole.WriteLine($"We can change {ConsoleColor.Yellow}foreground color");
ColoredConsole.WriteLine($"We can also change {ConsoleColor.White:background}background color");
```

Whenever we change colors the previous color is saved into a stack and there are special symbols for restoring previous colors:

```cs
ColoredConsole.WriteLine($"Change {ConsoleColor.Yellow}foreground color{Symbols.PREVIOUS_COLOR} and restore it back!");
ColoredConsole.WriteLine($"Change {ConsoleColor.Yellow:background}background color{Symbols.PREVIOUS_BACKGROUND_COLOR} and restore it back!");
```





## ASCII Art

Use [this link](https://patorjk.com/software/taag/) to convert text to ASCII art.

```cs
ColoredConsole.WriteLine(ConsoleColor.Blue, @"
   _____      _                    _    _____                      _      
  / ____|    | |                  | |  / ____|                    | |     
 | |     ___ | | ___  _ __ ___  __| | | |     ___  _ __  ___  ___ | | ___ 
 | |    / _ \| |/ _ \| '__/ _ \/ _` | | |    / _ \| '_ \/ __|/ _ \| |/ _ \
 | |___| (_) | | (_) | | |  __/ (_| | | |___| (_) | | | \__ \ (_) | |  __/
  \_____\___/|_|\___/|_|  \___|\__,_|  \_____\___/|_| |_|___/\___/|_|\___|
");
```


## How to Restore Previous Colors

As explained earlier, the first method is to use `WithColor()` to create a Context that when disposed will automatically restore the previous color:
```cs
using (ColoredConsole.WithColor(ConsoleColor.Cyan))
{
    // Do whatever you want...
}
// Foreground/Background Colors are automatically restored
``` 

But if user presses Ctrl-C then the Context won't be disposed, so it's good to capture the Ctrl-C and explicitly restore the colors:

```cs
System.Console.CancelKeyPress += (s, e) =>
{
    // This restores the default system colors
    Console.ResetColor();
};
```

Or if we are using a context we can do even better:

```cs
// The new context will capture current colors (even if they are not default system colors)
using (var consoleContext = ColoredConsole.WithColor(ConsoleColor.Green))
{
    System.Console.CancelKeyPress += (s, e) =>
    {
        // When Ctrl-C is pressed we can restore whatever colors we had before:
        consoleContext.RestorePreviousColor();
    };
}
```


## License
MIT License
