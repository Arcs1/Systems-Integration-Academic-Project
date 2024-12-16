using api.Models;
using Api.Models.Enum;
using Api.Routing;
using Api.XML;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    public class SubscriptionController : ApiController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Api.Properties.Settings.ConnStr"].ConnectionString;

        #region Get's Methods

        //GetAll all subscriptions
        [GetRoute("api/somiod/subscriptions")]
        public IEnumerable<Subscription> GetAll()
        {
            List<Subscription> subscriptions = new List<Subscription>();
            string sql = "SELECT * FROM Subscription ORDER BY Id";
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Subscription subscription = new Subscription();
                    {
                        subscription.Id = (int)reader["Id"];
                        subscription.Name = (string)reader["Name"];
                        subscription.Creation_dt = (DateTime)reader["Creation_dt"];
                        subscription.Parent = (int)reader["Parent"];
                        subscription.Event = (string)reader["Event"];
                        subscription.Endpoint = (string)reader["Endpoint"];
                    };

                    subscriptions.Add(subscription);
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
            return subscriptions;
        }


        //Get subscriptions by module id
        [GetRoute("api/somiod/subscriptions/{id:int}")]
        public IEnumerable<Subscription> GetSubscriptionsByModule(int id)
        {
            List<Subscription> subscriptions = new List<Subscription>();
            string sql = "SELECT * FROM Subscription Where Parent = @AppId ORDER BY Id";
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AppId", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Subscription subscription = new Subscription();
                        subscription.Id = (int)reader["Id"];
                        subscription.Name = (string)reader["Name"];
                        subscription.Creation_dt = (DateTime)reader["Creation_dt"];
                        subscription.Parent = (int)reader["Parent"];
                        subscription.Event = (string)reader["Event"];
                        subscription.Endpoint = (string)reader["Endpoint"];
                        subscriptions.Add(subscription);
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
            return subscriptions;
        }

        #endregion

        #region CRUD Methods

        //Post a new subscription
        [PostRoute("api/somiod/{appName}/{modName}")]
        public IHttpActionResult Post(string appName, string modName, HttpRequestMessage request)
        {
            #region Check if appName and modName exists in database
            //Verifico se a appName recebida no Link existe na base de dados
            string sqlApplication = "SELECT Id FROM Application WHERE UPPER(Name) = UPPER(@AppName)";
            string sqlModule = "SELECT Id, Name FROM Module WHERE UPPER(Name) = UPPER(@ModName) and Parent = @AppId";

            int appId = -1, modId = -1;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                //Verifico se a appName recebida no Link existe na base de dados
                SqlCommand cmdApp = new SqlCommand(sqlApplication, conn);
                cmdApp.Parameters.AddWithValue("@AppName", appName);

                SqlDataReader readerApp = cmdApp.ExecuteReader();
                while (readerApp.Read())
                {
                    appId = (int)readerApp["Id"];
                }

                readerApp.Close();
                if (appId == -1)
                {
                    conn.Close();
                    Debug.Print("[DEBUG] 'Applications does not exist' | Post() in SubscriptionController");
                    return Content(HttpStatusCode.BadRequest, "Application does not exist", Configuration.Formatters.XmlFormatter);
                }

                //Verico se o modName recebido no Link existe naquela app na base de dados
                SqlCommand cmdMod = new SqlCommand(sqlModule, conn);
                cmdMod.Parameters.AddWithValue("@ModName", modName);
                cmdMod.Parameters.AddWithValue("@AppId", appId);

                SqlDataReader readerMod = cmdMod.ExecuteReader();
                while (readerMod.Read())
                {
                    modId = (int)readerMod["Id"];
                }

                readerMod.Close();
                if (modId == -1)
                {
                    conn.Close();
                    Debug.Print("[DEBUG] 'Module does not exist in this application' | Post() in SubscriptionController");
                    return Content(HttpStatusCode.BadRequest, "Module does not exist in this application", Configuration.Formatters.XmlFormatter);
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Debug.Print("[DEBUG] 'Exception in Post() in SubscriptionController' | " + e.Message);

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return InternalServerError();
            }
            #endregion

            #region Check if XML is valid

            // Verifico o body da requisição
            HandlerXML handler = new HandlerXML();

            //Retiro todos os caracteres e espaços desnecessários
            string rawXml = request.Content.ReadAsStringAsync().Result
                .Replace(System.Environment.NewLine, String.Empty);

            //Verifico se a string que veio do request é XML
            if (!handler.IsValidStringXML(rawXml))
            {
                Debug.Print("[DEBUG] 'String is not XML' | Post() in SubscriptionController");
                return Content(HttpStatusCode.BadRequest, "Request is not XML", Configuration.Formatters.XmlFormatter);
            }

            //Verifica se o ficheiro XML está de acordo o XSD
            if (!handler.IsValidSubscriptionsSchemaXML(rawXml))
            {
                if (handler.IsValidDataSchemaXML(rawXml))
                {
                    DataController dataController = new DataController();
                    Response response = dataController.Post(handler, modId);
                    switch (response)
                    {
                        case Response.Ok:
                            return Content(HttpStatusCode.OK, "Data created successfully", Configuration.Formatters.XmlFormatter);                 

                        case Response.BadRequest_ContentEmpty:
                            return Content(HttpStatusCode.BadRequest, "Content is empty", Configuration.Formatters.XmlFormatter);

                        case Response.BadRequest_ContentAlreadyExists:
                            return Content(HttpStatusCode.BadRequest, "This Data content already exists in this module", Configuration.Formatters.XmlFormatter);

                        case Response.BadRequest_NoSubscriptions:
                            return Content(HttpStatusCode.BadRequest, "This Module has no subscriptions - Firstly add a Subscription, to send Data", Configuration.Formatters.XmlFormatter);

                        case Response.InternalServerError:
                            return InternalServerError();
                    }
                }

                Debug.Print("[DEBUG] 'Invalid Schema in XML' | Post() in SubscriptionController");
                return Content(HttpStatusCode.BadRequest, "Invalid Schema in XML", Configuration.Formatters.XmlFormatter);
            }

            //Começo o tratamento de dados
            Subscription subscription = new Subscription();
            subscription = handler.DealRequestSubscription();
            subscription.Parent = modId;

            Debug.Print("[DEBUG] 'Event: " + subscription.Event + " ' | Post() in SubscriptionController");

            if (!subscription.Event.ToLower().Equals("creation")
                && !subscription.Event.ToLower().Equals("deletion")
                && !subscription.Event.ToLower().Equals("both"))
            {
                Debug.Print("[DEBUG] 'Event is invalid' | Post() in SubscriptionController");
                return Content(HttpStatusCode.BadRequest, "Subscription event is invalid", Configuration.Formatters.XmlFormatter);
            }

            //Aqui inseri-mos o endpoint e o name caso não existam
            if (String.IsNullOrEmpty(subscription.Name))
            {
                subscription.Name = modName;
            }

            if (String.IsNullOrEmpty(subscription.Endpoint))
            {
                subscription.Endpoint = "127.0.0.1";
            }

            #endregion

            #region Verifico se já existe o nome da subscription com o mesmo evento naquele Module e Insiro na DB e no XML
            //Verifico se já existe alguma subscrition com o mesmo nome e com o mesmo evento da recebida pelo request
            string sqlVerifySubscriptionInModule = "SELECT COUNT(*) FROM Subscription WHERE Parent = @ParentId AND UPPER(Name) = UPPER(@Name) AND (UPPER(Event) = UPPER(@Event) OR UPPER(Event) = UPPER(@Both))";
            string sqlPostSubscription = "INSERT INTO Subscription(Name, Creation_dt, Parent, Event, Endpoint) VALUES (@Name, @Creation_dt, @Parent, @Event, @Endpoint)";
            string sqlSubscription = "SELECT * FROM Subscription WHERE Parent = @ParentId AND UPPER(Name) = UPPER(@Name) AND UPPER(Event) = UPPER(@Event)";

            try
            {
                conn.Open();
                SqlCommand cmdSubscriptionName = new SqlCommand(sqlVerifySubscriptionInModule, conn);
                cmdSubscriptionName.Parameters.AddWithValue("@ParentId", modId);
                cmdSubscriptionName.Parameters.AddWithValue("@Name", subscription.Name);
                cmdSubscriptionName.Parameters.AddWithValue("@Event", subscription.Event);
                cmdSubscriptionName.Parameters.AddWithValue("@Both", "both");

                if ((int)cmdSubscriptionName.ExecuteScalar() > 0)
                {
                    conn.Close();
                    Debug.Print("[DEBUG] 'This subscription name already exists in this module with the same event (" + subscription.Event + ")' | Post() in SubscriptionController");
                    return Content(HttpStatusCode.BadRequest, "This subscription name already exists in this module with the same event or exists with event 'both'", Configuration.Formatters.XmlFormatter);
                }

                SqlCommand cmdPost = new SqlCommand(sqlPostSubscription, conn);
                cmdPost.Parameters.AddWithValue("@Name", subscription.Name);
                cmdPost.Parameters.AddWithValue("@Creation_dt", DateTime.Now);
                cmdPost.Parameters.AddWithValue("@Parent", subscription.Parent);
                cmdPost.Parameters.AddWithValue("@Event", subscription.Event);
                cmdPost.Parameters.AddWithValue("@Endpoint", subscription.Endpoint);

                int numRows = cmdPost.ExecuteNonQuery();

                conn.Close();

                if (numRows > 0)
                {
                    //Vou fazer um select para ir buscar o Id e o creation_dt da subscription que acabei de inserir
                    conn.Open();
                    SqlCommand cmdGetSubscription = new SqlCommand(sqlSubscription, conn);
                    cmdGetSubscription.Parameters.AddWithValue("@ParentId", modId);
                    cmdGetSubscription.Parameters.AddWithValue("@Name", subscription.Name);
                    cmdGetSubscription.Parameters.AddWithValue("@Event", subscription.Event);

                    SqlDataReader reader = cmdGetSubscription.ExecuteReader();
                    while (reader.Read())
                    {
                        subscription.Id = (int)reader["Id"];
                        subscription.Name = (string)reader["Name"];
                        subscription.Creation_dt = (DateTime)reader["Creation_dt"];
                        subscription.Parent = (int)reader["Parent"];
                        subscription.Event = (string)reader["Event"];
                        subscription.Endpoint = (string)reader["Endpoint"];
                    }
                    reader.Close();
                    conn.Close();

                    handler.AddSubscription(subscription);
                    return Content(HttpStatusCode.OK, "Subscription added successfully", Configuration.Formatters.XmlFormatter);
                }
                return InternalServerError();
            }
            catch (Exception e)
            {
                Debug.Print("[DEBUG] 'Exception in Post() in SubscriptionController' | " + e.Message);

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return InternalServerError();
            }
            #endregion
        }

        //Delete method
        [Route("api/somiod/subscriptions/{id}")]
        public IHttpActionResult Delete(int id)
        {
            HandlerXML handler = new HandlerXML();
            string sqlGetSubscription = "SELECT * FROM Subscription WHERE Id = @Id";
            string sql = "DELETE FROM Subscription WHERE Id = @Id";
            SqlConnection conn = null;
            Subscription subscription = new Subscription();

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmdGet = new SqlCommand(sqlGetSubscription, conn);
                cmdGet.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmdGet.ExecuteReader();
                while (reader.Read())
                {
                    subscription.Id = (int)reader["Id"];
                    subscription.Name = (string)reader["Name"];
                    subscription.Parent = (int)reader["Parent"];
                    subscription.Creation_dt = (DateTime)reader["Creation_dt"];
                    subscription.Event = (string)reader["Event"];
                    subscription.Endpoint = (string)reader["Endpoint"];
                }
                reader.Close();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                int numRows = cmd.ExecuteNonQuery();
                conn.Close();

                if (numRows > 0)
                {
                    handler.DeleteSubscription(subscription);
                    return Content(HttpStatusCode.OK, "Subscription delete successfully", Configuration.Formatters.XmlFormatter);
                }
                return Content(HttpStatusCode.BadRequest, "Subscription does not exist", Configuration.Formatters.XmlFormatter);
            }
            catch (Exception e)
            {
                Debug.Print("[DEBUG] 'Exception in Delete() in SubscriptionController' | " + e.Message);
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
