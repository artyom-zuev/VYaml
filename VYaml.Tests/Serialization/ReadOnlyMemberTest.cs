using NUnit.Framework;
using VYaml.Tests.TypeDeclarations;

namespace VYaml.Tests.Serialization
{
    [TestFixture]
    public class ReadOnlyMemberTest : FormatterTestBase
    {
        [Test]
        public void Serialize_SkipsReadOnlyMembers()
        {
            Assert.That(Serialize(new WithReadOnlyMembers()),
                Is.EqualTo("a: 1\n" +
                           "b: 2\n" +
                           "privateSetter: 5\n"));
        }

        [Test]
        public void Deserialize_IgnoresKeysOfReadOnlyMembers()
        {
            var result = Deserialize<WithReadOnlyMembers>(
                "a: 10\n" +
                "sum: 99\n" +
                "getOnly: 99\n" +
                "readonlyField: 99\n" +
                "privateSetter: 50\n");
            Assert.That(result.A, Is.EqualTo(10));
            Assert.That(result.Sum, Is.EqualTo(12));
            Assert.That(result.GetOnly, Is.EqualTo(3));
            Assert.That(result.ReadonlyField, Is.EqualTo(4));
            Assert.That(result.PrivateSetter, Is.EqualTo(50));
        }

        [Test]
        public void Constructed_GetOnlyIsKept()
        {
            Assert.That(Serialize(new WithConstructedGetOnly(3)), Is.EqualTo("a: 3\n"));
            Assert.That(Deserialize<WithConstructedGetOnly>("a: 4\ndoubled: 99\n").Doubled, Is.EqualTo(8));
        }
    }
}
