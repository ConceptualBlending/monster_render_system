using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Hoax.Engine.Input
{
	public sealed class KeyboardGamePadButtons : IGamePadButtons {

		#region IGamePadButtons implementation

		//
		// Properties
		//
		public bool A {
			get {
				return Keyboard.GetState ().IsKeyDown (keyBinding.keyA);
			}
		}

		public bool B {
			get {
				return Keyboard.GetState ().IsKeyDown (keyBinding.keyB);
			}
		}

		public bool Back {
			get {
				return Keyboard.GetState ().IsKeyDown (keyBinding.keyBack);
			}
		}

		public bool BigButton {
			get {
				return Keyboard.GetState ().IsKeyDown (keyBinding.keyBigButton);
			}
		}

		public bool LeftShoulder {
			get {
				return Keyboard.GetState ().IsKeyDown (keyBinding.keyBumberLeft);
			}
		}

		public bool LeftStick {
			get {
				return Keyboard.GetState ().IsKeyDown (keyBinding.keyTriggerLeft);
			}
		}

		public bool RightShoulder {
			get {
				return Keyboard.GetState ().IsKeyDown (keyBinding.keyBumberRight);
			}
		}

		public bool RightStick {
			get {
				return Keyboard.GetState ().IsKeyDown (keyBinding.keyTriggerRight);
			}
		}

		public bool Start {
			get {
				return Keyboard.GetState ().IsKeyDown (keyBinding.keyStart);
			}
		}

		public bool X {
			get {
				return Keyboard.GetState ().IsKeyDown (keyBinding.keyX);
			}
		}

		public bool Y {
			get {
				return Keyboard.GetState ().IsKeyDown (keyBinding.keyY);
			}
		}

		#endregion

		//
		// Private fields
		//
		GamePadButtonsKeyBinding keyBinding;

		//
		// Constructors
		//
		public KeyboardGamePadButtons(GamePadButtonsKeyBinding keyBinding)
		{
			this.keyBinding = keyBinding;
		}
	}	
}
