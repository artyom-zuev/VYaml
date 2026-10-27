using VYaml.Annotations;

namespace VYaml.Tests.TypeDeclarations
{
    // Read-only members next to serialized ones, for checking that they are skipped instead of failing the type
    [YamlObject]
    public partial class WithReadOnlyMembers
    {
        public int A = 1;
        public int B { get; set; } = 2;
        public int Sum => A + B;
        public int GetOnly { get; } = 3;
        public readonly int ReadonlyField = 4;
        public int PrivateSetter { get; private set; } = 5;
    }

    // A get-only property set by the constructor is still serialized
    [YamlObject]
    public partial class WithConstructedGetOnly
    {
        public int A { get; }
        public int Doubled => A * 2;

        public WithConstructedGetOnly(int a)
        {
            A = a;
        }
    }
}
