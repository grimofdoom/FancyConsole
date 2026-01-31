using System;
using System.Collections.Generic;
using System.Text;

namespace FancyConsole.Text {
    public static partial class Print { 
        /// <summary>Singular item to define a color theme for printing out</summary>
        public class Theme(Color font, Color Background, TextEffects effects = TextEffects.NONE) {
            /// <summary>Color used for font/text</summary>
            public Color Font = font;
            /// <summary>Color used for background</summary>
            public Color Background = Background;

            /// <summary>This is the color that will be attempted to fill the rest of console, where FancyConsole fails to color fill</summary>
            public ConsoleColor backgroundEffect = ConsoleColor.Black;

            /// <summary>Specifies the text effects to apply.</summary>
            public TextEffects Effects = effects;

            /// <summary>Get a new theme with background and font color swapped</summary>
            public Theme GetFlipped() {
                return new(Background, Font);
            }

            /// <summary>Get a string with text effects applied to it</summary>
            public string Apply(string text) {
                string effectCodes = $"38;2;{Font.R};{Font.G};{Font.B};48;2;{Background.R};{Background.G};{Background.B}";
                if (Effects.HasFlag(TextEffects.BOLD)) effectCodes += "1;";
                if (Effects.HasFlag(TextEffects.DIM)) effectCodes += "2;";
                if (Effects.HasFlag(TextEffects.ITALIC)) effectCodes += "3;";
                if (Effects.HasFlag(TextEffects.UNDERLINE)) effectCodes += "4;";
                if (Effects.HasFlag(TextEffects.BLINK)) effectCodes += "5;";
                if (Effects.HasFlag(TextEffects.FASTBLINK)) effectCodes += "6;";
                if (Effects.HasFlag(TextEffects.INVERSE)) effectCodes += "7;";
                if (Effects.HasFlag(TextEffects.CONCEAL)) effectCodes += "8;";
                if (Effects.HasFlag(TextEffects.STRIKETHROUGH)) effectCodes += "9;";

                //No effect codes to apply
                if (effectCodes.Length == 0) return text;

                //Effect codes to apply, add to string and send back to printing
                return $"\x1b[{effectCodes.TrimEnd(';')}m{text}\x1b[0m{GetThemeString(NormalTheme)}";
            }
        }

        //Enum flags for defining text effects for Themes
        [Flags]
        public enum TextEffects {
            NONE = 0,
            BOLD = 1 << 0,
            DIM = 1 << 1,
            ITALIC = 1 << 2,
            UNDERLINE = 1 << 3,
            BLINK = 1 << 4,
            FASTBLINK = 1 << 5,
            INVERSE = 1 << 6,
            CONCEAL = 1 << 7,
            STRIKETHROUGH = 1 << 8
        }
    }
}
