using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace UrlShortener.Tests;

// API docs are part of the definition of done (CONTRIBUTING.md). These tests make the
// build say no when an endpoint, parameter or contract property ships undocumented,
// so no reviewer has to remember to ask.
public class OpenApiDocumentationTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Every_operation_has_a_summary_and_a_description()
    {
        using var document = await LoadOpenApiDocument();

        foreach (var (name, operation) in Operations(document))
        {
            Assert.True(HasText(operation, "summary"), $"{name} has no summary.");
            Assert.True(HasText(operation, "description"), $"{name} has no description.");
        }
    }

    [Fact]
    public async Task Every_parameter_is_described()
    {
        using var document = await LoadOpenApiDocument();

        foreach (var (name, operation) in Operations(document))
        {
            if (!operation.TryGetProperty("parameters", out var parameters))
            {
                continue;
            }

            foreach (var parameter in parameters.EnumerateArray())
            {
                var parameterName = parameter.GetProperty("name").GetString();
                Assert.True(HasText(parameter, "description"), $"{name}: parameter '{parameterName}' has no description.");
            }
        }
    }

    #region every-property-described
    [Fact]
    public async Task Every_contract_property_is_described()
    {
        using var document = await LoadOpenApiDocument();
        var schemas = document.RootElement.GetProperty("components").GetProperty("schemas");

        foreach (var schema in schemas.EnumerateObject().Where(s => IsOurContract(s.Name)))
        {
            foreach (var property in schema.Value.GetProperty("properties").EnumerateObject())
            {
                Assert.True(HasText(property.Value, "description"),
                    $"{schema.Name}.{property.Name} has no [Description].");
            }
        }
    }
    #endregion

    private async Task<JsonDocument> LoadOpenApiDocument()
    {
        var json = await factory.CreateClient().GetStringAsync("/openapi/v1.json");
        return JsonDocument.Parse(json);
    }

    private static IEnumerable<(string Name, JsonElement Operation)> Operations(JsonDocument document) =>
        from path in document.RootElement.GetProperty("paths").EnumerateObject()
        from operation in path.Value.EnumerateObject()
        where operation.Name is "get" or "post" or "put" or "patch" or "delete"
        select ($"{operation.Name.ToUpperInvariant()} {path.Name}", operation.Value);

    // Framework schemas (like HttpValidationProblemDetails) are documented by ASP.NET Core, not us.
    private static bool IsOurContract(string schemaName) =>
        schemaName is "CreateLinkRequest" or "LinkResponse";

    private static bool HasText(JsonElement element, string property) =>
        element.TryGetProperty(property, out var value) && !string.IsNullOrWhiteSpace(value.GetString());
}
