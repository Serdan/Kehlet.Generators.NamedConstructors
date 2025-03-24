using Kehlet.Generators.NamedConstructors.Analyzer;
using Kehlet.Generators.NamedConstructors.CodeFixes;
using Kehlet.Generators.NamedConstructors.Common;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Tests.Common;
using Xunit;

namespace Analyzer.Tests;

using Verifier = CSharpCodeFixVerifier<NamedConstructorsAnalyzer, NamedConstructorsCodeFixProvider, DefaultVerifier>;

public class NamedConstructorsCodeFixProviderTests
{
    [Fact]
    public async Task FixMissingPartialKeywordDiagnostic()
    {
        var original = SR.GetClassWithAttribute(true);
        var fixedSource = SR.GetPartialClassWithAttribute(true);

        var expected = Verifier.Diagnostic(DiagnosticDescriptors.MissingPartialKeyword)
                               .WithSpan(8, 18, 8, 29);

        await Verifier.VerifyCodeFixAsync(original, expected, fixedSource);
    }
}
