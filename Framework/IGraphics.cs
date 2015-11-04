using System;
using Android.Graphics;

namespace GameFramework
{
	public enum ImageFormat {
		ARGB8888, ARGB4444, RGB565 
	};

	public interface IGraphics
	{
		IImage NewImage(string fileName, ImageFormat format);
		void ClearScreen(int color);
		void DrawLine(int x, int y, int x2, int y2, int color);
		void DrawRect(int x, int y, int width, int height, int color);
		void DrawImage(IImage image, int x, int y, int srcX, int srcY,
			int srcWidth, int srcHeight);
		void DrawImage(IImage Image, int x, int y);
		void DrawString(String text, int x, int y, Paint paint);
		int Width { get; }
		int Height { get; }
		void DrawARGB(int a, int r, int g, int b);
	}
}

