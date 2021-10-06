using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ConexionSap
{
    class conexionmysql
    {


        public String strConexion = "server=localhost; user id = root; password= ; database = rovi;";

        //public MySqlConnection conectar = new MySqlConnection("server=198.46.95.69; user id = rovisa_panel ; password=Admin_001!?! ; database = rovisa_panel;");
        // public MySqlConnection conectar = new MySqlConnection("server=198.46.95.69; user id = rovisa_panel; password=Admin_001!?! ; database = rovisa_panel;");


        public MySqlConnection conectar = new MySqlConnection("server=localhost; user id = root; password= ; database = rovi;");

        public void abrir()
        {
            try
            {
                conectar.Open();
                Debug.WriteLine("Conexion mysql abierta");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void cerrar()
        {


            // Debug.WriteLine("Conexion mysql cerrada");
            conectar.Close();
        }

       
        public bool ejecutarQuery(string query)
        {
            try
            {
                abrir();
                MySqlCommand comando = new MySqlCommand(query, conectar);
                comando.ExecuteNonQuery();
                cerrar();
                return true;
            }
            catch (Exception ex)
            {

                 Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public int obtenerId(string query)
        {
            int id = 0;

            abrir();
            MySqlCommand comando = new MySqlCommand(query, conectar);
            MySqlDataReader reader = comando.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                id = reader.GetInt16(0);
                reader.Close();
                cerrar();
                return id;
            }
            else
            {
                 Debug.WriteLine("No existen ID RECETA");
                reader.Close();
                return reader.GetInt16(0);
            }

        }

        public double obtenerTotal(string query)
        {

            double total = 0;

            abrir();
            MySqlCommand comando = new MySqlCommand(query, conectar);
            MySqlDataReader reader = comando.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                total = reader.GetDouble(0);
                reader.Close();
                cerrar();
                return total;
            }
            else
            {
                 Debug.WriteLine("Error");
                reader.Close();
                return reader.GetInt16(0);
            }

        }

    }
}

