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

        private void getXQuery(IEnumerable<string> fields)
        {
            System.Text.StringBuilder sbSelect = new System.Text.StringBuilder();
            System.Text.StringBuilder sbFrom = new System.Text.StringBuilder();

            sbSelect.Append(" SELECT ");
            sbFrom.Append(" FROM ");

            fields = new List<string>(){
                "ROOM.ID",
                "ROOM.NAME",
                "ROOM.BUILDINGID_BUILDING_NAME"
            };

            foreach(string description in fields)
            {
                var aDescriptionSplitOnUnderscore = description.Split('_');
                var aFullName = aDescriptionSplitOnUnderscore[0];
                var aFullNameSplitOnDot = aFullName.Split('.');
                var aTableName = aFullNameSplitOnDot[0];
                var aFieldName = aFullNameSplitOnDot[1];
                /*ar aJointDescription = aDescriptionSplitOnUnderscore[1];
                var hasJointDescription = !String.IsNullOrEmpty(aJointDescription);
                var aJointDescriptionSplitOnUnderscore = hasJointDescription ? aJointDescription.Split('_') : null;
                var aJointTable = hasJointDescription ? aJointDescriptionSplitOnUnderscore[0] : null;
                var aJointField = hasJointDescription ? aJointDescriptionSplitOnUnderscore[1] : null;*/

                sbSelect.Append(" ," + aFullName + " ");

                if (true)
                {
                    var foreignKeys = db.GetForeignKeyProperties<Room>();
                }
            }
        }

        // GET: api/Teachers
        public IQueryable<Teacher> GetTeachers()
        {
            getXQuery(null);

            //var f = db.GetForeignKeyProperties<Room>();

            //var jim = db.GetType().GetProperties().Where(tc => tc.Name == "Rooms").Single();

            //var r = jim.ReflectedType;
            //var type = typeof r;

            //var myint = 5;
            //var myinttype = typeof(Int16);

            //var f2 = db.GetForeignKeyProperties<r>();

            //var s = DContext.GetTableName(typeof(Room), db);

               // var blogNames = db.Database.SqlQuery<object>(
                                   //"SELECT * FROM dbo.Rooms join dbo.Buildings on dbo.Rooms.BuildingId = dbo.Buildings.Id").ToList();

            

            //string sql = "SELECT * FROM dbo.Rooms join dbo.Buildings on dbo.Rooms.BuildingId = dbo.Buildings.Id";
           // dynamic t;

           //using (var connection = new SqlConnection(@"Data Source=.\SQLEXPRESS; Initial Catalog=D;User Id=sa;password=prosvasis;"))
           //{
            //    var orderDetail = connection.Query(sql).FirstOrDefault();

                //t = Newtonsoft.Json.JsonConvert.SerializeObject(orderDetail);
            //}



            return db.Teachers.Include(b => b.Country);
        }

        // GET: api/Teachers/5
        [ResponseType(typeof(Teacher))]
        public async Task<IHttpActionResult> GetTeacher(int id)
        {
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        // PUT: api/Teachers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTeacher(int id, Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teacher.Id)
            {
                return BadRequest();
            }

            db.Entry(teacher).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Teachers
        [ResponseType(typeof(Teacher))]
        public async Task<IHttpActionResult> PostTeacher(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teachers.Add(teacher);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = teacher.Id }, teacher);
        }

        // DELETE: api/Teachers/5
        [ResponseType(typeof(Teacher))]
        public async Task<IHttpActionResult> DeleteTeacher(int id)
        {
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            db.Teachers.Remove(teacher);
            await db.SaveChangesAsync();

            return Ok(teacher);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeacherExists(int id)
        {
            return db.Teachers.Count(e => e.Id == id) > 0;
        }
    }
}