﻿using System;
using Java.IO;

using Android.Content;

namespace Framework
{
	public interface IFileIO
	{
		FileInputStream ReadFile (string fileName);
		FileOutputStream WriteFile (string fileName);
		System.IO.Stream ReadAsset (string assetName);
		ISharedPreferences SharedPrefs { get; }
	}
}

