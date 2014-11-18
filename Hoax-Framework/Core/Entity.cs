using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using Hoax.Framework.Components.Services;
using Hoax.Framework.Common.Utils;

namespace Hoax.Framework.Components.Graphics2D
{
	public abstract class Entity : IReadOnlyGraph<Entity>
	{
		#region Constant Fields

		public static int UPDATE_FLAG_UPTODATE = 0x0;
		public static int UPDATE_FLAG_TRANSFORMATION = 0x1;

		#endregion

		#region Fields

		protected List<Entity> Children = new List<Entity> ();
		private Game game;

		#endregion

		#region Constructors

		public Entity (string identifier)
		{
			Transformation2D = new Transformation2D ();
			ContentState = new ContentState ();

			Identifier = identifier;
			UpdateFlags = UPDATE_FLAG_TRANSFORMATION;

			OnLoadContentEndsHandler += (sender, args) => ContentState.LocalContentIsLoaded = true;
			OnNodeLoadContentEndsHandler += (sender, args) => ContentState.EntireContentIsLoaded = true;

			OnUnloadContentEndsHandler += (sender, args) => ContentState.LocalContentIsUnloaded = true;
			OnNodeUnloadContentEndsHandler += (sender, args) => ContentState.EntireContentIsUnloaded = true;
		}

		#endregion

		#region Events

		public delegate void LoadContentStartHandler (object sender, ReferenceResourceEventArgs e);

		public delegate void LoadContentEndsHandler (object sender, ReferenceEventArg e);

		public delegate void NodeLoadContentStartsHandler (object sender, ReferenceEventArg e);

		public delegate void NodeLoadContentEndsHandler (object sender, ReferenceEventArg e);

		public delegate void UnloadContentStartHandler (object sender, ReferenceResourceEventArgs e);

		public delegate void UnloadContentEndsHandler (object sender, ReferenceEventArg e);

		public delegate void NodeUnloadContentStartsHandler (object sender, ReferenceEventArg e);

		public delegate void NodeUnloadContentEndsHandler (object sender, ReferenceEventArg e);

		private event LoadContentStartHandler _onLoadContentStartHandler;
		private event LoadContentEndsHandler _onLoadContentEndsHandler;
		private event NodeLoadContentStartsHandler _onNodeLoadContentStartsHandler;
		private event NodeLoadContentEndsHandler _onNodeLoadContentEndsHandler;
		private event UnloadContentStartHandler _onUnloadContentStartHandler;
		private event UnloadContentEndsHandler _onUnloadContentEndsHandler;
		private event NodeUnloadContentStartsHandler _onNodeUnloadContentStartsHandler;
		private event NodeUnloadContentEndsHandler _onNodeUnloadContentEndsHandler;

		public event LoadContentStartHandler OnLoadContentStartHandler {
			add {
				_onLoadContentStartHandler += value;
				ForEachChild (child => child.OnLoadContentStartHandler += value);
			}

			remove {
				_onLoadContentStartHandler -= value;
				ForEachChild (child => child.OnLoadContentStartHandler -= value);
			}
		}

		public event LoadContentEndsHandler OnLoadContentEndsHandler {
			add {
				_onLoadContentEndsHandler += value;
				ForEachChild (child => child.OnLoadContentEndsHandler += value);
			}

			remove {
				_onLoadContentEndsHandler -= value;
				ForEachChild (child => child.OnLoadContentEndsHandler -= value);
			}
		}

		public event NodeLoadContentStartsHandler OnNodeLoadContentStartsHandler {
			add {
				_onNodeLoadContentStartsHandler += value;
				ForEachChild (child => child.OnNodeLoadContentStartsHandler += value);
			}

			remove {
				_onNodeLoadContentStartsHandler -= value;
				ForEachChild (child => child.OnNodeLoadContentStartsHandler -= value);
			}
		}

		public event NodeLoadContentEndsHandler OnNodeLoadContentEndsHandler {
			add {
				_onNodeLoadContentEndsHandler += value;
				ForEachChild (child => child.OnNodeLoadContentEndsHandler += value);
			}

			remove {
				_onNodeLoadContentEndsHandler -= value;
				ForEachChild (child => child.OnNodeLoadContentEndsHandler -= value);
			}
		}

		protected void OnLoadContentStarts (ReferenceResourceEventArgs e)
		{
			if (_onLoadContentStartHandler != null)
				_onLoadContentStartHandler (this, e);
		}

		protected void OnLoadContentEnds (ReferenceEventArg e)
		{
			if (_onLoadContentEndsHandler != null)
				_onLoadContentEndsHandler (this, e);
		}

		protected void OnNodeLoadContentStarts (ReferenceEventArg e)
		{
			if (_onNodeLoadContentStartsHandler != null)
				_onNodeLoadContentStartsHandler (this, e);
		}

		protected void OnNodeLoadContentEnds (ReferenceEventArg e)
		{
			if (_onNodeLoadContentEndsHandler != null)
				_onNodeLoadContentEndsHandler (this, e);
		}

		public event UnloadContentStartHandler OnUnloadContentStartHandler {
			add {
				_onUnloadContentStartHandler += value;
				ForEachChild (child => child.OnUnloadContentStartHandler += value);
			}

			remove {
				_onUnloadContentStartHandler -= value;
				ForEachChild (child => child.OnUnloadContentStartHandler -= value);
			}
		}

		public event UnloadContentEndsHandler OnUnloadContentEndsHandler {
			add {
				_onUnloadContentEndsHandler += value;
				ForEachChild (child => child.OnUnloadContentEndsHandler += value);
			}

			remove {
				_onUnloadContentEndsHandler -= value;
				ForEachChild (child => child.OnUnloadContentEndsHandler -= value);
			}
		}

		public event NodeUnloadContentStartsHandler OnNodeUnloadContentStartsHandler {
			add {
				_onNodeUnloadContentStartsHandler += value;
				ForEachChild (child => child.OnNodeUnloadContentStartsHandler += value);
			}

			remove {
				_onNodeUnloadContentStartsHandler -= value;
				ForEachChild (child => child.OnNodeUnloadContentStartsHandler -= value);
			}
		}

		public event NodeUnloadContentEndsHandler OnNodeUnloadContentEndsHandler {
			add {
				_onNodeUnloadContentEndsHandler += value;
				ForEachChild (child => child.OnNodeUnloadContentEndsHandler += value);
			}

			remove {
				_onNodeUnloadContentEndsHandler -= value;
				ForEachChild (child => child.OnNodeUnloadContentEndsHandler -= value);
			}
		}

		protected void OnUnloadContentStarts (ReferenceResourceEventArgs e)
		{
			if (_onUnloadContentStartHandler != null)
				_onUnloadContentStartHandler (this, e);
		}

		protected void OnUnloadContentEnds (ReferenceEventArg e)
		{
			if (_onUnloadContentEndsHandler != null)
				_onUnloadContentEndsHandler (this, e);
		}

		protected void OnUnlodeLoadContentStarts (ReferenceEventArg e)
		{
			if (_onNodeUnloadContentStartsHandler != null)
				_onNodeUnloadContentStartsHandler (this, e);
		}

		protected void OnNodeUnloadContentEnds (ReferenceEventArg e)
		{
			if (_onNodeUnloadContentEndsHandler != null)
				_onNodeUnloadContentEndsHandler (this, e);
		}


		#endregion

		#region Properties

		public String Identifier { get; protected set; }

		public Transformation2D Transformation2D { get; protected set; }

		public ContentState ContentState { get; protected set; }

		public ICollection<string> Tags { 
			get {
				return _tags;
			}
		}

		public int UpdateFlags { get; protected set; }

		public Game Game {
			get {
				return game;
			}
			set {
				game = value;
				ForEachChild (child => child.Game = value);
			}
		}

		public Entity Parent {
			get;
			internal set;
		}

		private HashSet<string> _tags = new HashSet<string>();


		#endregion

		#region Indexers

		public Entity this [String identifier] {
			get {
				if (Identifier == identifier)
					return this;
				else {
					foreach (var child in Children)
						if (child.Identifier == identifier)
							return child;
					throw new IndexOutOfRangeException (identifier);
				}
			}
		}

		#endregion

		#region Methods

		public abstract void LoadContent ();

		public abstract void UnloadContent ();

		public void AddTag(string tag) {
			TagService.Instance.Add (this, tag);
			_tags.Add (tag.ToLower().Trim());
		}

		public bool HasTag(string tag) {
			return _tags.Contains (tag.ToLower().Trim());
		}

		public void RemoveTag(string tag) {
			if (TagService.Instance.Contains (this)) {
				TagService.Instance.Remove (this, tag);
			}
		}

		public void RemoveAllTag() {
			TagService.Instance.Remove (this);
		}

		public void ForEachDescendant (Action<Entity> action)
		{
			foreach (var child in Children) {
				action (child);
				child.ForEachDescendant (action);
			}
		}

		public void ForEachChild (Action<Entity> action)
		{
			foreach (var child in Children) {
				action (child);
			}
		}

		public static void Traverse<T> (Entity root, Action<Entity> action) where T : TraversalAlgorithm<Entity>, new()
		{
			T algorithm = new T();
			algorithm.RootNode = root;
			foreach (var vertex in algorithm)
				action (vertex);
		}

		public override int GetHashCode ()
		{
			return Identifier.GetHashCode ();
		}

		public override bool Equals (object obj)
		{
			return Identifier.Equals (obj);
		}

		public void Update (GameTime gameTime)
		{
			if ((UpdateFlags & Entity.UPDATE_FLAG_TRANSFORMATION) == Entity.UPDATE_FLAG_TRANSFORMATION) {
				#region Update Entity local and world translation
				Transformation2D.WorldMatrix =
					Matrix.CreateTranslation (new Vector3 (-Transformation2D.PivotPoint, 0)) *
					Matrix.CreateScale (new Vector3 (Transformation2D.LocalScale, 1)) *
				Matrix.CreateRotationZ (
						MathHelper.ToRadians (Transformation2D.LocalRotation)) *
					Matrix.CreateTranslation (new Vector3 (Transformation2D.LocalPosition, 0));

				if (Parent != null) {
					Transformation2D.WorldMatrix = Matrix.Multiply (Transformation2D.WorldMatrix,
						Matrix.CreateTranslation (
							new Vector3 (Parent.Transformation2D.PivotPoint, 0))
					);
					Transformation2D.WorldMatrix = Matrix.Multiply (Transformation2D.WorldMatrix,
						Parent.Transformation2D.WorldMatrix);
				}

				Vector3 pos, scale;

				Quaternion rot;
				if (!Transformation2D.WorldMatrix.Decompose (out scale, out rot, out pos))
					throw new Exception (Transformation2D.WorldMatrix.ToString());

				Transformation2D.WorldPosition = Transformation2D.WorldPosition.Project2D (pos);
				Transformation2D.WorldScale = Transformation2D.WorldScale.Project2D (scale);

				var direction = Vector2.Transform (Vector2.UnitX, rot);
				Transformation2D.WorldRotation = (float)Math.Atan2 (direction.Y, direction.X);
				Transformation2D.WorldRotation = float.IsNaN (Transformation2D.WorldRotation) ? 0 :
					MathHelper.ToDegrees (Transformation2D.WorldRotation);

				#endregion
				UpdateFlags ^= Entity.UPDATE_FLAG_TRANSFORMATION;

			}
			ForEachChild (child => child.Update (gameTime));
		}

		public void DetachChild (Entity child, Game game)
		{
			Children.Remove (child);
			child.Parent = null;
			child.Game = game;
		}

		public void AttachChild (Entity child)
		{
			Children.Add (child);
			child.Parent = this;
			child.Game = game;
		}

		public Entity Move (Vector2 position)
		{
			if (Transformation2D.LocalPosition != position) {
				Transformation2D.LocalPosition = position;
				forceTransformationUpdate ();
			}
			return this;
		}

		public Entity Rotate (float degree)
		{
			if (Transformation2D.LocalRotation != degree) {
				Transformation2D.LocalRotation = degree;
				forceTransformationUpdate ();
			}
			return this;
		}

		public Entity Scale (Vector2 scale)
		{
			if (scale.X == 0 || scale.Y == 0)
				throw new ArgumentException ("Scaling to zero is not possible");
			if (Transformation2D.LocalScale != scale) {
				Transformation2D.LocalScale = scale;
				forceTransformationUpdate ();
			}
			return this;
		}

		public ICollection<Entity> GetChildren ()
		{
			return Children;
		}

		private void forceTransformationUpdate() {
			UpdateFlags |= Entity.UPDATE_FLAG_TRANSFORMATION;
			ForEachDescendant(node => node.UpdateFlags |= Entity.UPDATE_FLAG_TRANSFORMATION);
		}


		#endregion

		#region Operator

		public static Entity operator + (Entity parent, Entity child)
		{
			parent.AttachChild (child);
			return parent;
		}

		public static Entity operator - (Entity parent, Entity child)
		{
			parent.DetachChild (child, parent.Game);
			return parent;
		}

		#endregion

		#region Classes

		public class ReferenceResourceEventArgs : EventArgs
		{
			#region Constructors

			public ReferenceResourceEventArgs (string identifier, string content)
			{
				Identifier = identifier;
				Content = content;
				Timestamp = DateTime.Now;
			}

			#endregion

			#region Properties

			public string Identifier { get; private set; }

			public string Content { get; private set; }

			public DateTime Timestamp { get; private set; }

			#endregion
		}

		public class ReferenceEventArg : EventArgs
		{
			#region Constructors

			public ReferenceEventArg (string identifier)
			{
				Identifier = identifier;
				Timestamp = DateTime.Now;
			}

			#endregion

			#region Properties

			public string Identifier { get; private set; }

			public DateTime Timestamp { get; private set; }

			#endregion
		}

		public class NodeLoadContentStateChangedEventArgs : EventArgs
		{
			#region Constructors

			public NodeLoadContentStateChangedEventArgs (string identifier)
			{
				Identifier = identifier;
				Timestamp = DateTime.Now;
			}

			#endregion

			#region Properties

			public string Identifier { get; private set; }

			public DateTime Timestamp { get; private set; }

			#endregion
		}

		#endregion
	}
}

