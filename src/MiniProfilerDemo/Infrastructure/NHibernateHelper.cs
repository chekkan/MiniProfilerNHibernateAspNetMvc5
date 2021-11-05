using System;
using System.Data.Common;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MiniProfilerDemo.Models;
using NHibernate;
using NHibernate.Driver;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;

namespace MiniProfilerDemo.Infrastructure
{
    public static class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory != null) return _sessionFactory;
                _sessionFactory = Fluently.Configure()
                    .Database(MsSqlConfiguration
                        .MsSql2012.ConnectionString(c =>
                            c.FromConnectionStringWithKey("Default"))
                        .Driver<MiniProfiledSqlClientDriver>())
                    .Mappings(m => m.AutoMappings
                        .Add(AutoMap.AssemblyOf<Employee>(new StoreConfiguration())))
                    .BuildSessionFactory();
                return _sessionFactory;
            }
        }

        public static ISession OpenSession() => SessionFactory.OpenSession();
    }

    public class StoreConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == typeof(Employee).Namespace;
        }
    }

    public class MiniProfiledSqlClientDriver : SqlClientDriver
    {
        public override DbCommand CreateCommand()
        {
            var dbCommand = base.CreateCommand();
            if (MiniProfiler.Current != null)
            {
                dbCommand = new ProfiledDbCommand(dbCommand, null, MiniProfiler.Current);
            }

            return dbCommand;
        }
    }
}