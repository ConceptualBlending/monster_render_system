using System;
using Microsoft.Xna.Framework;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Core
{
	public class ReferencePoint
	{
		//
		// Properties
		//
		public Vector2 LocalPosition 
		{
			get;
			private set;
		}

		public Vector2 GlobalPosition {
			get {
				return LocalPosition + ParentPosition;
			}
		}

		public string Name
		{
			get;
			private set;
		}

		//
		// Fields
		//
		private Vector2 ParentPosition = Vector2.Zero;

		public PointType Type {
			get;
			set;
		}

		public enum PointType {A,B}

		//
		// Constructor
		//
		public ReferencePoint(string name, Vector2 localPosition, PointType pt) {
			this.Name = name;
			this.LocalPosition = localPosition;
			this.Type = pt;
		}

		//
		// Methods
		//
		public void UpdateParentPosition(Vector2 parentPosition) {
			this.ParentPosition = parentPosition;
		}
	}
}

