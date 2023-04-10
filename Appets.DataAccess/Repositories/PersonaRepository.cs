using Appets.DataAccess.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Appets.DataAccess.Repositories
{
    public class PersonaRepository
    {
        public IEnumerable<tbPersonas> PersonaList()
        {
            const string query = @"UDP_tbPersonas_Select";
            var parameters = new DynamicParameters();


            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<tbPersonas>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;

            }


        }

        public IEnumerable<UDP_tbPersonas_EsEmpleadoResult> PersonaListado()
        {
            const string query = @"UDP_tbPersonas_EsEmpleado";
            var parameters = new DynamicParameters();


            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<UDP_tbPersonas_EsEmpleadoResult>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;

            }


        }


        public IEnumerable<UDP_tbPersonas_EsDonanteResult> PersonaListado2()
        {
            const string query = @"UDP_tbPersonas_EsDonante";
            var parameters = new DynamicParameters();


            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<UDP_tbPersonas_EsDonanteResult>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;

            }


        }

        public IEnumerable<UDP_tbPersonas_EsVoluntarioResult> PersonaListado3()
        {
            const string query = @"UDP_tbPersonas_EsVoluntario";
            var parameters = new DynamicParameters();


            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<UDP_tbPersonas_EsVoluntarioResult>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;

            }


        }


        public IEnumerable<UDP_tbPersonas_EsAdoptanteResult> PersonaListado4()
        {
            const string query = @"UDP_tbPersonas_EsAdoptante";
            var parameters = new DynamicParameters();


            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<UDP_tbPersonas_EsAdoptanteResult>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;

            }


        }


        public IEnumerable<UDP_tbPersonas_EsEmpleadoResult> PersonaListado5()
        {
            const string query = @"UDP_tbPersonas_EsEmpleado";
            var parameters = new DynamicParameters();


            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<UDP_tbPersonas_EsEmpleadoResult>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;

            }


        }


        public string Delete(int id)
        {
            const string query = @"UDP_tbPersonas_Delete";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.ExecuteScalar<string>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;
            }
        }

        public tbPersonas Find(int id)
        {
            const string query = @"UDP_tbPersonas_Find";
            var parameters = new DynamicParameters();
            parameters.Add("@per_Id", id, DbType.Int32, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.QueryFirstOrDefault<tbPersonas>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;

            }


        }

        public int Insert(
            string per_Identidad,
            string per_Nombres,
            string per_Apellidos,
            int per_Edad,
            DateTime per_FechaNacimiento,
            string per_Domicilio,
            string per_Telefono,
            string per_Correo,
            bool per_EsAdoptante,
            bool donante,
            bool empleado,
            bool voluntario)
        {
            int resultado = 0;
            const string sqlQueryPerson = "UDP_tbPersonas_Insert";

            var parameterPerson = new DynamicParameters();
            parameterPerson.Add("@per_Identidad",per_Identidad, DbType.String, ParameterDirection.Input);
            parameterPerson.Add("@per_Nombres", per_Nombres, DbType.String, ParameterDirection.Input);
            parameterPerson.Add("@per_Apellidos",per_Apellidos, DbType.String, ParameterDirection.Input);
            parameterPerson.Add("@per_Edad",per_Edad, DbType.Int32, ParameterDirection.Input);
            parameterPerson.Add("@per_FechaNacimiento", per_FechaNacimiento  , DbType.Date, ParameterDirection.Input);
            parameterPerson.Add("@per_Domicilio", per_Domicilio, DbType.String, ParameterDirection.Input);
            parameterPerson.Add("@per_Telefono", per_Telefono, DbType.String, ParameterDirection.Input);
            parameterPerson.Add("@per_Correo", per_Correo, DbType.String, ParameterDirection.Input);
            parameterPerson.Add("@per_EsAdoptante",per_EsAdoptante, DbType.Boolean, ParameterDirection.Input);
            parameterPerson.Add("@per_EsDonante", donante, DbType.Boolean, ParameterDirection.Input);
            parameterPerson.Add("@per_EsEmpleado", empleado, DbType.Boolean, ParameterDirection.Input);
            parameterPerson.Add("@per_EsVoluntario", voluntario, DbType.Boolean, ParameterDirection.Input);


            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<int>(sqlQueryPerson, parameterPerson, transaction, commandType: CommandType.StoredProcedure);
                    if (resultado > 0)
                    {

                    }
                    else
                    {
                        goto errorTransaction;
                    }
                    transaction.Commit();
                    db.Close();
                    db.Dispose();
                    return 1;

                errorTransaction:
                    transaction.Rollback();
                    db.Close();
                    db.Dispose();
                    return -1;
                }
            }
        }

        public int Update(int per_Id,
            string per_Identidad,
            string per_Nombres,
            string per_Apellidos, 
            int per_Edad,
            DateTime per_FechaNacimiento,
            string per_Domicilio,
            string per_Telefono,
            string per_Correo,
            bool per_EsAdoptante,
            bool donante,
            bool empleado,
            bool voluntario)
        {
            int resultado = 0;
            const string sqlQueryPerson = "UDP_tbPersona_Update";

            var parameterPerson = new DynamicParameters();
            parameterPerson.Add("@per_Id", per_Id, DbType.Int32, ParameterDirection.Input);
            parameterPerson.Add("@per_Identidad", per_Identidad, DbType.String, ParameterDirection.Input);
            parameterPerson.Add("@per_Nombres", per_Nombres, DbType.String, ParameterDirection.Input);
            parameterPerson.Add("@per_Apellidos", per_Apellidos, DbType.String, ParameterDirection.Input);
            parameterPerson.Add("@per_Edad", per_Edad, DbType.Int32, ParameterDirection.Input);
            parameterPerson.Add("@per_FechaNacimiento", per_FechaNacimiento, DbType.DateTime, ParameterDirection.Input);
            parameterPerson.Add("@per_Domicilio", per_Domicilio, DbType.String, ParameterDirection.Input);
            parameterPerson.Add("@per_Telefono", per_Telefono, DbType.String, ParameterDirection.Input);
            parameterPerson.Add("@per_Correo", per_Correo, DbType.String, ParameterDirection.Input);
            parameterPerson.Add("@per_EsAdoptante", per_EsAdoptante, DbType.Boolean, ParameterDirection.Input);
            parameterPerson.Add("@per_EsDonante", donante, DbType.Boolean, ParameterDirection.Input);
            parameterPerson.Add("@per_EsEmpleado", empleado, DbType.Boolean, ParameterDirection.Input);
            parameterPerson.Add("@per_EsVoluntario", voluntario, DbType.Boolean, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<int>(sqlQueryPerson, parameterPerson, transaction, commandType: CommandType.StoredProcedure);
                    if (resultado == 0)
                    {

                    }
                    else
                    {
                        goto errorTransaction;
                    }
                    transaction.Commit();
                    db.Close();
                    db.Dispose();
                    return per_Id;

                errorTransaction:
                    transaction.Rollback();
                    db.Close();
                    db.Dispose();
                    return -1;
                }
            }
        }
    }
}
