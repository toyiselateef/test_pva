// using System.Data;

// public static class ConnectionScope
// {
//     private static readonly AsyncLocal<Stack<IDbConnection>> connectionStack = new AsyncLocal<Stack<IDbConnection>>();

//     public static ConnectionScope Current { get; } = new ConnectionScope();

//     private ConnectionScope() { }

//     public IDisposable UseConnection(IDbConnection connection)
//     {
//         if (connectionStack.Value == null)
//             connectionStack.Value = new Stack<IDbConnection>();

//         connectionStack.Value.Push(connection);

//         return new ConnectionScopeHandle();
//     }

//     private class ConnectionScopeHandle : IDisposable
//     {
//         public void Dispose()
//         {
//             if (connectionStack.Value != null && connectionStack.Value.Count > 0)
//             {
//                 IDbConnection connection = connectionStack.Value.Pop();
//                 connectionFactory.ReleaseConnection(connection); // Release the connection back to the pool
//             }
//         }
//     }
// }