using CaseOrganizer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseOrganizer.Web.DAL
{
    public interface IContactDAL
    {
        List<Contact> ViewAllContacts();

        List<Contact> SearchContactsByLastName(string lastName);

        bool CreateNewContact(Contact newContact);

        bool UpdateContactEmail(int contactId, string newEmail);

        bool UpdateContactAddress(int contactId, string newAddress);

        bool UpdateContactPhone(int contactId, string newPhone);
    }
}