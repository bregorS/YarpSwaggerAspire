using ClientAPI;
using Shouldly;

namespace IntegrationTests;

[TestClass]
public class SwaggerClientTests()
{
    [TestMethod]
    public async Task PostFileUploadReturnsCreatedStatusCode()
    {
        // Arrange
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.YarpSwaggerTest_AppHost>();
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
            {
                clientBuilder.AddStandardResilienceHandler();
            });
        await using var app = await appHost.BuildAsync();
        var resourceNotificationService = app.Services.GetRequiredService<ResourceNotificationService>();
        await app.StartAsync();

        // Act
        var httpClientCommands = app.CreateHttpClient("commands");
        await resourceNotificationService.WaitForResourceAsync("commands", KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));

        var sut = new AdminClient(httpClientCommands);
        var file = File.ReadAllBytes("LoansReview.txt");
        var response = await sut.CreateDocumentAsync("LoansReview.txt", "Test file", ["tag1", "tag2"],
            new FileParameter(new MemoryStream(file), "LoansReview.txt", ""));

        response.StatusCode.ShouldBe((int)HttpStatusCode.Created);
    }
}
