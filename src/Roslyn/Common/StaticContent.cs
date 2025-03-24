using Kehlet.Generators.LoadAdditionalFiles;

namespace SourceGeneratorNamespace.Common;

[LoadAdditionalFiles(RegexFilter = @"\.cs", MemberNameSuffix = "Source")]
public static partial class StaticContent
{
    public static string MarkerAttributeName { get; } = typeof(MarkerAttribute).FullName!;

    public static string MarkerAttributeNamespace { get; } = typeof(MarkerAttribute).Namespace!;

    public static string MarkerAttributeFileName { get; } = MarkerAttributeNamespace + "." + MarkerAttributeName + ".g.cs";
}
