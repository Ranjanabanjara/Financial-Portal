using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_4.Models
{
    public class Invitation
    {
        
    
        public int Id { get; set; }
        public int HouseholdId { get; set; }
      
        public string ReceipentEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
      
        public int TTL { get; set; }
        public bool IsValid { get; set; }
        public Guid Code { get; set; }
        public virtual Household Household { get; set; }
      


    }
}
