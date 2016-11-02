using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CCi.Shared.Admin.AppCatalog.Models;
using System.Web.Http.Cors;
using ApplicationCatalog;
using AutoMapper;
using CCI.Shared.Admin.AppCatalog.Core.Repository;

namespace CCi.Shared.Admin.AppCatalog.Controllers
{
    [RoutePrefix("api/Applications")]
    public class ApplicationController : ApiController
    {
        IDeployedApplicationRepository _repo;

        public ApplicationController():this(new DeployedApplicationRepository(string.Empty, ConfigurationManager.ConnectionStrings["appCatalog"].ConnectionString )) { }

        public ApplicationController(IDeployedApplicationRepository repo)
        {
            _repo = repo;
        }


        [Route("")]
        public IEnumerable<ApplicationDto> Get()
        {
            var apps = _repo.GetApps();
            var dto = Mapper.Map<IEnumerable<DeployedApplication>, IEnumerable<ApplicationDto>>(apps);

            return dto;
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
