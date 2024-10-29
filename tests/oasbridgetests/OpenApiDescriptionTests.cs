using oasbridge;

namespace oasbridgetests;

public class UnitTest1
{
    /// <summary>
    /// 
    /// </summary>

    [Fact]
    public void BareMinimumTest()
    {
        // Arrange
        var oad = new OpenApiDescription();
        var document = oad.CreateCollection(OpenApiMetadata.Document);
        oad.Add(document, oad.Create(OpenApiMetadata.OpenApi, "3.0.0"));
        
        // Act
        var content = oad.Get<string>(document, OpenApiMetadata.OpenApi);       

        // Assert        
        Assert.NotNull(content);
        Assert.Equal("3.0.0", content.Value);
    }

    [Fact]
    public void VerySimpleOpenApi()
    {
        var oad = new OpenApiDescription();

        // Act
        var document = oad.CreateCollection(OpenApiMetadata.Document);
        
        oad.Add(document, oad.Create(OpenApiMetadata.OpenApi, "3.0.0"));

        var info = oad.Add(document,oad.CreateCollection(OpenApiDocumentation.Info));

        oad.Add(info, oad.Create(OpenApiDocumentation.Title, "Very Simple API"));
        oad.Add(info, oad.Create(OpenApiMetadata.Version, "1.0.0"));
        oad.Add(info, oad.Create(OpenApiDocumentation.Description, "This is a very simple API"));
        
        // Assert
        var content = oad.Get<string>(document, OpenApiMetadata.OpenApi);
        Assert.Equal("3.0.0", content!.Value);

        var infoContent = oad.GetCollection(document, OpenApiDocumentation.Info);
        Assert.NotNull(infoContent);

        var titleContent = oad.Get<string>(infoContent, OpenApiDocumentation.Title);
        Assert.Equal("Very Simple API", titleContent!.Value);
    }

    [Fact]
    public void VerySimpleOpenApiWithPaths()
    {
        var oad = new OpenApiDescription();

        // Act
        var document = oad.CreateCollection(OpenApiMetadata.Document);
        
        oad.Add(document, oad.Create(OpenApiMetadata.OpenApi, "3.0.0"));

        var info = oad.Add(document,oad.CreateCollection(OpenApiDocumentation.Info));

        oad.Add(info, oad.Create(OpenApiDocumentation.Title, "Very Simple API"));
        oad.Add(info, oad.Create(OpenApiMetadata.Version, "1.0.0"));
        oad.Add(info, oad.Create(OpenApiDocumentation.Description, "This is a very simple API"));

        var paths = oad.Add(document, oad.CreateCollection(OpenApiKnownLocation.Paths));
        var path = oad.Add(paths, oad.CreateCollection(OpenApiHttpSemantics.Path));
        oad.Add(path, oad.Create(OpenApiHttpSemantics.PathKey, "/api/hello")); 
        oad.Add(path, oad.Create(OpenApiDocumentation.Description, "This is a very simple API"));
        oad.Add(path, oad.Create(OpenApiHttpSemantics.Get, oad.CreateCollection(OpenApiHttpSemantics.Operation)));
        oad.Add(path, oad.Create(OpenApiHttpSemantics.Post, oad.CreateCollection(OpenApiHttpSemantics.Operation)));

        // Assert
        var content = oad.Get<string>(document, OpenApiMetadata.OpenApi);
        Assert.Equal("3.0.0", content!.Value);

        var infoContent = oad.GetCollection(document, OpenApiDocumentation.Info);
        Assert.NotNull(infoContent);

        var titleContent = oad.Get<string>(infoContent, OpenApiDocumentation.Title);
        Assert.Equal("Very Simple API", titleContent!.Value);

        var pathsContent = oad.GetCollection(document, OpenApiKnownLocation.Paths);
        Assert.NotNull(pathsContent);

        var pathContent = oad.GetCollection(pathsContent, OpenApiHttpSemantics.Path);
        Assert.NotNull(pathContent);

        var pathValue = oad.Get<string>(pathContent, OpenApiHttpSemantics.PathKey);
        Assert.Equal("/api/hello", pathValue!.Value);
    }

    [Fact]
    public void CreateOpenApiWithServerUrls() {

        var oad = new OpenApiDescription();

        // Act
        var document = oad.CreateCollection(OpenApiMetadata.Document);
        
        oad.Add(document, oad.Create(OpenApiMetadata.OpenApi, "3.0.0"));


        var servers = oad.Add(document, oad.CreateCollection(OpenApiKnownLocation.Servers));
        var server = oad.Add(servers, oad.CreateCollection(OpenApiHttpSemantics.Server));
        oad.Add(server, oad.Create(OpenApiHttpSemantics.Url, "http://localhost:5000"));
        oad.Add(server, oad.Create(OpenApiDocumentation.Description, "Localhost"));

        // Assert
        var content = oad.Get<string>(document, OpenApiMetadata.OpenApi);
        Assert.Equal("3.0.0", content!.Value);

        var serversContent = oad.GetCollection(document, OpenApiKnownLocation.Servers);
        Assert.NotNull(serversContent);

        var serverContent = oad.GetCollection(serversContent, OpenApiHttpSemantics.Server);
        Assert.NotNull(serverContent);

        var serverUrl = oad.Get<string>(serverContent, OpenApiHttpSemantics.Url);
        Assert.Equal("http://localhost:5000", serverUrl!.Value);
    }

    [Fact]
    public void ValidateTitleIsRequired() {
        
        var oad = new OpenApiDescription();
        
        // Act
        var info = oad.CreateCollection(OpenApiDocumentation.Info);

        oad.Add(info, oad.Create(OpenApiMetadata.Version, "1.0.0"));
        oad.Add(info, oad.Create(OpenApiDocumentation.Description, "This is a very simple API"));

        // Assert
        Assert.False(oad.Validate(info, OpenApiV3Constraints.Constraints));
    }

    [Fact]
    public void ValidateUnknownMemberInInfo() {
        
        var oad = new OpenApiDescription();

        // Act
        var info = oad.CreateCollection(OpenApiDocumentation.Info);

        oad.Add(info, oad.Create(OpenApiDocumentation.Title, "Very Simple API"));
        oad.Add(info, oad.Create(OpenApiMetadata.Version, "1.0.0"));
        oad.Add(info, oad.Create(OpenApiHttpSemantics.Url, "http://localhost:5000"));
 
        // Assert
        Assert.False(oad.Validate(info, OpenApiV3Constraints.Constraints));
    }

}