using Kehlet.Generators.LoadAdditionalFiles;

namespace Kehlet.Generators.NamedConstructors.Common;

[LoadAdditionalFiles(RegexFilter = @"\.cs", MemberNameSuffix = "Source")]
public static partial class StaticContent
{
    public static string MarkerAttributeName { get; } = typeof(WithNamedConstructorsAttribute).FullName!;

    public static string MarkerAttributeNamespace { get; } = typeof(WithNamedConstructorsAttribute).Namespace!;

    public static string MarkerAttributeFileName { get; } = MarkerAttributeNamespace + "." + MarkerAttributeName + ".g.cs";
}
