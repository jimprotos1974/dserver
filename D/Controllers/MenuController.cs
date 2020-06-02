using D.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using static System.Net.WebRequestMethods;

namespace D.Controllers
{
    public class MenuController : ApiController
    {
        // GET api/<controller>
        /*[HttpPost]
        public async void PostParams(HttpRequestMessage request)
        {
            var t = 1;

            var jsonString = await request.Content.ReadAsStringAsync();

            //return new JsonResult<string>(paramsAsJson, new JsonSerializerSettings(), Encoding.UTF8, this);
            //return new HttpResponseMessage { Content = new StringContent("{success: true, menu: [{text: 'Αίθουσες', leaf: true, cmd: 'EditMaster', command: 'ROOMS&LIST=ROOMS&FORM=ROOMS'}]}", System.Text.Encoding.UTF8, "application/json") };
        }*/

        public class ServiceRequest
        {
            public string Service { get; set; }
            public string Version { get; set; }
            public object Data { get; set; }
        }

        public async Task<GenericReply> PostParams(HttpRequestMessage request)
        {
            var jsonString = await request.Content.ReadAsStringAsync();

            var jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceRequest>(jsonString);
            var service = jsonObject.Service;

            service = service.ToLower();

            switch (service)
            {
                case "getaccessrights":
                    return GetAccessRights();
                case "getbrowserdesign":
                    return GetListDesign();
                case "getbrowserdata":
                    return GetBrowserData();
                case "getformdesign":
                    return GetViewerDesign();
                case "getdata":
                    return GetViewerData();
            }

            return new GenericReply();
        }

        public GenericReply GetAccessRights()
        {
            var accessRightsReply = new AccessRightsReply();
            var reply = new GenericReply();

            reply.data = accessRightsReply;

            return reply;
        }

        public GenericReply GetMenu(string name)
        {
            var reply = new GenericReply();

            var menu = new Menu();
            var menuItem1 = new Menu();
            var menuItem2 = new Menu();

            menuItem1.Text = "Text1";
            menuItem1.Description = "Description1";
            menuItem1.Type = "EditMaster";
            menuItem1.Command = "CUSTOMER";

            menuItem2.Text = "Text2";
            menuItem2.Description = "Description2";
            menuItem2.Type = "EditMaster";
            menuItem2.Command = "SALDOC";

            var list = new List<Menu>();

            list.Add(menuItem1);
            list.Add(menuItem2);

            reply.data = list;

            return reply;
        }

        public GenericReply GetListDesign()
        {
            var reply = new GenericReply();
            var design = new ListDesign();

            var field1 = new Field();
            var field2 = new Field();
            var field3 = new Field();
            var field4 = new Field();

            field1.name = "ROOMS.ID";
            field1.type = "int";

            field2.name = "ROOMS.NAME";
            field2.type = "string";

            field3.name = "ROOMS.BUILDINGID_BUILDINGS_NAME";
            field3.type = "string";

            field4.name = "ROOMS.BUILDINGID_BUILDINGS_ADDRESS";
            field4.type = "string";

            design.fields = new List<Field>()
            {
                field1,
                field2,
                field3,
                field4
            };

            var column1 = new Column();
            var column2 = new Column();
            var column3 = new Column();
            var column4 = new Column();

            column1.dataIndex = "ROOMS.ID";
            column1.header = "ROOMS.ID";

            column2.dataIndex = "ROOMS.NAME";
            column2.header = "ROOMS.NAME";

            column3.dataIndex = "ROOMS.BUILDINGID_BUILDINGS_NAME";
            column3.header = "ROOMS.BUILDINGID_BUILDINGS_NAME";

            column4.dataIndex = "ROOMS.BUILDINGID_BUILDINGS_ADDRESS";
            column4.header = "ROOMS.BUILDINGID_BUILDINGS_ADDRESS";

            design.columns = new List<Column>()
            {
                column1,
                column2,
                column3,
                column4
            };

            reply.data = design;

            return reply;
        }

        public GenericReply GetViewerDesign()
        {
            var reply = new GenericReply();
            var design = new ViewerDesign();

            var field1 = new Field();
            var field2 = new Field();

            var formField1 = new FormField();
            var formField2 = new FormField();

            var formContainer1 = new FormContainer();

            field1.name = "ROOMS.ID";
            field1.type = "int";

            field2.name = "ROOMS.NAME";
            field2.type = "string";

            formField1.name = "ROOMS.ID";
            formField1.xtype = "s1textfield";
            formField1.visible = true;
            formField1.flex = 2;

            formField2.name = "ROOMS.NAME";
            formField2.xtype = "s1textfield";
            formField2.visible = true;
            formField2.flex = 5;

            formContainer1.name = "";
            formContainer1.xtype = "s1cont";
            formContainer1.visible = true;
            formContainer1.items = new List<IRenderable>() {
                formField1,
                formField2
            };

            var model = new Model();

            model.relationship = "OneToOne";
            model.fields = new List<Field>()
            {
                field1,
                field2
            };

            design.model = new Dictionary<string, Model>();

            design.model.Add("ROOMS", model);

            design.form = new List<IRenderable>()
            {
                formContainer1
            };

            reply.data = design;

            return reply;
        }

        public class GenericReply
        {
            public bool success = true;
            public object data;
        }

        public class AccessRightsReply
        {
            public bool read = true;
            public bool write = true;
            public bool list = true;
            public bool locate = true;
        }

        public class ViewerData
        {
            public Dictionary<string, List<Dictionary<string, object>>> Data { get; set;}
        }
        
        public class Menu
        {
            public string Text { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }
            public string Command { get; set; }
            public List<Menu> Items { get; set; }
        }

        public class Model
        {
            public string relationship { get; set; }
            public List<Field> fields;
        }

        public interface IRenderable
        {

        }

        public class FormField : IRenderable
        {
            public string xtype { get; set; }
            public string name { get; set; }
            public bool visible { get; set; }
            public Int16 flex { get; set; }
        }

        public class FormContainer : IRenderable
        {
            public List<IRenderable> items { get; set; }
            public string xtype { get; set; }
            public string name { get; set; }
            public bool visible { get; set; }
            public Int16 flex { get; set; }
        }

        public class ViewerDesign
        {
            public Dictionary<string, Model> model { get; set; }
            public List<IRenderable> form { get; set; }
        }

        public class Column
        {
            public string dataIndex { get; set; }
		    public string header { get; set; }
		    public int width { get; set; }
        }

        public class Field
        {
            public string name { get; set; }
            public string type { get; set; }
        }

        public class ListDesign
        {
            public List<Field> fields { get; set; }
            public List<Column> columns { get; set; }
        }

        public class BrowserData
        {
            public object data { get; set; }
        }

        private DContext db = new DContext();

        private GenericReply GetBrowserData()
        {
            GenericReply reply = new GenericReply();

            var fields = new List<string>(){
                "ROOMS.ID",
                "ROOMS.NAME",
                "ROOMS.BUILDINGID",
                "ROOMS.BUILDINGID_BUILDINGS_NAME",
                "ROOMS.BUILDINGID_BUILDINGS_ADDRESS",
                "ROOMS.ALTERNATEBUILDINGID",
                "ROOMS.ALTERNATEBUILDINGID_BUILDINGS_NAME",
                "ROOMS.ALTERNATEBUILDINGID_BUILDINGS_ADDRESS"
            };

            D.Models.Interfaces.NavigableDbContext navigableDbContext = new Models.Interfaces.NavigableDbContext(db);

            D.Models.Interfaces.DatabaseGossiper databaseGossiper = new Models.Interfaces.DatabaseGossiper(navigableDbContext);

            D.Models.Interfaces.QueryBuilder qb = new D.Models.Interfaces.QueryBuilder(databaseGossiper);

            var q = qb.BuildBasicQuery(fields);

            D.Models.Interfaces.DatabaseCommander dc = new Models.Interfaces.DatabaseCommander();

            db.Database.SqlQuery<object>(q);

            var o = dc.ExecuteSql(q);

            reply.data = o;

            return reply;
        }

        private static void ShowDataTable(System.Data.DataTable table, Int32 length)
        {
            foreach (System.Data.DataColumn col in table.Columns)
            {
                System.Diagnostics.Debug.Write("{0,-" + length + "}", col.ColumnName);
            }
            System.Diagnostics.Debug.WriteLine("");

            foreach (System.Data.DataRow row in table.Rows)
            {
                foreach (System.Data.DataColumn col in table.Columns)
                {
                    if (col.DataType.Equals(typeof(DateTime)))
                        System.Diagnostics.Debug.Write("{0,-" + length + ":d}", row[col].ToString());
                    else if (col.DataType.Equals(typeof(Decimal)))
                        System.Diagnostics.Debug.Write("{0,-" + length + ":C}", row[col].ToString());
                    else
                        System.Diagnostics.Debug.Write("{0,-" + length + "}", row[col].ToString());
                }
                System.Diagnostics.Debug.WriteLine("");
            }
        }

        private static void ShowDataTable(System.Data.DataTable table)
        {
            ShowDataTable(table, 14);
        }


        private GenericReply GetViewerData()
        {
            // You can specify the Catalog, Schema, Table Name, Table Type to get
            // the specified table(s).
            // You can use four restrictions for Table, so you should create a 4 members array.
            String[] tableRestrictions = new String[4];

            // For the array, 0-member represents Catalog; 1-member represents Schema;
            // 2-member represents Table Name; 3-member represents Table Type.
            // Now we specify the Table Name of the table what we want to get schema information.
            tableRestrictions[2] = "Rooms";

            using (var conn = new System.Data.SqlClient.SqlConnection(@"Data Source=.\SQLEXPRESS; Initial Catalog=D;User Id=sa;password=prosvasis;"))
            {
                conn.Open();

                try
                {
                    System.Data.DataTable courseTableSchemaTable = conn.GetSchema("Tables", tableRestrictions);

                    System.Diagnostics.Debug.WriteLine("Schema Information of Course Tables:");
                    ShowDataTable(courseTableSchemaTable, 20);
                    System.Diagnostics.Debug.WriteLine("");
                } catch (Exception x)
                {

                }
            }

            using (var conn = new System.Data.SqlClient.SqlConnection(@"Data Source=.\SQLEXPRESS; Initial Catalog=D;User Id=sa;password=prosvasis;"))
            {
                string[] restrictions = new string[4] { null, null, "rooms", null };
                conn.Open();
                try
                {
                    var columnList = conn.GetSchema("Columns", restrictions).AsEnumerable().Select(s => s.Field<String>("Column_Name")).ToList();
                }
                catch (Exception exc)
                {

                }
            }











            GenericReply reply = new GenericReply();

            var fields = new List<string>(){
                "ROOMS.ID",
                "ROOMS.NAME",
                "ROOMS.BUILDINGID",
                "ROOMS.BUILDINGID_BUILDINGS_NAME",
                "ROOMS.BUILDINGID_BUILDINGS_ADDRESS",
                "ROOMS.ALTERNATEBUILDINGID",
                "ROOMS.ALTERNATEBUILDINGID_BUILDINGS_NAME",
                "ROOMS.ALTERNATEBUILDINGID_BUILDINGS_ADDRESS"
            };

            D.Models.Interfaces.NavigableDbContext navigableDbContext = new Models.Interfaces.NavigableDbContext(db);

            D.Models.Interfaces.DatabaseGossiper databaseGossiper = new Models.Interfaces.DatabaseGossiper(navigableDbContext);

            D.Models.Interfaces.QueryBuilder qb = new D.Models.Interfaces.QueryBuilder(databaseGossiper);

            var q = qb.BuildBasicQuery(fields);

            q = q + " where ROOMS.ID = 1";

            D.Models.Interfaces.DatabaseCommander dc = new Models.Interfaces.DatabaseCommander();

            var o = dc.ExecuteSql(q);

            reply.data = o;

            var data = new ViewerData();

            data.Data = new Dictionary<string, List<Dictionary<string, object>>>();

            data.Data.Add("ROOMS", new List<Dictionary<string, object>>()
            {
                new Dictionary<string, object>()
                {
                    {
                        "ID",
                        "1"
                    },
                    {
                        "NAME",
                        "AAAAAAAA"
                    }
                }
            });

            reply.data = data.Data;

            return reply;
        }
    }
}