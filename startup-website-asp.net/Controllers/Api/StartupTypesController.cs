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
using startup_website_asp.net.Models.EF;
using startup_website_asp.net.ViewModels;

namespace startup_website_asp.net.Controllers.Api
{
    public class StartupTypesController : ApiController
    {
        private StartupWebsite db = new StartupWebsite();

        [HttpGet]
        // GET: api/StartupTypes
        public IHttpActionResult GetStartupTypes()
        {
            List<StartupType> startupTypes = db.StartupTypes.ToList();
            List<StartupTypeApiModel> result = new List<StartupTypeApiModel>();
            foreach (var item in startupTypes)
            {
                StartupTypeApiModel startupTypeApiModel = new StartupTypeApiModel();
                startupTypeApiModel.Name = item.Name;
                startupTypeApiModel.MetaTitle = item.MetaTitle;
                startupTypeApiModel.ImageUrl = item.ImageUrl;
                result.Add(startupTypeApiModel);
            }
            return Ok(result);
        }

        // GET: api/StartupTypes/5
        [ResponseType(typeof(StartupType))]
        public IHttpActionResult GetStartupType(int id)
        {
            StartupType startupType = db.StartupTypes.Find(id);
            if (startupType == null)
            {
                return NotFound();
            }

            return Ok(startupType);
        }

        // PUT: api/StartupTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStartupType(int id, StartupType startupType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != startupType.StartupTypeId)
            {
                return BadRequest();
            }

            db.Entry(startupType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StartupTypeExists(id))
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

        // POST: api/StartupTypes
        [ResponseType(typeof(StartupType))]
        public IHttpActionResult PostStartupType(StartupType startupType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StartupTypes.Add(startupType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = startupType.StartupTypeId }, startupType);
        }

        // DELETE: api/StartupTypes/5
        [ResponseType(typeof(StartupType))]
        public IHttpActionResult DeleteStartupType(int id)
        {
            StartupType startupType = db.StartupTypes.Find(id);
            if (startupType == null)
            {
                return NotFound();
            }

            db.StartupTypes.Remove(startupType);
            db.SaveChanges();

            return Ok(startupType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StartupTypeExists(int id)
        {
            return db.StartupTypes.Count(e => e.StartupTypeId == id) > 0;
        }
    }
}