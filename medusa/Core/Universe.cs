using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Utils;
using System.Drawing;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Core
{
	public class Universe : List<ReferencePointContainer>
	{
		//private Image pointTexture1, pointTexture2;

		//
		// Constructor
		//
		public Universe() {
			//pointTexture1 = game.Content.Load<Texture2D> ("point");
			//pointTexture2 = game.Content.Load<Texture2D> ("point2");
		}

		//
		// Methods
		//
		public Bitmap Draw() 
		{
			Vector2 minLeft = this.GetDrawableObjectMinPosition ();
			Vector2 correction = new Vector2 (minLeft.X < 0 ? -minLeft.X : minLeft.X, minLeft.Y < 0 ? -minLeft.Y : minLeft.Y); 

			Bitmap result = new Bitmap ((int) this.GetOverallWidth (), (int) this.GetOverallHeight ());

			this.ForEach ( container => 
				{
					using (Graphics grD = Graphics.FromImage(result))            
					{
						grD.DrawImage(container.Texture, new RectangleF(correction.X + container.GlobalPosition.X, correction.Y + container.GlobalPosition.Y, container.Texture.Width, container.Texture.Height));                
					}
				//	batch.Draw (container.Texture, container.GlobalPosition + correction, Color.White);

				//	if (Config.ShowConnectionsPoints)
				//		Array.ForEach(container.LocalPoints, point => batch.Draw (point.Type == ReferencePoint.PointType.A ? pointTexture1 : pointTexture2, 
				//			point.GlobalPosition + new Vector2(-8,-8) + correction, Color.White));
				}
			);

			return result;
		}

		public bool Contains(string individualName)
		{
			for (int i = 0; i < this.Count; i++)
				if (this [i].Identifier == individualName)
					return true;
			return false;
		}

		public int GetOverallWidth ()
		{
			int maxX = 0;

			Vector2 minpos = this.GetDrawableObjectMinPosition ();
			Vector2 correction = new Vector2 (minpos.X < 0 ? -minpos.X : minpos.X, minpos.Y < 0 ? -minpos.Y : minpos.Y); 

			foreach (var container in this) {
				var objx = correction.X + container.GlobalPosition.X + container.Width;
				maxX = (int) Math.Max (maxX, objx);

			}
				
			return maxX;
		}

		public int GetOverallHeight ()
		{
			int maxY = 0;

			Vector2 minpos = this.GetDrawableObjectMinPosition ();
			Vector2 correction = new Vector2 (minpos.X < 0 ? -minpos.X : minpos.X, minpos.Y < 0 ? -minpos.Y : minpos.Y); 

			foreach (var container in this) {
				var objy = correction.Y + container.GlobalPosition.Y + container.Height;
				maxY = (int) Math.Max (maxY, objy);

			}

			return maxY;
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

			if (Config.VerboseMode)
				Console.WriteLine (string.Format ("Connect container={0}, ref1={1} with container={2}, ref1={3}", container1, ref1, container2, ref2));
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

		public override string ToString ()
		{
			string s = "";
			foreach (var o in this)
				s += o.ToString () + "\n\t";
			return string.Format ("[Universe] : \n\t"+s);
		}
	}
}

