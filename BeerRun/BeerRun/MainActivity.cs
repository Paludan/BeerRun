using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Implementation;
using Framework;
using Java.IO;
using System.Text;
using Android.Util;
using System.IO;

namespace BeerRun
{
	[Activity (Label = "BeerRun", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : AndroidGame
	{
		public string map;
		bool firstTimeCreate = true;

		public override Screen InitScreen {
			get {
				if (firstTimeCreate) {
					MusicManager.Load (this);
					firstTimeCreate = false;
				}

				var iStream = Resources.OpenRawResource (Resource.Raw.map1);
				map = convertStreamToString (iStream);

				return new SplashLoadingScreen (this);
			}
		}

		public override void OnBackPressed ()
		{
			CurrentScreen.BackButton ();
		}

		/// <summary>
		/// Converts the stream to a string.
		/// </summary>
		/// <returns>A string with the contents read from the stream.</returns>
		/// <param name="s">S.</param>
		private string convertStreamToString(Stream s)
		{
			BufferedReader br = new BufferedReader (new InputStreamReader(s));
			StringBuilder sb = new StringBuilder ();

			string line = null;
			try {
				while((line = br.ReadLine()) != null)
					sb.AppendLine(line);
			} catch (Exception e) {
				Log.Warn ("PARSING INPUT MAP", e.Message);
			} finally {
				try {
					s.Close();
				} catch (Exception e) {
					Log.Warn ("CLOSING STREAM", e.Message);
				}
			}

			return sb.ToString ();
		}

		protected override void OnResume ()
		{
			base.OnResume ();

			//Assets.Theme.Play;
		}

		protected override void OnPause ()
		{
			base.OnPause ();

			//Assets.Theme.Pause ();
		}

	}
}


