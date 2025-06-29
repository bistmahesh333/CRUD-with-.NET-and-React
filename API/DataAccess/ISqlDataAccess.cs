using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace DataAccess
{
    public interface ISqlDataAccess
    {
        IDbTransaction BeginTransaction(string connectionId = "DbConnString");
        NpgsqlParameter GetPgParam(string paramName, NpgsqlDbType paramDBType, ParameterDirection paramDirection, object paramValue);
        Task<IEnumerable<T>> LoadDataRefCursor<T, U>(
            string storedProcedure,
            U parameters,
            string connectionId = "DbConnString",
            CommandType commandType = CommandType.Text);
        Task<T> LoadSingleDataRefCursor<T, U>(
            string storedProcedure,
            U parameters,
            string connectionId = "DbConnString",
            CommandType commandType = CommandType.Text);
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "DbConnString", CommandType commandType = CommandType.Text);
        Task<T> LoadSingleDataAsync<T, U>(string storedProcedure, U parameters, string connectionId = "DbConnString", CommandType commandType = CommandType.Text);
        T LoadSingleData<T, U>(string storedProcedure, U parameters, string connectionId = "DbConnString", CommandType commandType = CommandType.Text);

        Task ExecuteAsync<T>
            (string storedProcedure, T parameters, string connectionId = "DbConnString", CommandType commandType = CommandType.Text, IDbTransaction? transaction = null);
        Task<U> ExecuteScalarAsync<T, U>
            (string storedProcedure, T parameters, string connectionId = "DbConnString", CommandType commandType = CommandType.Text, IDbTransaction? transaction = null);


        Task<T> LoadSingleDataRefCursor<T, C, U>(
        string storedProcedure,
        U parameters,
        string split,
        string connectionId = "DbConnString",
        CommandType commandType = CommandType.Text);

        Task<IEnumerable<T>> LoadDataRefCursor<T, C, U>(
            string storedProcedure,
            U parameters,
            string split = "",
            string connectionId = "DbConnString",
            CommandType commandType = CommandType.Text);

        Task<IEnumerable<T>> LoadDataRefCursor<T, C1, C2, U>(
          string storedProcedure,
          U parameters,
          string split = "",
          string connectionId = "DbConnString",
          CommandType commandType = CommandType.Text);

        Task<IEnumerable<T>> LoadDataRefCursor<T, C1, C2, C3, U>(
           string storedProcedure,
           U parameters,
           string split = "",
           string connectionId = "DbConnString",
           CommandType commandType = CommandType.Text);
    }
}