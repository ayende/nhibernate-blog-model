using System;
using System.Collections.Generic;

namespace Blog
{
	public class Blog
	{
		private ICollection<Post> posts = new HashSet<Post>();
		private ICollection<User> users = new HashSet<User>();
		private ICollection<Tag> tags = new HashSet<Tag>();
		
		private int id;
		private string title;
		private string subtitle;
		private bool allowsComments;
		private DateTime createdAt;

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

		public virtual ICollection<User> Users
		{
			get { return users; }
			set { users = value; }
		}

		public virtual ICollection<Tag> Tags
		{
			get { return tags; }
			set { tags = value; }
		}

		public virtual string Title
		{
			get { return title; }
			set { title = value; }
		}

		public virtual string Subtitle
		{
			get { return subtitle; }
			set { subtitle = value; }
		}

		public virtual bool AllowsComments
		{
			get { return allowsComments; }
			set { allowsComments = value; }
		}

		public virtual DateTime CreatedAt
		{
			get { return createdAt; }
			set { createdAt = value; }
		}
	}
}