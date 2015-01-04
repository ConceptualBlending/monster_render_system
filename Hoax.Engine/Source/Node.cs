﻿using System;
using System.Diagnostics;

namespace Hoax.Engine.Graphics2D
{
	public abstract class Node : Entity
	{
		public Node (string identifier) : base (identifier)
		{
		}

		public override void LoadContent ()
		{
			OnNodeLoadContentStarts (new ReferenceEventArg (Identifier));
			foreach (Node child in Children)
				child.LoadContent ();
			OnNodeLoadContentEnds (new ReferenceEventArg (Identifier));

		}

		public override void UnloadContent () 
		{
			foreach (Node child in Children)
				child.UnloadContent ();
		}
	}
}

