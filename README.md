# Bottlecap.Net.GraphQL.Generation

The purpose of this is to support the generation of GraphTypes for [.Net GraphQL](https://graphql-dotnet.github.io/) based on existing entities.

## Why does this exist?

I strive for seperation of concerns when building my applications. However, I also don't like repeating code and performing boiler plate tasks.

Therefore, I created this toolset to generate extendable GraphType objects based on defined entity/data classes.

## How does it work?
### Bottlecap.Net.GraphQL.Generation.Attributes

![Nuget](https://img.shields.io/nuget/v/bottlecap.net.graphql.generation.attributes.svg)

This provides attributes which are used to decorate classes that you wish to have graph types and dataloaders generated for. The aim was to support bespoke attributing at a minimum for most scenarios.

#### GraphType

Any class that has this attribute will have a GraphQL type generated for it. Specify `IsInput` if the graph type is used for input.

This will generate a `GraphType` field for each public property with descriptions defined using the **System.ComponentModel.Description** attribute.

If there are properties you don't want to expose, then there is a **GraphTypeProperty** attribute, which has an `IsIgnored` flag.

#### DataLoaders

This will generate an extension method for `GraphQL.IDataLoaderContextAccessor` for each public method in the class that returns a `Task<IDictionary<,>>`.

### Bottlecap.Net.GraphQL.Generation.Cli

![Nuget](https://img.shields.io/nuget/v/bottlecap.net.graphql.generation.cli.svg)

This is the CLI that utilises `Bottlecap.Net.GraphQL.Generation` by taking a given dll, and looking for all classes that implement a `Bottlecap.Net.GraphQL.Generation.Attributes` attribute. It will then generate all GraphQL types in the specified namespace at the specified output location.

#### Installation

You can install the application using [dotnet tool install](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-tool-install).

e.g.
```
dotnet tool install Bottlecap.Net.GraphQL.Generation.Cli --version 0.3.2-alpha --tool-path "C:\Repos\Applications\makeawishlist\tool"
```

#### Using

| Argument | Description |
|----------|-------------|
| -i       | The dll to load and find all `Bottlecap.Net.GraphQL.Generation.Attributes` to generate GraphQL types from |
| -o       | The output path of the generated classes. This will generate a single class file. |
| -n       | The namesoace the generated class will be in |

An example of running the application would look like the following.

```
dotnet Bottlecap.Net.GraphQL.Generation.Cli.dll -o <<OUTPUT>> -i <<INPUT DLL> -n GraphQLExample.Schemas
```

### Bottlecap.Net.GraphQL.Generation

![Nuget](https://img.shields.io/nuget/v/bottlecap.net.graphql.generation.svg)

This contains the main generation logic. If you're wanting to upgrade the generator, or generate types without using attributes, then this is the assembly you will want.

### GraphType Example

Below is an example of a data record defined for graph type generation

```
[GraphType]
public class User
{
    [Description("The id of the user")]
    public long Id { get; set; }

    [Description("The username of the user")]
    public string Username { get; set; }

    [GraphTypeProperty(IsIgnored = true)]
    [Description("The password of the user")]
    public string Password { get; set; }
}
```

and here is the result...

```
public partial class UserGraphType : ObjectGraphType<GraphQLExample.Data.User>
{
	public UserGraphType()
	{
		Name = "User";
		Field(x => x.Id, nullable: false)
        .Description("The id of the user");
		Field(x => x.Username, nullable: false)
        .Description("The username of the user");
		SetupFields();
	}

	partial void SetupFields();
}
```

### DataLoader Example

Below is an example of a method defined for data loader generation

```
[DataLoader]
public class UserRepository : IUserRepository
{
    public Task<IDictionary<long, User>> GetUsersByIdsAsync(IEnumerable<long> ids)
    {
        // Logic goes here
    }
}
```

and here is the result...

```
public static class IUserRepositoryExtensions
{
	 public static Task<GraphQLExample.Data.User> GetUsersByIdsAsync(this IDataLoaderContextAccessor accessor, 
      GraphQLExample.Data.IUserRepository repository,
      Func<System.Int64> keySelector)
    {
        var loader = accessor.Context.GetOrAddBatchLoader<System.Int64, GraphQLExample.Data.User>("GraphQLExample.Data.IUserRepository.GetUsersByIdsAsync", repository.GetUsersByIdsAsync);
        return loader.LoadAsync(keySelector());
    }
}
```
