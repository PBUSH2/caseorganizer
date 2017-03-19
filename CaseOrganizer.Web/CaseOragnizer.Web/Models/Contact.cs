using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseOrganizer.Web.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CaseId { get; set; }
    }
}