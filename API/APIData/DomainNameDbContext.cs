using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using API.Domain.Blog;


namespace API.Data
{
    /// <summary>
    /// Context class for accessing data using the underlying 
    /// entity framework.
    /// </summary>
    public class DomainNameDbContext : DbContext, IDbContext
    {
        public DomainNameDbContext()
            : base("CMS_APIs")
        {
            
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        //Database Objects
        
        public IDbSet<Blog> Blogs { get; set; }
        

       

        

       

       

        

        /// <summary>
        /// Set the entity set
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>DbSet of type T</returns>
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

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
}
