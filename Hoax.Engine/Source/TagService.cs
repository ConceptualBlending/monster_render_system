using System;
using System.Collections.Generic;
using Hoax.Engine.Graphics2D;

namespace Hoax.Engine.Services
{
	public sealed class TagService
	{
		#region Thread-Safe Singleton
		private static volatile TagService instance;
		private static object syncRoot = new Object();

		private TagService() {}

		public static TagService Instance
		{
			get 
			{
				if (instance == null) 
				{
					lock (syncRoot) 
					{
						if (instance == null) 
							instance = new TagService();
					}
				}

				return instance;
			}
		}
		#endregion

		private Dictionary<string, HashSet<Entity>> tagMap = new Dictionary<string, HashSet<Entity>>();

		public void Add(Entity entity, string tag) {
			string key = tag.ToLower ().Trim ();

			if (!tagMap.ContainsKey (key))
				tagMap.Add (key, new HashSet<Entity> ());
			tagMap [key].Add (entity);
		}

		public void Remove(Entity entity) {
			foreach (var key in tagMap.Keys)
				if (tagMap [key].Contains (entity)) {
					tagMap [key].Remove (entity);
					if (tagMap [key].Count == 0)
						Remove (key);
				}
		}

		public void Remove(Entity entity, string tag) {
			string key = tag.ToLower ().Trim ();

			if (tagMap [key].Contains (entity))
				tagMap [key].Remove (entity);
			if (tagMap [key].Count == 0)
				Remove (tag);
		}

		public void Remove(string tag) {
			string key = tag.ToLower ().Trim ();

			if (tagMap.ContainsKey (key))
				tagMap.Remove (key);
		}

		public bool Contains(Entity entity) {
			foreach (var key in tagMap.Keys)
				if (tagMap [key].Contains (entity))
					return true;
			return false;
		}

		public bool Contains(string key) {
			return tagMap.ContainsKey (key);
		}

		public ICollection<Entity> Get(string tag) {
			string key = tag.ToLower ().Trim ();
			if (!Contains (key))
				throw new IndexOutOfRangeException ("\"" + key + "\" is not a tag.");
			else
				return tagMap [key.ToLower ().Trim ()];
		}

		public ICollection<string> Get(Entity entity) {
			if (!Contains (entity))
				throw new IndexOutOfRangeException ("\"" + entity + "\" is not assigned to any tag.");
			else {
				HashSet<string> result = new HashSet<string> ();
				foreach (var key in tagMap.Keys)
					if (tagMap [key].Contains (entity))
						result.Add(key);
				return result;
			}
		}

		public int Count() {
			return tagMap.Count;
		}

	}
}

