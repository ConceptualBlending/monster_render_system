using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Hoax.Engine.Input
{

	internal class KeyboardGamePadTriggers : IGamePadTriggers {

		#region IGamePadTriggers implementation
		//
		// Properties
		//
		public float Left {
			get {
				return Keyboard.GetState ().IsKeyDown (keyBinding.keyLeft) ? 1.0f : 0.0f;
			}
		}
		public float Right {
			get {
				return Keyboard.GetState ().IsKeyDown (keyBinding.keyRight) ? 1.0f : 0.0f;
			}
		}
		#endregion

		//
		// Private fields
		//
		GamePadTriggerKeyBinding keyBinding;

		//
		// Constructors
		//
		public KeyboardGamePadTriggers(GamePadTriggerKeyBinding keyBinding)
		{
			this.keyBinding = keyBinding;
		}
	}
}
