using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("api/v1/documents", async ([FromForm] CreateDocumentRequest request) =>
{
    Console.WriteLine("CreateDocument called");
    return TypedResults.Created();
})
.Accepts<CreateDocumentRequest>("multipart/form-data")
.WithName("CreateDocument")
.WithTags("Document")
.WithDescription("Create a new system wide document e.g. Policy Conditions")
.Produces(StatusCodes.Status201Created)
.Produces(StatusCodes.Status401Unauthorized)
.Produces(StatusCodes.Status403Forbidden)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi()
.DisableAntiforgery();
;

app.Run();
