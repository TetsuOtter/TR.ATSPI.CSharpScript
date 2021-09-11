using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TR.ATSPI.CScript
{
	public class MyMetadataReferenceResolver : MetadataReferenceResolver
	{
		public string CallerDirectoryPath { get; }
		private int HashCode { get; }

		public MyMetadataReferenceResolver(in string callerDirectoryPath)
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
			if (other is not MyMetadataReferenceResolver resolver)
				return false;

			return resolver.HashCode.Equals(this.HashCode) && resolver.CallerDirectoryPath.Equals(this.CallerDirectoryPath);
		}

		public override int GetHashCode() => HashCode;

		public override ImmutableArray<PortableExecutableReference> ResolveReference(string reference, string? baseFilePath, MetadataReferenceProperties properties)
		{
			string pathToCheck = Path.IsPathRooted(reference) ? reference : Path.Combine(CallerDirectoryPath, reference);

			if (!File.Exists(pathToCheck))
			{
				var r = Assembly.ReflectionOnlyLoad(reference);
				return ImmutableArray.Create(MetadataReference.CreateFromFile(r.Location, properties));
			}

			PortableExecutableReference per = MetadataReference.CreateFromFile(pathToCheck, properties);

			return ImmutableArray.Create(per);
		}
	}
}
