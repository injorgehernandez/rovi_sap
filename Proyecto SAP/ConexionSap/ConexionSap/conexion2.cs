using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Diagnostics;

namespace ConexionSap
{
    class conexion2
    {
        //Cadena de Conexion
        string cadena = "data source =189.178.149.181,1433; initial catalog = ROVI_PROD; user id = web; password = RSI090407by8";

        public SqlConnection Conectarbd = new SqlConnection();

        //Constructor
        public conexion2()
        {
            Conectarbd.ConnectionString = cadena;
        }

        //Metodo para abrir la conexion
        public void abrir()
        {
            try
            {
                Conectarbd.Open();
                // Debug.WriteLine("Conexión abierta");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("error al abrir BD " + ex.Message);

                 Debug.WriteLine("error al abrir BD " + ex.Message);

            }
        }

        //Metodo para cerrar la conexion
        public void cerrar()
        {
            Conectarbd.Close();

            // Debug.WriteLine("Conexión cerrada");
        }
    }
}
