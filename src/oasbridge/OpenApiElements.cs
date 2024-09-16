
namespace oasbridge;

/// OpenApi Metadata Elements provide instructions to tooling on how to process the OpenAPI document.
public class OpenApiMetadata : OpenApiElement
{
    public static OpenApiMetadata Document { get; } = new() { Name = "document" };
    public static OpenApiMetadata OpenApi { get; } = new() { Name = "openapi" };
    public static OpenApiMetadata JsonSchemaDialect { get; } = new() { Name = "jsonSchemaDialect" };
    public static OpenApiMetadata Version { get; } = new() { Name = "version" };
    public static OpenApiMetadata Description { get; } = new() { Name = "description" };
}

/// <summary>
/// OpenApi Known Locations are locations in the OpenAPI document 
/// that can be referenced from elsewhere in the document.
/// </summary>
public class OpenApiKnownLocation : OpenApiElement
{
    public static OpenApiKnownLocation Components { get; } = new() { Name = "components" };
    public static OpenApiKnownLocation Security { get; } = new() { Name = "security" };
    public static OpenApiKnownLocation Tags { get; } = new() { Name = "tags" };
    public static OpenApiKnownLocation Servers { get; } = new() { Name = "servers" };
    public static OpenApiKnownLocation Paths { get; } = new() { Name = "paths" };
}

/// <summary>
/// OpenApi Documentation Elements provide information about the API but have no
/// behavioural semantics on the rest of the document, or on the API itself.
/// </summary>
public class OpenApiDocumentation : OpenApiElement
{
    public static OpenApiDocumentation Summary { get; } = new() { Name = "summary" };
    public static OpenApiDocumentation Description { get; } = new() { Name = "description" };
    public static OpenApiDocumentation ExternalDocs { get; } = new() { Name = "externalDocs" };
    public static OpenApiDocumentation Info { get; } = new() { Name = "info" };
    public static OpenApiDocumentation Title { get; } = new() { Name = "title" };
    public static OpenApiDocumentation TermsOfService { get; } = new() { Name = "termsOfService" };
    public static OpenApiDocumentation Contact { get; } = new() { Name = "contact" };
    public static OpenApiDocumentation License { get; } = new() { Name = "license" };

}

public class OpenApiHttpSemantics : OpenApiElement
{
    public static OpenApiHttpSemantics Get { get; } = new() { Name = "get" };
    public static OpenApiHttpSemantics Put { get; } = new() { Name = "put" };
    public static OpenApiHttpSemantics Post { get; } = new() { Name = "post" };
    public static OpenApiHttpSemantics Delete { get; } = new() { Name = "delete" };
    public static OpenApiHttpSemantics Options { get; } = new() { Name = "options" };
    public static OpenApiHttpSemantics Head { get; } = new() { Name = "head" };
    public static OpenApiHttpSemantics Patch { get; } = new() { Name = "patch" };
    public static OpenApiHttpSemantics Trace { get; } = new() { Name = "trace" };
    public static OpenApiHttpSemantics Path { get; } = new() { Name = "path" };
    public static OpenApiHttpSemantics PathKey { get; } = new() { Name = "path_key" };
    public static OpenApiHttpSemantics Operation { get; } = new() { Name = "operation" };
    public static OpenApiHttpSemantics Url { get; } = new() { Name = "url" };
    public static OpenApiHttpSemantics Server { get; } = new() { Name = "server" };
}


