using System;
using System.Collections.Generic;

namespace Blog
{
	public class Post
	{
		private int id;
		private Blog blog;
		private User user;
		private string title;
		private string text;
		private DateTime postedAt;

		private ICollection<Tag> tags = new HashSet<Tag>();
		private ICollection<Comment> comments = new HashSet<Comment>();
		private ICollection<Category> categories = new HashSet<Category>();

		public virtual int Id
		{
			get { return id; }
			set { id = value; }
		}

		public virtual Blog Blog
		{
			get { return blog; }
			set { blog = value; }
		}

		public virtual User User
		{
			get { return user; }
			set { user = value; }
		}

		public virtual string Title
		{
			get { return title; }
			set { title = value; }
		}

		public virtual string Text
		{
			get { return text; }
			set { text = value; }
		}

		public virtual DateTime PostedAt
		{
			get { return postedAt; }
			set { postedAt = value; }
		}

		public virtual ICollection<Comment> Comments
		{
			get { return comments; }
			set { comments = value; }
		}

		public virtual ICollection<Category> Categories
		{
			get { return categories; }
			set { categories = value; }
		}

		public virtual ICollection<Tag> Tags
		{
			get { return tags; }
			set { tags = value; }
		}


	}
}