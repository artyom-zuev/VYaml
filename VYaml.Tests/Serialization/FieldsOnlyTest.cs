using NUnit.Framework;
using VYaml.Tests.TypeDeclarations;

namespace VYaml.Tests.Serialization
{
    [TestFixture]
    public class FieldsOnlyTest : FormatterTestBase
    {
        [Test]
        public void Serialize_PropertiesSkippedByDefault()
        {
            Assert.That(Serialize(new FieldsOnlyByDefault()),
                Is.EqualTo("a: 1\n" +
                           "c: 3\n"));
        }

        [Test]
        public void Deserialize_PropertyKeysIgnoredByDefault()
        {
            var result = Deserialize<FieldsOnlyByDefault>(
                "a: 5\n" +
                "b: 9\n" +
                "c: 7\n" +
                "wrapped: 8\n");
            Assert.That(result.A, Is.EqualTo(5));
            Assert.That(result.B, Is.EqualTo(2));
            Assert.That(result.C, Is.EqualTo(7));
        }

        [Test]
        public void SerializeProperties_IncludesProperties()
        {
            Assert.That(Serialize(new FieldsAndProperties()),
                Is.EqualTo("a: 1\n" +
                           "b: 2\n"));
            var result = Deserialize<FieldsAndProperties>("a: 5\nb: 9\n");
            Assert.That(result.A, Is.EqualTo(5));
            Assert.That(result.B, Is.EqualTo(9));
        }

        [Test]
        public void SerializeProperties_CombinesWithNamingConvention()
        {
            Assert.That(Serialize(new FieldsAndPropertiesSnakeCase()),
                Is.EqualTo("field_value: 1\n" +
                           "property_value: 2\n"));
        }
    }
}
