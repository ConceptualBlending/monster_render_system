using System;

namespace Hoax.Engine.Input
{
	public interface IGamePadDPad
	{
		//
		// Properties
		//
		bool Left 
		{ 
			get;
		}

		bool Up 
		{ 
			get; 
		}

		bool Right 
		{ 
			get; 
		}

		bool Down 
		{ 
			get; 
		}
	}
}

