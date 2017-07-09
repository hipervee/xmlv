using Repository.Pattern.Repositories;
using Service.Pattern;
using XMLValidator.Core.Models;
using XMLValidator.Ef.Repository;

namespace XMLValidator.Ef.Service
{
    public interface ICustomerService : IService<Customer>
    {
        int GetTotalCustomers();
    }

    /// <summary>
    ///     All methods that are exposed from Repository in Service are overridable to add business logic,
    ///     business logic should be in the Service layer and not in repository for separation of concerns.
    /// </summary>
    public class CustomerService : Service<Customer>, ICustomerService
    {
        private readonly IRepositoryAsync<Customer> _repository;

        public CustomerService(IRepositoryAsync<Customer> repository) : base(repository)
        {
            _repository = repository;
        }

        public int GetTotalCustomers()
        {
            return _repository.GetTotalCustomers();
        }
    }
}


