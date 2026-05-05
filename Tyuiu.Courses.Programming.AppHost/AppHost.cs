var builder = DistributedApplication.CreateBuilder(args);

var pgUser = builder.AddParameter("postgresUser", secret: true);
var pgPassword = builder.AddParameter("postgresPassword", secret: true);

var postgres = builder.AddPostgres("postgres")
	.WithImage("postgres:17.7-alpine")
	.WithContainerName("database.server")
	.WithEnvironment("POSTGRES_USER", pgUser)
	.WithEnvironment("POSTGRES_PASSWORD", pgPassword)
	.WithEnvironment("POSTGRES_DB", "aspnetappdb")
	.WithVolume("postgres-data", "/var/lib/postgresql/data")
	.WithEndpoint(port: 5432, targetPort: 5432, name: "postgres")
	.WithLifetime(ContainerLifetime.Persistent);

var database = postgres.AddDatabase("aspnetappdb");

builder.AddProject<Projects.Tyuiu_Courses_Programming_Api>("tyuiu-courses-programming-api")
	.WithReference(database);

builder.Build().Run();
