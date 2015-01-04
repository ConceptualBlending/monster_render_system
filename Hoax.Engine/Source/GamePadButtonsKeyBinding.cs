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

		public Keys keyBumberLeft 
		{ 
			get; 
			set; 
		}

		public Keys keyTriggerLeft 
		{ 
			get; 
			set; 
		}

		public Keys keyBumberRight 
		{ 
			get; 
			set; 
		}

		public Keys keyTriggerRight 
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
			this.keyBumberLeft = keyLeftShoulder;
			this.keyTriggerLeft = keyLeftStick;
			this.keyBumberRight = keyRightShoulder;
			this.keyTriggerRight = keyRightStick;
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
				this.keyBumberLeft.GetHashCode() * 11 + this.keyTriggerLeft.GetHashCode() * 13 + this.keyBumberRight.GetHashCode() * 17 +
				this.keyTriggerRight.GetHashCode() * 19 + this.keyStart.GetHashCode() * 23 + this.keyX.GetHashCode() * 29 + this.keyY.GetHashCode() * 31;
		}

		//
		// Operators
		//
		public static bool operator == (GamePadButtonsKeyBinding left, GamePadButtonsKeyBinding right) {
			return left.keyA == right.keyA && left.keyB == right.keyB && left.keyBack == right.keyBack && left.keyBigButton == right.keyBigButton &&
				left.keyBumberLeft == right.keyBumberLeft && left.keyTriggerLeft == right.keyTriggerLeft && left.keyBumberRight == right.keyBumberRight &&
				left.keyTriggerRight == right.keyTriggerRight && left.keyStart == right.keyStart && left.keyX == right.keyX && left.keyY == right.keyY;
		}

		public static bool operator != (GamePadButtonsKeyBinding left, GamePadButtonsKeyBinding right) {
			return !(left == right);
		}
	}
}

