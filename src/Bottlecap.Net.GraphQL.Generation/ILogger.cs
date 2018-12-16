using System;
using System.Collections.Generic;
using System.Text;

namespace Bottlecap.Net.GraphQL.Generation
{
    public interface ILogger
    {
        void WriteInfo(string message);
        void WriteError(string message);
    }
}
