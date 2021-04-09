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
using WebApiCurdSerivce.Models;

namespace WebApiCurdSerivce.Controllers
{
    public class EmployeeController : ApiController
    {
        private CrudDBEntities db = new CrudDBEntities();

        // GET: api/Employee
        public IQueryable<EmployeeT> GetEmployeeTs()
        {
            return db.EmployeeTs;
        }

        // GET: api/Employee/5
        [ResponseType(typeof(EmployeeT))]
        public IHttpActionResult GetEmployeeT(int id)
        {
            EmployeeT employeeT = db.EmployeeTs.Find(id);
            if (employeeT == null)
            {
                return NotFound();
            }

            return Ok(employeeT);
        }

        // PUT: api/Employee/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeeT(int id, EmployeeT employeeT)
        {
          

            if (id != employeeT.EmployeeID)
            {
                return BadRequest();
            }

            db.Entry(employeeT).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeTExists(id))
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

        // POST: api/Employee
        [ResponseType(typeof(EmployeeT))]
        public IHttpActionResult PostEmployeeT(EmployeeT employeeT)
        {
        
            db.EmployeeTs.Add(employeeT);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employeeT.EmployeeID }, employeeT);
        }

        // DELETE: api/Employee/5
        [ResponseType(typeof(EmployeeT))]
        public IHttpActionResult DeleteEmployeeT(int id)
        {
            EmployeeT employeeT = db.EmployeeTs.Find(id);
            if (employeeT == null)
            {
                return NotFound();
            }

            db.EmployeeTs.Remove(employeeT);
            db.SaveChanges();

            return Ok(employeeT);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeTExists(int id)
        {
            return db.EmployeeTs.Count(e => e.EmployeeID == id) > 0;
        }
    }
}