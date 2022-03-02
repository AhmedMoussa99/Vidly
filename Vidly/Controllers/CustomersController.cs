using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;
using System.Data.Entity.Infrastructure.MappingViews;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private VidlyDbContext _context;
        public CustomersController()
        {
            _context = new VidlyDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult CustomerForm() 
        {
            var MembershipTypes = _context.membershipTypes.ToList();
            var ViewModel = new NewCustomerViewModel
            {
                MembershipTypes = MembershipTypes
            };
            return View(ViewModel);
        }
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewmodel = new NewCustomerViewModel
                {
                    customer = customer,
                    MembershipTypes = _context.membershipTypes
                };
                return View ("CustomerForm", viewmodel);
            }
            if(customer.Id==0)
            _context.customers.Add(customer);
            else
            {
                var customerInDb = _context.customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        
        }
        public ActionResult Edit(int Id) 
        {
            var customer = _context.customers.SingleOrDefault(c => c.Id == Id);
            if (customer == null)
                return HttpNotFound();
            var viewModel = new NewCustomerViewModel
            {
                customer = customer,
                MembershipTypes = _context.membershipTypes.ToList()
            };
            return View("CustomerForm" , viewModel);
        }
        // GET: Customers
       
        public ActionResult Index()
        {
            var customers = _context.customers.Include(c => c.MembershipType).ToList();
            return View(customers);

        }
        public ActionResult Details(int Id)
        {
            
            var customer = _context.customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == Id);
            if (customer == null)
                return HttpNotFound();
            return View(customer);
        }
        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
                {
                    new Customer{Id=1 , Name="John Smith"},
                     new Customer{Id = 2,Name="Mary Williams"}
                };

        }
    }
}