using System;

using R5T.T0041;
using R5T.T0044;


namespace R5T.X0004
{
    public static class Instances
    {
        public static IFileSystemOperator FileSystemOperator { get; } = T0044.FileSystemOperator.Instance;
        public static IPathOperator PathOperator { get; } = T0041.PathOperator.Instance;
    }
}
