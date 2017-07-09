using System.Data.Entity;
using Repository.Pattern.Ef6;
using XMLValidator.Core.Models;
using XMLValidator.Core.Models.Maps;

namespace XMLValidator.Ef.DbContext
{
    public partial class ValidatorContext : DataContext
    {
        static ValidatorContext()
        {
            Database.SetInitializer<ValidatorContext>(null);
        }

        public ValidatorContext()
            : base("Name=ValidatorContext")
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerMap());
        }
    }
}