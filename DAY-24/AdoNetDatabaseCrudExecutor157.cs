using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString =
        "Data Source=.;Initial Catalog=TestDB;Integrated Security=True";

        SqlConnection con = new SqlConnection(connectionString);

        con.Open();

        string query = "SELECT * FROM Students";

        SqlCommand cmd = new SqlCommand(query, con);

        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(reader[0] + " " + reader[1]);
        }

        con.Close();
    }
}