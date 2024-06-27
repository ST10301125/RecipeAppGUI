using System;

namespace RecipeAppGUI
{
    internal class PieSeries
    {
        public double StrokeThickness { get; set; }
        public double InsideLabelPosition { get; set; }
        public int AngleSpan { get; set; }
        public int StartAngle { get; set; }
        public object Slices { get; internal set; }

       
    }
}