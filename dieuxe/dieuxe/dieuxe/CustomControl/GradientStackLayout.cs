using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace dieuxe.CustomControl
{
    public class GradientStackLayout : StackLayout
    {   //Peropeties 
        public Color StartColor
        {
            get; set;
        }

        public Color EndColor
        {
            get; set;
        }

        public GradientOrientation GradientColorOrientation
        {
            get; set;
        }
        // gradient orientation enum
        public enum GradientOrientation
        {
            Vertical,
            Horizontal
        }
    }
}
