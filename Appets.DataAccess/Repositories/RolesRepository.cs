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
    public class RolesRepository
    {
        public IEnumerable<tbRoles> RolesList()
        {
            const string query = @"UDP_tbRoles_Select";
            var parameters = new DynamicParameters();


            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<tbRoles>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;

            }

        }



        public int Insert(tbRoles roles, IEnumerable<int> modulesItems)
        {
            int resultado = 0;
            const string sqlQueryRole = "UDP_Roles_Insert";
            const string sqlQueryRolePantallas = "UDP_tbRolesPantallas_Insert";
            var parameterRole = new DynamicParameters();
            parameterRole.Add("@rol_Nombre", roles.rol_Nombre, DbType.String, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    roles.rol_Id = db.ExecuteScalar<int>(sqlQueryRole, parameterRole, transaction, commandType: CommandType.StoredProcedure);
                    if (roles.rol_Id > 0)
                    {
                        var parametersRolePantallas = new DynamicParameters();
                        parametersRolePantallas.Add("@rol_Id", roles.rol_Id, DbType.Int32, ParameterDirection.Input);
                        foreach (var item in modulesItems)
                        {
                            parametersRolePantallas.Add("@modpan_Id", item, DbType.Int32, ParameterDirection.Input);
                            resultado = db.ExecuteScalar<int>(sqlQueryRolePantallas, parametersRolePantallas, transaction, commandType: CommandType.StoredProcedure);
                            if (resultado == 0)
                            {
                            }
                            else
                            {
                                goto errorTransaction;
                            }
                        }
                    }
                    else
                    {
                        goto errorTransaction;
                    }
                    transaction.Commit();
                    db.Close();
                    db.Dispose();
                    return roles.rol_Id;

                errorTransaction:
                    transaction.Rollback();
                    db.Close();
                    db.Dispose();
                    return -1;
                }
            }
        }



        public int Update(tbRoles roles, IEnumerable<int> modulesItems)
        {
            int resultadoInsert = 0;
            int resultadoUpdate = 0;
            int resultadoDelete = 0;
            const string sqlQueryRole = "UDP_tbRoles_Update";
            const string sqlQueryDeleteRolePantallas = "UDP_tbRolesPantallas_Delete";
            const string sqlQueryInsertRolePantallas = "UDP_tbRolesPantallas_Insert";

            var parameterRole = new DynamicParameters();

            parameterRole.Add("@rol_Id", roles.rol_Id, DbType.Int32, ParameterDirection.Input);
            parameterRole.Add("@rol_Nombre", roles.rol_Nombre, DbType.String, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    var parameterDelete = new DynamicParameters();
                    parameterDelete.Add("@rol_Id", roles.rol_Id, DbType.Int32, ParameterDirection.Input);
                    resultadoDelete = db.ExecuteScalar<int>(sqlQueryDeleteRolePantallas, parameterDelete, transaction, commandType: CommandType.StoredProcedure);

                    if (resultadoDelete == 0)
                    {
                        resultadoUpdate = db.ExecuteScalar<int>(sqlQueryRole, parameterRole, transaction, commandType: CommandType.StoredProcedure);
                        if (resultadoUpdate == 0)
                        {
                            var parametersRolePantallas = new DynamicParameters();
                            parametersRolePantallas.Add("@rol_Id", roles.rol_Id, DbType.Int32, ParameterDirection.Input);
                            foreach (var item in modulesItems)
                            {
                                parametersRolePantallas.Add("@modpan_Id", item, DbType.Int32, ParameterDirection.Input);
                                resultadoInsert = db.ExecuteScalar<int>(sqlQueryInsertRolePantallas, parametersRolePantallas, transaction, commandType: CommandType.StoredProcedure);
                                if (resultadoInsert == 0)
                                {
                                }
                                else
                                {
                                    goto errorTransaction;
                                }
                            }
                        }
                        else
                        {
                            goto errorTransaction;
                        }
                    }
                    else
                    {
                        goto errorTransaction;
                    }
                    transaction.Commit();
                    db.Close();
                    db.Dispose();
                    return roles.rol_Id;

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
