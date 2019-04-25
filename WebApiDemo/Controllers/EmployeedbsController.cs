using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EmployeeDataAccess;
using WebApiDemo.Models;
using System.Threading;

namespace WebApiDemo.Controllers
{
    public class EmployeedbsController : ApiController
    {
       private TestEntities db = new TestEntities();

        // GET: api/Employeedbs
       // [BasicAuthentication]
       [Authorize]
        public HttpResponseMessage GetEmployeedbs()
        {

            var employees = db.Employeedbs.ToList();

            return Request.CreateResponse(HttpStatusCode.OK, employees);

            //List<Employeedb> employees = new List<Employeedb>();
            //string username = Thread.CurrentPrincipal.Identity.Name;
            //switch (username.ToLower())
            //{
            //    case "female":
            //        return Request.CreateResponse(HttpStatusCode.OK, db.Employeedbs.Where(e => e.Gender.ToLower() == "female").ToList());
                  

            //    case "male":

            //        return Request.CreateResponse(HttpStatusCode.OK, db.Employeedbs.Where(e => e.Gender.ToLower() == "male").ToList());

            //    default:
            //        return Request.CreateResponse(HttpStatusCode.Unauthorized, "INVALID User");
            //}
           
        }

        // GET: api/Employeedbs/5
      [ResponseType(typeof(Employeedb))]
        public IHttpActionResult GetEmployeedb(int id)
        {
            Employeedb employeedb = db.Employeedbs.Find(id);
            if (employeedb == null)
            {
                return NotFound();
            }

            User user = db.Users.First();
            return Ok(user);
        }

        // PUT: api/Employeedbs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeedb([FromBody ]int id, Employeedb employeedb)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          

            db.Entry(employeedb).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeedbExists(id))
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

        // POST: api/Employeedbs
        [ResponseType(typeof(Employeedb))]
        public IHttpActionResult PostEmployeedb(Employeedb employeedb)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employeedbs.Add(employeedb);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employeedb.Id }, employeedb);
        }

        // DELETE: api/Employeedbs/5
        [ResponseType(typeof(Employeedb))]
        public IHttpActionResult DeleteEmployeedb(int id)
        {
            Employeedb employeedb = db.Employeedbs.Find(id);
            if (employeedb == null)
            {
                return NotFound();
            }

            db.Employeedbs.Remove(employeedb);
            db.SaveChanges();

            return Ok(employeedb);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeedbExists(int id)
        {
            return db.Employeedbs.Count(e => e.Id == id) > 0;
        }
    }
}