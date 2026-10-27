using VYaml.Annotations;

namespace VYaml.Tests.TypeDeclarations
{
    // Fields only by default: B and Wrapped are skipped, C is kept by [YamlMember]
    [YamlObject]
    public partial class FieldsOnlyByDefault
    {
        public int A = 1;
        public int B { get; set; } = 2;
        [YamlMember]
        public int C { get; set; } = 3;
        public int Wrapped
        {
            get => A;
            set => A = value;
        }
    }

    [YamlObject(SerializeProperties = true)]
    public partial class FieldsAndProperties
    {
        public int A = 1;
        public int B { get; set; } = 2;
    }

    [YamlObject(NamingConvention.SnakeCase, SerializeProperties = true)]
    public partial class FieldsAndPropertiesSnakeCase
    {
        public int FieldValue = 1;
        public int PropertyValue { get; set; } = 2;
    }
}
