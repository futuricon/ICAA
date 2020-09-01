using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ICAA.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ICAA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchGController : Controller
    {
        private readonly AppIdentityDbContext db;
        private readonly ITagRepository repository;
        public SearchGController(AppIdentityDbContext _db, ITagRepository repo)
        {
            repository = repo;
            db = _db;
        }

        [Produces("application/json")]
        [HttpGet("Search")]
        public IActionResult Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var tagName = repository.InfoTags
                    .Where(p => p.TagName.Contains(term))
                    .Select(t => t.TagName).ToList();
                return Ok(tagName);
            }
            catch
            {
                return BadRequest();
            }
        }
        //    // GET: api/<controller>
        //    [HttpGet]
        //    public IEnumerable<string> Get()
        //    {
        //        return new string[] { "value1", "value2" };
        //    }

        //    // GET api/<controller>/5
        //    [HttpGet("{id}")]
        //    public string Get(int id)
        //    {
        //        return "value";
        //    }

        //    // POST api/<controller>
        //    [HttpPost]
        //    public void Post([FromBody]string value)
        //    {
        //    }

        //    // PUT api/<controller>/5
        //    [HttpPut("{id}")]
        //    public void Put(int id, [FromBody]string value)
        //    {
        //    }

        //    // DELETE api/<controller>/5
        //    [HttpDelete("{id}")]
        //    public void Delete(int id)
        //    {
        //    }
        //}
    }
}
