using System.Collections.Generic;
using VYaml.Annotations;

namespace VYaml.Tests.TypeDeclarations
{
    // Members with initializers, for checking that keys missing from a document keep them
    [YamlObject]
    public partial class WithInitializers
    {
        public int Count = 3;
        public float Scale = 1.5f;
        public string Name = "default";
        public List<int> Values = new() { 1, 2 };
        public SimpleTypeOne Nested = new() { One = 7 };
        public int Plain;
    }

    [YamlObject]
    public partial struct StructWithInitializers
    {
        public int Count = 3;
        public float Scale = 1.5f;

        public StructWithInitializers()
        {
        }
    }
}
