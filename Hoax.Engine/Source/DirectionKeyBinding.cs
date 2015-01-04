using System;
using Microsoft.Xna.Framework.Input;

namespace Hoax.Engine.Input
{
	public struct DirectionKeyBinding
	{
		//
		// Properties
		//
		public Keys keyLeft 
		{ 
			get; 
			set; 
		}

		public Keys keyUp 
		{ 
			get; 
			set; 
		}

		public Keys keyRight 
		{ 
			get; 
			set; 
		}

		public Keys keyDown 
		{ 
			get; 
			set; 
		}

		//
		// Constructors
		//
		public DirectionKeyBinding(Keys keyLeft, Keys keyTop, Keys keyRight, Keys keyDown)
		{
			this = default(DirectionKeyBinding);
			this.keyLeft = keyLeft;
			this.keyUp = keyTop;
			this.keyRight = keyRight;
			this.keyDown = keyDown;
		}

		//
		// Methods
		//
		public override bool Equals (object obj)
		{
			return obj is DirectionKeyBinding && this == (DirectionKeyBinding)obj;
		}

		public override int GetHashCode ()
		{
			return this.keyDown.GetHashCode() * 2 + this.keyLeft.GetHashCode() * 3 + this.keyRight.GetHashCode() * 5 + this.keyUp.GetHashCode() * 7;
		}

		//
		// Operators
		//
		public static bool operator == (DirectionKeyBinding left, DirectionKeyBinding right) {
			return left.keyDown == right.keyDown && left.keyLeft == right.keyLeft && left.keyRight == right.keyRight && left.keyUp == right.keyUp;
		}

		public static bool operator != (DirectionKeyBinding left, DirectionKeyBinding right) {
			return !(left == right);
		}
	}
}

