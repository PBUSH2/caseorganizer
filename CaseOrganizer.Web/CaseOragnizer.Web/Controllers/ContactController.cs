using CaseOrganizer.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaseOrganizer.Web.Models;

namespace CaseOrganizer.Web.Controllers
{
    public class ContactController : Controller
    {
        IContactDAL contactDAL;
        public ContactController(IContactDAL contactDAL)
        {
            this.contactDAL = contactDAL;
        }
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewAllContacts()
        {

            
            return View("ViewAllContacts", contactDAL.ViewAllContacts());
        }
        public ActionResult SearchContactByLastName()
        {
            return View("SearchContactByLastName");
        }
        [HttpPost]
        public ActionResult SearchContactByLastName(string lastName)
        {
           
        
            return RedirectToAction("ViewContactByLastName", new { lastName = lastName });
        }
        public ActionResult ViewContactByLastName(string lastName)
        {
            List<Contact> contactList = contactDAL.SearchContactsByLastName(lastName);
            return View("ViewContactByLastName", contactList);
        }

   
    }
}