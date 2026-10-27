using VYaml.Annotations;

namespace VYaml.Tests.TypeDeclarations
{
    public enum PreserveCaseEnum
    {
        Standard,
        HeavyArmor,
    }

    // Keys every other convention would rewrite: an acronym, an underscore, a leading capital
    [YamlObject]
    public partial class WithMixedCaseKeys
    {
        public int HP = 10;
        public float tv_tg = 0.5f;
        public string Label = "a";
        public int camelCase = 1;
        [YamlMember("Alias_Key")]
        public int aliased = 2;
        [YamlMember]
        private int _hidden = 3;
        public PreserveCaseEnum kind = PreserveCaseEnum.HeavyArmor;
        public FlagsEnum flags = FlagsEnum.Read | FlagsEnum.Execute;

        public int Hidden() => _hidden;
    }

    [YamlObject(NamingConvention.PreserveCase)]
    public partial class WithPreserveCaseType
    {
        public int HP = 10;
        public string Label = "a";
    }
}
