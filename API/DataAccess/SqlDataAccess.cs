using Dapper;
using System.Data;
using Npgsql;
using NpgsqlTypes;
using System.Reflection;

namespace DataAccess
{
    public class SqlDataAccess : ISqlDataAccess, IDisposable
    {
        private readonly IConfiguration _config;
        private IDbConnection _connection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }
        public IDbTransaction BeginTransaction(string connectionId = "DbConnString")
        {
            _connection = new NpgsqlConnection(_config.GetConnectionString(connectionId));
            _connection.Open();
            var transaction = _connection.BeginTransaction();
            return transaction;
        }

        #region LoadData
        public async Task<IEnumerable<T>> LoadDataRefCursor<T, U>(
            string storedProcedure,
            U parameters,
            string connectionId = "DbConnString",
            CommandType commandType = CommandType.Text)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            using IDbConnection connection = new NpgsqlConnection(_config.GetConnectionString(connectionId));
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            var resultsReference =
                (IDictionary<string, object>)
                connection.Query<dynamic>(storedProcedure, parameters,
                commandType: commandType, transaction: transaction).Single();
            string resultSetName = (string)resultsReference[resultsReference.Keys.First()];
            string resultSetReferenceCommand = string.Format(@"FETCH ALL IN ""{0}""", resultSetName);

            var result = await connection.QueryAsync<T>(resultSetReferenceCommand,
                null, commandType: CommandType.Text, transaction: transaction);

            transaction.Commit();
            return result;
        }

        public async Task<T> LoadSingleDataRefCursor<T, C, U>(
           string storedProcedure,
           U parameters,
           string split,
           string connectionId = "DbConnString",
           CommandType commandType = CommandType.Text)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            using IDbConnection connection = new NpgsqlConnection(_config.GetConnectionString(connectionId));
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            var resultsReference =
                (IDictionary<string, object>)
                connection.Query<dynamic>(storedProcedure, parameters,
                commandType: commandType, transaction: transaction).Single();
            string resultSetName = (string)resultsReference[resultsReference.Keys.First()];
            string resultSetReferenceCommand = string.Format(@"FETCH FIRST IN ""{0}""", resultSetName);

            var result = await connection.QueryAsync<T, C, T>(resultSetReferenceCommand, (T parent, C child) =>
            {
                List<PropertyInfo> props = parent.GetType().GetProperties().ToList();
                PropertyInfo? prop = props.FirstOrDefault(x => x.PropertyType == typeof(C));
                prop.SetValue(parent, child);

                return parent;
            },
            null, splitOn: split, commandType: CommandType.Text, transaction: transaction);

            transaction.Commit();
            return result.SingleOrDefault();
        }

        public async Task<T> LoadSingleDataRefCursor<T, U>(
            string storedProcedure,
            U parameters,
            string connectionId = "DbConnString",
            CommandType commandType = CommandType.Text)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            using IDbConnection connection = new NpgsqlConnection(_config.GetConnectionString(connectionId));
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            var resultsReference =
                (IDictionary<string, object>)
                connection.Query<dynamic>(storedProcedure, parameters,
                commandType: commandType, transaction: transaction).Single();
            string resultSetName = (string)resultsReference[resultsReference.Keys.First()];
            string resultSetReferenceCommand = string.Format(@"FETCH FIRST IN ""{0}""", resultSetName);

            var result = await connection.QuerySingleOrDefaultAsync<T>(resultSetReferenceCommand,
                null, commandType: CommandType.Text, transaction: transaction);

            transaction.Commit();
            return result;
        }


        public async Task<IEnumerable<T>> LoadData<T, U>(
            string storedProcedure,
            U parameters,
            string connectionId = "DbConnString",
            CommandType commandType = CommandType.Text)
        {

            using IDbConnection connection = new NpgsqlConnection(_config.GetConnectionString(connectionId));
            //return await connection.QueryAsync<T>(storedProcedure, parameters,
            //    commandType: CommandType.StoredProcedure);

            var val = await connection.QueryAsync<T>(storedProcedure, parameters,
                commandType: CommandType.Text);
            return val;
            //connection.CommandText = "fetch all in \"" + refString + "\"";
            //connection.CommandType = CommandType.Text;
        }

        public async Task<T> LoadSingleDataAsync<T, U>(
            string storedProcedure,
            U parameters,
            string connectionId = "DbConnString",
            CommandType commandType = CommandType.Text)
        {
            using IDbConnection connection = new NpgsqlConnection(_config.GetConnectionString(connectionId));
            return await connection.QueryFirstOrDefaultAsync<T>(storedProcedure, parameters,
                commandType: CommandType.Text);
        }

        public T LoadSingleData<T, U>(
            string storedProcedure,
            U parameters,
            string connectionId = "DbConnString",
            CommandType commandType = CommandType.Text)
        {
            using IDbConnection connection = new NpgsqlConnection(_config.GetConnectionString(connectionId));
            return connection.QueryFirstOrDefault<T>(storedProcedure, parameters,
                commandType: CommandType.Text);
        }
        #endregion


        #region LoadDataWithChild
        public async Task<IEnumerable<T>> LoadDataRefCursor<T, C, U>(
            string storedProcedure,
            U parameters,
            string split = "",
            string connectionId = "DbConnString",
            CommandType commandType = CommandType.Text)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            using IDbConnection connection = new NpgsqlConnection(_config.GetConnectionString(connectionId));
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            var resultsReference =
                (IDictionary<string, object>)
                connection.Query<dynamic>(storedProcedure, parameters,
                commandType: commandType, transaction: transaction).Single();
            string resultSetName = (string)resultsReference[resultsReference.Keys.First()];
            string resultSetReferenceCommand = string.Format(@"FETCH ALL IN ""{0}""", resultSetName);

            var result = await connection.QueryAsync<T, C, T>(resultSetReferenceCommand, (T parent, C child) =>
            {
                List<PropertyInfo> props = parent.GetType().GetProperties().ToList();
                PropertyInfo? prop = props.FirstOrDefault(x => x.PropertyType == typeof(C));
                prop.SetValue(parent, child);

                return parent;
            },
                null, splitOn: split, commandType: CommandType.Text, transaction: transaction);

            transaction.Commit();
            return result;
        }

        //load data with 2 children 
        public async Task<IEnumerable<T>> LoadDataRefCursor<T, C1, C2, U>(
           string storedProcedure,
           U parameters,
           string split = "",
           string connectionId = "DbConnString",
           CommandType commandType = CommandType.Text)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            using IDbConnection connection = new NpgsqlConnection(_config.GetConnectionString(connectionId));
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            var resultsReference =
                (IDictionary<string, object>)
                connection.Query<dynamic>(storedProcedure, parameters,
                commandType: commandType, transaction: transaction).Single();
            string resultSetName = (string)resultsReference[resultsReference.Keys.First()];
            string resultSetReferenceCommand = string.Format(@"FETCH ALL IN ""{0}""", resultSetName);
            IEnumerable<T>? result;
            result = await connection.QueryAsync<T, C1, C2, T>(resultSetReferenceCommand, (T parent, C1 child1, C2 child2) =>
            {
                List<PropertyInfo> props = parent.GetType().GetProperties().ToList();
                PropertyInfo? prop1 = props.FirstOrDefault(x => x.PropertyType == typeof(C1));
                prop1.SetValue(parent, child1);

                PropertyInfo? prop2 = props.FirstOrDefault(x => x.PropertyType == typeof(C2));
                prop2.SetValue(parent, child2);

                return parent;
            },
                null, splitOn: split, commandType: CommandType.Text, transaction: transaction);

            transaction.Commit();

            return result;
        }

        //load data with 3 children 
        public async Task<IEnumerable<T>> LoadDataRefCursor<T, C1, C2, C3, U>(
           string storedProcedure,
           U parameters,
           string split = "",
           string connectionId = "DbConnString",
           CommandType commandType = CommandType.Text)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            using IDbConnection connection = new NpgsqlConnection(_config.GetConnectionString(connectionId));
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            var resultsReference =
                (IDictionary<string, object>)
                connection.Query<dynamic>(storedProcedure, parameters,
                commandType: commandType, transaction: transaction).Single();
            string resultSetName = (string)resultsReference[resultsReference.Keys.First()];
            string resultSetReferenceCommand = string.Format(@"FETCH ALL IN ""{0}""", resultSetName);
            IEnumerable<T>? result;
            result = await connection.QueryAsync<T, C1, C2, C3, T>(resultSetReferenceCommand, (T parent, C1 child1, C2 child2, C3 child3) =>
            {
                List<PropertyInfo> props = parent.GetType().GetProperties().ToList();
                PropertyInfo? prop1 = props.FirstOrDefault(x => x.PropertyType == typeof(C1));
                prop1.SetValue(parent, child1);

                PropertyInfo? prop2 = props.FirstOrDefault(x => x.PropertyType == typeof(C2));
                prop2.SetValue(parent, child2);

                PropertyInfo? prop3 = props.FirstOrDefault(x => x.PropertyType == typeof(C3));
                prop3.SetValue(parent, child3);

                return parent;
            },
                null, splitOn: split, commandType: CommandType.Text, transaction: transaction);

            transaction.Commit();

            return result;
        }
        #endregion

        #region Execute
        public async Task ExecuteAsync<T>(
           string storedProcedure,
           T parameters,
           string connectionId = "DbConnString",
           CommandType commandType = CommandType.Text,
           IDbTransaction? transaction = null
       )
        {
            using IDbConnection connection = new NpgsqlConnection(_config.GetConnectionString(connectionId));
            if (transaction == null)
                await connection.ExecuteAsync(storedProcedure, parameters,
                    commandType: commandType);
            else
                await _connection.ExecuteAsync(storedProcedure, parameters,
                commandType: commandType, transaction: transaction);
        }
        public async Task<U> ExecuteScalarAsync<T, U>(
            string storedProcedure,
            T parameters,
            string connectionId = "DbConnString",
            CommandType commandType = CommandType.Text,
            IDbTransaction? transaction = null
        )
        {
            //ToDo: Need to optimize the process
            U retVal;
            if (transaction == null)
            {
                using IDbConnection connection = new NpgsqlConnection(_config.GetConnectionString(connectionId));
                retVal = await connection.ExecuteScalarAsync<U>(storedProcedure, parameters,
                    commandType: commandType);
            }
            else
                retVal = await _connection.ExecuteScalarAsync<U>(storedProcedure, parameters,
                commandType: commandType, transaction: transaction);
            return retVal;
        }
        #endregion

        public NpgsqlParameter GetPgParam(string paramName, NpgsqlDbType paramDBType, ParameterDirection paramDirection, object paramValue)
        {
            NpgsqlParameter p = new NpgsqlParameter();
            p.ParameterName = paramName;
            p.NpgsqlDbType = paramDBType;
            p.Direction = paramDirection;
            p.Value = paramValue;
            return p;
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _connection?.Close();
        }
    }
}