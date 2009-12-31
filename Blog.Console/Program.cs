using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Environment = NHibernate.Cfg.Environment;

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
                    .SetProperty(Environment.QueryTranslator, typeof(EagerlyLoadingQueryTranslatorFactory).AssemblyQualifiedName)
                    .BuildSessionFactory();
                //new SchemaExport(configuration).Create(true, true);

                EagerlyLoadingQueryTranslatorFactory.RegisterFetchPaths<Post>("Blog", "User");

                using(var session = sessionFactory.OpenSession())
                using(var tx = session.BeginTransaction())
                {
                    var c = session.CreateQuery("from Post p where p.Title = 'a'")
                        .List();

                    tx.Commit();
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
            finally
            {
            }
        }
    }
}