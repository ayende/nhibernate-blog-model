using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Hql;
using NHibernate.Hql.Ast.ANTLR;

namespace Blog.Console
{
    public class EagerlyLoadingQueryTranslatorFactory : IQueryTranslatorFactory
    {
        static readonly Regex fromClause = new Regex(@"from \s+ ([\w_\.\d]+) (\s+ (as \s+)? ([\w_\d]+))?",
            RegexOptions.Compiled|RegexOptions.IgnoreCase|RegexOptions.IgnorePatternWhitespace);
       
        readonly ASTQueryTranslatorFactory inner = new ASTQueryTranslatorFactory();

        private static readonly IDictionary<string, string[]> fetchPaths = new Dictionary<string, string[]>();

        public static void RegisterFetchPaths<TEntity>(params string[] fetchPathsToLoad)
        {
            fetchPaths[typeof(TEntity).FullName] = fetchPathsToLoad;
        }

        public IQueryTranslator CreateQueryTranslator(string queryIdentifier, string queryString, IDictionary<string, IFilter> filters, ISessionFactoryImplementor factory)
        {
            queryString = PreParseQuery(queryString);
            return inner.CreateQueryTranslator(queryIdentifier, queryString, filters, factory);
        }

        public IFilterTranslator CreateFilterTranslator(string queryIdentifier, string queryString, IDictionary<string, IFilter> filters, ISessionFactoryImplementor factory)
        {
            queryString = PreParseQuery(queryString);
            return inner.CreateFilterTranslator(queryIdentifier, queryString, filters, factory);
        }

        private static string PreParseQuery(string queryString)
        {
            var match = fromClause.Match(queryString);
            if (match.Success == false)
                return queryString;
            var entityName = match.Groups[1].Value;
            string[] fetchPathsToLoad;
            if (fetchPaths.TryGetValue(entityName, out fetchPathsToLoad) == false)
                return queryString;

            string alias = string.IsNullOrEmpty(match.Groups[4].Value) ? entityName.Replace(".", "_") + "_alias" : match.Groups[4].Value;
            var sb = new StringBuilder()
                .Append("from ").Append(entityName).Append(" as ").Append(alias).Append(" ");
            foreach (var fethPath in fetchPathsToLoad)
            {
                sb.Append("left join fetch ").Append(alias).Append(".").Append(fethPath).Append(" ");
            }
            return queryString.Replace(match.Value, sb.ToString());
        }
    }
}