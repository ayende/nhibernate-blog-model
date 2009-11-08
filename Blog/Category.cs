using System.Collections.Generic;

namespace Blog
{
	public class Category
	{
		private int id;
		private ICollection<Post> posts = new HashSet<Post>();
		private string name;

		public virtual string Name
		{
			get { return name; }
			set { name = value; }
		}

		public virtual int Id
		{
			get { return id; }
			set { id = value; }
		}

		public virtual ICollection<Post> Posts
		{
			get { return posts; }
			set { posts = value; }
		}
	}
}