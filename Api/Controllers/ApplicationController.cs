using api.Models;
using Api.Routing;
using Api.XML;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Application = api.Models.Application;

namespace Api.Controllers
{
    public class ApplicationController : ApiController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Api.Properties.Settings.ConnStr"].ConnectionString;

        #region Get's Methods

        //Get all applications    
        [GetRoute("api/somiod/applications")]
        public IEnumerable<Application> GetAll()
        {
            List<Application> applications = new List<Application>();
            string sql = "SELECT * FROM Application ORDER BY Id";
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    //applications = new List<Application>();
                    while (reader.Read())
                    {
                        Application application = new Application();
                        application.Id = (int)reader["Id"];
                        application.Name = (string)reader["Name"];
                        application.Creation_dt = (DateTime)reader["Creation_dt"];
                        applications.Add(application);
                    }
                }

                reader.Close();
                conn.Close();

                return applications;

            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return applications;
        }


        //Get application by id
        [GetRoute("api/somiod/applications/{id:int}")]
        public IHttpActionResult GetApplicationById(int id)
        {
            Application application = null;
            string sql = "SELECT * FROM Application WHERE Id = @Id";
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    application = new Application();
                    {
                        application.Id = (int)reader["Id"];
                        application.Name = (string)reader["Name"];
                        application.Creation_dt = (DateTime)reader["Creation_dt"];
                    };
                }

                reader.Close();
                conn.Close();

                if (application == null)
                {
                    return NotFound();
                }
                return Ok(application);
            }
            catch (Exception)
            {
                //fechar a ligação à BD
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return NotFound();
            }
        }

        //Get application id by name
        [GetRoute("api/somiod/applications/{name}")]
        public IHttpActionResult GetApplicationByName(string name)
        {
            Application application = null;
            string sql = "SELECT * FROM Application WHERE UPPER(Name) = UPPER(@Name)";
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        application = new Application();
                        {
                            application.Id = (int)reader["Id"];
                            application.Name = (string)reader["Name"];
                            application.Creation_dt = (DateTime)reader["Creation_dt"];
                        };
                    }
                }
                reader.Close();
                conn.Close();

                if (application == null)
                {
                    return NotFound();
                }
                return Ok(application);
            }
            catch (Exception)
            {
                //fechar a ligação à BD
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return NotFound();
            }
        }

        //Get modules by application id
        [GetRoute("api/somiod/applications/{id:int}/modules")]
        public IEnumerable<Module> GetModulesByApplication(int id)
        {
            List<Module> modules = new List<Module>();
            string sql = "SELECT * FROM Module Where Parent = @AppId ORDER BY Id";
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
                    //modules = new List<Module>();
                    while (reader.Read())
                    {
                        Module module = new Module();
                        module.Id = (int)reader["Id"];
                        module.Name = (string)reader["Name"];
                        module.Parent = (int)reader["Parent"];
                        module.Creation_dt = (DateTime)reader["Creation_dt"];
                        modules.Add(module);
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
            return modules;
        }

        //Get module by application id
        [GetRoute("api/somiod/applications/{id:int}/modules/{moduleName}")]
        public IHttpActionResult GetModule(int id, string moduleName)
        {
            Module module = null;
            string sql = "SELECT * FROM Module Where Parent = @AppId and UPPER(Name) = UPPER(@ModuleName) ORDER BY Id";
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AppId", id);
                cmd.Parameters.AddWithValue("@ModuleName", moduleName);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    module = new Module();
                    module.Id = (int)reader["Id"];
                    module.Name = (string)reader["Name"];
                    module.Parent = (int)reader["Parent"];
                    module.Creation_dt = (DateTime)reader["Creation_dt"];
                }

                reader.Close();
                conn.Close();

                if (module == null)
                {
                    return NotFound();
                }
                return Ok(module);
            }
            catch (Exception)
            {
                //fechar a ligação à BD
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return NotFound();
            }
        }

        #endregion

        #region CRUD Operations

        //Post method
        [PostRoute("api/somiod")]
        [HttpPost]
        public IHttpActionResult Post(HttpRequestMessage request)
        {
            HandlerXML handler = new HandlerXML();

            //Retiro todos os caracteres e espaços desnecessários
            string rawXml = request.Content.ReadAsStringAsync().Result
                .Replace(System.Environment.NewLine, String.Empty);

            //Verifico se a string que veio do request é XML
            if (!handler.IsValidStringXML(rawXml))
            {
                Debug.Print("[DEBUG] 'String is not XML' | Post() in ApplicationController");
                return Content(HttpStatusCode.BadRequest, "Request is not XML", Configuration.Formatters.XmlFormatter);
            }

            //Verifica se o ficheiro XML está de acordo o XSD
            if (!handler.IsValidApplicationSchemaXML(rawXml))
            {
                Debug.Print("[DEBUG] 'Invalid Schema in XML' | Post() in ApplicationController");
                return Content(HttpStatusCode.BadRequest, "Invalid Schema in XML", Configuration.Formatters.XmlFormatter);
            }

            //Começo o tratamento de dados
            Application application = new Application();

            application.Name = handler.DealRequestApplication();

            if (String.IsNullOrEmpty(application.Name))
            {
                return Content(HttpStatusCode.BadRequest, "Name is Empty", Configuration.Formatters.XmlFormatter);
            }

            //Verifico se o nome da aplicação já existe
            string sqlVerifyApplication = "SELECT COUNT(*) FROM Application WHERE UPPER(Name) = UPPER(@Name)";
            string sqlGetApplication = "SELECT * FROM Application WHERE UPPER(Name) = UPPER(@Name)";
            string sqlPostApplication = "INSERT INTO Application (Name, Creation_dt) VALUES (@Name, @Creation_dt)";

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmdExists = new SqlCommand(sqlVerifyApplication, conn);
                cmdExists.Parameters.AddWithValue("@Name", application.Name);

                if ((int)cmdExists.ExecuteScalar() > 0)
                {
                    conn.Close();
                    return Content(HttpStatusCode.BadRequest, "Application name already exists", Configuration.Formatters.XmlFormatter);
                }

                SqlCommand cmdPost = new SqlCommand(sqlPostApplication, conn);
                cmdPost.Parameters.AddWithValue("@Name", application.Name);
                cmdPost.Parameters.AddWithValue("@Creation_dt", DateTime.Now);

                int numRows = cmdPost.ExecuteNonQuery();

                if (numRows > 0)
                {
                    //Vou fazer um select para ir buscar o id o creation_dt
                    SqlCommand cmdGet = new SqlCommand(sqlGetApplication, conn);
                    cmdGet.Parameters.AddWithValue("@Name", application.Name);

                    SqlDataReader reader = cmdGet.ExecuteReader();
                    while (reader.Read())
                    {
                        application.Id = (int)reader["Id"];
                        application.Name = (string)reader["Name"];
                        application.Creation_dt = (DateTime)reader["Creation_dt"];
                    }

                    reader.Close();
                    conn.Close();

                    handler.AddApplication(application);
                    return Content(HttpStatusCode.OK, "Application created successfully", Configuration.Formatters.XmlFormatter);
                }
                return InternalServerError();
            }
            catch (Exception e)
            {
                Debug.Print("[DEBUG] 'Exception in Post() in ApplicationController' | " + e.Message);

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return InternalServerError();
            }
        }

        //Put method
        [Route("api/somiod/applications/{id}")]
        public IHttpActionResult Put(int id, HttpRequestMessage request)
        {

            HandlerXML handler = new HandlerXML();

            //Retiro todos os caracteres e espaços desnecessários
            string rawXml = request.Content.ReadAsStringAsync().Result
                .Replace(System.Environment.NewLine, String.Empty);

            //Verifico se a string que veio do request é XML
            if (!handler.IsValidStringXML(rawXml))
            {
                Debug.Print("[DEBUG] 'String is not XML' | Put() in ApplicationController");
                return Content(HttpStatusCode.BadRequest, "Request is not XML", Configuration.Formatters.XmlFormatter);
            }

            //Verifica se o ficheiro XML está de acordo o XSD
            if (!handler.IsValidApplicationSchemaXML(rawXml))
            {
                Debug.Print("[DEBUG] 'Invalid Schema in XML' | Put() in ApplicationController");
                return Content(HttpStatusCode.BadRequest, "Invalid Schema in XML", Configuration.Formatters.XmlFormatter);
            }

            //Começo o tratamento de dados
            Application application = new Application();

            application.Name = handler.DealRequestApplication();

            if (String.IsNullOrEmpty(application.Name))
            {
                return Content(HttpStatusCode.BadRequest, "Name is Empty", Configuration.Formatters.XmlFormatter);
            }

            //Verifico se o nome da aplicação já existe
            string sqlVerifyApplication = "SELECT COUNT(*) FROM Application WHERE UPPER(Name) = UPPER(@Name)";
            string sqlGetApplication = "SELECT * FROM Application WHERE UPPER(Name) = UPPER(@Name)";
            string sql = "UPDATE Application SET Name = @Name WHERE Id = @Id";

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmdExists = new SqlCommand(sqlVerifyApplication, conn);
                cmdExists.Parameters.AddWithValue("@Name", application.Name);

                if ((int)cmdExists.ExecuteScalar() > 0)
                {
                    conn.Close();
                    return Content(HttpStatusCode.BadRequest, "Application name already exists", Configuration.Formatters.XmlFormatter);
                }

                SqlCommand cmdPut = new SqlCommand(sql, conn);
                cmdPut.Parameters.AddWithValue("@Name", application.Name);
                cmdPut.Parameters.AddWithValue("@Id", id);
                int numRows = cmdPut.ExecuteNonQuery();

                if (numRows > 0)
                {
                    //Vou fazer um select para ir buscar o id o creation_dt
                    SqlCommand cmdGet = new SqlCommand(sqlGetApplication, conn);
                    cmdGet.Parameters.AddWithValue("@Name", application.Name);

                    SqlDataReader reader = cmdGet.ExecuteReader();
                    while (reader.Read())
                    {
                        application.Id = (int)reader["Id"];
                        application.Name = (string)reader["Name"];
                        application.Creation_dt = (DateTime)reader["Creation_dt"];
                    }
                    reader.Close();
                    conn.Close();

                    handler.UpdateApplication(application);
                    return Content(HttpStatusCode.OK, "Application update successfully", Configuration.Formatters.XmlFormatter);
                }
                return Content(HttpStatusCode.BadRequest, "Application does not exist", Configuration.Formatters.XmlFormatter);
            }
            catch (Exception e)
            {
                Debug.Print("[DEBUG] 'Exception in Put() in ApplicationController' | " + e.Message);
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return InternalServerError();
            }
        }

        //Delete method
        [Route("api/somiod/applications/{id}")]
        public IHttpActionResult Delete(int id)
        {

            HandlerXML handler = new HandlerXML();

            string sqlGetApplication = "SELECT * FROM Application WHERE Id = @Id";
            string sqlSelectMod = "SELECT id FROM Module WHERE Parent = @Parent";

            string sqlDeleteApp = "DELETE FROM Application WHERE Id = @Id";
            string sqlDeleteMod = "DELETE FROM Module WHERE Id = @Id";
            string sqlDeleteSub = "DELETE FROM Subscription WHERE Parent = @Parent";
            string sqlDeleteData = "DELETE FROM Data  WHERE Parent = @Parent";
            SqlConnection conn = null;
            Application application = new Application();

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmdGet = new SqlCommand(sqlGetApplication, conn);
                cmdGet.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmdGet.ExecuteReader();
                while (reader.Read())
                {
                    application.Id = (int)reader["Id"];
                    application.Name = (string)reader["Name"];
                    application.Creation_dt = (DateTime)reader["Creation_dt"];
                }

                reader.Close();


                SqlCommand cmdIdMods = new SqlCommand(sqlSelectMod, conn);
                cmdIdMods.Parameters.AddWithValue("@Parent", id);
                SqlDataReader readerMod = cmdIdMods.ExecuteReader();
                List<int> ModIds = new List<int>();
                List<int> SubIds = new List<int>();
                List<int> DataIds = new List<int>();
                while (readerMod.Read())
                {
                    ModIds.Add((int)readerMod["Id"]);
                }
                readerMod.Close();

                foreach (int idMod in ModIds)
                {
                    SqlCommand cmdDeleteMod = new SqlCommand(sqlDeleteMod, conn);
                    cmdDeleteMod.Parameters.AddWithValue("@Id", idMod);
                    cmdDeleteMod.ExecuteNonQuery();

                    //Executar query Delete Subscription
                    SqlCommand cmdDeleteSub = new SqlCommand(sqlDeleteSub, conn);
                    cmdDeleteSub.Parameters.AddWithValue("@Parent", idMod);
                    cmdDeleteSub.ExecuteNonQuery();

                    //Executar query Delete Module
                    SqlCommand cmdDeleteData = new SqlCommand(sqlDeleteData, conn);
                    cmdDeleteData.Parameters.AddWithValue("@Parent", idMod);
                    cmdDeleteData.ExecuteNonQuery();
                }


                SqlCommand cmd = new SqlCommand(sqlDeleteApp, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                int numRows = cmd.ExecuteNonQuery();
                if (numRows > 0)
                {
                    conn.Close();
                    handler.DeleteApplication(application);
                    return Content(HttpStatusCode.OK, "Application delete successfully", Configuration.Formatters.XmlFormatter);
                }
                return Content(HttpStatusCode.BadRequest, "Application does not exist", Configuration.Formatters.XmlFormatter);
            }
            catch (Exception e)
            {
                Debug.Print("[DEBUG] 'Exception in Delete() in ApplicationController' | " + e.Message);

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
