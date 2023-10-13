using System;
using DL;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
      
      
        //----------------------Metodo Borrar-------------

        public static ML.Result DeleteLINQ(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                //Todo lo que se eje cute dnetro de using se libera al final, los recursos
                using (DL.JBLeenkenGroupEntities context = new DL.JBLeenkenGroupEntities())
                {
                    var query = (from a in context.Empleado
                                 where a.IdEmpleado == empleado.IdEmpleado
                                 select a).First();

                    context.Empleado.Remove(query);
                    context.SaveChanges();
                }
                result.Correct = true;


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Exception = ex;
            }
            return result;

        }

        //----------------------Metodo select *-------------

        public static ML.Result GetAllLINQ()
        {
          ML.Result result = new ML.Result();
            try
            {
                //Todo lo que se eje cute dnetro de using se libera al final, los recursos
                using (DL.JBLeenkenGroupEntities context = new DL.JBLeenkenGroupEntities())
                {
                    var empleadoLINQ = (from objEmpleado in context.Empleado
                                        join CatEntidadFederativa in context.CatEntidadFederativa on objEmpleado.IdEntidad equals CatEntidadFederativa.IdEntidad
                                        select new
                                        {
                                            IdEmpleado = objEmpleado.IdEmpleado,
                                            NumeroNomina = objEmpleado.NumeroNomina,
                                            Nombre = objEmpleado.Nombre,
                                            ApellidoPaterno = objEmpleado.ApellidoPaterno,
                                            ApellidoMaterno = objEmpleado.ApellidoMaterno,
                                            IdEntidad = CatEntidadFederativa.IdEntidad
                                        }).ToList();

                    result.Objects = new List<object>();
                    if (empleadoLINQ != null && empleadoLINQ.ToList().Count > 0)
                    {

                        foreach (var obj in empleadoLINQ)
                        {
                            ML.Empleado empleado1 = new ML.Empleado();


                            empleado1.IdEmpleado = obj.IdEmpleado;
                            empleado1.Nombre = obj.Nombre;
                            empleado1.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado1.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado1.CatEntidadFederativa = new ML.CatEntidadFederativa();
                            empleado1.CatEntidadFederativa.IdEntidad = obj.IdEntidad;
    
                            result.Objects.Add(empleado1);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "La tabla empleado no contiene registros";
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
        //----------------------Metodo select By Id-------------

        public static ML.Result GetByIdLINQ(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                //Todo lo que se eje cute dnetro de using se libera al final, los recursos
                using (DL_EF.JAlarconProgramacionNCapasEntities context = new DL_EF.JAlarconProgramacionNCapasEntities())
                {
                    var usuariosLINQ = (from objUsuario in context.USUARIOs
                                        join Rol in context.Rols on objUsuario.IdRol equals Rol.IdRol
                                        where objUsuario.IdUsuario == empleado.IdUsuario
                                        select new
                                        {
                                            IdUsuario = objUsuario.IdUsuario,
                                            NombreUsuario = objUsuario.Nombre,
                                            ApellidoPaterno = objUsuario.ApellidoPaterno,
                                            ApellidoMaterno = objUsuario.ApellidoMaterno,
                                            FechaNacimineto = objUsuario.FechaNacimiento,
                                            IdRol = Rol.IdRol,
                                            NombreRol = Rol.Nombre,
                                            UserName = objUsuario.UserName,
                                            Email = objUsuario.Email,
                                            contraseña = objUsuario.password,
                                            Sexo = objUsuario.Sexo,
                                            Telefono = objUsuario.Telefono,
                                            Celular = objUsuario.Celular,
                                            CURP = objUsuario.CURP
                                        }).Single();

                    result.Objects = new List<object>();
                    if (usuariosLINQ != null)
                    {


                        ML.Usuario usuario1 = new ML.Usuario();
                        usuario1.Nombre = usuariosLINQ.NombreUsuario;
                        usuario1.ApellidoPaterno = usuariosLINQ.ApellidoPaterno;
                        usuario1.ApellidoMaterno = usuariosLINQ.ApellidoMaterno;
                        usuario1.FechaNacimiento = usuariosLINQ.FechaNacimineto;
                        usuario1.Rol = new ML.Rol();
                        usuario1.Rol.IdRol = usuariosLINQ.IdRol;
                        usuario1.Rol.Nombre = usuariosLINQ.NombreRol;
                        usuario1.UserName = usuariosLINQ.UserName;
                        usuario1.Email = usuariosLINQ.Email;
                        usuario1.password = usuariosLINQ.contraseña;
                        usuario1.Sexo = usuariosLINQ.Sexo;
                        usuario1.Telefono = usuariosLINQ.Telefono;
                        usuario1.Celular = usuariosLINQ.Celular;
                        usuario1.CURP = usuariosLINQ.CURP;
                        result.Object = usuario1;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "La tabla usuario no contiene registros";
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
