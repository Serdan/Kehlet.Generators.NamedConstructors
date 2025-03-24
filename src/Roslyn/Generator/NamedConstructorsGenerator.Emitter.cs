namespace SourceGeneratorNamespace.Generator;

public partial class NamedConstructorsGenerator
{
    internal class Emitter(TargetTypeData typeData) : SyntaxDescriptionEmitter
    {
        public override Unit VisitNamedTypeBody(NamedTypeDescription description)
        {
            if (description.IsTargetNode is false)
            {
                return unit;
            }

            foreach (var constructor in typeData.Constructors)
            {
                Emitter.Tabs()
                       .Append("public static ")
                       .Append(description.Identifier)
                       .Append(" New")
                       .Append(constructor.ParameterList)
                       .Line(" =>")
                       .Indent()
                       .Tabs().Append("new").Append(constructor.InvocationList).Line(";")
                       .Unindent();
            }

            return unit;
        }
    }
}
