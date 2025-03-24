using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Kehlet.Generators.NamedConstructors.Generator;

internal record TargetTypeData(
    string FileName,
    CacheArray<ConstructorData> Constructors,
    ModuleDescription ModuleDescription
);

internal record ConstructorData
{
    public required string ParameterList { get; init; }
    public required string InvocationList { get; init; }

    public static ConstructorData New(ParameterListSyntax list)
    {
        var builder = ImmutableArray.CreateBuilder<string>();
        foreach (var parameter in list.Parameters)
        {
            builder.Add(parameter.Identifier.ValueText);
        }

        var invocationList = builder.ToImmutable();

        return new() { ParameterList = list.ToString(), InvocationList = "(" + string.Join(", ", invocationList) + ")" };
    }
}
