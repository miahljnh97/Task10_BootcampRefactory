using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DI.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace DI.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostController : ControllerBase
    {
        private readonly IDatabase _database;

        public PostController(IDatabase database)
        {
            _database = database;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _database.ReadPost();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _database.ReadById(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(Post post)
        {
            return Ok(_database.Create(post));
        }

        [HttpPatch("{id}")]
        public IActionResult Patch([FromBody]JsonPatchDocument<Post> post, int id)
        {
            return Ok(_database.UpdatePost(post, id));
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _database.DeletePost(id);
        }
    }
}
