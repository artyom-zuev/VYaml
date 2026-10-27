using VYaml.Annotations;

namespace VYaml.Tests.TypeDeclarations
{
    // An interface without [YamlObjectUnion] attributes: its members are the [YamlObject] types implementing it
    [YamlObject]
    public partial interface IDiscoveredUnion
    {
        int A { get; set; }
    }

    [YamlObject(SerializeProperties = true)]
    public partial class DiscoveredImpl1 : IDiscoveredUnion
    {
        public int A { get; set; }
        public string? B { get; set; }
    }

    // Derives from another member, so it has to be matched before its base to keep its own tag
    [YamlObject(SerializeProperties = true)]
    public partial class DiscoveredImpl1Derived : DiscoveredImpl1
    {
        public string? C { get; set; }
    }

    [YamlObject(SerializeProperties = true)]
    public partial struct DiscoveredStruct : IDiscoveredUnion
    {
        public int A { get; set; }
    }

    // Shares its name with a member in another namespace
    [YamlObject(SerializeProperties = true)]
    public partial class DiscoveredSameName : IDiscoveredUnion
    {
        public int A { get; set; }
    }

    // Not a [YamlObject], so not a member
    public class DiscoveredNotYamlObject : IDiscoveredUnion
    {
        public int A { get; set; }
    }

    [YamlObject(SerializeProperties = true)]
    public partial class WithDiscoveredUnionMember
    {
        public IDiscoveredUnion[]? Items { get; set; }
    }
}

namespace VYaml.Tests.TypeDeclarations.Other
{
    [YamlObject(SerializeProperties = true)]
    public partial class DiscoveredSameName : IDiscoveredUnion
    {
        public int A { get; set; }
    }
}
