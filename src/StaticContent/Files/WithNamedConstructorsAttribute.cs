using System;
using Microsoft.CodeAnalysis;
using static System.AttributeTargets;

namespace Kehlet.Generators.NamedConstructors.Files
{
    [AttributeUsage(Class | Struct)]
    [Embedded]
    internal class WithNamedConstructorsAttribute : Attribute;
}
