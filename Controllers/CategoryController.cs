using E_CommerceAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace E_CommerceAPI.Controllers
{
    public class CategoryController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Get api/Category
        public IHttpActionResult getCategories()
        {
            return Ok(db.Categories.ToList());
        }

        //Get api/Category/id
        public IHttpActionResult getCategory(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        //Put api/Category/id
        public IHttpActionResult PutCategory(int id,Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != category.Id)
            {
                return BadRequest();
            }
            db.Entry(category).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExict(id))
                {
                    return NotFound();
                }
                else
                {
                    HttpResponseMessage mes = new HttpResponseMessage()
                    {
                        ReasonPhrase = "conflict happend",
                        StatusCode = HttpStatusCode.Conflict
                    };
                    throw new HttpResponseException(mes);
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
        //post api/Category
        public IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Categories.Add(category);
            db.SaveChanges();

            return Created("Catergory Cearted", category);
        }
        //Delete api/Category/id
        public IHttpActionResult DeleteCategory(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            db.Categories.Remove(category);
            db.SaveChanges();
          
            return Ok(category);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public bool CategoryExict(int id)
        {
            return db.Categories.Count(a => a.Id == id) > 0;
        }
    }
}
