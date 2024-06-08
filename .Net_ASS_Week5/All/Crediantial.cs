namespace VisitorSystem
{
    public class Crediantial
    {
        public static readonly string DatabaseName = Environment.GetEnvironmentVariable("databaseName");
        public static readonly string ContainerName = Environment.GetEnvironmentVariable("containerName");
        public static readonly string CosmosEndpoint = Environment.GetEnvironmentVariable("cosmosUrl");
        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("primaryKey");
        public static readonly string ApiKey = Environment.GetEnvironmentVariable("apiKey");
    }
}
