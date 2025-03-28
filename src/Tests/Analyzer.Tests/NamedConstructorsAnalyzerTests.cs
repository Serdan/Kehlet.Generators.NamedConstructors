using Kehlet.Generators.NamedConstructors.Analyzer;
using Kehlet.Generators.NamedConstructors.Common;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Tests.Common;
using Xunit;

namespace Analyzer.Tests;

using Verifier = CSharpAnalyzerVerifier<NamedConstructorsAnalyzer, DefaultVerifier>;

public class NamedConstructorsAnalyzerTests
{
    [Fact]
    public async Task MissingPartialKeywordDiagnostic()
    {
        var text = SR.GetClassWithAttribute(true);

        var expected = Verifier.Diagnostic(DiagnosticDescriptors.MissingPartialKeyword)
                               .WithSpan(8, 18, 8, 29);

        await Verifier.VerifyAnalyzerAsync(text, expected);
    }
}
