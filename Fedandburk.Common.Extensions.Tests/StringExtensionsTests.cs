using NUnit.Framework;

namespace Fedandburk.Common.Extensions.Tests;

[TestFixture]
public class StringExtensionsTests
{
    [TestFixture]
    public class WhenIsNullOrEmptyCalled
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("string")]
        public void ResultOfIsNullOrEmptyShouldBeReturned(string input)
        {
            Assert.That(input.IsNullOrEmpty(), Is.EqualTo(string.IsNullOrEmpty(input)));
        }
    }

    [TestFixture]
    public class WhenNullOrWhiteSpaceCalled
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("string")]
        public void ResultOfIsNullOrWhiteSpaceShouldBeReturned(string input)
        {
            Assert.That(input.IsNullOrWhiteSpace(), Is.EqualTo(string.IsNullOrWhiteSpace(input)));
        }
    }
}