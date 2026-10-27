using NUnit.Framework;
using VYaml.Annotations;
using VYaml.Serialization;
using VYaml.Tests.TypeDeclarations;

namespace VYaml.Tests.Serialization
{
    [TestFixture]
    public class PreserveCaseTest : FormatterTestBase
    {
        static readonly YamlSerializerOptions PreserveCaseOptions = new() { NamingConvention = NamingConvention.PreserveCase };

        [Test]
        public void Serialize_OptionsKeepDeclaredNames()
        {
            Assert.That(Serialize(new WithMixedCaseKeys(), PreserveCaseOptions),
                Is.EqualTo("HP: 10\n" +
                           "tv_tg: 0.5\n" +
                           "Label: a\n" +
                           "camelCase: 1\n" +
                           "Alias_Key: 2\n" +
                           "hidden: 3\n" +
                           "kind: HeavyArmor\n" +
                           "flags: Read | Execute\n"));
        }

        [Test]
        public void Deserialize_OptionsReadDeclaredNames()
        {
            var result = Deserialize<WithMixedCaseKeys>(
                "HP: 20\n" +
                "tv_tg: 0.25\n" +
                "Label: b\n" +
                "camelCase: 4\n" +
                "Alias_Key: 5\n" +
                "hidden: 6\n" +
                "kind: Standard\n" +
                "flags: Write\n",
                PreserveCaseOptions);
            Assert.That(result.HP, Is.EqualTo(20));
            Assert.That(result.tv_tg, Is.EqualTo(0.25f));
            Assert.That(result.Label, Is.EqualTo("b"));
            Assert.That(result.camelCase, Is.EqualTo(4));
            Assert.That(result.aliased, Is.EqualTo(5));
            Assert.That(result.Hidden(), Is.EqualTo(6));
            Assert.That(result.kind, Is.EqualTo(PreserveCaseEnum.Standard));
            Assert.That(result.flags, Is.EqualTo(FlagsEnum.Write));
        }

        [Test]
        public void RoundTrip_Options()
        {
            var source = new WithMixedCaseKeys { HP = 7, Label = "c", kind = PreserveCaseEnum.Standard, flags = FlagsEnum.Read | FlagsEnum.Write };
            var yaml = Serialize(source, PreserveCaseOptions);
            Assert.That(Serialize(Deserialize<WithMixedCaseKeys>(yaml, PreserveCaseOptions), PreserveCaseOptions), Is.EqualTo(yaml));
        }

        // The default options still convert, so projects relying on lowerCamelCase keys are unaffected
        [Test]
        public void Serialize_DefaultOptionsUnchanged()
        {
            Assert.That(Serialize(new WithMixedCaseKeys()), Does.StartWith("hP: 10\n"));
            Assert.That(Serialize(new WithMixedCaseKeys()), Does.Contain("kind: heavyArmor\n"));
        }

        [Test]
        public void TypeLevel_KeepsDeclaredNamesUnderAnyOptions()
        {
            Assert.That(Serialize(new WithPreserveCaseType()), Is.EqualTo("HP: 10\nLabel: a\n"));
            var result = Deserialize<WithPreserveCaseType>("HP: 3\nLabel: b\n");
            Assert.That(result.HP, Is.EqualTo(3));
            Assert.That(result.Label, Is.EqualTo("b"));
        }
    }
}
