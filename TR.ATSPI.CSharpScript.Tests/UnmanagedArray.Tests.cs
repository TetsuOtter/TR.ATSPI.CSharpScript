using NUnit.Framework;

using System;
using System.Runtime.InteropServices;

namespace TR.ATSPI.CSharpScript.Tests
{
	public class UnmanagedArrayTests
	{
		const int MAX_ARRAY_SIZE = 255;

		[Test]
		public void ReadWriteTest()
		{
			Random r = new();
			IntPtr ptr = IntPtr.Zero;
			try
			{
				ptr = Marshal.AllocHGlobal(MAX_ARRAY_SIZE * sizeof(int));

				GlobalVariable.UnmanagedArray target = new(ptr);

				int[] expected_values = new int[MAX_ARRAY_SIZE];
				int[] actual_values = new int[MAX_ARRAY_SIZE];

				for (int i = 0; i < MAX_ARRAY_SIZE; i++)
				{
					int testCase = r.Next();
					target[i] = testCase;
					expected_values[i] = testCase;
				}

				for (int i = 0; i < MAX_ARRAY_SIZE; i++)
					actual_values[i] = target[i];

				CollectionAssert.AreEqual(expected_values, actual_values);
			}
			finally
			{
				if (ptr != IntPtr.Zero)
					Marshal.FreeHGlobal(ptr);
			}
		}

	}
}
