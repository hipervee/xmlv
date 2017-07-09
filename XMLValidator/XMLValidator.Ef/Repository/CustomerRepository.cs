using System.Linq;
using Repository.Pattern.Repositories;
using XMLValidator.Core.Models;

namespace XMLValidator.Ef.Repository
{
    // Exmaple: How to add custom methods to a repository.
    public static class CustomerRepository
    {
        public static int GetTotalCustomers(this IRepository<Customer> repository)
        {
            return repository
                .Queryable().Count();         
        }
    }
}
