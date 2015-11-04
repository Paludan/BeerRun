using System;

using Android.Runtime;

namespace Implementation
{
	public static class JavaLangSystem
	{
		static IntPtr class_ref;
		static IntPtr id_nanoTime;

		static JavaLangSystem ()
		{
			class_ref = JNIEnv.FindClass ("java/lang/System");
			id_nanoTime = JNIEnv.GetStaticMethodID (class_ref, "nanoTime", "()J");
		}

		[Register("nanoTime", "()J", "")]
		public static long NanoTime()
		{
			return JNIEnv.CallStaticLongMethod (class_ref, id_nanoTime);
		}
	}
}

