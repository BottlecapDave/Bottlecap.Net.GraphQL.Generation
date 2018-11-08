using System;
using System.Collections.Generic;
using System.Text;
using GraphQL.Types;

namespace TestNamespace
{
	public partial class UserGraphType : ObjectGraphType<Bottlecap.Net.GraphQL.Generator.ConsoleExample.Classes.User>
{
	public UserGraphType()
	{
		Name = "User";
		Field(x => x.Id, nullable: false).Description("The id of the user");
		Field(x => x.Username, nullable: false).Description("The username of the user");
	}
}

	public partial class ToDoListGraphType : ObjectGraphType<Bottlecap.Net.GraphQL.Generator.ConsoleExample.Classes.ToDoList>
{
	public ToDoListGraphType()
	{
		Name = "ToDoList";
		Field(x => x.Id, nullable: false).Description("The id of the to do list");
		Field(x => x.Name, nullable: false).Description("The name of the do list");
		Field<User>().Name("User").Description("The user who owns the list").Resolve(context => context.Source.User);
		Field<ListGraphType<String>>().Name("Tags").Description("The tags associated with the to do list").Resolve(context => context.Source.Tags);
		Field<ListGraphType<UserGraphType>>().Name("Owners").Description("The list of users who are owners of the list").Resolve(context => context.Source.Owners);
		Field(x => x.CreationUserId, nullable: true).Description("The id of the user who created this to do list");
		Field(x => x.CreationUserTimestamp, nullable: false).Description("The time at which the to do list was created");
	}
}

	public partial class UserInputGraphType : InputObjectGraphType<Bottlecap.Net.GraphQL.Generator.ConsoleExample.Classes.User>
{
	public UserInputGraphType()
	{
		Name = "UserInput";
		Field(x => x.Id, nullable: false).Description("The id of the user");
		Field(x => x.Username, nullable: false).Description("The username of the user");
		Field(x => x.Password, nullable: false).Description("The password of the user");
	}
}

}
