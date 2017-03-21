
using CaseOrganizer.Web.DAL;
using CaseOrganizer.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseOrganizer.Web.DAL
{
    class ContactSqlDAL : IContactDAL

    {
        public string connectionString;
        public ContactSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private const string SQL_SearchContactsByLastName = "Select * from contact where contact.last_name = @lastname;";
        private const string SQL_ViewAllContacts = "Select * from contact";
        private const string SQL_CreateContact = "Insert into contact (first_name, last_name, phone, email, address, birth_date) values(@firstname, @lastname, @phone, @email, @address, @birthdate);Select cast(scope_identity() as int);";
        private const string SQL_UpdateEmail = "Update contact set email = @newemail where contact_id = @contactid; Select cast(scope_identity() as int);";
        private const string SQL_UpdateContactAddress = "Update contact set address = @newaddress where contact_id = @contactid;";
        private const string SQL_UpdateContactPhone = "Update contact set phone = @phone where contact_id = @contactid;";

        public List<Contact> ViewAllContacts()
        {
            List<Contact> contactList = new List<Contact>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_ViewAllContacts, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Contact contact = new Contact()
                        {
                            ContactId = Convert.ToInt32(reader["contact_id"]),
                            FirstName = Convert.ToString(reader["first_name"]),
                            LastName = Convert.ToString(reader["last_name"]),
                        };

                        if (!reader.IsDBNull(5))
                        {
                            contact.Address = Convert.ToString(reader["address"]);
                        }
                        else
                        {
                            contact.Address = "";
                        }
                        if (!reader.IsDBNull(6))
                        {
                            contact.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        }
                        //CaseId = Convert.ToInt32(reader["case_id"]),
                        if (!reader.IsDBNull(7))
                            contact.Email = Convert.ToString(reader["email"]);
                        contact.Phone = Convert.ToString(reader["phone"]);

                        contactList.Add(contact);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return contactList;
        }
        public List<Contact> SearchContactsByLastName(string lastName)
        {
            List<Contact> contactList = new List<Contact>();
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL_SearchContactsByLastName, conn);
                cmd.Parameters.AddWithValue("@lastname", lastName);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Contact c = new Contact()
                    {
                        ContactId = Convert.ToInt32(reader["contact_id"]),
                        FirstName = Convert.ToString(reader["first_name"]),
                        LastName = Convert.ToString(reader["last_name"]),
                    };
                    if (!reader.IsDBNull(5))
                    {
                        c.Address = Convert.ToString(reader["address"]);
                    }
                    if (!reader.IsDBNull(6))
                    {
                        c.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                    }
                    //CaseId = Convert.ToInt32(reader["case_id"]),
                    if (!reader.IsDBNull(7))
                    {
                        c.Email = Convert.ToString(reader["email"]);
                    }
                    //if(!reader.IsDBNull(8))
                    //{
                    c.Phone = Convert.ToString(reader["phone"]);
                    //}
                    contactList.Add(c);

                }


            }
            catch (Exception)
            {

                throw;
            }
            return contactList;
        }
        public bool CreateNewContact(Contact newContact)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_CreateContact, conn);
                    cmd.Parameters.AddWithValue("@firstname", newContact.FirstName);
                    cmd.Parameters.AddWithValue("@lastname", newContact.LastName);
                    if (newContact.Phone != null)
                    {
                        cmd.Parameters.AddWithValue("@phone", newContact.Phone);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@phone", "");
                    }
                    if (newContact.Email != null)
                    {
                        cmd.Parameters.AddWithValue("@email", newContact.Email);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@email", "");
                    }
                    if (newContact.Address != null)
                    {
                        cmd.Parameters.AddWithValue("@address", newContact.Address);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@address", "");
                    }
                    if(newContact.BirthDate != null)
                    { 
                    cmd.Parameters.AddWithValue("@birthdate", newContact.BirthDate);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@birthdate", "");
                    }
                    int linesChanged = (int)cmd.ExecuteScalar();
                    return (linesChanged > 0);

                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool UpdateContactEmail(int contactId, string newEmail)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_UpdateEmail, conn);
                    cmd.Parameters.AddWithValue("@contactid", contactId);
                    cmd.Parameters.AddWithValue("@newemail", newEmail);
                    int changedRows = cmd.ExecuteNonQuery();
                    bool worked = (changedRows > 0);
                    return worked;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool UpdateContactAddress(int contactId, string newAddress)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL_UpdateContactAddress, conn);
                cmd.Parameters.AddWithValue("@contactid", contactId);
                cmd.Parameters.AddWithValue("@newaddress", newAddress);
                int rowsChanged = cmd.ExecuteNonQuery();

                bool worked = (rowsChanged > 0);

                return worked;


            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool UpdateContactPhone(int contactId, string newPhone)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL_UpdateContactPhone, conn);
                cmd.Parameters.AddWithValue("@phone", newPhone);
                cmd.Parameters.AddWithValue("@contactid", contactId);
                int changedRows = cmd.ExecuteNonQuery();

                bool worked = (changedRows > 0);
                return worked;

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
