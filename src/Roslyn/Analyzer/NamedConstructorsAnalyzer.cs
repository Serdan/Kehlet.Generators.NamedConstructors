﻿using System.Collections.Immutable;
using Kehlet.Generators.NamedConstructors.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Kehlet.Generators.NamedConstructors.Analyzer;

using SK = SyntaxKind;
using static DiagnosticDescriptors;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class NamedConstructorsAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
    [
        MissingPartialKeyword
    ];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(
            FindTargetType,
            SK.ClassDeclaration,
            SK.StructDeclaration,
            SK.RecordDeclaration,
            SK.RecordStructDeclaration
        );
    }

    private static void FindTargetType(SyntaxNodeAnalysisContext context)
    {
        var typeDeclarationNode = (TypeDeclarationSyntax) context.Node;

        var attributes = typeDeclarationNode.GetAttributesWithName(context.SemanticModel, StaticContent.MarkerAttributeName);
        if (attributes.IsEmpty)
        {
            return;
        }

        FindMissingPartial(context, typeDeclarationNode);
    }

    private static void FindMissingPartial(SyntaxNodeAnalysisContext context, TypeDeclarationSyntax typeDeclarationNode)
    {
        var hasPartial = typeDeclarationNode.Modifiers.Any(SK.PartialKeyword);
        if (hasPartial)
        {
            return;
        }

        var diagnostic = Diagnostic.Create(MissingPartialKeyword, typeDeclarationNode.Identifier.GetLocation());

        context.ReportDiagnostic(diagnostic);
    }
}
