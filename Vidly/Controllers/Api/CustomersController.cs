using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private VidlyDbContext _context;
        public CustomersController()
        {
            _context = new VidlyDbContext();
        }
        //Get /api/customers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.customers.ToList().Select(Mapper.Map<Customer , CustomerDto>);
        }
        // Get /api/customer/1
        public CustomerDto GetCustomer(int id)
        {
            var customer = _context.customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return Mapper.Map<Customer , CustomerDto> (customer);
        }
        // Post /api/customers
        [System.Web.Http.HttpPost]
        public CustomerDto CreateCustomer(CustomerDto customerDTO)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var customer = Mapper.Map<CustomerDto, Customer>(customerDTO);
            _context.customers.Add(customer);
            _context.SaveChanges();
            customerDTO.Id = customer.Id;
            return customerDTO;

        }
        // put /api/customer/1
        [System.Web.Http.HttpPut]
        public void UpdateCustomer(int id , CustomerDto CustomerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var CustomerInDb = _context.customers.SingleOrDefault(c => c.Id == id);
            if(CustomerInDb==null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            Mapper.Map(CustomerDto, CustomerInDb);
          
            _context.SaveChanges();

        }
        //Delete /api/customer/1
        [System.Web.Http.HttpDelete]
        public void DeleteCustomer(int id)
        {
            var CustomerInDb = _context.customers.SingleOrDefault(c => c.Id == id);
            if (CustomerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.customers.Remove(CustomerInDb);
            _context.SaveChanges();

        }

    }
}
