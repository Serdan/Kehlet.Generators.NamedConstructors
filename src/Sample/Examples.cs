using Kehlet.Generators.NamedConstructors.Files;

namespace Sample;

[WithNamedConstructors]
public class TargetType;

[WithNamedConstructors]
public partial class TargetType2(string s)
{
    public TargetType2(): this("") { }

    public TargetType2(int n) : this("") { }
}
