using Microsoft.CodeAnalysis;

using System;
using System.IO;

namespace TR.ATSPI.CScript
{
	public class MySourceReferenceResolver : SourceReferenceResolver
	{
		public string CallerDirectoryPath { get; }
		private int HashCode { get; }

		public MySourceReferenceResolver(in string callerDirectoryPath)
		{
			if (Directory.Exists(callerDirectoryPath))
				CallerDirectoryPath = callerDirectoryPath;
			else if (File.Exists(callerDirectoryPath))
				CallerDirectoryPath = Path.GetDirectoryName(callerDirectoryPath);
			else
				throw new DirectoryNotFoundException("パス " + callerDirectoryPath + " は不正です");

			HashCode = new Random().Next();
		}

		public override bool Equals(object? other)
		{
			if (other is not MySourceReferenceResolver resolver)
				return false;

			return resolver.HashCode.Equals(this.HashCode) && resolver.CallerDirectoryPath.Equals(this.CallerDirectoryPath);
		}

		public override int GetHashCode() => HashCode;

		public override string? NormalizePath(string path, string? baseFilePath) => Path.Combine(Path.GetDirectoryName(baseFilePath), path);

		public override Stream OpenRead(string resolvedPath) => File.OpenRead(resolvedPath);

		public override string? ResolveReference(string path, string? baseFilePath) => Path.IsPathRooted(path) ? path : Path.Combine(CallerDirectoryPath, path);
	}
}
