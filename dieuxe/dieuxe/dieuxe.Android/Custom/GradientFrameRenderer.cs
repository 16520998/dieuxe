using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using dieuxe.CustomControl;
using dieuxe.Droid.Custom;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(GradientFrame), typeof(GradientFrameRenderer))]

namespace dieuxe.Droid.Custom
{
    public class GradientFrameRenderer : VisualElementRenderer<Frame>
    {
        public GradientFrameRenderer(Context context) : base(context)
        {

        }
        protected override void DispatchDraw(Canvas canvas)
        {
            base.DispatchDraw(canvas);
            LinearGradient gradient = null;

            //for horizontal gradient
            if (((GradientFrame)Element).GradientColorOrientation == GradientFrame.GradientOrientation.Horizontal)
            {
                gradient = new Android.Graphics.LinearGradient(0, 0, Width, 0,



                     ((GradientFrame)Element).StartColor.ToAndroid(),
                     ((GradientFrame)Element).EndColor.ToAndroid(),

                     Android.Graphics.Shader.TileMode.Mirror);

            }
            //for vertical gradient
            if (((GradientFrame)Element).GradientColorOrientation == GradientFrame.GradientOrientation.Vertical)
            {
                gradient = new Android.Graphics.LinearGradient(0, 0, 0, Height,



                     ((GradientFrame)Element).StartColor.ToAndroid(),
                     ((GradientFrame)Element).EndColor.ToAndroid(),

                     Android.Graphics.Shader.TileMode.Mirror);

            }

            var paint = new Android.Graphics.Paint()
            {
                Dither = true,
            };
            paint.SetShader(gradient);
            canvas.DrawPaint(paint);
            base.DispatchDraw(canvas);
        }
    }
}