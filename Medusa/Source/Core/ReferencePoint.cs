﻿using System;
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

			if (Config.VerboseMode)
				Console.WriteLine (string.Format ("Create reference point name={0}, x={1}, y={2}", name, localPosition.X, localPosition.Y));
		}

		//
		// Methods
		//
		public void UpdateParentPosition(Vector2 parentPosition) {
			if (Config.VerboseMode)
				Console.WriteLine (string.Format ("Update reference point name={0}, parent old={1}, parent new={2}", this.Name, this.ParentPosition, parentPosition));

			this.ParentPosition = parentPosition;
		}

		public override string ToString ()
		{
			return string.Format ("[ReferencePoint: LocalPosition={0}, GlobalPosition={1}, Name={2}, Type={3}]", LocalPosition, GlobalPosition, Name, Type);
		}
	}
}

