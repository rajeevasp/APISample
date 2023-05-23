using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;


namespace API.Data.Configuration
{
    /// <summary>
    /// Context class for accessing data using the underlying 
    /// entity framework.
    /// </summary>
    public class DomainNameDbContext : DbContext
    {
        public DomainNameDbContext()
        {
            Configuration.ProxyCreationEnabled = false;
        }

        //Database Objects
        
    
        //Use the fluent API to customise the EF context. Add classes in the mapping directory deriving from
        //entitytypeconfiguration<t> to customise how the domain objects are mapped to the db tables
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DomainNameDbContext>(null);
            var types = Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => t.BaseType != null
                    && t.BaseType.IsGenericType
                    && t.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var t in types)
            {
                dynamic config = Activator.CreateInstance(t);
                modelBuilder.Configurations.Add(config);
            }
            base.OnModelCreating(modelBuilder);
        }
    }

    public class DomainNameDbContextLogging : DbContext
    {
        public DomainNameDbContextLogging()
        {
            Configuration.ProxyCreationEnabled = false;
        }

        //Database Objects
        //public IDbSet<BatchRunLog> BatchRunLogs { get; set; }
        //public IDbSet<BatchRunEventLog> BatchRunEventsLog { get; set; }
        //public IDbSet<ProcessGroup> ProcessGroups { get; set; }
        //public IDbSet<ProcessRegistry> ProcessRegistry { get; set; }
        ////public IDbSet<EventType> EventTypes { get; set; }
        //public IDbSet<GroupProcessMapping> GroupProcessMap { get; set; }
        //public IDbSet<ProcessSubGropus> ProcessSubGropus { get; set; }
        //public IDbSet<SubGroupProcessMap> SubGroupProcessMap { get; set; }
        //public IDbSet<Status> Status { get; set; }

        //Use the fluent API to customise the EF context. Add classes in the mapping directory deriving from
        //entitytypeconfiguration<t> to customise how the domain objects are mapped to the db tables
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DomainNameDbContextLogging>(null);
            var types = Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => t.BaseType != null
                    && t.BaseType.IsGenericType
                    && t.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var t in types)
            {
                dynamic config = Activator.CreateInstance(t);
                modelBuilder.Configurations.Add(config);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
