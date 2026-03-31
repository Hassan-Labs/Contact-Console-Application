using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Web;


namespace ContactsDataAccessLayer
{
    public class clsContactDataAccess
    {
        static public bool GetContactInfo(int ID, ref string FirstName, ref string LastName
            , ref string Email, ref string Phone, ref string Address, ref DateTime Date, ref int CountryID, ref string ImagePath)

        {

            bool isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectoinString);
            string quere = "select * from Contacts where ContactID = @ContactID";
            SqlCommand Command = new SqlCommand(quere, connection);
            Command.Parameters.AddWithValue("@ContactID", ID);
            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    isfound = true;
                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    Date = (DateTime)reader["DateOfBirth"];
                    CountryID = (int)reader["CountryID"];

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }

                }
                else
                {
                    isfound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                isfound = false;
            }

            finally
            {
                connection.Close();
            }

            return isfound;
        }



        static public int AddNewContact(string FirstName, string LastName, string Email, string Phone, string Address, DateTime Date
                       , int CountryID, string ImagePath)
        {
            int ContactID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectoinString);
            string quere = @"INSERT INTO Contacts
           (FirstName ,LastName,Email,Phone
           ,Address,DateOfBirth,CountryID,ImagePath)
        VALUES(@FirstName,@LastName,@Email,@Phone,@Address,@DateOfBirth,@CountryID,@ImagePath);
                    select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(quere, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", Date);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            if (ImagePath != "")
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }



            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    ContactID = InsertedID;
                }

            }


            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            finally
            {
                connection.Close();
            }

            return ContactID;
        }



        static public bool UpdateContact(int ContactID, string FirstName, string LastName, string Email, string Phone, string Address, DateTime Date
                       , int CountryID, string ImagePath)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectoinString);
            string quere = @"
            UPDATE Contacts
            SET FirstName = @FirstName
            ,LastName = @LastName
            ,Email = @Email
            ,Phone = @Phone
            ,Address = @Address
            ,DateOfBirth = @DateOfBirth
            ,CountryID = @CountryID
            ,ImagePath = @ImagePath
            WHERE ContactID = @ContactID";

            SqlCommand command = new SqlCommand(quere, connection);
            command.Parameters.AddWithValue("@ContactID", ContactID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", Date);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            if (ImagePath != "")
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
            }

            finally
            {
                connection.Close();
            }

            return (RowsAffected > 0);
        }


        static public bool DeleteContact(int ID)
        {
            int RowsAffected = 0;
            string quere = @"Delete From Contacts
                            Where ContactID = @ContactID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectoinString);

            SqlCommand command = new SqlCommand(quere, connection);
            command.Parameters.AddWithValue("@ContactID", ID);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
            }

            finally
            {
                connection.Close();
            }
            return (RowsAffected > 0);
        }


        static public DataTable ListContacts()
        {
            DataTable DataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectoinString);
            string quere = "Select * From Contacts";
            SqlCommand command = new SqlCommand(quere, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    DataTable.Load(reader);
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
            }
            
            finally
            {
                connection.Close();
            }

            return DataTable;

        }

        static public bool IsContactExist(int ID)
        {
            bool isfound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectoinString);
            string quere = "select found = 1 from Contacts where ContactID = @ContactID";
            SqlCommand command = new SqlCommand(quere, connection);
            command.Parameters.AddWithValue("@ContactID", ID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isfound = reader.HasRows;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return isfound;
        }

    }



}

