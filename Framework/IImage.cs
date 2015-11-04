using System;

namespace GameFramework
{
	public interface IImage
	{
		int Width { get; }
		int Height { get; }
		ImageFormat Format { get; }
		void Dispose();
	}
}

