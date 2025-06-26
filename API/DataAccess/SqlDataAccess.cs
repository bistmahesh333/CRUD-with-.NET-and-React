using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;

namespace API.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        // Get database connection
        private IDbConnection GetConnection(string connectionId = "DbConnString")
        {
            return new NpgsqlConnection(_config.GetConnectionString(connectionId));
        }

        // Begin a transaction
        public IDbTransaction BeginTransaction(string connectionId = "DbConnString")
        {
            var connection = GetConnection(connectionId);
            connection.Open();
            return connection.BeginTransaction();
        }

        // Create a PostgreSQL parameter
        public NpgsqlParameter GetPgParam(string paramName, NpgsqlDbType paramDBType, ParameterDirection paramDirection, object paramValue)
        {
            return new NpgsqlParameter
            {
                ParameterName = paramName,
                NpgsqlDbType = paramDBType,
                Direction = paramDirection,
                Value = paramValue ?? DBNull.Value
            };
        }

        // Execute query and return multiple records (normal result)
        public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "DbConnString", CommandType commandType = CommandType.Text)
        {
            using var connection = GetConnection(connectionId);
            return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: commandType);
        }

        // Return a single row result
        public async Task<T> LoadSingleDataAsync<T, U>(string storedProcedure, U parameters, string connectionId = "DbConnString", CommandType commandType = CommandType.Text)
        {
            using var connection = GetConnection(connectionId);
            return await connection.QueryFirstOrDefaultAsync<T>(storedProcedure, parameters, commandType: commandType);
        }

        // Same as above but synchronous
        public T LoadSingleData<T, U>(string storedProcedure, U parameters, string connectionId = "DbConnString", CommandType commandType = CommandType.Text)
        {
            using var connection = GetConnection(connectionId);
            return connection.QueryFirstOrDefault<T>(storedProcedure, parameters, commandType: commandType);
        }

        // For executing a command (insert/update/delete) with optional transaction
        public async Task ExecuteAsync<T>(string storedProcedure, T parameters, string connectionId = "DbConnString", CommandType commandType = CommandType.Text, IDbTransaction? transaction = null)
        {
            using var connection = transaction == null ? GetConnection(connectionId) : transaction.Connection;
            await connection.ExecuteAsync(storedProcedure, parameters, commandType: commandType, transaction: transaction);
        }

        // Execute and return scalar result (e.g., inserted id)
        public async Task<U> ExecuteScalarAsync<T, U>(string storedProcedure, T parameters, string connectionId = "DbConnString", CommandType commandType = CommandType.Text, IDbTransaction? transaction = null)
        {
            using var connection = transaction == null ? GetConnection(connectionId) : transaction.Connection;
            return await connection.ExecuteScalarAsync<U>(storedProcedure, parameters, commandType: commandType, transaction: transaction);
        }

        // Load multiple records from a refcursor (PostgreSQL specific)
        public async Task<IEnumerable<T>> LoadDataRefCursor<T, U>(string storedProcedure, U parameters, string connectionId = "DbConnString", CommandType commandType = CommandType.Text)
        {
            using var connection = (NpgsqlConnection)GetConnection(connectionId);
            await connection.OpenAsync();

            using var cmd = new NpgsqlCommand(storedProcedure, connection)
            {
                CommandType = commandType
            };

            foreach (var prop in parameters?.GetType().GetProperties() ?? Array.Empty<System.Reflection.PropertyInfo>())
            {
                cmd.Parameters.AddWithValue(prop.Name, prop.GetValue(parameters) ?? DBNull.Value);
            }

            using var reader = await cmd.ExecuteReaderAsync();
            var result = new List<T>();
            while (await reader.ReadAsync())
            {
                result.Add(reader.GetFieldValue<T>(0));
            }

            return result;
        }

        // Return a single object from refcursor
        public async Task<T> LoadSingleDataRefCursor<T, U>(string storedProcedure, U parameters, string connectionId = "DbConnString", CommandType commandType = CommandType.Text)
        {
            var data = await LoadDataRefCursor<T, U>(storedProcedure, parameters, connectionId, commandType);
            return data.FirstOrDefault()!;
        }
    }
}
