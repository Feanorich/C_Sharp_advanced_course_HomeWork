using Server.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Server.Controllers
{
    public class UserController : ApiController
    {
        [Route("getlistw")]        
        public DbSet<Workers> GetW() => DataBase.MyDB.Workers;

        [Route("getlistd")]        
        public DbSet<Departments> GetD() => DataBase.MyDB.Departments;

        [Route("adddep")]
        public HttpResponseMessage AddD([FromBody]Departments value)
        {
            if (DataBase.AddDep(value))
                return Request.CreateResponse(HttpStatusCode.Created);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("deldep")]
        public HttpResponseMessage DelD([FromBody]Departments value)
        {
            if (DataBase.DelDep(value))
                return Request.CreateResponse(HttpStatusCode.Accepted);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("editdep")]
        public HttpResponseMessage EditD([FromBody]Departments value)
        {
            if (DataBase.EditDep(value))
                return Request.CreateResponse(HttpStatusCode.Accepted);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("addw")]
        public HttpResponseMessage AddW([FromBody]Workers value)
        {
            if (DataBase.AddWorker(value))
                return Request.CreateResponse(HttpStatusCode.Created);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        [Route("delw")]
        public HttpResponseMessage DelW([FromBody]Workers value)
        {
            if (DataBase.DelWorker(value))
                return Request.CreateResponse(HttpStatusCode.Accepted);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        [Route("editw")]
        public HttpResponseMessage EditW([FromBody]Workers value)
        {
            if (DataBase.EditWorker(value))
                return Request.CreateResponse(HttpStatusCode.Accepted);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        [Route("defaultdata")]
        public HttpResponseMessage DD()
        {            
            if (DataBase.DefaultData())
                return Request.CreateResponse(HttpStatusCode.Accepted);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }

}
