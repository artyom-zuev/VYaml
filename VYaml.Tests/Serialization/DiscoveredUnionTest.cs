using NUnit.Framework;
using VYaml.Serialization;
using VYaml.Tests.TypeDeclarations;

namespace VYaml.Tests.Serialization
{
    [TestFixture]
    public class DiscoveredUnionTest : FormatterTestBase
    {
        [Test]
        public void Serialize_TagsWithTypeName()
        {
            Assert.That(Serialize<IDiscoveredUnion>(new DiscoveredImpl1 { A = 1, B = "foo" }),
                Is.EqualTo("!DiscoveredImpl1\n" +
                           "a: 1\n" +
                           "b: foo\n"));
            Assert.That(Serialize<IDiscoveredUnion>(new DiscoveredStruct { A = 2 }),
                Is.EqualTo("!DiscoveredStruct\n" +
                           "a: 2\n"));
        }

        [Test]
        public void Serialize_DerivedMemberKeepsItsTag()
        {
            Assert.That(Serialize<IDiscoveredUnion>(new DiscoveredImpl1Derived { A = 1, B = "foo", C = "bar" }),
                Does.StartWith("!DiscoveredImpl1Derived\n"));
        }

        [Test]
        public void Serialize_CollidingNamesTakeNamespace()
        {
            Assert.That(Serialize<IDiscoveredUnion>(new DiscoveredSameName { A = 1 }),
                Does.StartWith("!VYaml.Tests.TypeDeclarations.DiscoveredSameName\n"));
            Assert.That(Serialize<IDiscoveredUnion>(new TypeDeclarations.Other.DiscoveredSameName { A = 1 }),
                Does.StartWith("!VYaml.Tests.TypeDeclarations.Other.DiscoveredSameName\n"));
        }

        [Test]
        public void Deserialize_PicksTypeFromTag()
        {
            var result = Deserialize<WithDiscoveredUnionMember>(
                "items:\n" +
                "- !DiscoveredImpl1\n" +
                "  a: 1\n" +
                "  b: foo\n" +
                "- !DiscoveredImpl1Derived\n" +
                "  a: 2\n" +
                "  c: bar\n" +
                "- !DiscoveredStruct\n" +
                "  a: 3\n" +
                "- !VYaml.Tests.TypeDeclarations.Other.DiscoveredSameName\n" +
                "  a: 4\n");

            Assert.That(result.Items, Has.Length.EqualTo(4));
            Assert.That(result.Items![0], Is.TypeOf<DiscoveredImpl1>());
            Assert.That(((DiscoveredImpl1)result.Items[0]).B, Is.EqualTo("foo"));
            Assert.That(result.Items[1], Is.TypeOf<DiscoveredImpl1Derived>());
            Assert.That(((DiscoveredImpl1Derived)result.Items[1]).C, Is.EqualTo("bar"));
            Assert.That(result.Items[2], Is.TypeOf<DiscoveredStruct>());
            Assert.That(result.Items[3], Is.TypeOf<TypeDeclarations.Other.DiscoveredSameName>());
            Assert.That(result.Items[3].A, Is.EqualTo(4));
        }

        [Test]
        public void Serialize_NonMemberThrows()
        {
            Assert.Throws<YamlSerializerException>(() =>
                Serialize<IDiscoveredUnion>(new DiscoveredNotYamlObject { A = 1 }));
            Assert.Throws<YamlSerializerException>(() =>
                Serialize(new WithDiscoveredUnionMember { Items = new IDiscoveredUnion[] { new DiscoveredImpl1(), new DiscoveredNotYamlObject() } }));
        }

        [Test]
        public void Deserialize_NonMemberTagThrows()
        {
            Assert.Throws<YamlSerializerException>(() =>
                Deserialize<IDiscoveredUnion>("!DiscoveredNotYamlObject\na: 1\n"));
        }
    }
}
