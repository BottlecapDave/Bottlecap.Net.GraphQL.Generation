using Bottlecap.Net.GraphQL.Generation.Attributes;

namespace GraphQLExample.Data
{
    [GraphType]
    public enum ToDoListState
    {
        Created,
        InProgress,
        Complete
    }
}
