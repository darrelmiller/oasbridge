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
        var document = Oad.CreateCollection(OpenApiMetadata.Document);
        Oad.Add(document, Oad.Create(OpenApiMetadata.OpenApi, "3.0.0"));
        
        // Act
        var content = Oad.Get<string>(document, OpenApiMetadata.OpenApi);       

        // Assert        
        Assert.NotNull(content);
        Assert.Equal("3.0.0", content.Value);
    }

    [Fact]
    public void VerySimpleOpenApi()
    {
        // Act
        var document = Oad.CreateCollection(OpenApiMetadata.Document);
        
        Oad.Add(document, Oad.Create(OpenApiMetadata.OpenApi, "3.0.0"));

        var info = Oad.Add(document,Oad.CreateCollection(OpenApiDocumentation.Info));

        Oad.Add(info, Oad.Create(OpenApiDocumentation.Title, "Very Simple API"));
        Oad.Add(info, Oad.Create(OpenApiMetadata.Version, "1.0.0"));
        Oad.Add(info, Oad.Create(OpenApiDocumentation.Description, "This is a very simple API"));
        
        // Assert
        var content = Oad.Get<string>(document, OpenApiMetadata.OpenApi);
        Assert.Equal("3.0.0", content!.Value);

        var infoContent = Oad.GetCollection(document, OpenApiDocumentation.Info);
        Assert.NotNull(infoContent);

        var titleContent = Oad.Get<string>(infoContent, OpenApiDocumentation.Title);
        Assert.Equal("Very Simple API", titleContent!.Value);
    }

    [Fact]
    public void VerySimpleOpenApiWithPaths()
    {
        // Act
        var document = Oad.CreateCollection(OpenApiMetadata.Document);
        
        Oad.Add(document, Oad.Create(OpenApiMetadata.OpenApi, "3.0.0"));

        var info = Oad.Add(document,Oad.CreateCollection(OpenApiDocumentation.Info));

        Oad.Add(info, Oad.Create(OpenApiDocumentation.Title, "Very Simple API"));
        Oad.Add(info, Oad.Create(OpenApiMetadata.Version, "1.0.0"));
        Oad.Add(info, Oad.Create(OpenApiDocumentation.Description, "This is a very simple API"));

        var paths = Oad.Add(document, Oad.CreateCollection(OpenApiKnownLocation.Paths));
        var path = Oad.Add(paths, Oad.CreateCollection(OpenApiHttpSemantics.Path));
        Oad.Add(path, Oad.Create(OpenApiHttpSemantics.PathKey, "/api/hello")); // This is a duplicate use of the key
        Oad.Add(path, Oad.Create(OpenApiDocumentation.Description, "This is a very simple API"));
        Oad.Add(path, Oad.Create(OpenApiHttpSemantics.Get, Oad.CreateCollection(OpenApiHttpSemantics.Operation)));
        Oad.Add(path, Oad.Create(OpenApiHttpSemantics.Post, Oad.CreateCollection(OpenApiHttpSemantics.Operation)));

        // Assert
        var content = Oad.Get<string>(document, OpenApiMetadata.OpenApi);
        Assert.Equal("3.0.0", content!.Value);

        var infoContent = Oad.GetCollection(document, OpenApiDocumentation.Info);
        Assert.NotNull(infoContent);

        var titleContent = Oad.Get<string>(infoContent, OpenApiDocumentation.Title);
        Assert.Equal("Very Simple API", titleContent!.Value);

        var pathsContent = Oad.GetCollection(document, OpenApiKnownLocation.Paths);
        Assert.NotNull(pathsContent);

        var pathContent = Oad.GetCollection(pathsContent, OpenApiHttpSemantics.Path);
        Assert.NotNull(pathContent);

        var pathValue = Oad.Get<string>(pathContent, OpenApiHttpSemantics.PathKey);
        Assert.Equal("/api/hello", pathValue!.Value);
    }

    [Fact]
    public void CreateOpenApiWithServerUrls() {
        // Act
        var document = Oad.CreateCollection(OpenApiMetadata.Document);
        
        Oad.Add(document, Oad.Create(OpenApiMetadata.OpenApi, "3.0.0"));


        var servers = Oad.Add(document, Oad.CreateCollection(OpenApiKnownLocation.Servers));
        var server = Oad.Add(servers, Oad.CreateCollection(OpenApiHttpSemantics.Server));
        Oad.Add(server, Oad.Create(OpenApiHttpSemantics.Url, "http://localhost:5000"));
        Oad.Add(server, Oad.Create(OpenApiDocumentation.Description, "Localhost"));

        // Assert
        var content = Oad.Get<string>(document, OpenApiMetadata.OpenApi);
        Assert.Equal("3.0.0", content!.Value);

        var serversContent = Oad.GetCollection(document, OpenApiKnownLocation.Servers);
        Assert.NotNull(serversContent);

        var serverContent = Oad.GetCollection(serversContent, OpenApiHttpSemantics.Server);
        Assert.NotNull(serverContent);

        var serverUrl = Oad.Get<string>(serverContent, OpenApiHttpSemantics.Url);
        Assert.Equal("http://localhost:5000", serverUrl!.Value);
    }

    [Fact]
    public void ValidateTitleIsRequired() {
        // Act

        var info = Oad.CreateCollection(OpenApiDocumentation.Info);

        Oad.Add(info, Oad.Create(OpenApiMetadata.Version, "1.0.0"));
        Oad.Add(info, Oad.Create(OpenApiDocumentation.Description, "This is a very simple API"));

        // Assert
        Assert.False(Oad.Validate(info, OpenApiV3Constraints.Constraints));
    }

    [Fact]
    public void ValidateUnknownMemberInInfo() {
        // Act

        var info = Oad.CreateCollection(OpenApiDocumentation.Info);

        Oad.Add(info, Oad.Create(OpenApiDocumentation.Title, "Very Simple API"));
        Oad.Add(info, Oad.Create(OpenApiMetadata.Version, "1.0.0"));
        Oad.Add(info, Oad.Create(OpenApiHttpSemantics.Url, "http://localhost:5000"));
 
        // Assert
        Assert.False(Oad.Validate(info, OpenApiV3Constraints.Constraints));
    }

}