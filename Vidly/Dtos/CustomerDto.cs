using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
       
        
        public byte MembershipTypeId { get; set; }
        
        
        public DateTime? BirthDate { get; set; }
    }
}