using System;

using Implementation;
using Framework;
using System.Collections.Generic;

namespace BeerRun
{
	public class Robot : AbsEntity
	{
		private bool ducked, jumped, movingRight;
		public bool Ducked {
			get { return ducked; }
			set { ducked = value; }
		}
		public bool Jumped {
			get { return jumped; }
		}
		public bool MovingRight {
			get { return movingRight; }
		}
		public bool ReadyToFire {
			get {
				return true;
			}
		}

		private List<Projectile> projectiles;
		public List<Projectile> Projectiles{
			get { return projectiles; }
		}

		public Robot (int x, int y)
			:base (x, y, PictureManager.Pictures ["characater1"])
		{
			
		}

		public void Jump(){

		}

		public void Duck(){

		}

		public void Shoot(){

		}

		public void MoveRight(){

		}

		public void StopMovingRight(){

		}
	}

}

