using System;
using System.Collections.Generic;
using Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Utils;
using System.Drawing;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Core
{
	public class ReferencePointContainer
	{
		//
		// Properties
		//
		public Vector2 GlobalPosition 
		{
			get;
			private set;
		}

		public ReferencePoint[] LocalPoints 
		{
			get;
			private set;
		}
			
		public String Identifier { 
			get; 
			set; 
		}

		public Bitmap Texture {
			get;
			set;
		}

		public int Width {
			get {
				return Texture.Width;
			}
		}

		public int Height {
			get {
				return Texture.Height;
			}
		}

		//
		// Fields
		//
		private List<ReferencePointContainer> childs = new List<ReferencePointContainer>();

		//
		// Constructor
		//
		public ReferencePointContainer(string identifer, Bitmap texture, params ReferencePoint[] localPoints) {
			this.LocalPoints = localPoints;
			this.Identifier = identifer;
			this.Texture = texture;
			this.GlobalPosition = Vector2.Zero;
			checkLocalPointsInBounds (texture, localPoints);

			if (Config.VerboseMode)
				Console.WriteLine ("Create container name={0}, #reference points={1}", identifer, localPoints.Length);
		}

		//
		// Methods
		//
		public void Translate(Vector2 deltaPosition) {
			if (Config.VerboseMode)
				Console.WriteLine (string.Format ("Move container name={0}, global position old={1}, global position new={1}", this.Identifier, this.GlobalPosition, GlobalPosition += deltaPosition));

			GlobalPosition += deltaPosition;
			Array.ForEach (LocalPoints, point => point.UpdateParentPosition (GlobalPosition));
			Array.ForEach (childs.ToArray(), child => child.Translate (GlobalPosition));
		}

		void checkLocalPointsInBounds (Image texture, ReferencePoint[] localPoints)
		{
			var maxwidth = texture.Width;
			var maxheight = texture.Height;
			foreach (var point in localPoints) {
				var x = point.LocalPosition.X;
				var y = point.LocalPosition.Y;

				if (x > maxwidth || y > maxheight || x < 0 || y < 0) {
					var msg = string.Format ("Local point (x={0}, y={1}) definition outside bounds (xmin=0, ymin=0, " +
					          "xmax={2}, ymax={3}) of texture \"{4}\"", x, y, maxwidth, maxheight, Identifier);
					throw new Exception (msg);
				}
			}
		}

		public override string ToString ()
		{
			return string.Format ("[ReferencePointContainer: GlobalPosition={0}, Identifier={2}, Width={4}, Height={5}]", GlobalPosition, LocalPoints, Identifier, Texture, Width, Height);
		}
	}
}

