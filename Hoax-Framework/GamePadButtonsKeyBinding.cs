using System;
using Microsoft.Xna.Framework.Input;

namespace Hoax.Engine.Input
{
	public struct GamePadButtonsKeyBinding
	{
		//
		// Properties
		//
		public Keys keyA 
		{ 
			get; 
			set; 
		}

		public Keys keyB 
		{ 
			get; 
			set; 
		}

		public Keys keyBack 
		{ 
			get; 
			set; 
		}

		public Keys keyBigButton 
		{ 
			get; 
			set; 
		}

		public Keys keyLeftShoulder 
		{ 
			get; 
			set; 
		}

		public Keys keyLeftStick 
		{ 
			get; 
			set; 
		}

		public Keys keyRightShoulder 
		{ 
			get; 
			set; 
		}

		public Keys keyRightStick 
		{ 
			get; 
			set; 
		}

		public Keys keyStart 
		{ 
			get; 
			set; 
		}

		public Keys keyX 
		{ 
			get; 
			set; 
		}

		public Keys keyY 
		{ 
			get; 
			set; 
		}

		//
		// Constructors
		//
		public GamePadButtonsKeyBinding(Keys keyA, Keys keyB, Keys keyBack, Keys keyBigButton, Keys keyLeftShoulder, Keys keyLeftStick, 
			Keys keyRightShoulder, Keys keyRightStick, Keys keyStart, Keys keyX, Keys keyY)
		{
			this = default(GamePadButtonsKeyBinding);
			this.keyA = keyA;
			this.keyB = keyB;
			this.keyBack = keyBack;
			this.keyBigButton = keyBigButton;
			this.keyLeftShoulder = keyLeftShoulder;
			this.keyLeftStick = keyLeftStick;
			this.keyRightShoulder = keyRightShoulder;
			this.keyRightStick = keyRightStick;
			this.keyStart = keyStart;
			this.keyX = keyX;
			this.keyY = keyY;
		}

		//
		// Methods
		//
		public override bool Equals (object obj)
		{
			return obj is GamePadButtonsKeyBinding && this == (GamePadButtonsKeyBinding)obj;
		}

		public override int GetHashCode ()
		{
			return this.keyA.GetHashCode() * 2 + this.keyB.GetHashCode() * 3 + this.keyBack.GetHashCode() * 5 + this.keyBigButton.GetHashCode() * 7 + 
				this.keyLeftShoulder.GetHashCode() * 11 + this.keyLeftStick.GetHashCode() * 13 + this.keyRightShoulder.GetHashCode() * 17 +
				this.keyRightStick.GetHashCode() * 19 + this.keyStart.GetHashCode() * 23 + this.keyX.GetHashCode() * 29 + this.keyY.GetHashCode() * 31;
		}

		//
		// Operators
		//
		public static bool operator == (GamePadButtonsKeyBinding left, GamePadButtonsKeyBinding right) {
			return left.keyA == right.keyA && left.keyB == right.keyB && left.keyBack == right.keyBack && left.keyBigButton == right.keyBigButton &&
				left.keyLeftShoulder == right.keyLeftShoulder && left.keyLeftStick == right.keyLeftStick && left.keyRightShoulder == right.keyRightShoulder &&
				left.keyRightStick == right.keyRightStick && left.keyStart == right.keyStart && left.keyX == right.keyX && left.keyY == right.keyY;
		}

		public static bool operator != (GamePadButtonsKeyBinding left, GamePadButtonsKeyBinding right) {
			return !(left == right);
		}
	}
}

