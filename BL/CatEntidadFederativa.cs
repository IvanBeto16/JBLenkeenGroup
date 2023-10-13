using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CatEntidadFederativa
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JBLeenkenGroupEntities context = new DL.JBLeenkenGroupEntities())
                {
                    var query = (from entidad in context.CatEntidadFederativa
                                 select new
                                 {
                                     IdEntidad = entidad.IdEntidad,
                                     Estado = entidad.Estado
                                 });
                    result.Objects = new List<object>();
                    if (query != null && query.ToList().Count > 0)
                    {
                        foreach (var item in query)
                        {
                            ML.CatEntidadFederativa entidad = new ML.CatEntidadFederativa();

                            entidad.IdEntidad = item.IdEntidad;
                            entidad.Estado = item.Estado;

                            result.Objects.Add(entidad);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay Estados en la tabla";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Exception = ex;
            }
            return result;
        }
    }
}
