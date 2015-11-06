using System;

using Implementation;
using Framework;
using System.Collections.Generic;
using Android.Graphics;

namespace BeerRun
{
	public class Robot : AbsEntity
	{
		#region properties and variables
		private readonly int JUMPHEIGHT = -15, MOVESPEED = 5;

		private bool _ducked, _jumped, _movingRight, _movingLeft;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="BeerRun.Robot"/> is ducked.
		/// </summary>
		/// <value><c>true</c> if ducked; otherwise, <c>false</c>.</value>
		public bool Ducked {
			get { return _ducked; }
			set { _ducked = value; }
		}
		/// <summary>
		/// Gets a value indicating whether this <see cref="BeerRun.Robot"/> is jumped.
		/// </summary>
		/// <value><c>true</c> if jumped; otherwise, <c>false</c>.</value>
		public bool Jumped {
			get { return _jumped; }
		}
		/// <summary>
		/// Gets a value indicating whether this <see cref="BeerRun.Robot"/> is moving right.
		/// </summary>
		/// <value><c>true</c> if moving right; otherwise, <c>false</c>.</value>
		public bool MovingRight {
			get { return _movingRight; }
		}
		/// <summary>
		/// Gets a value indicating whether this <see cref="BeerRun.Robot"/> is moving left.
		/// </summary>
		/// <value><c>true</c> if moving left; otherwise, <c>false</c>.</value>
		public bool MovingLeft {
			get { return _movingLeft; }
		}
		/// <summary>
		/// Gets a value indicating whether this <see cref="BeerRun.Robot"/> is ready to fire.
		/// </summary>
		/// <value><c>true</c> if ready to fire; otherwise, <c>false</c>.</value>
		public bool ReadyToFire {
			get {
				return true;
			}
		}

		/// <summary>
		/// Gets the speed along x-axis.
		/// </summary>
		/// <value>The speed x.</value>
		public int speedX{
			get { return _speedX; }
		}

		private List<Projectile> projectiles;
		public List<Projectile> Projectiles{
			get { return projectiles; }
		}

		private Dictionary<string, Collider> colliders;
		public Dictionary<string, Collider> Colliders {
			get { return colliders; }
		}
		#endregion

		public Robot (int x, int y)
			:base (x, y, PictureManager.Pictures ["characater1"])
		{
			projectiles = new List<Projectile> ();
			colliders = new Dictionary<string, Collider> ();

			initializeColliders ();
		}

		private void initializeColliders(){
			colliders.Add ("upper_body", new Collider (-34, -63, 34, 0));
			colliders.Add ("lower_body", new Collider (-34, 0, 34, 128));
			colliders.Add ("left_hand", new Collider(-60, -31, -34, -11));
			colliders.Add ("right_hand", new Collider (34, -31, 60, -11));
			colliders.Add ("left_foot", new Collider (-50, 20, 0, 35));
			colliders.Add ("right_foot", new Collider (0, 20, 50, 35));

			collisionBoundary = new Collider (-110, -110, 70, 70);
		}

		public void Jump(){
			if (!_jumped) {
				_speedY = JUMPHEIGHT;
				_jumped = true;
				_image = PictureManager.Pictures ["character_jumped"];
			}
		}

		public void Landed(Tile t){
			_jumped = false;
			_speedY = 0;
			_y = t.y - (_image.Height / 2);
		}

		public void Duck(){
			if (!_jumped) {
				_speedX = 0;
				_ducked = true;
				_image = PictureManager.Pictures ["character_ducked"];
			}
		}

		public void Shoot(){
			if (ReadyToFire) {
				Projectile p = new Projectile (_x, _y);
				projectiles.Add (p);
			}
		}

		/// <summary>
		/// Moves to the right.
		/// </summary>
		public void MoveRight(){
			if (!_ducked) {
				_speedX = MOVESPEED;
				_movingLeft = false;
				_movingRight = true;
			}
		}

		/// <summary>
		/// Moves to the left.
		/// </summary>
		public void MoveLeft(){
			if (!_ducked) {
				_speedX = -MOVESPEED;
				_movingRight = false;
				_movingLeft = true;
			}
		}

		public void StopMovingRight(){
			_movingRight = false;
			stop ();
		}

		public void StopMovingLeft(){
			_movingLeft = false;
			stop ();
		}

		private void stop(){
			if (!_movingLeft && !_movingRight)
				_speedX = 0;

			if (_movingLeft && !_movingRight)
				MoveLeft ();

			if (!_movingLeft && _movingRight)
				MoveRight ();
		}

		#region implemented abstract members of AbsEntity

		public override void Die ()
		{
			throw new NotImplementedException ();
		}

		public override void Update (float deltaTime)
		{
			//Updates x-position
			if (_speedX < 0)
				_x += _speedX;
			
			if (_x <= 200 || _speedX > 0) {
				_x += _speedX;
			}

			//Updates y-position
			_y += _speedY;

			//Handles jumping
			_speedY += 1;

			if (_speedY > 3)
				_jumped = true;

			//Prevents going left at start
			int halfImgWidth = Image.Width / 2;
			if (x + speedX <= halfImgWidth)
				_x = halfImgWidth;

			foreach (var collider in colliders) {
				collider.Value.Update (_x, _y);
			}
			collisionBoundary.Update (_x, _y);
		}

		public override void Paint (IGraphics graphics)
		{
			base.Paint (graphics);
		}

		#endregion
	}

}

