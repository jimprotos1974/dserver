using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using D.Models;
using Dapper;

namespace D.Controllers
{
    public class TeachersController : ApiController
    {
        private DContext db = new DContext();

        private object test(IEnumerable<string> fields)
        {
            System.Text.StringBuilder sbSelect = new System.Text.StringBuilder();
            System.Text.StringBuilder sbFrom = new System.Text.StringBuilder();

            sbSelect.Append(" SELECT ");
            sbFrom.Append(" FROM ");

            fields = new List<string>(){
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

            return o;
        }

        // GET: api/Teachers
        public object GetRooms()
        {
            return test(null);

            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomExists(int id)
        {
            return db.Rooms.Count(e => e.Id == id) > 0;
        }
    }
}