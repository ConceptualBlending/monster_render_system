using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HoaxFramework
{
	public static class GraphicsDeviceExtension
	{
		private static Stack<RenderTarget2D> renderTargetStack = new Stack<RenderTarget2D>();
		private static Stack<Color[]> renderTargetDataStack = new Stack<Color[]>();

		public static void pushRenderTarget (this GraphicsDevice graphicsDevice, RenderTarget2D target)
		{
			xxxPushRenderTarget (graphicsDevice, target);
		}

		public static void popRenderTarget (this GraphicsDevice graphicsDevice)
		{
			xxxPopRenderTarget (graphicsDevice);
		}

		public static void xxxPushRenderTarget (this GraphicsDevice graphicsDevice, RenderTarget2D target)
		{
			/*if (renderTargetStack.Count == 0) {
				renderTargetStack.Push (target);
			}
			Color[] data = new Color[ renderTargetStack.Peek().Width * renderTargetStack.Peek().Height ];
			renderTargetStack.Peek().GetData(data);
			renderTargetDataStack.Push (data);

			graphicsDevice.SetRenderTarget (target);
			renderTargetStack.Push (target);*/

			graphicsDevice.SetRenderTarget (target);
		}

		public static void xxxPopRenderTarget (this GraphicsDevice graphicsDevice)
		{
		/*	Color[] data = renderTargetDataStack.Pop ();
			RenderTarget2D target = renderTargetStack.Pop ();

			target.SetData (data);

			System.Console.WriteLine ("Data:" + renderTargetDataStack.Count);
			System.Console.WriteLine ("Targets: " + renderTargetStack.Count);

			graphicsDevice.SetRenderTarget (target);*/

			graphicsDevice.SetRenderTarget (null);
		}
	}
}

