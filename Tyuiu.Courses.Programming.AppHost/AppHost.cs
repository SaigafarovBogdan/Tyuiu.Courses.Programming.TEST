var builder = DistributedApplication.CreateBuilder(args);

var pgPassword = builder.AddParameter("postgresPassword", secret: true);

var postgres = builder.AddPostgres("postgres", password: pgPassword)
	.WithImage("postgres:17.7-alpine")
	.WithContainerName("database.server")
	.WithEnvironment("POSTGRES_DB", "aspnetappdb")
	.WithVolume("postgres-data", "/var/lib/postgresql/data")
	.WithEndpoint(port: 6033, targetPort: 5432, name: "out", isProxied: false)
	.WithLifetime(ContainerLifetime.Persistent);
var database = postgres.AddDatabase("aspnetappdb");

builder.AddProject<Projects.Tyuiu_Courses_Programming_Api>("tyuiu-courses-programming-api")
	.WithReference(database)
	.WaitFor(database);

builder.Build().Run();
