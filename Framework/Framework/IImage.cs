using System;

namespace Framework
{
	public interface IImage
	{
		int Width { get; }
		int Height { get; }
		ImageFormat Format { get; }
		void Dispose();
	}
}

