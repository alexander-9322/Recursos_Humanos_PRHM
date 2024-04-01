using DB.Models;
using DB.Models.Cruds;
using DB.Models.Pagos;
using DB.Models.Sesion;
using DB.Models.Usuarios;
using System.Data;
using System.Data.SqlClient;

namespace DB.DBConnection
{
    public class Servicios
    {
        private RHEntities db = new RHEntities();

        public DataSet Login(Login login)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("ObtenerInformacionPersonaPorCredenciales", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@correo", login.correo);
                    sqlCommand.Parameters.AddWithValue("@clave", login.clave);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public DataSet ObtenerInformacion_X_Cedula(long cedula)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SP_ObtenerInformacion_X_Cedula", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@cedula", cedula);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public DataSet Crear_Persona_Usuario_Empleado(Usuario user)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SP_CrearPersona_Empleado_Usuario", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Cedula", user.PERSONA.Cedula);
                    sqlCommand.Parameters.AddWithValue("@Nombre", user.PERSONA.Nombre);
                    sqlCommand.Parameters.AddWithValue("@Apellido1", user.PERSONA.Apeliido1);
                    sqlCommand.Parameters.AddWithValue("@Apellido2", user.PERSONA.Apellido2);
                    sqlCommand.Parameters.AddWithValue("@Fecha_Nacimiento", user.PERSONA.Fecha_Nacimiento);
                    sqlCommand.Parameters.AddWithValue("@Fecha_Ingreso", user.EMPLEADO.Fecha_Ingreso);
                    sqlCommand.Parameters.AddWithValue("@Salario", user.EMPLEADO.Salario);
                    sqlCommand.Parameters.AddWithValue("@Id_Administrador", user.EMPLEADO.Jefatura_Id_Empleado);
                    sqlCommand.Parameters.AddWithValue("@Correo", user.USUARIO.Correo);
                    sqlCommand.Parameters.AddWithValue("@Clave", user.USUARIO.Clave);
                    sqlCommand.Parameters.AddWithValue("@Activo", user.USUARIO.Activo);
                    sqlCommand.Parameters.AddWithValue("@Bloqueado", user.USUARIO.Bloqueado);
                    sqlCommand.Parameters.AddWithValue("@Otras_Sennas", user.DIRECCION.Otras_sennas);
                    sqlCommand.Parameters.AddWithValue("@IdDistrito", user.DIRECCION.Distrito_idDistrito);
                    sqlCommand.Parameters.AddWithValue("@IdCanton", user.DIRECCION.Distrito_Canton_idCanton);
                    sqlCommand.Parameters.AddWithValue("@IdProvincia", user.DIRECCION.Distrito_Canton_Provincia_idProvincia);
                    sqlCommand.Parameters.AddWithValue("@Telefono", user.TELEFONO.idTelefono);
                    sqlCommand.Parameters.AddWithValue("@ActivoTelefono", 1);
                    sqlCommand.Parameters.AddWithValue("@IdTipoCedula", user.PERSONA.Catalago_Cedula_IdCatalago_Cedula);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }


        public DataSet MODIFICAR_INFORMACION_USUARIO(Usuario user)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SP_CrearModificarPersonaEmpleadoUsuario", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Cedula", user.PERSONA.Cedula);
                    sqlCommand.Parameters.AddWithValue("@Nombre", user.PERSONA.Nombre);
                    sqlCommand.Parameters.AddWithValue("@Apellido1", user.PERSONA.Apeliido1);
                    sqlCommand.Parameters.AddWithValue("@Apellido2", user.PERSONA.Apellido2);
                    sqlCommand.Parameters.AddWithValue("@Fecha_Ingreso", user.EMPLEADO.Fecha_Ingreso);
                    sqlCommand.Parameters.AddWithValue("@Salario", user.EMPLEADO.Salario);
                    sqlCommand.Parameters.AddWithValue("@Id_Administrador", user.EMPLEADO.Jefatura_Id_Empleado);
                    sqlCommand.Parameters.AddWithValue("@Correo", user.USUARIO.Correo);
                    sqlCommand.Parameters.AddWithValue("@Activo", user.USUARIO.Activo);
                    sqlCommand.Parameters.AddWithValue("@Bloqueado", user.USUARIO.Bloqueado);
                    sqlCommand.Parameters.AddWithValue("@Direccion", user.DIRECCION.Otras_sennas);
                    sqlCommand.Parameters.AddWithValue("@ID_Distrito", user.DIRECCION.Distrito_idDistrito);
                    sqlCommand.Parameters.AddWithValue("@ID_Provincia", user.DIRECCION.Distrito_Canton_Provincia_idProvincia);
                    sqlCommand.Parameters.AddWithValue("@ID_Canton", user.DIRECCION.Distrito_Canton_idCanton);
                    sqlCommand.Parameters.AddWithValue("@Telefono", user.TELEFONO.idTelefono);
                    sqlCommand.Parameters.AddWithValue("@TelefonoActivo", user.TELEFONO.Activo);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public DataSet Modificar_Datos_Usuario(Usuario user)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SP_ModificarPersonaEmpleadoUsuario", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Cedula", user.PERSONA.Cedula);
                    sqlCommand.Parameters.AddWithValue("@Correo", user.USUARIO.Correo);
                    sqlCommand.Parameters.AddWithValue("@Clave", user.USUARIO.Clave);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public DataSet CalcularPago_Planilla_X_Empleado (Planilla planilla)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SP_CalcularPago_Planilla_X_Empleado", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Cedula", planilla.PERSONA.Cedula);
                    sqlCommand.Parameters.AddWithValue("@Horas_Laboradas", planilla.PLANILLA.Horas_Laboradas);
                    sqlCommand.Parameters.AddWithValue("@ID_Deduccion", planilla.PLANILLA.Catalago_Deducciones_idCatalago_Deducciones);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public DataSet Editar_CalcularPago_Planilla_X_Empleado(Planilla planilla)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SP_Editar_CalcularPago_Planilla_X_Empleado", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@IdPlanilla", planilla.PLANILLA.Id_Planilla);
                    sqlCommand.Parameters.AddWithValue("@Cedula", planilla.PERSONA.Cedula);
                    sqlCommand.Parameters.AddWithValue("@Horas_Laboradas", planilla.PLANILLA.Horas_Laboradas);
                    sqlCommand.Parameters.AddWithValue("@ID_Deduccion", planilla.PLANILLA.Catalago_Deducciones_idCatalago_Deducciones);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public DataSet Calcular_Aguinaldo(persona persona)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SP_Calcular_Aplicar_Aguinaldo", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Cedula", persona.Cedula);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public DataSet Eliminar_Aguinaldo(calculo_aguinaldo aguinaldo)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SP_EliminarCalculoAguinaldo", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@IdAguinaldo", aguinaldo.Id_Aguinaldo);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public DataSet Validar_Dias_Vacaciones(long cedula)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SP_CalcularVacaciones_X_Usuario", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Cedula", cedula);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public DataSet Crear_Vacaciones(Vacaciones vacacion)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SP_CrearVacaciones", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Cedula", vacacion.PERSONA.Cedula);
                    sqlCommand.Parameters.AddWithValue("@Fecha_Inicio", vacacion.VACACION.Fecha_Inicio);
                    sqlCommand.Parameters.AddWithValue("@Cantidad_Dias", vacacion.VACACION.Cantidad_Dias);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public DataSet Modificar_Vacaciones(Vacaciones vacacion)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SP_ModificarVacaciones", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Cedula", vacacion.PERSONA.Cedula);
                    sqlCommand.Parameters.AddWithValue("@Estado", vacacion.APROBACION.Estado);
                    sqlCommand.Parameters.AddWithValue("@Comentario", vacacion.APROBACION.Comentario);
                    sqlCommand.Parameters.AddWithValue("@Fecha_Inicio", vacacion.VACACION.Fecha_Inicio);
                    sqlCommand.Parameters.AddWithValue("@Cantidad_Dias", vacacion.VACACION.Cantidad_Dias);
                    sqlCommand.Parameters.AddWithValue("@IDAprobacion", vacacion.VACACION.Aprobaciones_IdAprobaciones);
                    

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public DataSet Eliminar_Vacaciones(vacacione vacacion)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SP_BorrarVacaciones", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@IDAprobacion", vacacion.Aprobaciones_IdAprobaciones);


                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public DataSet Get_Info_Permisos()
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("Get_Info_Permisos", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;


                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public DataSet Get_Info_HorasExtras()
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("Get_Info_HorasExtras", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;


                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public DataSet Get_Info_Evaluacion()
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("Get_Info_Evaluacion", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;


                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }
    }
}
