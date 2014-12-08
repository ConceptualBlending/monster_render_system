using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Hoax.Engine.Input
{
	public sealed class KeyboardGamePad : IGamePad
	{
		#region IGamePad implementation
		public GamePadState GetState ()
		{
			return gamePadState;
		}
		public bool SetVibration (float leftMotor, float rightMotor)
		{
			return true;
		}
		#endregion

		//
		// Private fields
		//
		private GamePadState gamePadState;


		//
		// Constructors
		//
		public KeyboardGamePad (GamePadButtonsKeyBinding gamePadButtonsKeyBinding, DirectionKeyBinding dPadKeyBinding, DirectionKeyBinding thumbStickLeftBinding,
			DirectionKeyBinding thumbStickRightBinding, GamePadTriggerKeyBinding gamePadTriggerKeyBinding)
		{
			gamePadState = new GamePadState () {
				Buttons = new KeyboardGamePadButtons (gamePadButtonsKeyBinding),
				DPad = new KeyboardGamePadDPad (dPadKeyBinding),
				IsConnected = true,
				ThumbSticks = new KeyboardThumbSticks (thumbStickLeftBinding, thumbStickRightBinding),
				Triggers = new KeyboardGamePadTriggers (gamePadTriggerKeyBinding)
			};
		}
	}		
}

