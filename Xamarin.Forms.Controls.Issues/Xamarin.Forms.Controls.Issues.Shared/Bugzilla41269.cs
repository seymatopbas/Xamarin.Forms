using System;
using System.Threading;
using Xamarin.Forms.CustomAttributes;
using Xamarin.Forms.Internals;

#if UITEST
using Xamarin.UITest;
using NUnit.Framework;
using Xamarin.Forms.Core.UITests;
#endif

namespace Xamarin.Forms.Controls.Issues
{
//#if UITEST
//	[Category(UITestCategories.)]
//#endif

	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Bugzilla, 941269, "", PlatformAffected.All)]
	public class Bugzilla41269 : TestNavigationPage 
	{
		protected override void Init()
		{
			_rendererCount = new Label {Text = "..."};
			_pageCount = new Label { Text = "..." };
			_pvcCount = new Label { Text = "..." };

			Log.Listeners.Add(
				new DelegateLogListener((c, m) => Device.BeginInvokeOnMainThread(() =>
				{
					if (c == "41269 Page")
					{
						_pageCount.Text = m;
					}

					if (c == "41269 Renderer")
					{
						_rendererCount.Text = m;
					}

					if (c == "41269 PVC")
					{
						_pvcCount.Text = m;
					}
				})));

			PushAsync(Root());
		}

		Label _rendererCount;
		Label _pageCount;
		Label _pvcCount;

		ContentPage Root()
		{
			var button = new Button { Text = "Push custom page" };
			button.Clicked += (sender, args) => Navigation.PushAsync(new _41269Page(), false);

			var layout = new StackLayout()
			{
				Children = { _pageCount, _rendererCount, _pvcCount, button }
			};

			var result = new ContentPage { Content = layout };

			result.Appearing += (sender, args) =>
			{
				GC.Collect();
				GC.WaitForPendingFinalizers();
			};

			return result;
		}

		[Preserve(AllMembers = true)]
		public class _41269Page : ContentPage
		{
			static int s_count;

			public _41269Page()
			{
				Title = "41269 Page";
				Interlocked.Increment(ref s_count);
				Log.Warning("41269 Page", $"_41269 page instance count is: {s_count}");
			}

			~_41269Page()
			{
				Interlocked.Decrement(ref s_count);
				Log.Warning("41269 Page", $"_41269 page instance count is: {s_count}");
			}
		}

#if UITEST
		//[Test]
		//public void _41269Test()
		//{
		//	//RunningApp.WaitForElement(q => q.Marked(""));
		//}
#endif
	}
}