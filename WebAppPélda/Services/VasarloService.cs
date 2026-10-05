using MySql.Data.MySqlClient;
using WebAppPélda.Models;

namespace WebAppPélda.Services
{
    public class VasarloService
    {
        #region Read
        public List<Customer> GetAllCustomer()
        {
            List<Customer> Customers = new List<Customer>();
            string connectionString = "SERVER = localhost;" +
                          "DATABASE= webapppeldadb;" +
                          "UID = root;" +
                          "PASSWORD =;";
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            string sql = "SELECT * FROM vasarlo";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //A beolvasott adatok feldolgozása
                Customer customer = new Customer();
                customer.Id = reader.GetInt32("Id");
                customer.Nev = reader.GetString("Nev");
                customer.Cim = reader.GetString("Cim");
                customer.Email = reader.GetString("Email");
                customer.Telefon = reader.GetString("Telefon");
                customer.Pontszam = reader.GetInt32("Pontszam");
                Customers.Add(customer);
            }
            conn.Close();
            return Customers;
        }

        public Customer GetById(int id)
        {
            Customer result = new Customer();
            try
            {
                string connectionString = "SERVER = localhost;" +
                              "DATABASE= webapppeldadb;" +
                              "UID = root;" +
                              "PASSWORD =;";
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                string sql = "SELECT * FROM vasarlo WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    result.Id = reader.GetInt32("Id");
                    result.Nev = reader.GetString("Nev");
                    result.Cim = reader.GetString("Cim");
                    result.Email = reader.GetString("Email");
                    result.Telefon = reader.GetString("Telefon");
                    result.Pontszam = reader.GetInt32("Pontszam");
                }
                else 
                {
                    Console.WriteLine("Nincs ilyen vásárló!");
                }
                conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Hiba történt a vásárló lekérdezése során: " + ex.Message);
            }
        }
        #endregion
        public string POSTCustomer(Customer customer)
        {
            try
            {
                string connectionString = "SERVER = localhost;" +
                          "DATABASE= webapppeldadb;" +
                          "UID = root;" +
                          "PASSWORD =;";
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                string sql = "INSERT INTO vasarlo(Nev, Cim, Email, Telefon, Pontszam) VALUES (@nev, @cim, @email, @telefon, @pontszam)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nev", customer.Nev);
                cmd.Parameters.AddWithValue("@cim", customer.Cim);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.Parameters.AddWithValue("@telefon", customer.Telefon);
                cmd.Parameters.AddWithValue("@pontszam", customer.Pontszam);
                int sorokszama = cmd.ExecuteNonQuery();
                conn.Close();
                if (sorokszama > 0)
                {
                    return "Sikeres beszúrás!";
                }
                else
                {
                    return "Sikertelen beszúrás!";
                }
            }
            catch (Exception ex)
            {
                return "Hiba történt: " + ex.Message;

            }
        }

        public string PUTCUstomer(Customer Customer)
        {
            try
            {
                string connectionString = "SERVER = localhost;" +
                          "DATABASE= webapppeldadb;" +
                          "UID = root;" +
                          "PASSWORD =;";
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();



                string sql = "UPDATE vasarlo SET (Nev = @nev, Cim = @cim, Email = @email, Telefon = @telefon, Pontszam = @ontszam) WHERE Id = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nev", Customer.Nev);
                cmd.Parameters.AddWithValue("@cim", Customer.Cim);
                cmd.Parameters.AddWithValue("@email", Customer.Email);
                cmd.Parameters.AddWithValue("@telefon", Customer.Telefon);
                cmd.Parameters.AddWithValue("@pontszam", Customer.Pontszam);
                int sorokszama = cmd.ExecuteNonQuery();
                conn.Close();
                if (sorokszama > 0)
                {
                    return "Sikeres Frissítés!";
                }
                else
                {
                    return "Ismeretlen vásárló";
                }
            }
            catch (Exception ex)
            {
                return "Hiba történt a frissítés során: " + ex.Message;

            }
        }

        public string DELETECustomer(int id)
        {
            {
                string connectionString = "SERVER = localhost;" +
                              "DATABASE= webapppeldadb;" +
                              "UID = root;" +
                              "PASSWORD =;";
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                string sql = "DELETE FROM vasarlo WHERE Id =@id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                int sorokszama = cmd.ExecuteNonQuery();
                conn.Close();
                if (sorokszama > 0)
                {
                    return "Sikeres törlés!";
                }
                else
                {
                    return "Ismeretlen vásárló!";
                }
                
            }
        }


    }
}