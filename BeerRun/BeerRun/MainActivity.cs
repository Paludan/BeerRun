using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Implementation;

namespace BeerRun
{
	[Activity (Label = "BeerRun", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : AndroidGame
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
		}
	}
}


