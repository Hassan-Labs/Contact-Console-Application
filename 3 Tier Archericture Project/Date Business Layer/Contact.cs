using System;
using System.Data;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsContact
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CountryID { get; set; }
        public string ImagePath { get; set; }


        public clsContact()
        {
            this.Id = 0;
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = 0;
            this.ImagePath = "";

            Mode = enMode.AddNew;
        }

        private clsContact(int id, string firstName, string lastName, string email, string phone, string address, DateTime dateOfBirth, int countryID, string imagePath)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
            DateOfBirth = dateOfBirth;
            CountryID = countryID;
            ImagePath = imagePath;
            Mode = enMode.Update;
        }

        static public clsContact Find(int ID)
        {
            string FirstName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime Date = DateTime.Now;
            int CountryID = -1;

            if (clsContactDataAccess.GetContactInfo(ID, ref FirstName, ref LastName, ref Email, ref Phone, ref Address, ref Date, ref CountryID, ref ImagePath))
            {
                return new clsContact(ID, FirstName, LastName, Email, Phone, Address, Date, CountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewContact()
        {
            this.Id = clsContactDataAccess.AddNewContact(this.FirstName, this.LastName, this.Email, this.Phone, this.Address, this.DateOfBirth, this.CountryID, this.ImagePath); ;
            return (Id != -1);
        }

        private bool _UpdateContact()
        {
            return (clsContactDataAccess.UpdateContact(this.Id, this.FirstName, this.LastName, this.Email, this.Phone, this.Address, this.DateOfBirth, this.CountryID, this.ImagePath));
        }

        static public bool DeleteContact(int ID)
        {
            return (clsContactDataAccess.DeleteContact(ID));
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewContact())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                            return false;
                    }
                case enMode.Update:
                    {
                        return _UpdateContact();
                    }


            }
            return false;

        }

        static public DataTable ListContacts()
        {
            return clsContactDataAccess.ListContacts();
        }
        
        static public bool IsContactExist(int ID)
        {
            return (clsContactDataAccess.IsContactExist(ID));
        }
        
    }
}
