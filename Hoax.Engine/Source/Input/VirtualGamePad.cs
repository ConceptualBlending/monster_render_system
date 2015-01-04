using System;

namespace Hoax.Engine.Input
{
	/// <summary>
	/// Allows retrieval of user interaction with an virtual controller and setting of controller vibration motors.
	/// </summary>
	public class VirtualGamePad
	{
		//
		// Properties
		//
		public VirtualGamePadState State {
			get;
			private set;
		}

		/// <summary>
		/// Sets the vibration motor speeds on an Controller.
		/// </summary>
		/// <returns><b>true</b> if the vibration motors were successfully set; <b>true</b> if the controller was unable to process the request.</returns>
		/// <param name="leftMotor">The speed of the left motor, between 0.0 and 1.0. This motor is a low-frequency motor.</param>
		/// <param name="rightMotor">The speed of the right motor, between 0.0 and 1.0. This motor is a high-frequency motor.</param>
		bool SetVibration (float leftMotor, float rightMotor)
		{
			return this.vibrationAction.Invoke (leftMotor, rightMotor);
		}

		//
		// Fields
		//
		private Func<float, float, bool> vibrationAction;

		//
		// Constructor
		//
		public VirtualGamePad(Func<float, float, bool> vibrationFunction, VirtualGamePadState state) 
		{
			this.vibrationAction = vibrationAction;
			this.State = state;
		}
	}
}

