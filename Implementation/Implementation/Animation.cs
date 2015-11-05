using System;
using System.Collections.Generic;
using Framework;

namespace Implementation
{
	public class Animation
	{
		private class AnimFrame
		{
			public IImage Image;
			public int EndTime;

			public AnimFrame(IImage image, int endTime){
				this.Image = image;
				this.EndTime = endTime;
			}
		}
		private List<AnimFrame> frames;
		private int currentFrameIndex;
		private long animTime;
		private long totalDuration;

		public IImage Image {
			get {
				if (frames.Count == 0)
					return null;
				return frames [currentFrameIndex].Image;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Implementation.Animation"/> class.
		/// </summary>
		public Animation ()
		{
			frames = new List<AnimFrame> ();
			totalDuration = 0;

			lock (this) {
				animTime = 0;
				currentFrameIndex = 0;
			}
		}

		/// <summary>
		/// Adds the frame to the animation.
		/// </summary>
		/// <param name="frame">Frame.</param>
		/// <param name="duration">Duration.</param>
		public void AddFrame(IImage frame, int duration)
		{
			lock (this) {
				frames.Add (new AnimFrame(frame, duration));
				totalDuration += duration;
			}
		}

		/// <summary>
		/// Updates the animation with the specified time.
		/// </summary>
		/// <param name="elapsedTime">Elapsed time.</param>
		public void Update(long elapsedTime){
			lock (this) {
				if (frames.Count > 1) {
					animTime += elapsedTime;
					if (animTime >= totalDuration) {
						totalDuration = 0;
						currentFrameIndex = 0;
					}

					while (animTime > frames [currentFrameIndex].EndTime)
						currentFrameIndex++;
				}
			}
		}
	}
}

