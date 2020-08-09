using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreAnimation;
using CoreGraphics;
using dieuxe.CustomControl;
using dieuxe.iOS.Custom;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GradientStackLayout), typeof(GradientStackLayoutRenderer))]

namespace dieuxe.iOS.Custom
{
    public class GradientStackLayoutRenderer : VisualElementRenderer<StackLayout>
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            if (this.Element != null)
            {
                if (this.Element is GradientStackLayout)
                {

                    var obj = (GradientStackLayout)this.Element;
                    CGColor StartColor = obj.StartColor.ToCGColor();
                    CGColor EndColor = obj.EndColor.ToCGColor();
                    var gradientLayer = new CAGradientLayer();
                    gradientLayer.Frame = rect;
                    gradientLayer.Colors = new CGColor[] { StartColor, EndColor };
                    //for horizontal gradient                     if (obj.GradientColorOrientation == GradientStackLayout.GradientOrientation.Horizontal)
                    {
                        gradientLayer.StartPoint = new CGPoint(0.0, 0.5);
                        gradientLayer.EndPoint = new CGPoint(1.0, 0.5);
                    }
                    NativeView.Layer.InsertSublayer(gradientLayer, 0);
                }
            }
        }
    }

}