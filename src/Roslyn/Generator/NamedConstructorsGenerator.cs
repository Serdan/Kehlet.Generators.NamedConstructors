using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGeneratorNamespace.Common;

namespace SourceGeneratorNamespace.Generator;

using static TargetFilterOptions;
using static StaticContent;

[Generator]
public partial class NamedConstructorsGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx =>
        {
            ctx.AddSource<EmbeddedAttribute>(EmbeddedAttributeSource);
            ctx.AddSource(MarkerAttributeFileName, MarkerAttributeSource);
        });

        var typeValues = context.CreateTargetProvider(MarkerAttributeName, Partial, ConcreteType, Parser.Parse).Choose();

        context.RegisterSourceOutput(typeValues, GenerateCode);
    }

    internal static void GenerateCode(SourceProductionContext context, TargetTypeData data)
    {
        var emitter = new Emitter(data);
        emitter.Visit(data.ModuleDescription);
        context.AddSourceUTF8(data.FileName, emitter.ToString());
    }

    private static bool ConcreteType(SyntaxNode node, CancellationToken _) =>
        node is TypeDeclarationSyntax and not InterfaceDeclarationSyntax;
}
