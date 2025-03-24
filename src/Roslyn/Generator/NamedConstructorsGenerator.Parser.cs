using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SourceGeneratorNamespace.Generator;

public partial class NamedConstructorsGenerator
{
    internal static class Parser
    {
        public static Option<TargetTypeData> Parse(GeneratorAttributeSyntaxContext context, CancellationToken token)
        {
            var targetNode = (TypeDeclarationSyntax) context.TargetNode;

            var moduleData = SyntaxHelper.GetTargetWithContext(targetNode);
            if (moduleData.IsNone)
            {
                return None;
            }

            var builder = ImmutableArray.CreateBuilder<ConstructorData>();
            foreach (var childNode in targetNode.ChildNodes())
            {
                switch (childNode)
                {
                    case ConstructorDeclarationSyntax constructorNode:
                        builder.Add(ConstructorData.New(constructorNode.ParameterList));
                        break;
                    case ParameterListSyntax parameterList:
                        builder.Add(ConstructorData.New(parameterList));
                        break;
                }
            }

            var constructors = builder.ToImmutable();
            if (constructors.IsDefaultOrEmpty)
            {
                return None;
            }

            var fileName = NamespaceVisitor.GetFileName(targetNode, moduleData.UnsafeValue);

            return new TargetTypeData(
                fileName,
                CacheArray.Create(constructors),
                moduleData.UnsafeValue
            );
        }
    }

    internal class NamespaceVisitor : SyntaxDescriptionWalker
    {
        public Option<string> Namespace = None;

        public override Unit VisitNamespace(NamespaceDescription description)
        {
            if (description.IsFileScoped)
            {
                Namespace = description.Name;
                return unit;
            }

            Namespace = Namespace.Map(x => x + ".").DefaultValue("") + description.Name;

            return base.VisitNamespace(description);
        }

        public static string GetFileName(TypeDeclarationSyntax targetNode, ModuleDescription module)
        {
            var @namespace = GetFullNamespace(module).Map(x => x + ".").DefaultValue("");
            var identifier = targetNode.Identifier.ValueText;
            var fileName = @namespace + identifier;
            if (targetNode.Arity > 0)
            {
                fileName += $"`{targetNode.Arity}";
            }

            fileName += ".g.cs";
            return fileName;
        }

        public static Option<string> GetFullNamespace(ModuleDescription description)
        {
            var visitor = new NamespaceVisitor();
            visitor.Visit(description);
            return visitor.Namespace;
        }
    }
}
