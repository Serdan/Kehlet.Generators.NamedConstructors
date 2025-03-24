namespace Kehlet.Generators.NamedConstructors
{
    using System;
    using Microsoft.CodeAnalysis;
    using static System.AttributeTargets;

    [AttributeUsage(Class | Struct)]
    [Embedded]
    internal class WithNamedConstructorsAttribute : Attribute;
}
