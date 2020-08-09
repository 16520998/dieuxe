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

[assembly: ExportRenderer(typeof(GradientStackLayout), typeof(GradientStackLayoutRenderer))]

namespace dieuxe.Droid.Custom
{
    public class GradientStackLayoutRenderer : VisualElementRenderer<StackLayout>
    {
        public GradientStackLayoutRenderer(Context context) : base(context)
        {

        }
        protected override void DispatchDraw(Canvas canvas)
        {
            base.DispatchDraw(canvas);
            LinearGradient gradient = null;

            // For Horizontal Gradient
            if (((GradientStackLayout)Element).GradientColorOrientation == GradientStackLayout.GradientOrientation.Horizontal)
            {
                gradient = new Android.Graphics.LinearGradient(0, 0, Width, 0,



                     ((GradientStackLayout)Element).StartColor.ToAndroid(),
                     ((GradientStackLayout)Element).EndColor.ToAndroid(),

                     Android.Graphics.Shader.TileMode.Mirror);

            }
            //For Vertical Gradient
            if (((GradientStackLayout)Element).GradientColorOrientation == GradientStackLayout.GradientOrientation.Vertical)
            {
                gradient = new Android.Graphics.LinearGradient(0, 0, 0, Height,



                     ((GradientStackLayout)Element).StartColor.ToAndroid(),
                     ((GradientStackLayout)Element).EndColor.ToAndroid(),

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