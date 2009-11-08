using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using HibernatingRhinos.NHibernate.Profiler.Appender;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Dialect.Function;
using NHibernate.Tool.hbm2ddl;

namespace Blog.Console
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                Configuration configuration = new Configuration()
                    .Configure("hibernate.cfg.xml");
                ISessionFactory sessionFactory = configuration
                    .BuildSessionFactory();
                //new SchemaExport(configuration).Create(true, true);
                
                Post post;

                using (var session = sessionFactory.OpenSession())
                using (var tx = session.BeginTransaction())
                {
                    var user = new User
                    {
                        CreatedAt = DateTime.Now,
                        Username = "ayende"
                    };
                    session.Save(user);
                    post = new Post
                    {
                        PostedAt = DateTime.Now,
                        User = user
                    };
                    session.Save(post);
                    tx.Commit();
                }

                var postId = post.Id;

               
                using (var session = sessionFactory.OpenSession())
                using (var tx = session.BeginTransaction())
                {
                    // get the entity
                    post = session.Get<Post>(postId); 
                    // force the association to be eagerly loaded
                    System.Console.WriteLine(post.User.Username); 
                    tx.Commit();
                }

                using (var session = sessionFactory.OpenSession())
                using (var tx = session.BeginTransaction())
                {
                    // get the user for the entity
                    var anotherUser = session.Get<User>(post.User.Id);

                    // will return false
                    ReferenceEquals(anotherUser, post.User);

                    // opps, user instance with the same id but with different
                    // reference was detected
                    session.SaveOrUpdate(post);

                    tx.Commit();
                }

               
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }
    }
}