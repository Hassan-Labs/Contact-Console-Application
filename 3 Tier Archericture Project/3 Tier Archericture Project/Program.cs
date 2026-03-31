using ContactsBusinessLayer;
using System;
using System.Data;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;


namespace Presentation_Layer
{
    internal class Program
    {
        static public void TestFindContact(int ID)
        {
            clsContact Contact1 = clsContact.Find(ID);
            if(Contact1 != null)
            {
                Console.WriteLine(Contact1.Id);
                Console.WriteLine(Contact1.FirstName);
                Console.WriteLine(Contact1.LastName);
                Console.WriteLine(Contact1.Email);
                Console.WriteLine(Contact1.Phone);
                Console.WriteLine(Contact1.Address);
                Console.WriteLine(Contact1.DateOfBirth);
                Console.WriteLine(Contact1.CountryID);
                Console.WriteLine(Contact1.ImagePath);
            }
            else
            {
                Console.WriteLine("Could not find The Contact With ID [" + ID + "]");
            }
        }
        static public void TestAddNewContact()
        {
            clsContact Contact1 = new clsContact();
            Contact1.FirstName = "Salma";
            Contact1.LastName = "Hasan";
            Contact1.Email = "Salma@st.com";
            Contact1.Phone = "098744";
            Contact1.Address = "42 st building 4";
            Contact1.DateOfBirth = new DateTime(2004, 4, 4, 12, 52, 15);
            Contact1.CountryID = 1;
            Contact1.ImagePath = "";

            if(Contact1.Save())
            {
                Console.WriteLine("The Contact With ID [" + Contact1.Id + "] Has Been Added Successfuly!");
            }
            else
            {
                Console.WriteLine("The Contact With ID [" + Contact1.Id + "] Has NOT Been Added");
            }
        }

        static public void TestUpdateContact(int ID)
        {
            clsContact Contact1 = clsContact.Find(ID);
            if (Contact1 != null)
            {
                Contact1.FirstName = "Sami";
                Contact1.LastName = "Hakem";
                Contact1.Email = "Sami@st.com";
                Contact1.Phone = "0987243244";
                Contact1.Address = "44 st building 4";
                Contact1.DateOfBirth = new DateTime(2004, 4, 4, 12, 52, 15);
                Contact1.CountryID = 1;
                Contact1.ImagePath = "";
            }
            else
            {
                Console.WriteLine("Contact Not Found!");
            }

            if (Contact1.Save())
            {
                Console.WriteLine("The Contact[" + Contact1.Id + "] Has Been Updated Successfuly");
            }
            else
            {
                Console.WriteLine("The Contact [" + Contact1.Id + "] Has NOT Been Updated!");
            }     
        }
    
        static public void TestDeleteContact(int ID)
        {
            if(clsContact.DeleteContact(ID))
            {
                Console.WriteLine("The Contact [" + ID + "] Has Been Deleted Successfuly");
            }
            else
            {
                Console.WriteLine("The Contact [" + ID + "] Has NOT Been Deleted!");
            }
        }
      
        static public void TestListContacts()
        {
            DataTable DataTable = clsContact.ListContacts();
            Console.WriteLine("List Contact: ");
            foreach (DataRow row in DataTable.Rows)
            {
                Console.WriteLine($"{row ["FirstName"]}, {row["LastName"]} { row["CountryID"]}");
            }
        }
         
        static public void TestIsContactExist(int ID)
        {
            if(clsContact.IsContactExist(ID))
            {
                Console.WriteLine("Contact [" + ID + "] Is Exist");
            }
            else
                Console.WriteLine("Contact [" + ID + "] Is NOT Exist");
        }
        static void Main(string[] args)
        {
            // TestFindContact(2);
            //TestAddNewContact();
            //  TestUpdateContact(2);
            // TestDeleteContact(18);
            //TestListContacts();
            TestIsContactExist(1);
            Console.ReadKey();

         
        }
    }
}
