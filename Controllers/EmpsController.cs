using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using WebAppConsuminAPI.Data;
using WebAppConsuminAPI.Models;

namespace WebAppConsuminAPI.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using WebAppConsuminAPI.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Emp>("Emps");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmpsController : ODataController
    {
        private EmpDbContext db = new EmpDbContext();

        // GET: odata/Emps
        [EnableQuery]
        public IQueryable<Emp> GetEmps()
        {
            return db.Emps;
        }

        // GET: odata/Emps(5)
        [EnableQuery]
        public SingleResult<Emp> GetEmp([FromODataUri] int key)
        {
            return SingleResult.Create(db.Emps.Where(emp => emp.Id == key));
        }

        // PUT: odata/Emps(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Emp> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Emp emp = db.Emps.Find(key);
            if (emp == null)
            {
                return NotFound();
            }

            patch.Put(emp);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(emp);
        }

        // POST: odata/Emps
        public IHttpActionResult Post(Emp emp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Emps.Add(emp);
            db.SaveChanges();

            return Created(emp);
        }

        // PATCH: odata/Emps(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Emp> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Emp emp = db.Emps.Find(key);
            if (emp == null)
            {
                return NotFound();
            }

            patch.Patch(emp);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(emp);
        }

        // DELETE: odata/Emps(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Emp emp = db.Emps.Find(key);
            if (emp == null)
            {
                return NotFound();
            }

            db.Emps.Remove(emp);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpExists(int key)
        {
            return db.Emps.Count(e => e.Id == key) > 0;
        }
    }
}
