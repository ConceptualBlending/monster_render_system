using System;
using Microsoft.Xna.Framework;

namespace HoaxFramework
{
	public sealed class Graphics
	{
		private static volatile Graphics instance;
		private static object syncRoot = new Object();

		private Graphics() {}

		public static Graphics Instance
		{
			get 
			{
				if (instance == null) 
				{
					lock (syncRoot) 
					{
						if (instance == null) 
							instance = new Graphics();
					}
				}

				return instance;
			}
		}

		public void SetContext(Canvas canvas)
		{
		}

		public void DrawPolyline(Vector2[] points, Color lineColor, bool autoClose = true) 
		{
			throw new NotImplementedException ();
		}

		public void DrawPolygon(Vector2[] points, Color lineColor, Color fillColor)
		{

		}

	}
}

