using System;
using Microsoft.Xna.Framework;

namespace Hoax.Engine.Input
{
	public interface IGamePadThumbSticks
	{
		//
		// Properties
		//
		Vector2 Left 
		{ 
			get; 
		}

		Vector2 Right 
		{ 
			get; 
		}
	}
}

