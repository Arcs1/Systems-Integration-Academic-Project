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
using System.Web.Services.Description;
using Application = api.Models.Application;

namespace Api.Controllers
{
    public class ModuleController : ApiController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Api.Properties.Settings.ConnStr"].ConnectionString;

        #region Get's Methods

        //GetAll all modules
        [GetRoute("api/somiod/modules")]
        public IEnumerable<Module> GetAll()
        {
            List<Module> modules = new List<Module>();
            string sql = "SELECT * FROM Module ORDER BY Id";
            SqlConnection conn = null;
        
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Module module = new Module();
                    {
                        module.Id = (int)reader["Id"];
                        module.Name = (string)reader["Name"];
                        module.Creation_dt = (DateTime)reader["Creation_dt"];
                        module.Parent = (int)reader["Parent"];
                    };

                    modules.Add(module);
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


        [GetRoute("api/somiod/modules/resources")]
        public IEnumerable<Object> GetAllResources()
        {
            string sqlGetApps = "SELECT * FROM Application";
            string sqlGetApp = "SELECT * FROM Application Where Id = @Id";
            string sqlGetMods = "SELECT * FROM Module WHERE Parent = @Parent";
            string sqlGetSub = "SELECT * FROM Subscription WHERE Parent = @Parent";
            string sqlGetData = "SELECT * FROM Data WHERE Parent = @Parent";
            SqlConnection conn = null;

            List<Object> allModules = new List<Object>();

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                //applications
                SqlCommand cmd = new SqlCommand(sqlGetApps, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                List<Application> applications= new List<Application>();

                
                if (reader.HasRows)
                {
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



                List<Module> modules = new List<Module>();

                foreach (Application app in applications)
                {
                    //obter modulos da applicação
                    SqlCommand cmdMods = new SqlCommand(sqlGetMods, conn);
                    cmdMods.Parameters.AddWithValue("@Parent", app.Id);
                    SqlDataReader readerMods = cmdMods.ExecuteReader();
                    if (readerMods.HasRows)
                    {
                        while (readerMods.Read())
                        {
                            Module module = new Module();
                            module.Id = (int)readerMods["Id"];
                            module.Name = (string)readerMods["Name"];
                            module.Creation_dt = (DateTime)readerMods["Creation_dt"];
                            module.Parent = (int)readerMods["Parent"];
                            modules.Add(module);

                        }
                        readerMods.Close();
                    }
                }

              
                    foreach (Module module in modules)
                    {
                        //obter subscrições atribuidas a cada modulo
                        SqlCommand cmdSubs = new SqlCommand(sqlGetSub, conn);
                        cmdSubs.Parameters.AddWithValue("@Parent", module.Id);
                        SqlDataReader readerSubs = cmdSubs.ExecuteReader();
                        List<Object> aux2 = new List<Object>();
                        if (readerSubs.HasRows)
                        {
                            while (readerSubs.Read())
                            {
                                Subscription subscription = new Subscription();
                                subscription.Id = (int)readerSubs["Id"];
                                subscription.Name = (string)readerSubs["Name"];
                                subscription.Creation_dt = (DateTime)readerSubs["Creation_dt"];
                                subscription.Parent = (int)readerSubs["Parent"];
                                subscription.Event = (string)readerSubs["Event"];
                                subscription.Endpoint = (string)readerSubs["Endpoint"];
                                aux2.Add(subscription);

                            }
                        }
                        readerSubs.Close();


                        //obter data atribuidas a cada modulo
                        SqlCommand cmdData = new SqlCommand(sqlGetData, conn);
                        cmdData.Parameters.AddWithValue("@Parent", module.Id);
                        SqlDataReader readerData = cmdData.ExecuteReader();
                        List<Object> aux3 = new List<Object>();
                        if (readerData.HasRows)
                        {
                            allModules.Add("Data");
                            while (readerData.Read())
                            {
                                Data data = new Data();
                                data.Id = (int)readerData["Id"];
                                data.Content = (string)readerData["Content"];
                                data.Creation_dt = (DateTime)readerData["Creation_dt"];
                                data.Parent = (int)readerData["Parent"];
                                aux3.Add(data);

                            }
                        }
                        readerData.Close();

                        SqlCommand cmdApp = new SqlCommand(sqlGetApp, conn);
                        cmdApp.Parameters.AddWithValue("@Id", module.Parent);
                        SqlDataReader readerapp = cmdApp.ExecuteReader();

                         Application application = new Application();
                        if (readerapp.HasRows)
                        {
                            while (readerapp.Read())
                            {
                                application.Id = (int)readerapp["Id"];
                                application.Name = (string)readerapp["Name"];
                                application.Creation_dt = (DateTime)readerapp["Creation_dt"];
                            }
                        }
                        readerapp.Close();

                    Object aux = new
                        {
                            module.Id,
                            module.Name,
                            Parent = application,
                            Subscriptions = aux2,
                            Data = aux3
                        };
                        allModules.Add("Module");
                        allModules.Add(aux);
                    }
                conn.Close();

                return allModules;
            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return allModules;
        }

        //GetAll a module by id
        [GetRoute("api/somiod/modules/{id:int}")]
        public IHttpActionResult GetModuleById(int id)
        {
            Module module = null;
            string sql = "SELECT * FROM Module WHERE Id = @Id";
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    module = new Module();
                    while (reader.Read())
                    {
                        module.Id = (int)reader["Id"];
                        module.Name = (string)reader["Name"];
                        module.Creation_dt = (DateTime)reader["Creation_dt"];
                        module.Parent = (int)reader["Parent"];
                    }
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

        //GetAll by name
        [GetRoute("api/somiod/modules/{name}")]
        public IHttpActionResult GetModuleByName(string name)
        {
            Module module = new Module();
            string sql = "SELECT * FROM Module WHERE UPPER(Name) = UPPER(@Name)";
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
                        module.Id = (int)reader["Id"];
                        module.Name = (string)reader["Name"];
                        module.Creation_dt = (DateTime)reader["Creation_dt"];
                        module.Parent = (int)reader["Parent"];
                    }
                }

                reader.Close();
                conn.Close();

                if (module.Name == null)
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

        //Get Subscriptions by module id
        [GetRoute("api/somiod/modules/{id:int}/subscriptions")]
        public IEnumerable<Subscription> GetSubscriptionsByModule(int id)
        {
            List<Subscription> subscriptions = new List<Subscription>();
            string sql = "SELECT * FROM Subscription Where Parent = @ModId ORDER BY Id";
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
                    //modules = new List<Module>();
                    while (reader.Read())
                    {
                        Subscription subscription = new Subscription();
                        subscription.Id = (int)reader["Id"];
                        subscription.Name = (string)reader["Name"];
                        subscription.Parent = (int)reader["Parent"];
                        subscription.Creation_dt = (DateTime)reader["Creation_dt"];
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

        //Get subscription with name by module id
        [GetRoute("api/somiod/modules/{id:int}/subscriptions/{subscriptionName}")]
        public IHttpActionResult GetSubscriptionByNameOfModule(int id, string subscriptionName)
        {
            Subscription subscription = null;
            string sql = "SELECT * FROM Subscription Where Parent = @moduleId and UPPER(Name) = UPPER(@SubsName)";
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@moduleId", id);
                cmd.Parameters.AddWithValue("@SubsName", subscriptionName);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    subscription = new Subscription();
                    subscription.Id = (int)reader["Id"];
                    subscription.Name = (string)reader["Name"];
                    subscription.Parent = (int)reader["Parent"];
                    subscription.Creation_dt = (DateTime)reader["Creation_dt"];
                    subscription.Event = (string)reader["Event"];
                    subscription.Endpoint = (string)reader["Endpoint"];
                }

                reader.Close();
                conn.Close();

                if (subscription == null)
                {
                    return NotFound();
                }
                return Ok(subscription);
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


        //GetAll applications by module

        [GetRoute("api/somiod/modules/applications")]
        public IEnumerable<Application> GetAllApplicationsByModule()
        {
            List<Application> applications = new List<Application>();

            string sql = "Select * From Application Join Module on Application.Id = Module.Parent Where Module.Name LIKE '%_command';";

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    Application application = new Application();
                    {
                        application.Id = (int)reader["Id"];
                        application.Name = (string)reader["Name"];
                        application.Creation_dt = (DateTime)reader["Creation_dt"];
                    };

                    applications.Add(application);
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
            return applications;
        }

        //GetAll applications no module

        [GetRoute("api/somiod/modules/no/applications")]
        public IEnumerable<Application> GetAllApplicationsNoModule()
        {
            List<Application> applications = new List<Application>();

            string sql = "Select * From Application WHERE id NOT IN ( Select Application.Id  From Application  Left Join Module on Application.Id = Module.Parent  Where Module.Name LIKE '%_command');";

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Application application = new Application();
                    {
                        application.Id = (int)reader["Id"];
                        application.Name = (string)reader["Name"];
                        application.Creation_dt = (DateTime)reader["Creation_dt"];
                    };

                    applications.Add(application);
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
            return applications;
        }

        [GetRoute("api/somiod/modules/noCommands")]
        public IEnumerable<Module> GetAllmodulesNoCommands()
        {
            List<Module> modules = new List<Module>();

            string sql = "Select * From Module WHERE Name NOT LIKE '%_command';";

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Module module = new Module();
                    {
                        module.Id = (int)reader["Id"];
                        module.Name = (string)reader["Name"];
                        module.Creation_dt = (DateTime)reader["Creation_dt"];
                        module.Parent = (int)reader["Parent"];
                    };

                    modules.Add(module);
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

        [GetRoute("api/somiod/modules/noCommand")]
        public IEnumerable<Module> GetmodulesNoCommand()
        {
            List<Module> modules = new List<Module>();

            string sql = "Select * From Module WHERE Name NOT LIKE '%_command' AND Parent IN (Select Application.id From Application Left Join Module on Application.id = Module.Parent WHERE Module.Name LIKE '%_command' );";

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Module module = new Module();
                    {
                        module.Id = (int)reader["Id"];
                        module.Name = (string)reader["Name"];
                        module.Creation_dt = (DateTime)reader["Creation_dt"];
                        module.Parent = (int)reader["Parent"];
                    };

                    modules.Add(module);
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

        [GetRoute("api/somiod/modules/noCommand/{idApp}")]
        public IEnumerable<Module> GetmodulesNoCommandById(int idApp)
        {
            List<Module> modules = new List<Module>();

            string sql = "Select * From Module WHERE Name NOT LIKE '%_command' AND Parent = @idApp;";

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idApp", idApp);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Module module = new Module();
                    {
                        module.Id = (int)reader["Id"];
                        module.Name = (string)reader["Name"];
                        module.Creation_dt = (DateTime)reader["Creation_dt"];
                        module.Parent = (int)reader["Parent"];
                    };

                    modules.Add(module);
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

        #endregion

        #region CRUD Operations

        //Post a new module
        [PostRoute("api/somiod/{appName}")]
        public IHttpActionResult Post(string appName, HttpRequestMessage request)
        {
            #region Check if appName exists in database
            //Verifico se a appName recebida no Link existe na base de dados
            string sqlApplication = "SELECT Id FROM Application WHERE UPPER(Name) = UPPER(@AppName)";
            int parentId = -1;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlApplication, conn);
                cmd.Parameters.AddWithValue("@AppName", appName);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    parentId = (int)reader["Id"];                   
                }

                reader.Close();
                conn.Close();

                if (parentId == -1)
                {
                    conn.Close();
                    Debug.Print("[DEBUG] 'Applications does not exist' | Post() in ModuleController");
                    return Content(HttpStatusCode.BadRequest, "Parent application does not exist", Configuration.Formatters.XmlFormatter);
                }
            }
            catch (Exception e)
            {
                Debug.Print("[DEBUG] 'Exception in Post() in ModuleController' | " + e.Message);

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
                Debug.Print("[DEBUG] 'String is not XML' | Post() in ModuleController");
                return Content(HttpStatusCode.BadRequest, "Request is not XML", Configuration.Formatters.XmlFormatter);
            }

            //Verifica se o ficheiro XML está de acordo o XSD
            if (!handler.IsValidModulesSchemaXML(rawXml))
            {
                Debug.Print("[DEBUG] 'Invalid Schema in XML' | Post() in ModuleController");
                return Content(HttpStatusCode.BadRequest, "Invalid Schema in XML", Configuration.Formatters.XmlFormatter);
            }

            //Começo o tratamento de dados
            Module module = new Module();
            module.Name = handler.DealRequestModule();
            module.Parent = parentId;

            if (String.IsNullOrEmpty(module.Name))
            {
                return Content(HttpStatusCode.BadRequest, "Name is Empty", Configuration.Formatters.XmlFormatter);
            }
            #endregion

            #region Verifico se já existe o nome do module naquela App e Insiro na DB e no XML
            //Verifico se já existe um module com o mesmo nome naquele ParentId
            string sqlVerifyModuleInApp = "SELECT COUNT(*) FROM Module WHERE Parent = @ParentId AND UPPER(Name) = UPPER(@Name)";
            string sqlPostModule = "INSERT INTO Module(Name, Creation_dt, Parent) VALUES (@Name, @Creation_dt, @Parent)";
            string sqlModule = "SELECT * FROM Module WHERE Parent = @ParentId AND UPPER(Name) = UPPER(@Name)";


            try
            {
                conn.Open();
                SqlCommand cmdModuleName = new SqlCommand(sqlVerifyModuleInApp, conn);
                cmdModuleName.Parameters.AddWithValue("@ParentId", module.Parent);
                cmdModuleName.Parameters.AddWithValue("@Name", module.Name);

                if ((int)cmdModuleName.ExecuteScalar() > 0)
                {
                    conn.Close();
                    Debug.Print("[DEBUG] 'This module name already exists in this application' | Post() in ModuleController");
                    return Content(HttpStatusCode.BadRequest, "This module name already exists in this application", Configuration.Formatters.XmlFormatter);
                }
                

                SqlCommand cmdPost = new SqlCommand(sqlPostModule, conn);
                cmdPost.Parameters.AddWithValue("@Name", module.Name);
                cmdPost.Parameters.AddWithValue("@Creation_dt", DateTime.Now);
                cmdPost.Parameters.AddWithValue("@Parent", module.Parent);
                
                int numRows = cmdPost.ExecuteNonQuery();

                conn.Close();

                if (numRows > 0)
                {
                    //Vou fazer um select para ir buscar o Id e o creation_dt do module que acabei de inserir
                    conn.Open();
                    SqlCommand cmdGetModule = new SqlCommand(sqlModule, conn);
                    cmdGetModule.Parameters.AddWithValue("@ParentId", module.Parent);
                    cmdGetModule.Parameters.AddWithValue("@Name", module.Name);

                    SqlDataReader reader = cmdGetModule.ExecuteReader();
                    while (reader.Read())
                    {
                        module.Id = (int)reader["Id"];
                        module.Name = (string)reader["Name"];
                        module.Creation_dt = (DateTime)reader["Creation_dt"];
                        module.Parent = (int)reader["Parent"];
                    }
                    reader.Close();
                    conn.Close();

                    handler.AddModule(module);
                    return Content(HttpStatusCode.OK, "Module created successfully", Configuration.Formatters.XmlFormatter);
                }
                return InternalServerError();
            }
            catch (Exception e)
            {
                Debug.Print("[DEBUG] 'Exception in Post() in ModuleController' | " + e.Message);

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return InternalServerError();
            }
            #endregion
        }

        //Update a module
        [Route("api/somiod/modules/{id}")]
        public IHttpActionResult Put(int id, HttpRequestMessage request)
        {

            HandlerXML handler = new HandlerXML();

            //Retiro todos os caracteres e espaços desnecessários
            string rawXml = request.Content.ReadAsStringAsync().Result
                .Replace(System.Environment.NewLine, String.Empty);

            //Verifico se a string que veio do request é XML
            if (!handler.IsValidStringXML(rawXml))
            {
                Debug.Print("[DEBUG] 'String is not XML' | Put() in ModuleController");
                return Content(HttpStatusCode.BadRequest, "Request is not XML", Configuration.Formatters.XmlFormatter);
            }

            //Verifica se o ficheiro XML está de acordo o XSD
            if (!handler.IsValidModulesSchemaXML(rawXml))
            {
                Debug.Print("[DEBUG] 'Invalid Schema in XML' | Put() in ModuleController");
                return Content(HttpStatusCode.BadRequest, "Invalid Schema in XML", Configuration.Formatters.XmlFormatter);
            }

            //Começo o tratamento de dados
            Module moduleNew = new Module();
            Module module = new Module();

            moduleNew.Name = handler.DealRequestModule();

            if (String.IsNullOrEmpty(moduleNew.Name))
            {
                Debug.Print("[DEBUG] 'Name is Empty' | Put() in ModuleController");
                return Content(HttpStatusCode.BadRequest, "Name is Empty", Configuration.Formatters.XmlFormatter);
            }

            // Comandos SQL
            string sqlGetModule = "SELECT * FROM Module WHERE Id = @Id";
            string sqlVerifyModuleInApp = "SELECT COUNT(*) FROM Module WHERE Parent = @ParentId AND UPPER(Name) = UPPER(@Name)";

            string sql = "UPDATE Module SET Name = @Name WHERE Id = @Id";
            
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                // Verificar o module existe
                SqlCommand cmdGet = new SqlCommand(sqlGetModule, conn);
                cmdGet.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmdGet.ExecuteReader();
                if (!reader.HasRows)
                {

                    reader.Close();
                    conn.Close();
                 
                    Debug.Print("[DEBUG] 'Module not found' | Put() in ModuleController");
                    return NotFound();

                }

                // Ler os dados do reader
                while (reader.Read())
                {
                    module.Id = (int)reader["Id"];
                    module.Name = (string)reader["Name"];
                    module.Creation_dt = (DateTime)reader["Creation_dt"];
                    module.Parent = (int)reader["Parent"];
                }
                
                reader.Close();

                // Verificar se existe um module na aplicação atual com o novo nome
                SqlCommand cmdVerifyModuleInApp = new SqlCommand(sqlVerifyModuleInApp, conn);
                cmdVerifyModuleInApp.Parameters.AddWithValue("@ParentId", module.Parent);
                cmdVerifyModuleInApp.Parameters.AddWithValue("@Name", moduleNew.Name);
                if ((int)cmdVerifyModuleInApp.ExecuteScalar() > 0)
                {
                    conn.Close();
                    Debug.Print("[DEBUG] 'This module name already exists in this application' | Put() in ModuleController");
                    return Content(HttpStatusCode.BadRequest, "This module name already exists in this application", Configuration.Formatters.XmlFormatter);
                }

                // Atualizar o module
                SqlCommand cmdPut = new SqlCommand(sql, conn);
                cmdPut.Parameters.AddWithValue("@Name", moduleNew.Name);
                cmdPut.Parameters.AddWithValue("@Id", id);
                // Executar comando cmdPut
                int numRows = cmdPut.ExecuteNonQuery();
                if (numRows > 0)
                {
                    conn.Close();
                    module.Name = moduleNew.Name;
                    handler.UpdateModule(module);
                    Debug.Print("[DEBUG] 'Module updated successfully' | Put() in ModuleController");
                    return Content(HttpStatusCode.OK, "Module update successfully", Configuration.Formatters.XmlFormatter);

                }
                conn.Close();
                return InternalServerError();

            }
            catch (Exception e)
            {
                Debug.Print("[DEBUG] 'Exception in Put() in ModuleController' | " + e.Message);
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return InternalServerError();
            }
        }

        //Delete a module
        [Route("api/somiod/modules/{id}")]
        public IHttpActionResult Delete(int id)
        {

            HandlerXML handler = new HandlerXML();
            Module module = new Module();

            // Comandos SQL
            string sqlVerifyModule = "SELECT COUNT(*) FROM Module WHERE Id = @Id";
            string sqlVerifySub = "SELECT COUNT(*) FROM Subscription WHERE Parent = @Id";
            string sqlVerifyData = "SELECT COUNT(*) FROM Data WHERE Parent = @Id";
            string sqlGetModule = "SELECT * FROM Module WHERE Id = @Id";
            string sqlDeleteSub = "DELETE FROM Subscription WHERE Parent = @Id";
            string sqlDeleteData = "DELETE FROM Data WHERE Parent = @Id";
            string sqlDeleteModule = "DELETE FROM Module WHERE Id = @Id";
            //Delete Data

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                // Verificar se o module existe
                SqlCommand cmdExist = new SqlCommand(sqlVerifyModule, conn);
                cmdExist.Parameters.AddWithValue("@Id", id);
                if ((int)cmdExist.ExecuteScalar() == 0)
                {
                    conn.Close();
                    Debug.Print("[DEBUG] 'Module not found' | Delete() in ModuleController");
                    return NotFound();
                }

                // Obter o module
                SqlCommand cmdGet = new SqlCommand(sqlGetModule, conn);
                cmdGet.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmdGet.ExecuteReader();
                if (!reader.HasRows)
                {

                    reader.Close();
                    conn.Close();
                    Debug.Print("[DEBUG] 'Module not found' | Put() in ModuleController");
                    return NotFound();

                }

                // Ler os dados do reader
                while (reader.Read())
                {
                    module.Id = (int)reader["Id"];
                    module.Name = (string)reader["Name"];
                    module.Creation_dt = (DateTime)reader["Creation_dt"];
                    module.Parent = (int)reader["Parent"];
                }

                reader.Close();

                // Verificar se o module tem subscrições
                SqlCommand cmdVerifySub = new SqlCommand(sqlVerifySub, conn);
                cmdVerifySub.Parameters.AddWithValue("@Id", id);
                if ((int)cmdVerifySub.ExecuteScalar() > 0)
                {

                    // Delete das subscrições
                    SqlCommand cmdDeleteSub = new SqlCommand(sqlDeleteSub, conn);
                    cmdDeleteSub.Parameters.AddWithValue("@Id", id);
                    // Executar comando cmdDeleteSub
                    if (cmdDeleteSub.ExecuteNonQuery() == 0)
                    {
                        conn.Close();
                        Debug.Print("[DEBUG] 'Error deleting subscriptions' | Delete() in ModuleController");
                        return InternalServerError();
                    }


                }

                // Verificar se o module tem subscrições
                SqlCommand cmdVerifyData = new SqlCommand(sqlVerifyData, conn);
                cmdVerifyData.Parameters.AddWithValue("@Id", id);
                if ((int)cmdVerifyData.ExecuteScalar() > 0)
                {

                    // Delete das subscrições
                    SqlCommand cmdDeleteData = new SqlCommand(sqlDeleteData, conn);
                    cmdDeleteData.Parameters.AddWithValue("@Id", id);
                    // Executar comando cmdDeleteSub
                    if (cmdDeleteData.ExecuteNonQuery() == 0)
                    {
                        conn.Close();
                        Debug.Print("[DEBUG] 'Error deleting subscriptions' | Delete() in ModuleController");
                        return InternalServerError();
                    }

                }

                // Delete Module
                SqlCommand cmdDelete = new SqlCommand(sqlDeleteModule, conn);
                cmdDelete.Parameters.AddWithValue("@Id", id);
                int numRows = cmdDelete.ExecuteNonQuery();
                if (numRows > 0)
                {
                    conn.Close();
                    handler.DeleteModule(module);
                    Debug.Print("[DEBUG] 'Module deleted successfully' | Delete() in ModuleController");
                    return Content(HttpStatusCode.OK, "Module deleted successfully", Configuration.Formatters.XmlFormatter);
                }
                else
                {
                    conn.Close();
                    Debug.Print("[DEBUG] 'Module was not deleted successfully' | Delete() in ModuleController");
                    return InternalServerError();
                }

            }
            catch (Exception e)
            {
                Debug.Print("[DEBUG] 'Exception in Delete() in ModuleController' | " + e.Message);
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
