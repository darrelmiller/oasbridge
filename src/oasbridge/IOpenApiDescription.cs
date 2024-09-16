namespace oasbridge;

public interface IOpenApiDescription {
    IOpenApiValueContent<T> Create<T>(IOpenApiElement openApiElement, T value);
    IOpenApiContentCollection CreateCollection(IOpenApiElement openApiElement);
    T Add<T>(IOpenApiContentCollection parentContent,  T openApiContent) where T : IOpenApiContent;
    IOpenApiValueContent<T>? Get<T>(IOpenApiContentCollection parent, IOpenApiElement openApiElement);
    IOpenApiContentCollection? GetCollection(IOpenApiContentCollection parent, IOpenApiElement openApiElement);
    bool Validate(IOpenApiContent content, Dictionary<IOpenApiElement, IOpenApiConstraint> constraints);
}


public interface IOpenApiElement
{
    string Name { get; set; }
}

public interface IOpenApiContent
{
    IOpenApiElement Element { get; set; }
    Type? ValueType { get; set; }
}

public interface IOpenApiValueContent<T> : IOpenApiContent
{

    public T? Value { get; set; }
}

public interface IOpenApiContentCollection : IOpenApiContent
{
    List<IOpenApiContent> Contents { get; set; }
}

public interface IOpenApiConstraint
{
}

public interface IOpenApiValueConstraint : IOpenApiConstraint
{
    public Type? ValueType { get; set; }
}

public class IOpenApiCollectionConstraint : IOpenApiConstraint
{
    OpenApiElement[]? AllowedChildren { get; set; }
    OpenApiElement[]? RequiredChildren { get; set; }
}
