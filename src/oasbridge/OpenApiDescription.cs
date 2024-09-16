using System.Runtime.InteropServices;
using System.Text.Json;

namespace oasbridge;


public class OpenApiDescription : IOpenApiDescription 
{

    public IOpenApiValueContent<T> Create<T>(IOpenApiElement openApiElement, T value) 
    {
        return new OpenApiValueContent<T>() { Element = openApiElement,
            Value = value,
            ValueType = typeof(T)
        };
    }
    public IOpenApiContentCollection CreateCollection(IOpenApiElement openApiElement) 
    {
        return new OpenApiContentCollection() { Element = openApiElement };
    }

    public T Add<T>(IOpenApiContentCollection parentContent,  T openApiContent) where T : IOpenApiContent
    {
        parentContent.Contents.Add(openApiContent);
        return openApiContent;
    }

    public IOpenApiValueContent<T>? Get<T>(IOpenApiContentCollection parent, IOpenApiElement openApiElement)
    {
        return parent.Contents.FirstOrDefault(e => e.Element.Name == openApiElement.Name) as OpenApiValueContent<T>;
    }

    public IOpenApiContentCollection? GetCollection(IOpenApiContentCollection parent, IOpenApiElement openApiElement)
    {
        return parent.Contents.FirstOrDefault(e => e.Element.Name == openApiElement.Name) as OpenApiContentCollection;
    }

    public bool Validate(IOpenApiContent content, Dictionary<IOpenApiElement, IOpenApiConstraint> constraints)
    {
        if (constraints.TryGetValue(content.Element, out IOpenApiConstraint? constraint))
        {
            switch (constraint)
            {
                case OpenApiValueConstraint valueConstraint:
                    if (content.ValueType != valueConstraint.ValueType)
                    {
                        return false;
                    }
                    break;
                case OpenApiCollectionConstraint collectionConstraint:
                    {
                        if (content is OpenApiContentCollection collection)
                        {
                            if (collectionConstraint.RequiredChildren != null)
                            {
                                foreach (var requiredChild in collectionConstraint.RequiredChildren)
                                {
                                    if (!collection.Contents.Any(e => e.Element.Name == requiredChild.Name))
                                    {
                                        return false;
                                    }
                                }
                            }
                            if (collectionConstraint.AllowedChildren != null)
                            {
                                foreach (var child in collection.Contents)
                                {
                                    if (!collectionConstraint.AllowedChildren.Any(e => e.Name == child.Element.Name))
                                    {
                                        return false;
                                    }
                                }
                            }
                            foreach (var child in collection.Contents)
                            {
                                if (!Validate(child, constraints))
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    }
            }
        }
        return true;
        
    }


    public IOpenApiContentCollection Load(JsonDocument jsonDocument) {
        
        var document = CreateCollection(OpenApiMetadata.Document);
        var rootElement = jsonDocument.RootElement;
        
        Load(OpenApiMetadata.Document, document, rootElement);
        return document;
    }

    private void Load(OpenApiMetadata element, IOpenApiContentCollection content, JsonElement rootElement)
    {
        foreach (var property in rootElement.EnumerateObject())
        {
            var propertyElement = new OpenApiElement() { Name = property.Name };
            // Get the constraint for the property to determine its type
            // TODO            
        }
    }
}

/// <summary>
/// OpenApiElement is the base class for all semantic concepts in an OpenAPI document.
/// </summary>
public class OpenApiElement : IOpenApiElement
{
    public required string Name { get; set; }
}

/// <summary>
/// OpenApiContent is the base class for all content in an instance of an OpenAPI document.
/// </summary>
public abstract class OpenApiContent : IOpenApiContent
{
    public required IOpenApiElement Element { get; set; }
    public Type? ValueType { get; set; }
}

/// <summary>
/// OpenApiValueContent is a content element that has a primitive value.
/// </summary>
public class OpenApiValueContent<T> : IOpenApiValueContent<T>
{
    public required IOpenApiElement Element { get; set; }
    public Type? ValueType { get; set; }  = typeof(T);
    public T? Value { get; set; }
}

/// <summary>
/// OpenApiContentCollection is a content element that contains other content elements.
/// </summary>
public class OpenApiContentCollection : IOpenApiContentCollection
{
    public required IOpenApiElement Element { get; set; }
    public Type? ValueType { get; set; }
    public List<IOpenApiContent> Contents { get; set; } = [];
}
