using System;
using System.Collections.Generic;
using System.Text;
using GraphQL.Types;

namespace TestNamespace
{
	public class UserGraphType : ObjectGraphType<Bottlecap.Net.GraphQL.Generator.ConsoleExample.Classes.User>
{
	public UserGraphType()
	{
		Name = "User";
		Field(x => x.Id).Description("The id of the user");
		Field(x => x.Username).Description("The username of the user");
		Field(x => x.Password).Description("The password of the user");
	}
}

	public class ToDoListGraphType : ObjectGraphType<Bottlecap.Net.GraphQL.Generator.ConsoleExample.Classes.ToDoList>
{
	public ToDoListGraphType()
	{
		Name = "ToDoList";
		Field(x => x.Id).Description("The id of the to do list");
		Field(x => x.Name).Description("The name of the do list");
		Field<UserGraphType>().Name("User").Description("The user who owns the list");
		Field(x => x.CreationUserId).Description("The id of the user who created this to do list");
		Field<DateTimeGraphType>().Name("CreationUserTimestamp").Description("The time at which the to do list was created");
	}
}

}
