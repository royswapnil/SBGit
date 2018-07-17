using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SterlingBankLMS.Data.Database
{
    public class WebAppDbContext<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim> : BaseContext
          where TUser : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>
          where TRole : IdentityRole<TKey, TUserRole>
          where TUserLogin : IdentityUserLogin<TKey>
          where TUserRole : IdentityUserRole<TKey>
          where TUserClaim : IdentityUserClaim<TKey>
    {
        public WebAppDbContext() : base()
        {
        }

        public WebAppDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null) {
                throw new ArgumentNullException("modelBuilder");
            }
            EntityTypeConfiguration<TUser> configuration = modelBuilder.Entity<TUser>().ToTable("AspNetUsers");
            configuration.HasMany<TUserRole>(u => u.Roles).WithRequired().HasForeignKey<TKey>(ur => ur.UserId);
            configuration.HasMany<TUserClaim>(u => u.Claims).WithRequired().HasForeignKey<TKey>(uc => uc.UserId);
            configuration.HasMany<TUserLogin>(u => u.Logins).WithRequired().HasForeignKey<TKey>(ul => ul.UserId);
            IndexAttribute indexAttribute = new IndexAttribute("UserNameIndex")
            {
                IsUnique = true
            };
            configuration.Property((Expression<Func<TUser, string>>) (u => u.UserName)).IsRequired().HasMaxLength(0x100).HasColumnAnnotation("Index", new IndexAnnotation(indexAttribute));
            configuration.Property((Expression<Func<TUser, string>>) (u => u.Email)).HasMaxLength(0x100);
            modelBuilder.Entity<TUserRole>().HasKey(r => new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("AspNetUserRoles");
            modelBuilder.Entity<TUserLogin>().HasKey(l => new { LoginProvider = l.LoginProvider, ProviderKey = l.ProviderKey, UserId = l.UserId }).ToTable("AspNetUserLogins");
            modelBuilder.Entity<TUserClaim>().ToTable("AspNetUserClaims");
            EntityTypeConfiguration<TRole> configuration2 = modelBuilder.Entity<TRole>().ToTable("AspNetRoles");
            IndexAttribute attribute2 = new IndexAttribute("RoleNameIndex")
            {
                IsUnique = true
            };
            configuration2.Property((Expression<Func<TRole, string>>) (r => r.Name)).IsRequired().HasMaxLength(0x100).HasColumnAnnotation("Index", new IndexAnnotation(attribute2));
            configuration2.HasMany<TUserRole>(r => r.Users).WithRequired().HasForeignKey<TKey>(ur => ur.RoleId);
            base.OnModelCreating(modelBuilder);
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            if ((entityEntry != null) && (entityEntry.State == EntityState.Added)) {
                List<DbValidationError> source = new List<DbValidationError>();
                TUser user = entityEntry.Entity as TUser;
                if (user != null) {
                    if (this.Users.Any<TUser>(u => string.Equals(u.UserName, user.UserName))) {
                        source.Add(new DbValidationError("User", string.Format(CultureInfo.CurrentCulture, IdentityResources.DuplicateUserName, new object[] { user.UserName })));
                    }
                    if (this.RequireUniqueEmail && this.Users.Any<TUser>(u => string.Equals(u.Email, user.Email))) {
                        source.Add(new DbValidationError("User", string.Format(CultureInfo.CurrentCulture, IdentityResources.DuplicateEmail, new object[] { user.Email })));
                    }
                }
                else {
                    TRole role = entityEntry.Entity as TRole;
                    if ((role != null) && this.Roles.Any<TRole>(r => string.Equals(r.Name, role.Name))) {
                        source.Add(new DbValidationError("Role", string.Format(CultureInfo.CurrentCulture, IdentityResources.RoleAlreadyExists, new object[] { role.Name })));
                    }
                }
                if (source.Any<DbValidationError>()) {
                    return new DbEntityValidationResult(entityEntry, source);
                }
            }
            return base.ValidateEntity(entityEntry, items);
        }

        public bool RequireUniqueEmail { get; set; }

        public virtual IDbSet<TRole> Roles { get; set; }

        public virtual IDbSet<TUser> Users { get; set; }


    }
}