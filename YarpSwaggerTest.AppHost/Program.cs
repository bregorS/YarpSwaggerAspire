var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Commands>("commands");

builder.AddProject<Projects.Queries>("queries");

builder.AddProject<Projects.Gateway>("gateway");

builder.Build().Run();
