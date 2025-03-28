﻿using Kehlet.Generators.NamedConstructors.Generator;
using Microsoft.CodeAnalysis;
using Tests.Common;

namespace Generator.Tests;

public class GeneratorTests
{
    [Fact]
    public Task VerifyVerify() => VerifyChecks.Run();

    [Fact]
    public Task GenerateEmptyPartialClass()
    {
        var generator = new NamedConstructorsGenerator();
        var comp = CompilationFactory.PartialClassWithAttribute();
        var driver = GeneratorDriverFactory.Default(generator).RunGeneratorsAndUpdateCompilation(comp, out var resultComp, out var d);

        var compdiags = comp.GetDiagnostics();
        var resultDiags = resultComp.GetDiagnostics();
        
        var result = driver.GetRunResult().Results.Single();
        Assert.Empty(resultComp.GetDiagnostics().Where(x => x.Severity is not (DiagnosticSeverity.Info or DiagnosticSeverity.Hidden)));
        Assert.Equal(3, result.GeneratedSources.Length);
        return Verify(result.GeneratedSources.Last().SourceText.ToString());
    }
}
