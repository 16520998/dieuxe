package crc648a38e54fb9ce2fd9;


public class GradientFrameRenderer
	extends crc643f46942d9dd1fff9.VisualElementRenderer_1
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_dispatchDraw:(Landroid/graphics/Canvas;)V:GetDispatchDraw_Landroid_graphics_Canvas_Handler\n" +
			"";
		mono.android.Runtime.register ("dieuxe.Droid.Custom.GradientFrameRenderer, dieuxe.Android", GradientFrameRenderer.class, __md_methods);
	}


	public GradientFrameRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == GradientFrameRenderer.class)
			mono.android.TypeManager.Activate ("dieuxe.Droid.Custom.GradientFrameRenderer, dieuxe.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public GradientFrameRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == GradientFrameRenderer.class)
			mono.android.TypeManager.Activate ("dieuxe.Droid.Custom.GradientFrameRenderer, dieuxe.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public GradientFrameRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == GradientFrameRenderer.class)
			mono.android.TypeManager.Activate ("dieuxe.Droid.Custom.GradientFrameRenderer, dieuxe.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void dispatchDraw (android.graphics.Canvas p0)
	{
		n_dispatchDraw (p0);
	}

	private native void n_dispatchDraw (android.graphics.Canvas p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
