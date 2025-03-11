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

app.MapGet("api/v1/documents", async (string? searchTerm, string? sortColumn, bool? sortByAscending, int? page, int? pageSize) =>
{
    return new List<Queries.Document>();
})
.WithName("GetAllDocuments")
.WithTags("Document")
.WithDescription("Get a list of documents including all revisions")
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status401Unauthorized)
.Produces(StatusCodes.Status403Forbidden)
.Produces(StatusCodes.Status500InternalServerError)
.Produces<List<Queries.Document>>(StatusCodes.Status200OK)
.WithOpenApi()
;

app.MapGet("api/v1/documents/{id}", async (Guid id) =>
{
    return new List<Queries.Document>();
})
.WithName("GetDocument")
.WithTags("Document")
.WithDescription("Get a single document by id")
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status401Unauthorized)
.Produces(StatusCodes.Status403Forbidden)
.Produces(StatusCodes.Status500InternalServerError)
.Produces<Queries.Document>(StatusCodes.Status200OK)
.WithOpenApi()
;

app.Run();