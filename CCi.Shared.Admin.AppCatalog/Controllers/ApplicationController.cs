using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CCi.Shared.Admin.AppCatalog.Models;

namespace CCi.Shared.Admin.AppCatalog.Controllers
{
    public class ApplicationController : ApiController
    {
        // GET api/values
        public IEnumerable<ApplicationDto> Get()
        {
            return new ApplicationDto[]
            {
                new ApplicationDto()
                {
                    ProjectId = Guid.NewGuid(),
                    UniqueId = "name1"
                },
                new ApplicationDto()
                {
                    ProjectId = Guid.NewGuid(),
                    UniqueId = "name2"
                }
            };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
