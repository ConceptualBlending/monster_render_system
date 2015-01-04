using System;
using Microsoft.Xna.Framework;
using Hoax.Engine.Extensions;

namespace Hoax.Engine.Graphics2D
{
	public class Transformation2D
	{
		public Transformation2D ()
		{
			LocalScale = WorldScale = Vector2.One;
			LocalPosition = WorldPosition = Vector2.Zero;
			LocalRotation = WorldRotation = .0f;
			WorldMatrix = new Matrix ();
		}

		public Vector2 LocalPosition { get; internal set; }

		public Vector2 WorldPosition { get; internal set; }

		public Vector2 LocalScale { get; internal set; }

		public Vector2 WorldScale { get; internal set; }

		public float LocalRotation { get; internal set; }

		public float WorldRotation { get; internal set; }

		public Vector2 PivotPoint { get; internal set; }

		public Matrix WorldMatrix { get; internal set; }

		public void UpdateWorldTransformation() 
		{
			Vector3 pos, scale;

			Quaternion rot;
			if (!WorldMatrix.Decompose (out scale, out rot, out pos))
				throw new Exception (WorldMatrix.ToString());

			WorldPosition = WorldPosition.Project2D (pos);
			WorldScale = WorldScale.Project2D (scale);

			var direction = Vector2.Transform (Vector2.UnitX, rot);
			WorldRotation = (float)Math.Atan2 (direction.Y, direction.X);
			WorldRotation = float.IsNaN (WorldRotation) ? 0 :
				MathHelper.ToDegrees (WorldRotation);
		}

	}

}

