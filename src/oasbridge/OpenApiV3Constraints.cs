namespace oasbridge;


public abstract class OpenApiConstraint
{
}
/// <summary>
/// Describes the constraints on a specific OpenAPI element for a specific version of the OpenAPI specification
/// </summary>
public class OpenApiValueConstraint : IOpenApiConstraint
{
    public static OpenApiValueConstraint String { get; } = new() { ValueType = typeof(string) };
    public Type? ValueType { get; set; }
}

public class OpenApiCollectionConstraint : IOpenApiCollectionConstraint
{
    public OpenApiElement[]? AllowedChildren { get; set; }
    public OpenApiElement[]? RequiredChildren { get; set; }
}


public static class OpenApiV3Constraints
{
    public static Dictionary<IOpenApiElement, IOpenApiConstraint> Constraints { get; set; } = new() {
        {OpenApiMetadata.Document, new OpenApiCollectionConstraint {
            AllowedChildren = [
                OpenApiMetadata.OpenApi,
                OpenApiDocumentation.Info,
                OpenApiKnownLocation.Paths,
                OpenApiKnownLocation.Components,
                OpenApiKnownLocation.Security,
                OpenApiKnownLocation.Tags,
                OpenApiDocumentation.ExternalDocs
            ]}},
        {OpenApiDocumentation.Info, new OpenApiCollectionConstraint {
            RequiredChildren = [OpenApiDocumentation.Title, OpenApiMetadata.Version],
            AllowedChildren = [
                OpenApiDocumentation.Title,
                OpenApiDocumentation.Summary,
                OpenApiDocumentation.Description,
                OpenApiDocumentation.TermsOfService,
                OpenApiDocumentation.Contact,
                OpenApiDocumentation.License,
                OpenApiMetadata.Version
            ]}},
        {OpenApiMetadata.OpenApi, OpenApiValueConstraint.String},
        {OpenApiMetadata.Version, OpenApiValueConstraint.String},
        {OpenApiMetadata.Description, OpenApiValueConstraint.String},
        {OpenApiDocumentation.Title, OpenApiValueConstraint.String},
        {OpenApiDocumentation.Summary, OpenApiValueConstraint.String},
        {OpenApiDocumentation.TermsOfService, OpenApiValueConstraint.String},
        {OpenApiKnownLocation.Servers, new OpenApiCollectionConstraint {
            AllowedChildren = [OpenApiHttpSemantics.Server] }},
        {OpenApiHttpSemantics.Server, new OpenApiCollectionConstraint {
            AllowedChildren = [
                OpenApiHttpSemantics.Url,
                OpenApiDocumentation.Description
                //OpenApiHttpSemantics.Variables
            ]}},
    };
}
