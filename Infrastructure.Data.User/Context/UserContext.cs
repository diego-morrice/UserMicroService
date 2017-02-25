using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Domain.User.Interfaces.Common;
using Infrastructure.CrossCutting.Validation;
using Infrastructure.Data.User.Config;
using Infrastructure.Data.User.Interfaces;
using Infrastructure.Data.User.Mapping;

namespace Infrastructure.Data.User.Context
{
    public class UserContext : BaseDbContext, IUserContext
    {
        public UserContext()
            : this("UserContext")
        {
            //Configuration.LazyLoadingEnabled = true;
        }

        protected UserContext(string connectionStringName)
            : base(connectionStringName)
        {
            Users = Set<Domain.User.Entities.User>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Properties<DateTime>()
                .Configure(p => p.HasColumnType("datetime2"));

            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new AddressMap());
            modelBuilder.Configurations.Add(new PersonalDataMap());
        }

        public DbSet<Domain.User.Entities.User> Users { get; }

        public override int SaveChanges()
        {
            // Get added entries
            IEnumerable<ObjectStateEntry> addedEntryCollection = GetObjectContext()
                .ObjectStateManager
                .GetObjectStateEntries(EntityState.Added)
                .Where(m => m != null && m.Entity != null);

            // Get modified entries
            IEnumerable<ObjectStateEntry> modifiedEntryCollection = GetObjectContext()
                .ObjectStateManager
                .GetObjectStateEntries(EntityState.Modified)
                .Where(m => m != null && m.Entity != null);

            // Set audit fields of added entries
            foreach (ObjectStateEntry entry in addedEntryCollection)
            {
                var addedEntity = entry.Entity as IAuditableEntity;
                if (addedEntity != null)
                {
                    addedEntity.CreatedDate = DateTime.Now;
                    addedEntity.LastUpdatedDate = DateTime.Now;
                }

            }

            // Set audit fields of modified entries
            foreach (ObjectStateEntry entry in modifiedEntryCollection)
            {
                var modifiedEntity = entry.Entity as IAuditableEntity;
                if (modifiedEntity != null)
                {
                    modifiedEntity.LastUpdatedDate = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }
}