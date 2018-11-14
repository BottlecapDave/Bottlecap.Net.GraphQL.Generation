# Bottlecap.Net.GraphQL.Generation

The purpose of this is to support the generation of GraphTypes for [.Net GraphQL](https://graphql-dotnet.github.io/) based on existing entities.

## Why does this exist?

I strive for seperation of concerns when building my applications. However, I also don't like repeating code and performing boiler plate tasks.

Therefore, I created this toolset to generate extendable GraphType objects based on defined entity/data classes.

## How does it work?
### Bottlecap.Net.GraphQL.Generation.Attributes

![Nuget](https://img.shields.io/nuget/v/bottlecap.net.graphql.generation.attributes.svg)

This provides attributes which are used to decorate classes that you wish to have graph types generated for. The aim was to support bespoke attributing at a minimum for most scenarios.

Therefore, this will generate a field for each public property using descriptions defined using the **System.ComponentModel.Description** attribute.

If there are properties you don't want to expose, then is a **GraphTypeProperty** attribute.

### Bottlecap.Net.GraphQL.Generation.Console

![Nuget](https://img.shields.io/nuget/v/bottlecap.net.graphql.generation.console.svg)

// TODO Explain how the console application works

```
dotnet Bottlecap.Net.GraphQL.Generation.Console.dll -n GraphQLExample.Schemas -o <<OUTPUT>> -i <<INPUT DLL>
```

### Bottlecap.Net.GraphQL.Generation

![Nuget](https://img.shields.io/nuget/v/bottlecap.net.graphql.generation.svg)

This contains the main generation logic. If you're wanting to upgrade the generator, or generate types without using attributes, then this is the assembly you will want.

### Example

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
