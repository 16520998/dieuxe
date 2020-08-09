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

[assembly: ExportRenderer(typeof(GradientFrame), typeof(GradientFrameRenderer))]

namespace dieuxe.iOS.Custom
{
    public class GradientFrameRenderer : VisualElementRenderer<Frame>
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            if (this.Element != null)
            {
                if (this.Element is GradientFrame)
                {

                    var obj = (GradientFrame)this.Element;
                    CGColor StartColor = obj.StartColor.ToCGColor();
                    CGColor EndColor = obj.EndColor.ToCGColor();
                    var gradientLayer = new CAGradientLayer();
                    gradientLayer.Frame = rect;
                    gradientLayer.Colors = new CGColor[] { StartColor, EndColor };
                    //for horizontal gra                     if (obj.GradientColorOrientation == GradientFrame.GradientOrientation.Horizontal)
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