# FancyConsole
Welcome to the FancyConsole project. This was originally designed as a set of helper classes for my own text based game engine, however grew too much that I had to separate it and I felt others may get use out of it. 
To my point of view, this project encapsulated what personally feels like a more modern way of interact with the console itself, while still being easy to use and understand. I have so far implimented 2 core systems.
In the future, I will be adding more helper methods/classes to help with text based console interaction and control - obfuscating the more complex parts of console interaction, with literal one-liners.

## Print

Print is primarily focused with either putting text to the console, or interacting with the text on the screen. FancyConsole is focused primarily on text based interaction instead of gui based (such as NCurses).

### Color
I created a color class from scratch, using int instead of bytes (uses more data, but I HATE wraparound of byte if you exceed 255). A fair chunk of helped methods and operations have been added to allow for some
extra color manipulations and color themeing to be done easier. Ansi TrueColor is the main target for color output.
### Theme
A theme is font/background color, ANSI effects, and some helper methods to make it easier to apply themes to text output. This lets you more easily create consistancy AND swap all your colors in one spot. 
Print automatically uses a DefaultTheme for all core output, when not being overridden in method. You can change this at any time for your own, so you do not have to pass theme info every time.

## Read

Read is primarily focused on getting input from player/user. These helper methods handle conversions of input types with validation to requested type of info you request from input. These methods are designed
to run in a loop, with a MaxPlayerTries (default 10) for how many times a response can be requested, before YOUR chosen default value is returned instead. These value types will be expanded in the future.