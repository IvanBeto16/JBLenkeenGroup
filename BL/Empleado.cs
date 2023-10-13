using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        //Metodos usando LINQ
        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JBLeenkenGroupEntities context = new DL.JBLeenkenGroupEntities())
                {
                    DL.Empleado emp = new DL.Empleado();

                    emp.IdEmpleado = empleado.IdEmpleado;
                    emp.Nombre = empleado.Nombre;
                    emp.ApellidoPaterno = empleado.ApellidoPaterno;
                    emp.ApellidoMaterno = empleado.ApellidoMaterno;
                    emp.NumeroNomina = empleado.NumeroNomina;
                    emp.CatEntidadFederativa.IdEntidad = empleado.CatEntidadFederativa.IdEntidad;

                    context.Empleado.Add(emp);
                    context.SaveChanges();
                    result.Correct = true;
                }

            }catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Exception = ex;
            }
            return result;
        }

        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JBLeenkenGroupEntities context = new DL.JBLeenkenGroupEntities())
                {
                    var query = (from i in context.Empleado
                                 where i.IdEmpleado == empleado.IdEmpleado
                                 select i).SingleOrDefault();
                    if (query != null)
                    {
                        query.IdEmpleado = empleado.IdEmpleado;
                        query.Nombre = empleado.Nombre;
                        query.ApellidoPaterno = empleado.ApellidoPaterno;
                        query.ApellidoMaterno = empleado.ApellidoMaterno;
                        query.NumeroNomina = empleado.NumeroNomina;
                        query.IdEntidad = empleado.CatEntidadFederativa.IdEntidad;

                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ha ocurrido un error, no se pudo actualizar el empleado";
                    }
                    
                }
            }catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Correct = false;
                result.Exception = ex;
            }
            return result;
        }
    }
}
