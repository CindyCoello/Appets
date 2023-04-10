using Appets.DataAccess.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Appets.DataAccess.Repositories
{
    public class UsuariosRepository
    {
        public tbUsuarios Login(string contraseña, string identidad)
        {
            const string SqlQuery = "UDP_tbUsuario_Login";
            var result = new tbUsuarios();
            var parameters = new DynamicParameters();
            parameters.Add("@usu_Identidad", identidad, DbType.String, ParameterDirection.Input);
            parameters.Add("@usu_Contraseña", contraseña, DbType.String, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                result = db.QueryFirstOrDefault<tbUsuarios>(SqlQuery, parameters, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public IEnumerable<UDP_tbUsuarios_SelectResult> UsuariosList()
        {
            const string query = @"UDP_tbUsuarios_Select";
            var parameters = new DynamicParameters();
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<UDP_tbUsuarios_SelectResult>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;
            }
        }


        public UDP_tbUsuarios_SelectResult Find(int id)
        {
            const string query = @"UDP_tbUsuarios_Find";
            var parameters = new DynamicParameters();
            parameters.Add("@usu_Id", id, DbType.Int32, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.QueryFirstOrDefault<UDP_tbUsuarios_SelectResult>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;
            }
        }


        public tbUsuarios ActualizarContraseña(string contraseña, int id, string identidad)
        {
            var resultado = new tbUsuarios();
            const string sqlQuery = @"UDP_tbUsuarios_UpdateContraseña";

            var parameters = new DynamicParameters();
            parameters.Add("@usu_Contraseña", contraseña, DbType.String, ParameterDirection.Input);
            parameters.Add("@usu_Id", id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@usu_Identidad", identidad, DbType.String, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<tbUsuarios>(sqlQuery, parameters, transaction, commandType: CommandType.StoredProcedure);
                    if (resultado == null)
                    {

                    }
                    else
                    {
                        goto errorTransaction;
                    }
                    transaction.Commit();
                    db.Close();
                    db.Dispose();
                    return resultado;

                errorTransaction:
                    transaction.Rollback();
                    db.Close();
                    db.Dispose();
                    return resultado;
                }
            }
        }

        public int Insert(
            string usu_Identidad,
            string usu_PrimerNombre,
            string usu_PrimerApellido,
            string usu_SegundoNombre,
            string usu_SegundoApellido,
            string usu_Telefono,
            string usu_Contraseña,
            int rol_Id)
        {
            int resultado = 0;
            const string sqlQueryEspc = "UDP_tbUsuarios_Insert";

            var parameterEspc = new DynamicParameters();
            parameterEspc.Add("@usu_Identidad", usu_Identidad, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@usu_PrimerNombre", usu_PrimerNombre, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@usu_PrimerApellido", usu_PrimerApellido, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@usu_SegundoNombre", usu_SegundoNombre, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@usu_SegundoApellido", usu_SegundoApellido, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@usu_Telefono", usu_Telefono, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@usu_Contraseña", usu_Contraseña, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@rol_Id", rol_Id, DbType.Int32, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<int>(sqlQueryEspc, parameterEspc, transaction, commandType: CommandType.StoredProcedure);
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

        public int Update(
            int usu_Id, 
            string usu_Identidad,
            string usu_PrimerNombre,
            string usu_PrimerApellido,
            string usu_SegundoNombre,
            string usu_SegundoApellido,
            string usu_Telefono,
            int rol_Id)
        {
            int resultado = 0;
            const string sqlQueryEspc = "UDP_tbUsuarios_Update";

            var parameterEspc = new DynamicParameters();
            parameterEspc.Add("@usu_Id", usu_Id, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@usu_Identidad", usu_Identidad, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@usu_PrimerNombre", usu_PrimerNombre, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@usu_PrimerApellido", usu_PrimerApellido, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@usu_SegundoNombre", usu_SegundoNombre, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@usu_SegundoApellido", usu_SegundoApellido, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@usu_Telefono", usu_Telefono, DbType.String, ParameterDirection.Input);
            //parameterEspc.Add("@usu_Contraseña", usu_Contraseña, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@rol_Id", rol_Id, DbType.Int32, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<int>(sqlQueryEspc, parameterEspc, transaction, commandType: CommandType.StoredProcedure);
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
                    return usu_Id;

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
