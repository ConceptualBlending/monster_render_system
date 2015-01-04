using System;

namespace Hoax.Engine.Input
{
	public interface IGamePad
	{
		//
		// Methods
		//
		GamePadState GetState ();

		bool SetVibration (float leftMotor, float rightMotor);
	}
}

