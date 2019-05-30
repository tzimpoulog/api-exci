using ExciApi.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExciApi.Controllers
{
    //[Authorize]
    public class PoiController : ApiController
    {
        // GET api/values
        public async Task<List<Poi>> Get()
        {
            using (var dbContext = Request.GetOwinContext().Get<ApplicationDbContext>())
            {
                return await dbContext.Pois.ToListAsync();
            }
        }

        // GET api/values/5
        public async Task<Poi> Get(int id)
        {
            using (var dbContext = Request.GetOwinContext().Get<ApplicationDbContext>())
            {
                return await dbContext.Pois.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        // POST api/values
        public async Task Post([FromBody]Poi value)
        {
            using (var dbContext = Request.GetOwinContext().Get<ApplicationDbContext>())
            {
                dbContext.Pois.AddOrUpdate(value);
                await dbContext.SaveChangesAsync();
            }
        }

        // PUT api/values/5
        public async Task Put(int id, [FromBody]Poi value)
        {
            using (var dbContext = Request.GetOwinContext().Get<ApplicationDbContext>())
            {
                var foundItem = await dbContext.Pois.FindAsync(value.Id);
                if (foundItem == null) return;


                dbContext.Entry(foundItem).CurrentValues.SetValues(value);
                await dbContext.SaveChangesAsync();
            }
        }

        // DELETE api/values/5
        public async Task Delete(int id)
        {
            using (var dbContext = Request.GetOwinContext().Get<ApplicationDbContext>())
            {
                var foundItem = await dbContext.Pois.FirstOrDefaultAsync(x => x.Id == id);
                if (foundItem == null) return;
                dbContext.Entry(foundItem).State = EntityState.Deleted;
                await dbContext.SaveChangesAsync();
            }

        }
    }
}
