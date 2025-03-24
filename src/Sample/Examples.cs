using SourceGeneratorNamespace;

namespace Sample;

[Marker]
public class TargetType;

[Marker]
public partial class TargetType2(string s)
{
    public TargetType2(): this("") { }

    public TargetType2(int n) : this("") { }
}
