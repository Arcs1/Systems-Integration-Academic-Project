using api.Models;
using Api.Models.Enum;
using Api.Routing;
using Api.XML;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Http;
using uPLibrary.Networking.M2Mqtt;

namespace Api.Controllers
{
    public class DataController : ApiController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Api.Properties.Settings.ConnStr"].ConnectionString;

        #region Gets Methods

        //Get data by module id
        [GetRoute("api/somiod/data/{id:int}")]
        public IEnumerable<Data> GetDataByModule(int id)
        {
            List<Data> dataSets = new List<Data>();
            string sql = "SELECT * FROM Data Where Parent = @ModId ORDER BY Id";
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ModId", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Data data = new Data();
                        data.Id = (int)reader["Id"];
                        data.Creation_dt = (DateTime)reader["Creation_dt"];
                        data.Content = (string)reader["Content"];
                        data.Parent = (int)reader["Parent"];
                        dataSets.Add(data);
                    }
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return dataSets;
        }
        #endregion

        #region CRUD Methods

        //Post data
        public Response Post(HandlerXML handler, int modId)
        {
            //Começo o tratamento de dados
            Data data = new Data();
            data = handler.DealRequestData();
            data.Parent = modId;

            Debug.Print("[DEBUG] 'Data: " + data.Content + " ' | Post() in DataController");

            if (String.IsNullOrEmpty(data.Content))
            {
                return Response.BadRequest_ContentEmpty;
            }

            #region Insiro na DB e no XML e faço o publish no MQTT
            //Verifico se já existe alguma subscrition com o mesmo nome e com o mesmo evento da recebida pelo request
            string sqlPostData = "INSERT INTO Data(Content, Creation_dt, Parent) VALUES (@Content, @Creation_dt, @Parent)";
            string sqlData = "SELECT * FROM Data WHERE Parent = @ParentId AND UPPER(Content) = UPPER(@Content)";

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                #region Select Subscriptions

                string sqlSubscription = "SELECT * FROM Subscription WHERE Parent = @ParentId And (LOWER(Event) = 'creation' Or LOWER(Event) = 'both')";

                SqlCommand cmdSqlGetSubscriptionsOfModule = new SqlCommand(sqlSubscription, conn);
                cmdSqlGetSubscriptionsOfModule.Parameters.AddWithValue("@ParentId", data.Parent);

                SqlDataReader readerSubscriptions = cmdSqlGetSubscriptionsOfModule.ExecuteReader();
                List<Subscription> subscriptions = new List<Subscription>();
                while (readerSubscriptions.Read())
                {
                    Subscription subscription = new Subscription();
                    subscription.Id = (int)readerSubscriptions["Id"];
                    subscription.Name = (string)readerSubscriptions["Name"];
                    subscription.Creation_dt = (DateTime)readerSubscriptions["Creation_dt"];
                    subscription.Parent = (int)readerSubscriptions["Parent"];
                    subscription.Endpoint = (string)readerSubscriptions["Endpoint"];
                    subscription.Event = (string)readerSubscriptions["Event"];
                    subscriptions.Add(subscription);
                }
                readerSubscriptions.Close();
                #endregion

                #region MQTT Publish
                if (subscriptions.Count == 0)
                {
                    return Response.BadRequest_NoSubscriptions;
                }
                else
                {
                    
                    foreach (Subscription subscription in subscriptions)
                    {
                        Thread thread = new Thread(() =>
                        {
                            MqttClient mClient;
                        
                            try
                            {
                                mClient = new MqttClient(subscription.Endpoint);

                                if (mClient.IsConnected)
                                {
                                    Debug.Print("Estou conectado");
                                    mClient.Disconnect();
                                }

                                mClient.Connect(Guid.NewGuid().ToString());



                                mClient.Publish(subscription.Name + "/creation", Encoding.UTF8.GetBytes("'" + data.Content + "'  (" + subscription.Endpoint + " - " + subscription.Name + "/creation) - " + DateTime.Now));

                            }
                            catch (Exception)
                            {
                                Debug.Print("Não foi possivel establecer conexão with: " + subscription.Endpoint);
                            }
                        });
                        thread.Start();
                    }
                }

                #endregion

                #region Post Data e Select Data

                SqlCommand cmdSqlPostData = new SqlCommand(sqlPostData, conn);
                cmdSqlPostData.Parameters.AddWithValue("@Content", data.Content);
                cmdSqlPostData.Parameters.AddWithValue("@Creation_dt", DateTime.Now);
                cmdSqlPostData.Parameters.AddWithValue("@Parent", data.Parent);

                int numRows = cmdSqlPostData.ExecuteNonQuery();
                conn.Close();

                if (numRows > 0)
                {
                    //Vou fazer um select para ir buscar o Id e o creation_dt da data que acabei de inserir
                    conn.Open();
                    SqlCommand cmdGetData = new SqlCommand(sqlData, conn);
                    cmdGetData.Parameters.AddWithValue("@ParentId", modId);
                    cmdGetData.Parameters.AddWithValue("@Content", data.Content);

                    SqlDataReader reader = cmdGetData.ExecuteReader();
                    while (reader.Read())
                    {
                        data.Id = (int)reader["Id"];
                        data.Content = (string)reader["Content"];
                        data.Creation_dt = (DateTime)reader["Creation_dt"];
                        data.Parent = (int)reader["Parent"];
                    }
                    reader.Close();
                    conn.Close();

                    //Adicionar a data ao XML
                    handler.AddData(data);
                    return Response.Ok;
                }
                return Response.InternalServerError;
                #endregion
            }
            catch (Exception e)
            {

                Debug.Print("[DEBUG] 'Exception in Post() in DataController' | " + e.Message);

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return Response.InternalServerError;
            }
            #endregion

        }

        //Delete data
        [Route("api/somiod/data/{id}")]
        public IHttpActionResult Delete(int id)
        {
            HandlerXML handler = new HandlerXML();
            string sqlGetData = "SELECT * FROM Data WHERE Id = @Id";
            string sql = "DELETE FROM Data WHERE Id = @Id";
            string sqlSubscription = "SELECT * FROM Subscription WHERE Parent = @ParentId And (LOWER(Event) = 'deletion' Or LOWER(Event) = 'both')";

            SqlConnection conn = null;
            Data data = new Data();

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                #region Select Data
                SqlCommand cmdGet = new SqlCommand(sqlGetData, conn);
                cmdGet.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmdGet.ExecuteReader();
                while (reader.Read())
                {
                    data.Id = (int)reader["Id"];
                    data.Content = (string)reader["Content"];
                    data.Creation_dt = (DateTime)reader["Creation_dt"];
                    data.Parent = (int)reader["Parent"];
                }

                reader.Close();
                #endregion

                #region Select Subscriptions

                SqlCommand cmdSqlGetSubscriptionsOfModule = new SqlCommand(sqlSubscription, conn);
                cmdSqlGetSubscriptionsOfModule.Parameters.AddWithValue("@ParentId", data.Parent);

                SqlDataReader readerSubscriptions = cmdSqlGetSubscriptionsOfModule.ExecuteReader();
                List<Subscription> subscriptions = new List<Subscription>();
                while (readerSubscriptions.Read())
                {
                    Subscription subscription = new Subscription();
                    subscription.Id = (int)readerSubscriptions["Id"];
                    subscription.Name = (string)readerSubscriptions["Name"];
                    subscription.Creation_dt = (DateTime)readerSubscriptions["Creation_dt"];
                    subscription.Parent = (int)readerSubscriptions["Parent"];
                    subscription.Endpoint = (string)readerSubscriptions["Endpoint"];
                    subscription.Event = (string)readerSubscriptions["Event"];
                    subscriptions.Add(subscription);
                }
                readerSubscriptions.Close();
                #endregion

                #region MQTT Publish
                if (subscriptions.Count == 0)
                {
                    return Content(HttpStatusCode.BadRequest, "This Module has no subscriptions - Firstly add a Subscription, to delelte Data", Configuration.Formatters.XmlFormatter);
                }
                else
                {                 
                    foreach (Subscription subscription in subscriptions)
                    {
                        Thread thread = new Thread(() =>
                        {
                            MqttClient mClient;
                            try
                            {
                                mClient = new MqttClient(subscription.Endpoint);

                                if (mClient.IsConnected)
                                {
                                    Debug.Print("Estou conectado");
                                    mClient.Disconnect();
                                }

                                mClient.Connect(Guid.NewGuid().ToString());

                                mClient.Publish(subscription.Name + "/deletion", Encoding.UTF8.GetBytes("'" + data.Content + "'  (" + subscription.Endpoint + " - " + subscription.Name + "/deletion) - " + DateTime.Now));

                            }
                            catch (Exception)
                            {
                                Debug.Print("Não foi possivel establecer conexão with: " + subscription.Endpoint);
                            }
                        });
                        thread.Start();
                    }
                }

                #endregion

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                int numRows = cmd.ExecuteNonQuery();
                conn.Close();

                if (numRows > 0)
                {
                    handler.DeleteData(data);
                    return Content(HttpStatusCode.OK, "Data delete successfully", Configuration.Formatters.XmlFormatter);
                }
                return Content(HttpStatusCode.BadRequest, "Data does not exist", Configuration.Formatters.XmlFormatter);

            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return InternalServerError();
            }
        }

        #endregion
    }
}
