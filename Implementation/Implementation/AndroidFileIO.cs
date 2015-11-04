using System;

using GameFramework;
using Java.IO;
using Android.Content;
using Android.Content.Res;
using Android.Preferences;
using System.IO;

namespace Implementation
{
	public class AndroidFileIO : IFileIO
	{
		Context ctxt;
		AssetManager assets;
		string externalStoragePath;

		public AndroidFileIO (Context context)
		{
			this.ctxt = context;
			this.assets = context.Assets;
			this.externalStoragePath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + Java.IO.File.Separator;// System.IO.File.Separator;
		}

		#region IFileIO implementation

		public FileInputStream ReadFile (string fileName)
		{
			return new FileInputStream (externalStoragePath + fileName);
		}

		public FileOutputStream WriteFile (string fileName)
		{
			return new FileOutputStream (externalStoragePath + fileName);
		}

		public Stream ReadAsset (string assetName)
		{
			return assets.Open (assetName);
		}

		public ISharedPreferences SharedPrefs {
			get {
				return PreferenceManager.GetDefaultSharedPreferences (ctxt);
			}
		}

		#endregion
	}
}

