﻿using Kehlet.Generators.LoadAdditionalFiles;
using Kehlet.Generators.NamedConstructors.Common;

namespace Tests.Common;

[LoadAdditionalFiles(RegexFilter = @"\.cs$", MemberNameSuffix = "Source", MemberKind = MemberKind.Property)]
public static partial class SR
{
    private static readonly string EmbeddedAttribute =
        """
        namespace Microsoft.CodeAnalysis
        {
            /// <summary>
            /// Hides internal types even with InternalsVisibleTo.
            /// IncrementalGeneratorPostInitializationContext.AddEmbeddedAttributeDefinition() will be available from Roslyn 4.14
            /// This file should be removed at that time. 
            /// </summary>
            internal sealed class EmbeddedAttribute : Attribute;
        }
        """;

    public static string GetClassWithAttribute(bool includeGeneratorTypes = false) =>
        $$"""
        using System;
        using Microsoft.CodeAnalysis;
        using {{StaticContent.MarkerAttributeNamespace}};

        namespace MyTestNamespace.SubNamespace
        {
            [WithNamedConstructors]
            public class MyTestClass;
        }

        {{(includeGeneratorTypes ? EmbeddedAttribute : "")}}
        {{(includeGeneratorTypes ? WithNamedConstructorsAttributeSource : "")}}
        """;

    public static string GetPartialClassWithAttribute(bool includeGeneratorTypes = false) =>
        $$"""
        using System;
        using Microsoft.CodeAnalysis;
        using {{StaticContent.MarkerAttributeNamespace}};

        namespace MyTestNamespace.SubNamespace
        {
            [WithNamedConstructors]
            public partial class MyTestClass;
        }

        {{(includeGeneratorTypes ? EmbeddedAttribute : "")}}
        {{(includeGeneratorTypes ? WithNamedConstructorsAttributeSource : "")}}
        """;
}
