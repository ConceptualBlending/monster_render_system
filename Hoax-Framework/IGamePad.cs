using System;

namespace Hoax.Engine.Input
{
	public interface IGamePad
	{
		GamePadState GetState ();
		bool SetVibration (float leftMotor, float rightMotor);
	}
}

