using System;
using System.Linq;
using System.Collections.Generic;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using XMLValidator.Core.Models;
using XMLValidator.Ef.DbContext;
using XMLValidator.Ef.Service;

namespace XMLValidator.Black
{
    class Program
    {
        static void Main(string[] args)
        {
            ICustomerService _customerService;
            using (IDataContextAsync _dataContext = new ValidatorContext())
            {
                using (IUnitOfWorkAsync _unitOfWorkAsync = new UnitOfWork(_dataContext))
                {
                    IRepositoryAsync<Customer> _repo = new Repository<Customer>(_dataContext, _unitOfWorkAsync);
                    _customerService = new CustomerService(_repo);

                    //Add a Customer
                    var c = new Customer()
                    {
                        CustomerID = "XOOOO",
                        CompanyName = "CompanyName"
                    };

                    _repo.Insert(c);
                    _unitOfWorkAsync.SaveChanges();


                    var customers = _repo.Queryable().ToList();
                    customers.ForEach(o =>
                   {
                       Console.WriteLine("{0}, {1}", o.CustomerID, o.ContactName);
                   });

                    var totalCustomers = _customerService.GetTotalCustomers();
                    Console.WriteLine("Total Customers : {0}", totalCustomers);
                }
            }

            Console.Read();
        }
    }
}



