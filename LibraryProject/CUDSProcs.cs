// Licence file C:\Users\afm\Documents\ReversePOCO.txt not found.
// Please obtain your licence file at www.ReversePOCO.co.uk, and place it in your documents folder shown above.
// Defaulting to Trial version.


// ------------------------------------------------------------------------------------------------
// WARNING: Failed to load provider "System.Data.SqlClient" - Cannot open database "Northwind" requested by the login. The login failed. Login failed for user 'DESKTOP-H3CFCGF\afm'.
// Allowed providers:
//    "System.Data.Odbc"
//    "System.Data.OleDb"
//    "System.Data.OracleClient"
//    "System.Data.SqlClient"
//    "Microsoft.SqlServerCe.Client.4.0"

/*   at System.Data.SqlClient.SqlInternalConnectionTds..ctor(DbConnectionPoolIdentity identity, SqlConnectionString connectionOptions, SqlCredential credential, Object providerInfo, String newPassword, SecureString newSecurePassword, Boolean redirectedUserInstance, SqlConnectionString userConnectionOptions, SessionData reconnectSessionData, DbConnectionPool pool, String accessToken, Boolean applyTransientFaultHandling, SqlAuthenticationProviderManager sqlAuthProviderManager)
   at System.Data.SqlClient.SqlConnectionFactory.CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, Object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection, DbConnectionOptions userOptions)
   at System.Data.ProviderBase.DbConnectionFactory.CreatePooledConnection(DbConnectionPool pool, DbConnection owningObject, DbConnectionOptions options, DbConnectionPoolKey poolKey, DbConnectionOptions userOptions)
   at System.Data.ProviderBase.DbConnectionPool.CreateObject(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
   at System.Data.ProviderBase.DbConnectionPool.UserCreateRequest(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
   at System.Data.ProviderBase.DbConnectionPool.TryGetConnection(DbConnection owningObject, UInt32 waitForMultipleObjectsTimeout, Boolean allowCreate, Boolean onlyOneCheckConnection, DbConnectionOptions userOptions, DbConnectionInternal& connection)
   at System.Data.ProviderBase.DbConnectionPool.TryGetConnection(DbConnection owningObject, TaskCompletionSource`1 retry, DbConnectionOptions userOptions, DbConnectionInternal& connection)
   at System.Data.ProviderBase.DbConnectionFactory.TryGetConnection(DbConnection owningConnection, TaskCompletionSource`1 retry, DbConnectionOptions userOptions, DbConnectionInternal oldConnection, DbConnectionInternal& connection)
   at System.Data.ProviderBase.DbConnectionInternal.TryOpenConnectionInternal(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource`1 retry, DbConnectionOptions userOptions)
   at System.Data.SqlClient.SqlConnection.TryOpenInner(TaskCompletionSource`1 retry)
   at System.Data.SqlClient.SqlConnection.TryOpen(TaskCompletionSource`1 retry)
   at System.Data.SqlClient.SqlConnection.Open()
   at Microsoft.VisualStudio.TextTemplating1A7FF5DE2D50A0036F15CB161C726C6EBEAC3C0C30C3EFA105BC4D1DAD67F77D613E3143DDD63198DE09400FAECB3755C49A689AD5158D6FA41ACC12225185BC.GeneratedTextTransformation.DatabaseReader.Init() in C:\Users\afm\source\repos\LibraryProject\LibraryProject\EF.Reverse.POCO.v3.ttinclude:line 12212
   at Microsoft.VisualStudio.TextTemplating1A7FF5DE2D50A0036F15CB161C726C6EBEAC3C0C30C3EFA105BC4D1DAD67F77D613E3143DDD63198DE09400FAECB3755C49A689AD5158D6FA41ACC12225185BC.GeneratedTextTransformation.SqlServerDatabaseReader.Init() in C:\Users\afm\source\repos\LibraryProject\LibraryProject\EF.Reverse.POCO.v3.ttinclude:line 15616
   at Microsoft.VisualStudio.TextTemplating1A7FF5DE2D50A0036F15CB161C726C6EBEAC3C0C30C3EFA105BC4D1DAD67F77D613E3143DDD63198DE09400FAECB3755C49A689AD5158D6FA41ACC12225185BC.GeneratedTextTransformation.Generator.Init(DatabaseReader databaseReader, String singleDbContextSubNamespace) in C:\Users\afm\source\repos\LibraryProject\LibraryProject\EF.Reverse.POCO.v3.ttinclude:line 4141
   at Microsoft.VisualStudio.TextTemplating1A7FF5DE2D50A0036F15CB161C726C6EBEAC3C0C30C3EFA105BC4D1DAD67F77D613E3143DDD63198DE09400FAECB3755C49A689AD5158D6FA41ACC12225185BC.GeneratedTextTransformation.GeneratorFactory.Create(FileManagementService fileManagementService, Type fileManagerType, String singleDbContextSubNamespace) in C:\Users\afm\source\repos\LibraryProject\LibraryProject\EF.Reverse.POCO.v3.ttinclude:line 6293*/
// ------------------------------------------------------------------------------------------------

