using GraphQL;
using GraphQL.DataLoader;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.SystemTextJson;
using GraphQLExample;
using GraphQLExample.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddGraphQL(builder => builder
    .AddHttpMiddleware<GraphQLExample.Schemas.Schema>()
    .AddSchema<GraphQLExample.Schemas.Schema>(GraphQL.DI.ServiceLifetime.Scoped)
    .ConfigureExecutionOptions(options =>
    {
        options.EnableMetrics = true;
        var logger = options.RequestServices.GetRequiredService<ILogger<Program>>();
        options.UnhandledExceptionDelegate = ctx =>
        {
            logger.LogError("{Error} occurred", ctx.OriginalException.Message);
            return Task.CompletedTask;
        };
    })
    .AddSystemTextJson()
    .AddDataLoader()
    .AddGraphTypes(typeof(GraphQLExample.Schemas.Schema).Assembly)
);

var app = builder.Build();

app.UseGraphQL<GraphQLExample.Schemas.Schema>();

app.UseGraphQLPlayground(new PlaygroundOptions
{
    BetaUpdates = true,
    RequestCredentials = RequestCredentials.Omit,
    HideTracingResponse = false,

    EditorCursorShape = EditorCursorShape.Line,
    EditorTheme = EditorTheme.Light,
    EditorFontSize = 14,
    EditorReuseHeaders = true,
    EditorFontFamily = "Consolas",

    PrettierPrintWidth = 80,
    PrettierTabWidth = 2,
    PrettierUseTabs = true,

    SchemaDisableComments = false,
    SchemaPollingEnabled = true,
    SchemaPollingEndpointFilter = "*localhost*",
    SchemaPollingInterval = 5000,

    Headers = new Dictionary<string, object>
    {
        ["MyHeader1"] = "MyValue",
        ["MyHeader2"] = 42,
    },
});

app.Run();
