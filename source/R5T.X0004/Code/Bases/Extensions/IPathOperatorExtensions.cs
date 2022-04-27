using System;
using System.IO;

using R5T.T0041;

using R5T.Magyar;

using Documentation = R5T.X0004.Documentation;

using Instances = R5T.X0004.Instances;


namespace System
{
    public static class IPathOperatorExtensions
    {
        /// <summary>
        /// <inheritdoc cref="Documentation.TestingOutputDirectoryFilePath"/>
        /// </summary>
        public static string GetTestingOutputDirectoryFilePath_WithoutExistenceCheck(this IPathOperator _,
            string outputDirectoryRelativeFilePath)
        {
            var testingEquivalentExecutableFilePath = Instances.FileSystemOperator.GetExecutableFilePathEquivalentForTesting();

            var outputDirectoryPath = Instances.PathOperator.GetDirectoryPathOfFilePath(testingEquivalentExecutableFilePath);

            var filePath = Instances.PathOperator.GetFilePath(outputDirectoryPath, outputDirectoryRelativeFilePath);
            return filePath;
        }

        /// <summary>
        /// <inheritdoc cref="Documentation.TestingOutputDirectoryFilePath"/>
        /// </summary>
        public static WasFound<string> TestingOutputDirectoryFilePathExists(this IPathOperator _,
            string outputDirectoryRelativeFilePath)
        {
            var filePath = _.GetTestingOutputDirectoryFilePath_WithoutExistenceCheck(
                outputDirectoryRelativeFilePath);

            var exists = Instances.FileSystemOperator.FileExists(filePath);

            var output = WasFound.From(exists, filePath);
            return output;
        }

        /// <summary>
        /// <inheritdoc cref="Documentation.TestingOutputDirectoryFilePath"/>
        /// </summary>
        public static string GetTestingOutputDirectoryFilePath_WithExistenceCheck(this IPathOperator _,
            string outputDirectoryRelativeFilePath)
        {
            var filePathWasFound = _.TestingOutputDirectoryFilePathExists(
                outputDirectoryRelativeFilePath);

            if(!filePathWasFound)
            {
                throw new FileNotFoundException("Output directory-relative file path not found.", filePathWasFound.Result);
            }

            return filePathWasFound.Result;
        }

        /// <summary>
        /// <inheritdoc cref="Documentation.TestingOutputDirectoryFilePath"/>
        /// Chooses <see cref="GetTestingOutputDirectoryFilePath_WithExistenceCheck(IPathOperator, string)"/> as the default.
        /// </summary>
        public static string GetTestingOutputDirectoryFilePath(this IPathOperator _,
            string outputDirectoryRelativeFilePath)
        {
            var output = _.GetTestingOutputDirectoryFilePath_WithExistenceCheck(
                outputDirectoryRelativeFilePath);
            
            return output;
        }
    }
}

