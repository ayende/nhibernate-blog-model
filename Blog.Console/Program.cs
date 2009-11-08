using System;
using NHibernate;
using NHibernate.Cfg;

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
               
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }
    }
}