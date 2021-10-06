using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace ConexionSap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        conexion conexionbd = new conexion();
        conexionmysql conexionMysql = new conexionmysql();
        conexion2 conexionMysql2 = new conexion2();

        conexion2 conexionbd2 = new conexion2();
        private void Form1_Load(object sender, EventArgs e)
        {
            //conexion conexionbd = new conexion();
            //conexionbd.abrir();
            timer1.Start();

        }

        private void button1_Click(object sender, EventArgs e)


        {










            Debug.WriteLine(Convert.ToString("Prueba"));
            conexionbd.abrir();
            string query = "SELECT NumAtCard,U_TS_EstatusVenta,DocDueDate,Comments,CntctCode,DocEntry FROM ORDR";

            try
            {


                 MessageBox.Show("Consulta 1pp");



                SqlCommand comando = new SqlCommand(query, conexionbd.Conectarbd);
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {


                    String NumAtCard = lector.GetValue(0).ToString();
                    String U_TS_EstatusVenta = lector.GetValue(1).ToString();
                    String DocDueDate = Convert.ToDateTime(lector.GetValue(2).ToString()).ToString("yyyy-MM-dd");
                    String Comments = lector.GetValue(3).ToString();
                    String CntctCode = lector.GetValue(4).ToString();
                    String DocEntry = lector.GetValue(5).ToString();


                    String fecha = Convert.ToDateTime(lector.GetValue(2).ToString()).ToString("yyyy-MM-dd");



                    String queryvalidar = "select * from ordr where DocEntry = " + lector.GetValue(5).ToString();
                    conexionMysql.abrir();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = conexionMysql.conectar;
                    comand.CommandText = queryvalidar;
                    MySqlDataReader reader = comand.ExecuteReader();



                    if (reader.HasRows)
                    {


                        Debug.WriteLine("Ya está 2");

                        reader.Close();
                        conexionMysql.cerrar();

                        try
                        {

                            String query_update = "UPDATE ordr SET NumAtCard = @NumAtCard, U_TS_EstatusVenta = @U_TS_EstatusVenta, DocDueDate = @DocDueDate, Comments = @Comments, CntctCode = @CntctCode WHERE DocEntry = @DocEntry;";

                            conexionMysql.abrir();
                            MySqlCommand cmd = new MySqlCommand(query_update, conexionMysql.conectar);
                            cmd.Parameters.AddWithValue("@NumAtCard", NumAtCard);
                            cmd.Parameters.AddWithValue("@U_TS_EstatusVenta", U_TS_EstatusVenta);
                            cmd.Parameters.AddWithValue("@DocDueDate", DocDueDate);
                            cmd.Parameters.AddWithValue("@Comments", Comments);
                            cmd.Parameters.AddWithValue("@CntctCode", CntctCode);
                            cmd.Parameters.AddWithValue("@DocEntry", DocEntry);
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            conexionMysql.cerrar();
                           // MessageBox.Show("Se actualizó  2");
                        }
                        catch (Exception ex)
                        {

                            //MessageBox.Show(ex.Message);
                             Debug.WriteLine("Error en actualizar 2");
                        }



                    }
                    else
                    {


                        reader.Close();
                        conexionMysql.cerrar();

                        try
                        {

                            String query_update = "insert into ordr(NumAtCard,U_TS_EstatusVenta,DocDueDate,Comments,CntctCode, DocEntry) values(@NumAtCard,@U_TS_EstatusVenta,@DocDueDate,@Comments,@CntctCode,@DocEntry);";
                            //reader.Close();
                            conexionMysql.abrir();
                            MySqlCommand cmd = new MySqlCommand(query_update, conexionMysql.conectar);

                            cmd.Parameters.AddWithValue("@NumAtCard", NumAtCard);
                            cmd.Parameters.AddWithValue("@U_TS_EstatusVenta", U_TS_EstatusVenta);
                            cmd.Parameters.AddWithValue("@DocDueDate", DocDueDate);
                            cmd.Parameters.AddWithValue("@Comments", Comments);
                            cmd.Parameters.AddWithValue("@CntctCode", CntctCode);
                            cmd.Parameters.AddWithValue("@DocEntry", DocEntry);
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            conexionMysql.cerrar();
                            //MessageBox.Show("Se inserto  2");
                        }
                        catch (Exception ex)
                        {

                             MessageBox.Show(ex.Message);
                             Debug.WriteLine("Error en insertar 2");
                        }











                        conexionbd2.abrir();


                        //string queryOrdrDesc = "SELECT * FROM ( SELECT T0.NumAtCard, T0.DocNum, T0.Comments, T1.ItemCode, T1.Quantity, T1.PriceBefDi, ISNULL(T2.BatchNum, T4.BatchNum) AS 'Lote/Serie', CASE WHEN T3.ManBtchNum = 'Y' THEN 'Lote' WHEN T3.ManSerNum = 'Y' THEN 'Serie' ELSE 'Ninguno' END AS 'Gestion' FROM OINV T0 INNER JOIN INV1 T1 ON T0.DocEntry = T1.DocEntry LEFT JOIN IBT1 T2 ON T2.BaseEntry = T1.DocEntry AND T2.BaseType = T1.ObjType AND T2.BaseLinNum = T1.LineNum INNER JOIN OITM T3 ON T3.ItemCode = T1.ItemCode LEFT JOIN IBT1 T4 ON T4.BaseEntry = T1.BaseEntry AND T4.BaseType = T1.BaseType AND T4.BaseLinNum = T1.BaseLine WHERE T3.ManSerNum  <> 'Y' AND T0.CANCELED = 'N' AND T0.NumAtCard  <> '' UNION ALL SELECT T0.NumAtCard AS 'Numero Pedido', T0.DocNum, T0.Comments, T1.ItemCode, T1.Quantity, T1.PriceBefDi, ISNULL( T22.DistNumber, T44.DistNumber ) AS 'Lote/Serie', 'Serie' AS 'Gestion' FROM OINV T0 INNER JOIN INV1 T1 ON T0.DocEntry = T1.DocEntry LEFT JOIN SRI1 T2 ON T2.BaseEntry = T1.DocEntry AND T2.BaseType = T1.ObjType AND T2.BaseLinNum = T1.LineNum LEFT JOIN OSRN T22 ON T2.SysSerial = T22.SysNumber INNER JOIN OITM T3 ON T3.ItemCode = T1.ItemCode LEFT JOIN SRI1 T4 ON T4.BaseEntry = T1.BaseEntry AND T4.BaseType = T1.BaseType AND T4.BaseLinNum = T1.BaseLine LEFT JOIN OSRN T44 ON T4.SysSerial = T44.SysNumber WHERE T3.ManSerNum = 'Y' AND T0.CANCELED = 'N' AND T0.NumAtCard  <> '' ) T00 WHERE T00.NumAtCard = '" + NumAtCard + "' ORDER BY T00.DocNum ASC ";
                        // Debug.WriteLine(queryOrdrDesc);

                        string queryOrdrDesc = "SELECT T0.[DocNum] AS '# Factura' , T1.DocEntry , T6.[DocNum] AS '# Orden de Venta' , T0.CardCode AS 'Código del Cliente' , T0.CardName AS 'Nombre del Cliente' , T0.[NumAtCard] , T1.[ItemCode] AS 'Código de Articulo' , T1.[Dscription] AS 'Descripción' , T7.ItmsGrpNam AS 'Grupo de Articulo' , T1.[Quantity] AS 'Cantidad' , T1.[PriceBefDi] AS 'Precio Unitario' , T4.DistNumber AS 'Número de serie' , T4.MnfSerial AS 'Número de serie del fabricante' , T4.LotNumber AS 'Número de serie de lote' FROM OINV T0 INNER JOIN INV1 T1 ON T0.[DocEntry] = T1.[DocEntry] INNER JOIN OITM T2 ON T1.ItemCode = T2.ItemCode INNER JOIN SRI1 T3 ON T3.[BaseEntry] = T1.[DocEntry] AND T3.BaseType = T1.ObjType AND T3.BaseLinNum = T1.LineNum AND T1.ItemCode = T3.ItemCode INNER JOIN OSRN T4 ON T4.ItemCode = T3.ItemCode AND T3.SysSerial = T4.SysNumber LEFT JOIN RDR1 T5 ON T1.BaseEntry = T5.DocEntry AND T1.BaseType = T5.ObjType AND T1.BaseLine = T5.LineNum LEFT JOIN ORDR T6 ON T6.DocEntry = T5.DocEntry INNER JOIN OITB T7 ON T2.ItmsGrpCod = T7.ItmsGrpCod WHERE T2.ItmsGrpCod = 101  AND T0.NumAtCard = '" + NumAtCard + "' ";

                        try
                        {


                            SqlCommand comando2 = new SqlCommand(queryOrdrDesc, conexionbd2.Conectarbd);
                            SqlDataReader lector2 = comando2.ExecuteReader();

                            while (lector2.Read())
                            {


                                String NumAtCard2 = lector2.GetValue(5).ToString();
                                String DocNum = lector2.GetValue(0).ToString();
                                String ItemCode = lector2.GetValue(6).ToString();
                                String Dscription = lector2.GetValue(7).ToString();
                                String ItmsGrpNam = lector2.GetValue(8).ToString();
                                String Quantity = lector2.GetValue(9).ToString();
                                String PriceBefDi = lector2.GetValue(10).ToString();
                                String DistNumber = lector2.GetValue(11).ToString();
                                String MnfSerial = lector2.GetValue(12).ToString();
                                String LotNumber = lector2.GetValue(13).ToString();



                                try
                                {

                                    String query_update = "insert into ordr_dsc(NumAtCard, DocNum ,ItemCode, Dscription, ItmsGrpNam,Quantity,PriceBefDi,DistNumber,MnfSerial,LotNumber) values(@NumAtCard, @DocNum , @ItemCode, @Dscription, @ItmsGrpNam,@Quantity, @PriceBefDi, @DistNumber, @MnfSerial, @LotNumber); ";
                                    //reader.Close();

                                    // Debug.WriteLine(query_update);
                                    conexionMysql.abrir();
                                    MySqlCommand cmd = new MySqlCommand(query_update, conexionMysql.conectar);

                                    cmd.Parameters.AddWithValue("@NumAtCard", NumAtCard2);
                                    cmd.Parameters.AddWithValue("@DocNum", DocNum);
                                    cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
                                    cmd.Parameters.AddWithValue("@Dscription", Dscription);
                                    cmd.Parameters.AddWithValue("@ItmsGrpNam", ItmsGrpNam);
                                    cmd.Parameters.AddWithValue("@Quantity", Quantity);
                                    cmd.Parameters.AddWithValue("@PriceBefDi", PriceBefDi);

                                    cmd.Parameters.AddWithValue("@DistNumber", DistNumber);
                                    cmd.Parameters.AddWithValue("@MnfSerial", MnfSerial);

                                    cmd.Parameters.AddWithValue("@LotNumber", LotNumber);

                                    cmd.Prepare();
                                    cmd.ExecuteNonQuery();
                                    conexionMysql.cerrar();
                                    Debug.WriteLine("Se inserto  5");
                                }
                                catch (Exception ex)
                                {

                                     MessageBox.Show(ex.Message);
                                     Debug.WriteLine("Error en insertar 5");
                                }




                            }
                            //lector.Close();

                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("error al abrir BD " + ex.Message);
                        }

                        conexionbd2.cerrar();


                    }

                }
                //lector.Close();

            }
            catch (Exception ex)
            {

               Debug.WriteLine("error al abrir BD " + ex.Message);
            }

            conexionbd.cerrar();



            conexionbd.abrir();
            string queryUsuario = "SELECT Name, FirstName, MiddleName, LastName, E_MailL,CntctCode FROM OCPR";
            try
            {
                 MessageBox.Show("Consulta 2");
                SqlCommand comando = new SqlCommand(queryUsuario, conexionbd.Conectarbd);
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    String Name = lector.GetValue(0).ToString();
                    String FirstName = lector.GetValue(1).ToString();
                    String MiddleName = lector.GetValue(2).ToString();
                    String LastName = lector.GetValue(3).ToString();
                    String E_MailL = lector.GetValue(4).ToString();
                    String CntctCode = lector.GetValue(5).ToString();
                    String Tipo_Usuario = "Cliente";
                    String Status = "0";



                    String Password = BCrypt.Net.BCrypt.HashPassword("Rovi2021");



                    String queryvalidar = "select * from ocpr where CntctCode = " + lector.GetValue(5).ToString();
                    conexionMysql.abrir();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = conexionMysql.conectar;
                    comand.CommandText = queryvalidar;
                    MySqlDataReader reader = comand.ExecuteReader();

                    if (reader.HasRows)
                    {

                        Debug.WriteLine("Ya está 2");

                        reader.Close();
                        conexionMysql.cerrar();

                        try
                        {


                            String query_update = "UPDATE ocpr SET Name= @Name,FirstName= @FirstName, MiddleName= @MiddleName, LastName= @LastName, E_MailL= @E_MailL WHERE CntctCode= @CntctCode;";

                            conexionMysql.abrir();
                            MySqlCommand cmd = new MySqlCommand(query_update, conexionMysql.conectar);

                            cmd.Parameters.AddWithValue("@Name", Name);
                            cmd.Parameters.AddWithValue("@FirstName", FirstName);
                            cmd.Parameters.AddWithValue("@MiddleName", MiddleName);
                            cmd.Parameters.AddWithValue("@LastName", LastName);
                            cmd.Parameters.AddWithValue("@E_MailL", E_MailL);
                            cmd.Parameters.AddWithValue("@CntctCode", CntctCode);
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            conexionMysql.cerrar();
                            Debug.WriteLine("Se actualizó  2");
                        }
                        catch (Exception ex)
                        {

                             Debug.WriteLine(ex.Message);
                             Debug.WriteLine("Error en actualizar 2");
                        }


                    }
                    else
                    {
                        reader.Close();
                        conexionMysql.cerrar();

                        try
                        {

                            String query_update = "insert into ocpr(Name,FirstName, MiddleName, LastName, E_MailL,CntctCode, Tipo_Usuario, Status,Password) values(@Name,@FirstName, @MiddleName, @LastName, @E_MailL,@CntctCode,@Tipo_Usuario, @Status,@Password);";
                            reader.Close();
                            conexionMysql.abrir();
                            MySqlCommand cmd = new MySqlCommand(query_update, conexionMysql.conectar);

                            cmd.Parameters.AddWithValue("@Name", Name);
                            cmd.Parameters.AddWithValue("@FirstName", FirstName);
                            cmd.Parameters.AddWithValue("@MiddleName", MiddleName);
                            cmd.Parameters.AddWithValue("@LastName", LastName);
                            cmd.Parameters.AddWithValue("@E_MailL", E_MailL);
                            cmd.Parameters.AddWithValue("@CntctCode", CntctCode);

                            cmd.Parameters.AddWithValue("@Tipo_Usuario", Tipo_Usuario);
                            cmd.Parameters.AddWithValue("@Status", Status);
                            cmd.Parameters.AddWithValue("@Password", Password);
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            conexionMysql.cerrar();
                            Debug.WriteLine("Se inserto  2");
                        }
                        catch (Exception ex)
                        {

                             Debug.WriteLine(ex.Message);
                             Debug.WriteLine("Error en insertar 2");
                        }


                    }

                }
                lector.Close();

            }
            catch (Exception ex)
            {

                Debug.WriteLine("error al abrir BD " + ex.Message);
            }

            conexionbd.cerrar();





            conexionbd.abrir();

            string queryServicios = "SELECT T0.callID, T2.Name as callType, T3.Descriptio, T0.manufSN, T0.itemName, T0.itemCode, T0.descrption, T0.contctCode,T0.customer,T0.DocNum, T0.U_TS_PedidoRelacionado, T0.internalSN, T0.closeDate, T0.closeTime FROM OSCL T0 INNER JOIN OSCT T2 ON T0.callType = T2.callTypeID INNER JOIN OSCS T3 ON T0.status = T3.statusID"; 
            try
            {
                 Debug.WriteLine("Consulta 3ok");
                SqlCommand comando = new SqlCommand(queryServicios, conexionbd.Conectarbd);
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {


                    String callID = lector.GetValue(0).ToString();
                    String callType = lector.GetValue(1).ToString();
                    String status = lector.GetValue(2).ToString();
                    String manufSN = lector.GetValue(3).ToString();
                    String itemName = lector.GetValue(4).ToString();
                    String itemCode = lector.GetValue(5).ToString();
                    String descrption = lector.GetValue(6).ToString();
                    String contctCode = lector.GetValue(7).ToString();
                    String customer = lector.GetValue(8).ToString();
                    String DocNum = lector.GetValue(9).ToString();
                    String U_TS_PedidoRelacionado = lector.GetValue(10).ToString();
                    String internalSN = lector.GetValue(11).ToString();
                    String closeDate = lector.GetValue(12).ToString();
                    String closeTime = lector.GetValue(13).ToString();

                    if (closeDate != "")
                    {
                        closeDate = Convert.ToDateTime(lector.GetValue(12).ToString()).ToString("yyyy-MM-dd");

                    }

                    String queryvalidar = "select * from oscl where callID = " + lector.GetValue(0).ToString();
                    conexionMysql.abrir();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = conexionMysql.conectar;
                    comand.CommandText = queryvalidar;
                    MySqlDataReader reader = comand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Debug.WriteLine("Ya está 2");

                        reader.Close();
                        conexionMysql.cerrar();

                        try
                        {

                            String query_update = "UPDATE oscl SET  callType=@callType, status=@status, manufSN=@manufSN, itemName=@itemName,itemCode=@itemCode,descrption=@descrption,contctCode=@contctCode,customer=@customer, DocNum=@DocNum,U_TS_PedidoRelacionado=@U_TS_PedidoRelacionado,internalSN=@internalSN, closeDate=@closeDate, closeTime=@closeTime WHERE callID = @callID;";
                            reader.Close();


                            conexionMysql.abrir();
                            MySqlCommand cmd = new MySqlCommand(query_update, conexionMysql.conectar);

                            cmd.Parameters.AddWithValue("@callType", callType);
                            cmd.Parameters.AddWithValue("@status", status);
                            cmd.Parameters.AddWithValue("@manufSN", manufSN);
                            cmd.Parameters.AddWithValue("@itemName", itemName);
                            cmd.Parameters.AddWithValue("@itemCode", itemCode);
                            cmd.Parameters.AddWithValue("@descrption", descrption);
                            cmd.Parameters.AddWithValue("@contctCode", contctCode);
                            cmd.Parameters.AddWithValue("@customer", customer);
                            cmd.Parameters.AddWithValue("@callID", callID);
                            cmd.Parameters.AddWithValue("@DocNum", DocNum);
                            cmd.Parameters.AddWithValue("@U_TS_PedidoRelacionado", U_TS_PedidoRelacionado);
                            cmd.Parameters.AddWithValue("@internalSN", internalSN);
                            cmd.Parameters.AddWithValue("@closeDate", closeDate);
                            cmd.Parameters.AddWithValue("@closeTime", closeTime);

                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            conexionMysql.cerrar();
                            Debug.WriteLine("Se actualizó  3");
                        }
                        catch (Exception ex)
                        {

                             Debug.WriteLine(ex.Message);
                             Debug.WriteLine("Error en actualizar 3");
                        }


                    }
                    else
                    {

                        reader.Close();
                        conexionMysql.cerrar();

                        try
                        {

                            String query_insert = "insert into oscl(callID, callType, status, manufSN, itemName,itemCode,descrption,contctCode,customer,DocNum,U_TS_PedidoRelacionado,internalSN, closeDate, closeTime) values(@callID, @callType, @status, @manufSN, @itemName, @itemCode, @descrption, @contctCode, @customer, @DocNum, @U_TS_PedidoRelacionado, @internalSN, @closeDate, @closeTime);";
                            reader.Close();
                            // Debug.WriteLine(query_insert);
                            conexionMysql.abrir();
                            MySqlCommand cmd = new MySqlCommand(query_insert, conexionMysql.conectar);

                            cmd.Parameters.AddWithValue("@callType", callType);
                            cmd.Parameters.AddWithValue("@status", status);
                            cmd.Parameters.AddWithValue("@manufSN", manufSN);
                            cmd.Parameters.AddWithValue("@itemName", itemName);
                            cmd.Parameters.AddWithValue("@itemCode", itemCode);
                            cmd.Parameters.AddWithValue("@descrption", descrption);
                            cmd.Parameters.AddWithValue("@contctCode", contctCode);
                            cmd.Parameters.AddWithValue("@customer", customer);
                            cmd.Parameters.AddWithValue("@callID", callID);
                            cmd.Parameters.AddWithValue("@DocNum", DocNum);
                            cmd.Parameters.AddWithValue("@U_TS_PedidoRelacionado", U_TS_PedidoRelacionado);
                            cmd.Parameters.AddWithValue("@internalSN", internalSN);
                            cmd.Parameters.AddWithValue("@closeDate", closeDate);
                            cmd.Parameters.AddWithValue("@closeTime", closeTime);
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            conexionMysql.cerrar();
                            Debug.WriteLine("Se inserto  3");
                        }
                        catch (Exception ex)
                        {

                             Debug.WriteLine(ex.Message);
                             Debug.WriteLine("Error en insertar 3");
                        }

                       

                    }

                }
                lector.Close();

            }
            catch (Exception ex)
            {

                 Debug.WriteLine("error al abrir BD " + ex.Message);
            }

            conexionbd.cerrar();



            conexionbd.abrir();

            string queryActividades = "SELECT T3.ClgCode, T0.[callID] , T3.endDate , T3.endTIME , T4.U_NAME , T3.Notes FROM OSCL T0 INNER JOIN OCLG T3 ON T0.callID = T3.parentId AND T0.ObjType = T3.parentType INNER JOIN OUSR T4 ON T3.AttendUser = T4.USERID ";



            try
            {
                 Debug.WriteLine("Consulta 4 ok");
                SqlCommand comando = new SqlCommand(queryActividades, conexionbd.Conectarbd);
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {


                    String ClgCode = lector.GetValue(0).ToString();
                    String callID = lector.GetValue(1).ToString();
                    String endTIME = lector.GetValue(3).ToString();
                    String U_NAME = lector.GetValue(4).ToString();
                    String Notes = lector.GetValue(5).ToString();


                    // Debug.WriteLine(lector.GetValue(2).ToString());

                    String endDate = lector.GetValue(2).ToString();

                    if (endDate != "")
                    {
                        endDate = Convert.ToDateTime(lector.GetValue(2).ToString()).ToString("yyyy-MM-dd");

                    }

                    // Debug.WriteLine(CloseDate);

                    String queryvalidar = "select * from oclg where ClgCode = " + lector.GetValue(0).ToString();
                    conexionMysql.abrir();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = conexionMysql.conectar;
                    comand.CommandText = queryvalidar;
                    MySqlDataReader reader = comand.ExecuteReader();

                    if (reader.HasRows)
                    {

                        Debug.WriteLine("Ya está 4");

                        reader.Close();
                        conexionMysql.cerrar();

                        try
                        {


                            String query_update = "UPDATE oclg SET callID=@callID, endDate=@endDate,endTIME=@endTIME, U_NAME=@U_NAME, Notes=@Notes WHERE ClgCode = @ClgCode;";
                            reader.Close();


                            conexionMysql.abrir();
                            MySqlCommand cmd = new MySqlCommand(query_update, conexionMysql.conectar);

                            cmd.Parameters.AddWithValue("@Notes", Notes);
                            cmd.Parameters.AddWithValue("@callID", callID);
                            cmd.Parameters.AddWithValue("@ClgCode", ClgCode);
                            cmd.Parameters.AddWithValue("@endDate", endDate);
                            cmd.Parameters.AddWithValue("@endTIME", endTIME);
                            cmd.Parameters.AddWithValue("@U_NAME", U_NAME);

                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            conexionMysql.cerrar();
                            Debug.WriteLine("Se actualizó  4");
                        }
                        catch (Exception ex)
                        {

                             Debug.WriteLine(ex.Message);
                             Debug.WriteLine("Error en actualizar 4");
                        }

                        
                    }
                    else
                    {
                        reader.Close();
                        conexionMysql.cerrar();

                        try
                        {
                            String query_insert = "insert into oclg(ClgCode,callID,endDate,endTIME,U_NAME,Notes) values(@ClgCode,@callID,@endDate,@endTIME,@U_NAME,@Notes);";
                            reader.Close();
                            conexionMysql.abrir();
                            MySqlCommand cmd = new MySqlCommand(query_insert, conexionMysql.conectar);


                            cmd.Parameters.AddWithValue("@Notes", Notes);
                            cmd.Parameters.AddWithValue("@callID", callID);
                            cmd.Parameters.AddWithValue("@ClgCode", ClgCode);
                            cmd.Parameters.AddWithValue("@endDate", endDate);
                            cmd.Parameters.AddWithValue("@endTIME", endTIME);
                            cmd.Parameters.AddWithValue("@U_NAME", U_NAME);

                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            conexionMysql.cerrar();
                            Debug.WriteLine("Se inserto  4");
                        }
                        catch (Exception ex)
                        {

                             Debug.WriteLine(ex.Message);
                             Debug.WriteLine("Error en insertar 4");
                        }

                       

                    }


                }
                lector.Close();

            }
            catch (Exception ex)
            {

                Debug.WriteLine("error al abrir BD " + ex.Message);
            }

            conexionbd.cerrar();


        }


        private void label1_Click(object sender, EventArgs e)
        {



        }

        private void timer1_Tick(object sender, EventArgs e) {











            Debug.WriteLine(Convert.ToString("Prueba"));
            conexionbd.abrir();
            string query = "SELECT NumAtCard,U_TS_EstatusVenta,DocDueDate,Comments,CntctCode,DocEntry FROM ORDR";

            try
            {


                Debug.WriteLine("Consulta 1pp");



                SqlCommand comando = new SqlCommand(query, conexionbd.Conectarbd);
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {


                    String NumAtCard = lector.GetValue(0).ToString();
                    String U_TS_EstatusVenta = lector.GetValue(1).ToString();
                    String DocDueDate = Convert.ToDateTime(lector.GetValue(2).ToString()).ToString("yyyy-MM-dd");
                    String Comments = lector.GetValue(3).ToString();
                    String CntctCode = lector.GetValue(4).ToString();
                    String DocEntry = lector.GetValue(5).ToString();


                    String fecha = Convert.ToDateTime(lector.GetValue(2).ToString()).ToString("yyyy-MM-dd");



                    String queryvalidar = "select * from ORDR where DocEntry = " + lector.GetValue(5).ToString();
                    conexionMysql.abrir();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = conexionMysql.conectar;
                    comand.CommandText = queryvalidar;
                    MySqlDataReader reader = comand.ExecuteReader();



                    if (reader.HasRows)
                    {


                        Debug.WriteLine("Ya está 2");

                        reader.Close();
                        conexionMysql.cerrar();

                        try
                        {

                            String query_update = "UPDATE ORDR SET NumAtCard = @NumAtCard, U_TS_EstatusVenta = @U_TS_EstatusVenta, DocDueDate = @DocDueDate, Comments = @Comments, CntctCode = @CntctCode WHERE DocEntry = @DocEntry;";

                            conexionMysql.abrir();
                            MySqlCommand cmd = new MySqlCommand(query_update, conexionMysql.conectar);
                            cmd.Parameters.AddWithValue("@NumAtCard", NumAtCard);
                            cmd.Parameters.AddWithValue("@U_TS_EstatusVenta", U_TS_EstatusVenta);
                            cmd.Parameters.AddWithValue("@DocDueDate", DocDueDate);
                            cmd.Parameters.AddWithValue("@Comments", Comments);
                            cmd.Parameters.AddWithValue("@CntctCode", CntctCode);
                            cmd.Parameters.AddWithValue("@DocEntry", DocEntry);
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            conexionMysql.cerrar();
                            Debug.WriteLine("Se actualizó  2");
                        }
                        catch (Exception ex)
                        {

                            Debug.WriteLine(ex.Message);
                            Debug.WriteLine("Error en actualizar 2");
                        }



                    }
                    else
                    {


                        reader.Close();
                        conexionMysql.cerrar();

                        try
                        {

                            String query_update = "insert into ORDR(NumAtCard,U_TS_EstatusVenta,DocDueDate,Comments,CntctCode, DocEntry) values(@NumAtCard,@U_TS_EstatusVenta,@DocDueDate,@Comments,@CntctCode,@DocEntry);";
                            //reader.Close();
                            conexionMysql.abrir();
                            MySqlCommand cmd = new MySqlCommand(query_update, conexionMysql.conectar);

                            cmd.Parameters.AddWithValue("@NumAtCard", NumAtCard);
                            cmd.Parameters.AddWithValue("@U_TS_EstatusVenta", U_TS_EstatusVenta);
                            cmd.Parameters.AddWithValue("@DocDueDate", DocDueDate);
                            cmd.Parameters.AddWithValue("@Comments", Comments);
                            cmd.Parameters.AddWithValue("@CntctCode", CntctCode);
                            cmd.Parameters.AddWithValue("@DocEntry", DocEntry);
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            conexionMysql.cerrar();
                            Debug.WriteLine("Se inserto  2");
                        }
                        catch (Exception ex)
                        {

                            Debug.WriteLine(ex.Message);
                            Debug.WriteLine("Error en insertar 2");
                        }











                        conexionbd2.abrir();


                        string queryOrdrDesc = "SELECT * FROM ( SELECT T0.NumAtCard, T0.DocNum, T0.Comments, T1.ItemCode, T1.Quantity, T1.PriceBefDi, ISNULL(T2.BatchNum, T4.BatchNum) AS 'Lote/Serie', CASE WHEN T3.ManBtchNum = 'Y' THEN 'Lote' WHEN T3.ManSerNum = 'Y' THEN 'Serie' ELSE 'Ninguno' END AS 'Gestion' FROM OINV T0 INNER JOIN INV1 T1 ON T0.DocEntry = T1.DocEntry LEFT JOIN IBT1 T2 ON T2.BaseEntry = T1.DocEntry AND T2.BaseType = T1.ObjType AND T2.BaseLinNum = T1.LineNum INNER JOIN OITM T3 ON T3.ItemCode = T1.ItemCode LEFT JOIN IBT1 T4 ON T4.BaseEntry = T1.BaseEntry AND T4.BaseType = T1.BaseType AND T4.BaseLinNum = T1.BaseLine WHERE T3.ManSerNum  <> 'Y' AND T0.CANCELED = 'N' AND T0.NumAtCard  <> '' UNION ALL SELECT T0.NumAtCard AS 'Numero Pedido', T0.DocNum, T0.Comments, T1.ItemCode, T1.Quantity, T1.PriceBefDi, ISNULL( T22.DistNumber, T44.DistNumber ) AS 'Lote/Serie', 'Serie' AS 'Gestion' FROM OINV T0 INNER JOIN INV1 T1 ON T0.DocEntry = T1.DocEntry LEFT JOIN SRI1 T2 ON T2.BaseEntry = T1.DocEntry AND T2.BaseType = T1.ObjType AND T2.BaseLinNum = T1.LineNum LEFT JOIN OSRN T22 ON T2.SysSerial = T22.SysNumber INNER JOIN OITM T3 ON T3.ItemCode = T1.ItemCode LEFT JOIN SRI1 T4 ON T4.BaseEntry = T1.BaseEntry AND T4.BaseType = T1.BaseType AND T4.BaseLinNum = T1.BaseLine LEFT JOIN OSRN T44 ON T4.SysSerial = T44.SysNumber WHERE T3.ManSerNum = 'Y' AND T0.CANCELED = 'N' AND T0.NumAtCard  <> '' ) T00 WHERE T00.NumAtCard = '" + NumAtCard + "' ORDER BY T00.DocNum ASC ";
                        // Debug.WriteLine(queryOrdrDesc);
                        try
                        {


                            SqlCommand comando2 = new SqlCommand(queryOrdrDesc, conexionbd2.Conectarbd);
                            SqlDataReader lector2 = comando2.ExecuteReader();

                            while (lector2.Read())
                            {


                                String NumAtCard2 = lector2.GetValue(0).ToString();
                                String DocNum = lector2.GetValue(1).ToString();
                                String Comments2 = lector2.GetValue(2).ToString();
                                String ItemCode = lector2.GetValue(3).ToString();
                                String Quantity = lector2.GetValue(4).ToString();
                                String PriceBefDi = lector2.GetValue(5).ToString();
                                String DistNumber = lector2.GetValue(6).ToString();
                                String Serie = lector2.GetValue(7).ToString();




                                try
                                {

                                    String query_update = "insert into ordr_dsc(NumAtCard, DocNum ,Comments, ItemCode, Quantity,PriceBefDi,DistNumber,Serie) values(@NumAtCard, @DocNum ,@Comments, @ItemCode, @Quantity, @PriceBefDi, @DistNumber, @Serie); ";
                                    //reader.Close();

                                    // Debug.WriteLine(query_update);
                                    conexionMysql.abrir();
                                    MySqlCommand cmd = new MySqlCommand(query_update, conexionMysql.conectar);

                                    cmd.Parameters.AddWithValue("@NumAtCard", NumAtCard2);
                                    cmd.Parameters.AddWithValue("@DocNum", DocNum);
                                    cmd.Parameters.AddWithValue("@Comments", Comments2);
                                    cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
                                    cmd.Parameters.AddWithValue("@Quantity", Quantity);
                                    cmd.Parameters.AddWithValue("@PriceBefDi", PriceBefDi);
                                    cmd.Parameters.AddWithValue("@DistNumber", DistNumber);
                                    cmd.Parameters.AddWithValue("@Serie", Serie);

                                    cmd.Prepare();
                                    cmd.ExecuteNonQuery();
                                    conexionMysql.cerrar();
                                    Debug.WriteLine("Se inserto  5");
                                }
                                catch (Exception ex)
                                {

                                    Debug.WriteLine(ex.Message);
                                    Debug.WriteLine("Error en insertar 5");
                                }




                            }
                            //lector.Close();

                        }
                        catch (Exception ex)
                        {

                            Debug.WriteLine("error al abrir BD " + ex.Message);
                        }

                        conexionbd2.cerrar();


                    }

                }
                //lector.Close();

            }
            catch (Exception ex)
            {

                Debug.WriteLine("error al abrir BD " + ex.Message);
            }

            conexionbd.cerrar();



            conexionbd.abrir();
            string queryUsuario = "SELECT Name, FirstName, MiddleName, LastName, E_MailL,CntctCode FROM OCPR";
            try
            {
                Debug.WriteLine("Consulta 2");
                SqlCommand comando = new SqlCommand(queryUsuario, conexionbd.Conectarbd);
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    String Name = lector.GetValue(0).ToString();
                    String FirstName = lector.GetValue(1).ToString();
                    String MiddleName = lector.GetValue(2).ToString();
                    String LastName = lector.GetValue(3).ToString();
                    String E_MailL = lector.GetValue(4).ToString();
                    String CntctCode = lector.GetValue(5).ToString();
                    String Tipo_Usuario = "Cliente";
                    String Status = "0";



                    String Password = BCrypt.Net.BCrypt.HashPassword("Rovi2021");



                    String queryvalidar = "select * from OCPR where CntctCode = " + lector.GetValue(5).ToString();
                    conexionMysql.abrir();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = conexionMysql.conectar;
                    comand.CommandText = queryvalidar;
                    MySqlDataReader reader = comand.ExecuteReader();

                    if (reader.HasRows)
                    {

                        Debug.WriteLine("Ya está 2");

                        reader.Close();
                        conexionMysql.cerrar();

                        try
                        {


                            String query_update = "UPDATE OCPR SET Name= @Name,FirstName= @FirstName, MiddleName= @MiddleName, LastName= @LastName, E_MailL= @E_MailL WHERE CntctCode= @CntctCode;";

                            conexionMysql.abrir();
                            MySqlCommand cmd = new MySqlCommand(query_update, conexionMysql.conectar);

                            cmd.Parameters.AddWithValue("@Name", Name);
                            cmd.Parameters.AddWithValue("@FirstName", FirstName);
                            cmd.Parameters.AddWithValue("@MiddleName", MiddleName);
                            cmd.Parameters.AddWithValue("@LastName", LastName);
                            cmd.Parameters.AddWithValue("@E_MailL", E_MailL);
                            cmd.Parameters.AddWithValue("@CntctCode", CntctCode);
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            conexionMysql.cerrar();
                            Debug.WriteLine("Se actualizó  2");
                        }
                        catch (Exception ex)
                        {

                            Debug.WriteLine(ex.Message);
                            Debug.WriteLine("Error en actualizar 2");
                        }


                    }
                    else
                    {
                        reader.Close();
                        conexionMysql.cerrar();

                        try
                        {

                            String query_update = "insert into OCPR(Name,FirstName, MiddleName, LastName, E_MailL,CntctCode, Tipo_Usuario, Status,Password) values(@Name,@FirstName, @MiddleName, @LastName, @E_MailL,@CntctCode,@Tipo_Usuario, @Status,@Password);";
                            reader.Close();
                            conexionMysql.abrir();
                            MySqlCommand cmd = new MySqlCommand(query_update, conexionMysql.conectar);

                            cmd.Parameters.AddWithValue("@Name", Name);
                            cmd.Parameters.AddWithValue("@FirstName", FirstName);
                            cmd.Parameters.AddWithValue("@MiddleName", MiddleName);
                            cmd.Parameters.AddWithValue("@LastName", LastName);
                            cmd.Parameters.AddWithValue("@E_MailL", E_MailL);
                            cmd.Parameters.AddWithValue("@CntctCode", CntctCode);

                            cmd.Parameters.AddWithValue("@Tipo_Usuario", Tipo_Usuario);
                            cmd.Parameters.AddWithValue("@Status", Status);
                            cmd.Parameters.AddWithValue("@Password", Password);
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            conexionMysql.cerrar();
                            Debug.WriteLine("Se inserto  2");
                        }
                        catch (Exception ex)
                        {

                            Debug.WriteLine(ex.Message);
                            Debug.WriteLine("Error en insertar 2");
                        }


                    }

                }
                lector.Close();

            }
            catch (Exception ex)
            {

                Debug.WriteLine("error al abrir BD " + ex.Message);
            }

            conexionbd.cerrar();





            conexionbd.abrir();

            string queryServicios = "SELECT T0.callID, T2.Name as callType, T3.Descriptio, T0.manufSN, T0.itemName, T0.itemCode, T0.descrption, T0.contctCode,T0.customer,T0.DocNum, T0.U_TS_PedidoRelacionado, T0.internalSN, T0.closeDate, T0.closeTime FROM OSCL T0 INNER JOIN OSCT T2 ON T0.callType = T2.callTypeID INNER JOIN OSCS T3 ON T0.status = T3.statusID";
            try
            {
                Debug.WriteLine("Consulta 3ok");
                SqlCommand comando = new SqlCommand(queryServicios, conexionbd.Conectarbd);
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {


                    String callID = lector.GetValue(0).ToString();
                    String callType = lector.GetValue(1).ToString();
                    String status = lector.GetValue(2).ToString();
                    String manufSN = lector.GetValue(3).ToString();
                    String itemName = lector.GetValue(4).ToString();
                    String itemCode = lector.GetValue(5).ToString();
                    String descrption = lector.GetValue(6).ToString();
                    String contctCode = lector.GetValue(7).ToString();
                    String customer = lector.GetValue(8).ToString();
                    String DocNum = lector.GetValue(9).ToString();
                    String U_TS_PedidoRelacionado = lector.GetValue(10).ToString();
                    String internalSN = lector.GetValue(11).ToString();
                    String closeDate = lector.GetValue(12).ToString();
                    String closeTime = lector.GetValue(13).ToString();

                    if (closeDate != "")
                    {
                        closeDate = Convert.ToDateTime(lector.GetValue(12).ToString()).ToString("yyyy-MM-dd");

                    }

                    String queryvalidar = "select * from OSCL where callID = " + lector.GetValue(0).ToString();
                    conexionMysql.abrir();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = conexionMysql.conectar;
                    comand.CommandText = queryvalidar;
                    MySqlDataReader reader = comand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Debug.WriteLine("Ya está 2");

                        reader.Close();
                        conexionMysql.cerrar();

                        try
                        {

                            String query_update = "UPDATE OSCL SET  callType=@callType, status=@status, manufSN=@manufSN, itemName=@itemName,itemCode=@itemCode,descrption=@descrption,contctCode=@contctCode,customer=@customer, DocNum=@DocNum,U_TS_PedidoRelacionado=@U_TS_PedidoRelacionado,internalSN=@internalSN, closeDate=@closeDate, closeTime=@closeTime WHERE callID = @callID;";
                            reader.Close();


                            conexionMysql.abrir();
                            MySqlCommand cmd = new MySqlCommand(query_update, conexionMysql.conectar);

                            cmd.Parameters.AddWithValue("@callType", callType);
                            cmd.Parameters.AddWithValue("@status", status);
                            cmd.Parameters.AddWithValue("@manufSN", manufSN);
                            cmd.Parameters.AddWithValue("@itemName", itemName);
                            cmd.Parameters.AddWithValue("@itemCode", itemCode);
                            cmd.Parameters.AddWithValue("@descrption", descrption);
                            cmd.Parameters.AddWithValue("@contctCode", contctCode);
                            cmd.Parameters.AddWithValue("@customer", customer);
                            cmd.Parameters.AddWithValue("@callID", callID);
                            cmd.Parameters.AddWithValue("@DocNum", DocNum);
                            cmd.Parameters.AddWithValue("@U_TS_PedidoRelacionado", U_TS_PedidoRelacionado);
                            cmd.Parameters.AddWithValue("@internalSN", internalSN);
                            cmd.Parameters.AddWithValue("@closeDate", closeDate);
                            cmd.Parameters.AddWithValue("@closeTime", closeTime);

                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            conexionMysql.cerrar();
                            Debug.WriteLine("Se actualizó  3");
                        }
                        catch (Exception ex)
                        {

                            Debug.WriteLine(ex.Message);
                            Debug.WriteLine("Error en actualizar 3");
                        }


                    }
                    else
                    {

                        reader.Close();
                        conexionMysql.cerrar();

                        try
                        {

                            String query_insert = "insert into OSCL(callID, callType, status, manufSN, itemName,itemCode,descrption,contctCode,customer,DocNum,U_TS_PedidoRelacionado,internalSN, closeDate, closeTime) values(@callID, @callType, @status, @manufSN, @itemName, @itemCode, @descrption, @contctCode, @customer, @DocNum, @U_TS_PedidoRelacionado, @internalSN, @closeDate, @closeTime);";
                            reader.Close();
                            // Debug.WriteLine(query_insert);
                            conexionMysql.abrir();
                            MySqlCommand cmd = new MySqlCommand(query_insert, conexionMysql.conectar);

                            cmd.Parameters.AddWithValue("@callType", callType);
                            cmd.Parameters.AddWithValue("@status", status);
                            cmd.Parameters.AddWithValue("@manufSN", manufSN);
                            cmd.Parameters.AddWithValue("@itemName", itemName);
                            cmd.Parameters.AddWithValue("@itemCode", itemCode);
                            cmd.Parameters.AddWithValue("@descrption", descrption);
                            cmd.Parameters.AddWithValue("@contctCode", contctCode);
                            cmd.Parameters.AddWithValue("@customer", customer);
                            cmd.Parameters.AddWithValue("@callID", callID);
                            cmd.Parameters.AddWithValue("@DocNum", DocNum);
                            cmd.Parameters.AddWithValue("@U_TS_PedidoRelacionado", U_TS_PedidoRelacionado);
                            cmd.Parameters.AddWithValue("@internalSN", internalSN);
                            cmd.Parameters.AddWithValue("@closeDate", closeDate);
                            cmd.Parameters.AddWithValue("@closeTime", closeTime);
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            conexionMysql.cerrar();
                            Debug.WriteLine("Se inserto  3");
                        }
                        catch (Exception ex)
                        {

                            Debug.WriteLine(ex.Message);
                            Debug.WriteLine("Error en insertar 3");
                        }



                    }

                }
                lector.Close();

            }
            catch (Exception ex)
            {

                Debug.WriteLine("error al abrir BD " + ex.Message);
            }

            conexionbd.cerrar();



            conexionbd.abrir();

            string queryActividades = "SELECT T3.ClgCode, T0.[callID] , T3.endDate , T3.endTIME , T4.U_NAME , T3.Notes FROM OSCL T0 INNER JOIN OCLG T3 ON T0.callID = T3.parentId AND T0.ObjType = T3.parentType INNER JOIN OUSR T4 ON T3.AttendUser = T4.USERID ";



            try
            {
                Debug.WriteLine("Consulta 4 ok");
                SqlCommand comando = new SqlCommand(queryActividades, conexionbd.Conectarbd);
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {


                    String ClgCode = lector.GetValue(0).ToString();
                    String callID = lector.GetValue(1).ToString();
                    String endTIME = lector.GetValue(3).ToString();
                    String U_NAME = lector.GetValue(4).ToString();
                    String Notes = lector.GetValue(5).ToString();


                    // Debug.WriteLine(lector.GetValue(2).ToString());

                    String endDate = lector.GetValue(2).ToString();

                    if (endDate != "")
                    {
                        endDate = Convert.ToDateTime(lector.GetValue(2).ToString()).ToString("yyyy-MM-dd");

                    }

                    // Debug.WriteLine(CloseDate);

                    String queryvalidar = "select * from OCLG where ClgCode = " + lector.GetValue(0).ToString();
                    conexionMysql.abrir();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = conexionMysql.conectar;
                    comand.CommandText = queryvalidar;
                    MySqlDataReader reader = comand.ExecuteReader();

                    if (reader.HasRows)
                    {

                        Debug.WriteLine("Ya está 4");

                        reader.Close();
                        conexionMysql.cerrar();

                        try
                        {


                            String query_update = "UPDATE OCLG SET callID=@callID, endDate=@endDate,endTIME=@endTIME, U_NAME=@U_NAME, Notes=@Notes WHERE ClgCode = @ClgCode;";
                            reader.Close();


                            conexionMysql.abrir();
                            MySqlCommand cmd = new MySqlCommand(query_update, conexionMysql.conectar);

                            cmd.Parameters.AddWithValue("@Notes", Notes);
                            cmd.Parameters.AddWithValue("@callID", callID);
                            cmd.Parameters.AddWithValue("@ClgCode", ClgCode);
                            cmd.Parameters.AddWithValue("@endDate", endDate);
                            cmd.Parameters.AddWithValue("@endTIME", endTIME);
                            cmd.Parameters.AddWithValue("@U_NAME", U_NAME);

                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            conexionMysql.cerrar();
                            Debug.WriteLine("Se actualizó  4");
                        }
                        catch (Exception ex)
                        {

                            Debug.WriteLine(ex.Message);
                            Debug.WriteLine("Error en actualizar 4");
                        }


                    }
                    else
                    {
                        reader.Close();
                        conexionMysql.cerrar();

                        try
                        {
                            String query_insert = "insert into OCLG(ClgCode,callID,endDate,endTIME,U_NAME,Notes) values(@ClgCode,@callID,@endDate,@endTIME,@U_NAME,@Notes);";
                            reader.Close();
                            conexionMysql.abrir();
                            MySqlCommand cmd = new MySqlCommand(query_insert, conexionMysql.conectar);


                            cmd.Parameters.AddWithValue("@Notes", Notes);
                            cmd.Parameters.AddWithValue("@callID", callID);
                            cmd.Parameters.AddWithValue("@ClgCode", ClgCode);
                            cmd.Parameters.AddWithValue("@endDate", endDate);
                            cmd.Parameters.AddWithValue("@endTIME", endTIME);
                            cmd.Parameters.AddWithValue("@U_NAME", U_NAME);

                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            conexionMysql.cerrar();
                            Debug.WriteLine("Se inserto  4");
                        }
                        catch (Exception ex)
                        {

                            Debug.WriteLine(ex.Message);
                            Debug.WriteLine("Error en insertar 4");
                        }



                    }


                }
                lector.Close();

            }
            catch (Exception ex)
            {

                Debug.WriteLine("error al abrir BD " + ex.Message);
            }

            conexionbd.cerrar();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
