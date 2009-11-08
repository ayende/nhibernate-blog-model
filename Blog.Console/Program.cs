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
                
               

               
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }
    }
}