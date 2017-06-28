using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.ControlGallery.iOS;
using Xamarin.Forms.Controls.Issues;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

//[assembly: ExportRenderer(typeof(Bugzilla41269._41269Page), typeof(_41269Renderer))]
namespace Xamarin.Forms.ControlGallery.iOS
{
	public class _41269Renderer : PageRenderer
	{
		static int s_count;

		~_41269Renderer()
		{
			Interlocked.Decrement(ref s_count);
			Log.Warning("41269 Renderer", $"_41269 renderer instance count is: {s_count}");
		}

		public _41269Renderer()
		{
			Interlocked.Increment(ref s_count);
			Log.Warning("41269 Renderer", $"_41269 renderer instance count is: {s_count}");
		}
	}
}
