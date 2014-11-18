using System;
using Microsoft.Xna.Framework;

namespace Hoax.Framework.Components.Graphics2D
{
	public class Transformation2D
	{
		public Transformation2D ()
		{
			LocalScale = WorldScale = Vector2.One;
			LocalPosition = WorldPosition = Vector2.Zero;
			LocalRotation = WorldRotation = .0f;
		}

		public Vector2 LocalPosition { get; internal set; }

		public Vector2 WorldPosition { get; internal set; }

		public Vector2 LocalScale { get; internal set; }

		public Vector2 WorldScale { get; internal set; }

		public float LocalRotation { get; internal set; }

		public float WorldRotation { get; internal set; }

		public Vector2 PivotPoint { get; internal set; }

		public Matrix WorldMatrix { get; internal set; }

	}
}

