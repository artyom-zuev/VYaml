using NUnit.Framework;
using VYaml.Tests.TypeDeclarations;

namespace VYaml.Tests.Serialization
{
    [TestFixture]
    public class InitializerTest : FormatterTestBase
    {
        [Test]
        public void Deserialize_MissingKeysKeepInitializers()
        {
            var result = Deserialize<WithInitializers>("count: 5\n");
            Assert.That(result.Count, Is.EqualTo(5));
            Assert.That(result.Scale, Is.EqualTo(1.5f));
            Assert.That(result.Name, Is.EqualTo("default"));
            Assert.That(result.Values, Is.EqualTo(new[] { 1, 2 }));
            Assert.That(result.Nested.One, Is.EqualTo(7));
            Assert.That(result.Plain, Is.EqualTo(0));
        }

        [Test]
        public void Deserialize_PresentKeysReplaceInitializers()
        {
            var result = Deserialize<WithInitializers>(
                "count: 5\n" +
                "scale: 2\n" +
                "name: set\n" +
                "values: [9]\n" +
                "nested: { one: 8 }\n" +
                "plain: 4\n");
            Assert.That(result.Count, Is.EqualTo(5));
            Assert.That(result.Scale, Is.EqualTo(2f));
            Assert.That(result.Name, Is.EqualTo("set"));
            Assert.That(result.Values, Is.EqualTo(new[] { 9 }));
            Assert.That(result.Nested.One, Is.EqualTo(8));
            Assert.That(result.Plain, Is.EqualTo(4));
        }

        [Test]
        public void Deserialize_NullKeepsNull()
        {
            var result = Deserialize<WithInitializers>("name: null\nvalues: null\n");
            Assert.That(result.Name, Is.Null);
            Assert.That(result.Values, Is.Null);
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void Deserialize_StructMissingKeysKeepInitializers()
        {
            var result = Deserialize<StructWithInitializers>("count: 5\n");
            Assert.That(result.Count, Is.EqualTo(5));
            Assert.That(result.Scale, Is.EqualTo(1.5f));
        }
    }
}
