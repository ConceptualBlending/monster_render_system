using System;
using HoaxFramework;
using HoaxFramework.CaptainCash.Core;
using Hoax.Engine.Services;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCash.Core
{
	public class CaptainCashLauncher : GameWindow
	{
		#region implemented abstract members of GameWindow

		public override AbstractScene GetStartScene ()
		{
			return new SceneBlue (this, base.GraphicsDevice);
		}

		#endregion




	}
}

