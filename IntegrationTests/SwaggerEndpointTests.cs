namespace IntegrationTests
{
    [TestClass]
    public class SwaggerEndpointTests
    {
        [TestMethod]
        public async Task GetSwaggerResourcesFromGatewayReturnsOkStatusCode()
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

            var httpClientQueries = app.CreateHttpClient("queries");
            await resourceNotificationService.WaitForResourceAsync("queries", KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));

            var httpClientGateway = app.CreateHttpClient("gateway");
            await resourceNotificationService.WaitForResourceAsync("gateway", KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));

            // Assert
            var response = await httpClientGateway.GetAsync("/swagger/index.html");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task GetSwaggerResourcesFromQueryApiReturnsOkStatusCode()
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

            var httpClientQueries = app.CreateHttpClient("queries");
            await resourceNotificationService.WaitForResourceAsync("queries", KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));

            var httpClientGateway = app.CreateHttpClient("gateway");
            await resourceNotificationService.WaitForResourceAsync("gateway", KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));

            // Assert
            var response = await httpClientQueries.GetAsync("/swagger/index.html");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task GetSwaggerResourcesFromCommandApiReturnsOkStatusCode()
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

            var httpClientQueries = app.CreateHttpClient("queries");
            await resourceNotificationService.WaitForResourceAsync("queries", KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));

            var httpClientGateway = app.CreateHttpClient("gateway");
            await resourceNotificationService.WaitForResourceAsync("gateway", KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));

            // Assert
            var response = await httpClientCommands.GetAsync("/swagger/index.html");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
