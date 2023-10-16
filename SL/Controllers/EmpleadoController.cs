using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL.Controllers
{
    [RoutePrefix("api/Empleado")]
    public class EmpleadoController : ApiController
    {
        [Route("")]
        [HttpPost]
        public IHttpActionResult Add([FromBody]ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.Add(empleado);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("{idEmpleado}")]
        [HttpPut]
        public IHttpActionResult Update(int idEmpleado, [FromBody]ML.Empleado empleado)
        {
            empleado.IdEmpleado = idEmpleado;
            ML.Result result = BL.Empleado.Update(empleado);
            if(result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }


























































































































































































//Empieza parte de justino
    }
}