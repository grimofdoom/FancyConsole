using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FancyConsole.Text {
    /// <summary>Defines a generic Color, for primary use in TrueColor through the Console</summary>
    public class Color {
        public int R;
        public int B;
        public int G;

        public Color(int r, int g, int b) {
            R = Clamp(r);
            G = Clamp(g);
            B = Clamp(b);
        }

        public Color(byte r, byte g, byte b) {
            R = r;
            G = g;
            B = b;
        }

        public Color(float r, float g, float b) {
            R = Clamp(r);
            G = Clamp(g);
            B = Clamp(b);
        }

        public Color(double r, double g, double b) {
            R = Clamp(r);
            G = Clamp(g);
            B = Clamp(b);
        }

        // --------------- Implicit conversions from tuples, for lazy writing ---------------
        //Int
        public static implicit operator Color((int r, int g, int b) value) =>
            new (Clamp(value.r), Clamp(value.g), Clamp(value.b));
        //Byte
        public static implicit operator Color((byte r, byte g, byte b) value) =>
            new (value.r, value.g, value.b);
        //Float
        public static implicit operator Color((float r, float g, float b) value) =>
            new (Clamp(value.r), Clamp(value.g), Clamp(value.b));
        //Double (will anyone even do this? doubt. Why not though.)
        public static implicit operator Color((double r, double g, double b) value) =>
            new (Clamp(value.r), Clamp(value.g), Clamp(value.b));



        // --------------- Operator overloads ---------------

        //Addition
        public static Color operator +(Color a, Color b) {
            return new Color(
                Math.Clamp(a.R + b.R, 0, 255),
                Math.Clamp(a.G + b.G, 0, 255),
                Math.Clamp(a.B + b.B, 0, 255)
            );
        }
        public static Color operator +(Color a, float b) {
            return new Color(
                Math.Clamp((int)(a.R + b), 0, 255),
                Math.Clamp((int)(a.G + b), 0, 255),
                Math.Clamp((int)(a.B + b), 0, 255)
            );
        }
        public static Color operator +(Color a, int b) {
            return new Color(
                Math.Clamp(a.R + b, 0, 255),
                Math.Clamp(a.G + b, 0, 255),
                Math.Clamp(a.B + b, 0, 255)
            );
        }


        //Subtraction
        public static Color operator -(Color a, Color b) {
            return new Color(
                Math.Clamp(a.R - b.R, 0, 255),
                Math.Clamp(a.G - b.G, 0, 255),
                Math.Clamp(a.B - b.B, 0, 255)
            );
        }
        public static Color operator -(Color a, float b) {
            return new Color(
                Math.Clamp((int)(a.R - b), 0, 255),
                Math.Clamp((int)(a.G - b), 0, 255),
                Math.Clamp((int)(a.B - b), 0, 255)
            );
        }
        public static Color operator -(Color a, int b) {
            return new Color(
                Math.Clamp(a.R - b, 0, 255),
                Math.Clamp(a.G - b, 0, 255),
                Math.Clamp(a.B - b, 0, 255)
            );
        }


        //Multiplication
        public static Color operator *(Color a, Color b) {
            return new Color(
                Math.Clamp((a.R * b.R) / 255, 0, 255),
                Math.Clamp((a.G * b.G) / 255, 0, 255),
                Math.Clamp((a.B * b.B) / 255, 0, 255)
            );
        }
        public static Color operator *(Color a, float b) {
            return new Color(
                Math.Clamp((int)(a.R * b), 0, 255),
                Math.Clamp((int)(a.G * b), 0, 255),
                Math.Clamp((int)(a.B * b), 0, 255)
            );
        }
        public static Color operator *(Color a, int b) {
            return new Color(
                Math.Clamp(a.R * b, 0, 255),
                Math.Clamp(a.G * b, 0, 255),
                Math.Clamp(a.B * b, 0, 255)
            );
        }


        //Division
        public static Color operator /(Color a, Color b) {
            return new Color(
                Math.Clamp(b.R == 0 ? 255 : (a.R * 255) / b.R, 0, 255),
                Math.Clamp(b.G == 0 ? 255 : (a.G * 255) / b.G, 0, 255),
                Math.Clamp(b.B == 0 ? 255 : (a.B * 255) / b.B, 0, 255)
            );
        }
        public static Color operator /(Color a, float b) {
            return new Color(
                Math.Clamp((int)(a.R / b), 0, 255),
                Math.Clamp((int)(a.G / b), 0, 255),
                Math.Clamp((int)(a.B / b), 0, 255)
            );
        }
        public static Color operator /(Color a, int b) {
            return new Color(
                Math.Clamp(a.R / b, 0, 255),
                Math.Clamp(a.G / b, 0, 255),
                Math.Clamp(a.B / b, 0, 255)
            );
        }


        //Increment/Decrement, why not?
        public static Color operator ++(Color a) {
            return new Color(
                Math.Clamp(a.R + 1, 0, 255),
                Math.Clamp(a.G + 1, 0, 255),
                Math.Clamp(a.B + 1, 0, 255)
            );
        }

        public static Color operator --(Color a) {
            return new Color(
                Math.Clamp(a.R - 1, 0, 255),
                Math.Clamp(a.G - 1, 0, 255),
                Math.Clamp(a.B - 1, 0, 255)
            );
        }


        //Comparators
        public static bool operator ==(Color a, Color b) {
            return a.R == b.R && a.G == b.G && a.B == b.B;
        }

        public static bool operator !=(Color a, Color b) {
            return !(a == b);
        }

        public static bool operator >(Color a, Color b) {
            return (a.R + a.G + a.B) > (b.R + b.G + b.B);
        }
        public static bool operator <(Color a, Color b) {
            return (a.R + a.G + a.B) < (b.R + b.G + b.B);
        }
        public static bool operator >=(Color a, Color b) {
            return (a.R + a.G + a.B) >= (b.R + b.G + b.B);
        }
        public static bool operator <=(Color a, Color b) {
            return (a.R + a.G + a.B) <= (b.R + b.G + b.B);
        }

        //Extras that compiler complains for
        public override bool Equals(object? obj) {
            return obj is Color c && this == c; ;
        }
        public override int GetHashCode() {
            return HashCode.Combine(R, G, B);
        }



        // ---- Utility methods ----


        // - Lazy shorthands
        /// <summary>Local shorthand to convert and clamp int to byte</summary>
        private static int Clamp(int value) => (int)Math.Clamp(value, 0, 255);
        /// <summary>Local shorthand to convert and clamp float to byte</summary>
        private static int Clamp(float value) => (int)Math.Clamp((int)value, 0, 255);
        /// <summary>Local shorthand to convert and clamp double to byte</summary>
        private static int Clamp(double value) => (int)Math.Clamp((int)value, 0, 255);




        /// <summary>Get the inverse color</summary>
        public Color GetInverse() {
            return (255 - R, 255 - G, 255 - B);
        }

        /// <summary>Get a darker version of the color by a given amount (0-1)</summary>
        public Color GetDarken(float amount) {
            return GetDarken(this, amount);
        }

        /// <summary>Get a darker version of a given color by a given amount (0-1)</summary>
        public static Color GetDarken(Color a, float amount) {
            float r = a.R * (1 - amount);
            float g = a.G * (1 - amount);
            float b = a.B * (1 - amount);
            return (r, g, b);
        }

        /// <summary>Get a new color that is a perceptually darker version of the current color at 70% darker</summary>
        public Color GetDarkenPerceptual() {
            return (
                R * 0.7,
                G * 0.7,
                B * 0.7
            );
        }

        ///<summary>Get a lighter version of the color by a given amount (0 to 1)</summary>
        public Color GetLighten(float amount) {
            return GetLighten(this, amount);
        }

        /// <summary>Creates a lighter version of the specified color by blending it with white by the given amount.</summary>
        public static Color GetLighten(Color a, float amount) {
            int r = (int)(a.R + (255 - a.R) * amount);
            int g = (int)(a.G + (255 - a.G) * amount);
            int b = (int)(a.B + (255 - a.B) * amount);
            return new Color(r, g, b);
        }

        /// <summary>Get a lighter version of the color perceptually by 30%</summary>
        public Color GetLightenPerceptual() {
            return new(
                R + (int)((255 - R) * 0.3),
                G + (int)((255 - G) * 0.3),
                B + (int)((255 - B) * 0.3)
                );
        }

        /// <summary>Get a version of the color with saturation adjusted by the specified amount.</summary>
        public Color GetSaturate(float amount) {
            return GetSaturate(this, amount);
        }

        public static Color GetSaturate(Color a, float amount) {
            float r = a.R / 255f;
            float g = a.G / 255f;
            float b = a.B / 255f;
            float gray = r * 0.299f + g * 0.587f + b * 0.114f;
            r = gray + (r - gray) * amount;
            g = gray + (g - gray) * amount;
            b = gray + (b - gray) * amount;
            return new Color(
                r * 255,
                g * 255,
                b * 255
            );
        }

        /// <summary>Get the average/mix of this color and another color</summary>
        public Color GetMix(Color a) {
            return GetMix(this, a);
        }

        /// <summary>Get the average/mix of three colors</summary>
        public static Color GetMix(Color a, Color b) {
            return new Color(
                (a.R + b.R) / 3,
                (a.G + b.G) / 3,
                (a.B + b.B) / 3
            );
        }

        /// <summary>Get a linear interpolation between this color and another color by the given amount (0-1)</summary>
        public Color GetLerp(Color a, float amount) {
            return GetLerp(this, a, amount);
        }

        /// <summary>Get a linear interpolation between two colors by the given amount (0-1)</summary>
        public static Color GetLerp(Color a, Color b, float amount) {
            return new Color(
                a.R + (b.R - a.R) * amount,
                a.G + (b.G - a.G) * amount,
                a.B + (b.B - a.B) * amount
            );
        }

        /// <summary>Get a version of the color with its hue rotated by the specified degrees.</summary>
        public Color GetHue(float degrees) {
            return GetHue(this, degrees);
        }

        //This section was generated with the help of ChatGPT
        //I am bad with color math. Please fix if any issues.
        /// <summary>Get a version of the color with its hue rotated by the specified degrees.</summary>
        public static Color GetHue(Color a, float degrees) {
            float r = a.R / 255f;
            float g = a.G / 255f;
            float b = a.B / 255f;
            float u = (float)Math.Cos(degrees * Math.PI / 180);
            float w = (float)Math.Sin(degrees * Math.PI / 180);
            float newR = (0.299f + 0.701f * u + 0.168f * w) * r +
                          (0.587f - 0.587f * u + 0.330f * w) * g +
                          (0.114f - 0.114f * u - 0.497f * w) * b;
            float newG = (0.299f - 0.299f * u - 0.328f * w) * r +
                          (0.587f + 0.413f * u + 0.035f * w) * g +
                          (0.114f - 0.114f * u + 0.292f * w) * b;
            float newB = (0.299f - 0.3f * u + 1.25f * w) * r +
                          (0.587f - 0.588f * u - 1.05f * w) * g +
                          (0.114f + 0.886f * u - 0.203f * w) * b;
            return new Color(
                Math.Min(Math.Max(newR * 255, 0), 255),
                Math.Min(Math.Max(newG * 255, 0), 255),
                Math.Min(Math.Max(newB * 255, 0), 255)
            );
        }
    }
}
