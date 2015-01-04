using System;
using C5;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;
using Medusa2;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Core
{
	public class Universe : ArrayList<ReferencePointContainer>
	{
		private Texture2D pointTexture1, pointTexture2;

		//
		// Constructor
		//
		public Universe(Game game) {
			pointTexture1 = game.Content.Load<Texture2D> ("point");
			pointTexture2 = game.Content.Load<Texture2D> ("point2");
		}

		//
		// Methods
		//
		public void Draw(SpriteBatch batch) 
		{
			Vector2 minLeft = this.GetDrawableObjectMinPosition ();
			Vector2 correction = new Vector2 (minLeft.X < 0 ? -minLeft.X : minLeft.X, minLeft.Y < 0 ? -minLeft.Y : minLeft.Y); 

			this.ForEach ( container => 
				{
					batch.Draw (container.Texture, container.GlobalPosition + correction, Color.White);

					if (Config.ShowConnectionsPoints)
						Array.ForEach(container.LocalPoints, point => batch.Draw (point.Type == ReferencePoint.PointType.A ? pointTexture1 : pointTexture2, 
							point.GlobalPosition + new Vector2(-8,-8) + correction, Color.White));
				}
			);
		}

		public bool Contains(string individualName)
		{
			for (int i = 0; i < this.size; i++)
				if (this [i].Identifier == individualName)
					return true;
			return false;
		}

		public float GetMaxWidth ()
		{
			return this.Max (container => container.Width);
		}

		public float GetMaxHeight ()
		{
			return this.Max (container => container.Height);
		}

		public Vector2 GetDrawableObjectMinPosition ()
		{
			var x = this.Min (container => container.GlobalPosition.X); 
			var y = this.Min (container => container.GlobalPosition.Y);

			return new Vector2 (x, y);
		}

		public Vector2 GetDrawableObjectMaxPosition ()
		{
			var x = this.Max (container => container.GlobalPosition.X); 
			var y = this.Max (container => container.GlobalPosition.Y);

			return new Vector2 (x, y);
		}

		public float GetMaximumX ()
		{
			return this.Max (container => container.LocalPoints.Max (point => point.GlobalPosition.X));
		}

		public float GetMaximumY ()
		{
			return this.Max (container => container.LocalPoints.Max (point => point.GlobalPosition.Y));
		}

		public void Connect (string container1, string ref1, string container2, string ref2)
		{
			checkIdentifierUnequal (container1, container2);

			ReferencePointContainer c1, c2;
			findContainer (container1, container2, out c1, out c2);

			ReferencePoint lp1, lp2;
			findPoints (c1, ref1, c2, ref2, out lp1, out lp2);

			calculateMovement (c1, lp1, c2, lp2);
		}

		void findPoints (ReferencePointContainer c1, string ref1, ReferencePointContainer c2, string ref2, out ReferencePoint lp1, out ReferencePoint lp2)
		{
			lp1 = (from point in c1.LocalPoints where point.Name == ref1 select point).First();
			lp2 = (from point in c2.LocalPoints where point.Name == ref2 select point).First();
			if (lp1 == null || lp2 == null) {
				var msg = string.Format ("Container \"{0}\" or \"{1}\" doesn't contain a reference point called \"{2}\" or \"{3}\".", 
					c1.Identifier, c2.Identifier, ref1, ref2);
				throw new Exception (msg);
			}
		}

		void findContainer (string container1, string container2, out ReferencePointContainer c1, out ReferencePointContainer c2)
		{
			c1 = (from container in this where container.Identifier == container1 select container).First();
			c2 = (from container in this where container.Identifier == container2 select container).First();
			if (c1 == null || c2 == null) {
				var msg = string.Format ("Unable to locate container called \"{0}\" or \"{1}\".", 
					container1, container2);
				throw new Exception (msg);
			}
		}

		void checkIdentifierUnequal (string container1, string container2)
		{
			if (container1.Trim ().ToLower () == container2.Trim ().ToLower ()) {
				var msg = string.Format ("It's not allowed that container \"{0}\" is connected to itself.", 
					container1);
				throw new Exception (msg);
			}
		}

		private void calculateMovement (ReferencePointContainer c1, ReferencePoint lp1, ReferencePointContainer c2, ReferencePoint lp2)
		{
			var globalPoint1 = lp1.GlobalPosition;
			var globalPoint2 = lp2.GlobalPosition;
			var delta = globalPoint2 - globalPoint1;
			GroupService.Instance.Apply (c1, groupMember => groupMember.Translate (delta));
			GroupService.Instance.Group (c1, c2);
		}

		private void ForEach(Action<ReferencePointContainer> action) {
			foreach (var element in this)
				action.Invoke (element);
		}
	}
}

