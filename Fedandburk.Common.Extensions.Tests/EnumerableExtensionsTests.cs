using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace Fedandburk.Common.Extensions.Tests;

[TestFixture]
[SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
[SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
public class EnumerableExtensionsTests
{
    [TestFixture]
    public class WhenIndexOfCalled
    {
        [TestFixture]
        public class AndTheEnumerableIsNull
        {
            [Test]
            public void ExceptionShouldBeThrown()
            {
                Assert.Throws<ArgumentNullException>(() => default(IEnumerable).IndexOf(null));
            }
        }

        [TestFixture]
        public class AndTheEqualityComparerIsNull
        {
            [Test]
            public void ExceptionShouldBeThrown()
            {
                Assert.Throws<ArgumentNullException>(() => Substitute.For<IEnumerable>().IndexOf(null, null));
            }
        }

        [Test]
        public void EqualityComparerShouldBeUsed()
        {
            var equalityComparer = Substitute.For<IEqualityComparer>();
            equalityComparer.Equals(Arg.Any<object>(), Arg.Any<object>()).Returns(args => Equals(args[0], args[1]));

            var enumerable = Enumerable.Range(0, 10);

            var result = enumerable.IndexOf(5, equalityComparer);

            equalityComparer.Received().Equals(Arg.Any<object>(), Arg.Any<object>());

            Assert.That(result, Is.EqualTo(5));
        }
    }

    [TestFixture]
    public class WhenGenericIndexOfCalled
    {
        [TestFixture]
        public class AndTheEnumerableIsNull
        {
            [Test]
            public void ExceptionShouldBeThrown()
            {
                Assert.Throws<ArgumentNullException>(() => default(IEnumerable<object>).IndexOf(null));
            }
        }

        [TestFixture]
        public class AndTheEqualityComparerIsNull
        {
            [Test]
            public void ExceptionShouldBeThrown()
            {
                Assert.Throws<ArgumentNullException>(() => Enumerable.Empty<object>().IndexOf(null, null));
            }
        }

        [Test]
        public void EqualityComparerShouldBeUsed()
        {
            var equalityComparer = Substitute.For<IEqualityComparer<int>>();
            equalityComparer.Equals(Arg.Any<int>(), Arg.Any<int>()).Returns(args => Equals(args[0], args[1]));

            var enumerable = Enumerable.Range(0, 10);

            var result = enumerable.IndexOf(5, equalityComparer);

            equalityComparer.Received().Equals(Arg.Any<int>(), Arg.Any<int>());

            Assert.That(result, Is.EqualTo(5));
        }
    }

    [TestFixture]
    public class WhenTakeCalled
    {
        [TestFixture]
        public class AndTheEnumerableIsNull
        {
            [Test]
            public void ExceptionShouldBeThrown()
            {
                Assert.Throws<ArgumentNullException>(() => default(IEnumerable<object>).Take(0, 0));
            }
        }

        [TestFixture]
        public class AndTheEnumerableIsList
        {
            [Test]
            [TestCase(0, 0)]
            [TestCase(0, 1)]
            [TestCase(3, 0)]
            [TestCase(3, 3)]
            public void CorrectSubEnumerableShouldBeReturned(int index, int length)
            {
                var enumerable = Enumerable.Range(0, 10).ToList();

                var result = enumerable.Take(index, length);

                Assert.That(result, Is.EqualTo(enumerable.ToList().GetRange(index, length)));
            }
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(3, 0)]
        [TestCase(3, 3)]
        public void CorrectSubEnumerableShouldBeReturned(int index, int length)
        {
            var enumerable = Enumerable.Range(0, 10);

            var result = enumerable.Take(index, length);

            Assert.That(result, Is.EqualTo(enumerable.ToList().GetRange(index, length)));
        }
    }

    [TestFixture]
    public class WhenIsNullOrEmptyCalled
    {
        [TestFixture]
        public class AndTheEnumerableIsNull
        {
            [Test]
            public void TrueShouldBeReturned()
            {
                Assert.That(default(IEnumerable).IsNullOrEmpty(), Is.True);
            }
        }

        [TestFixture]
        public class AndTheEnumerableCountIsZero
        {
            [Test]
            public void TrueShouldBeReturned()
            {
                Assert.That(Enumerable.Empty<int>().IsNullOrEmpty(), Is.True);
            }
        }

        [TestFixture]
        public class AndTheEnumerableCountIsNotZero
        {
            [Test]
            public void FalseShouldBeReturned()
            {
                Assert.That(Enumerable.Range(0, 10).IsNullOrEmpty(), Is.False);
            }
        }
    }

    [TestFixture]
    public class WhenCountCalled
    {
        [TestFixture]
        public class AndTheEnumerableIsNull
        {
            [Test]
            public void ExceptionShouldBeThrown()
            {
                Assert.Throws<ArgumentNullException>(() => default(IEnumerable<object>).Count());
            }
        }

        [TestFixture]
        public class AndTheEnumerableIsCollection
        {
            [Test]
            [TestCase(0)]
            [TestCase(10)]
            public void FalseShouldBeReturned(int count)
            {
                var enumerable = Enumerable.Range(0, count).ToList();

                Assert.That(count, Is.EqualTo(enumerable.Count()));
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(10)]
        public void FalseShouldBeReturned(int count)
        {
            var enumerable = Enumerable.Range(0, count) as IEnumerable;

            Assert.That(count, Is.EqualTo(enumerable.Count()));
        }
    }

    [TestFixture]
    public class WhenSumCalled
    {
        [TestFixture]
        public class AndTheEnumerableIsNull
        {
            [Test]
            public void ExceptionShouldBeThrown()
            {
                Assert.Throws<ArgumentNullException>(() => default(IEnumerable<TimeSpan>).Sum());
            }
        }

        [Test]
        [TestCase(new long[0])]
        [TestCase(new[] { 10L })]
        [TestCase(new[] { 100L, 1000L, 10000L })]
        public void SumShouldBeReturned(long[] values)
        {
            var enumerable = values.Select(TimeSpan.FromTicks);

            Assert.That(TimeSpan.FromTicks(values.Sum()), Is.EqualTo(enumerable.Sum()));
        }
    }

    [TestFixture]
    public class WhenSumWithSelectorCalled
    {
        [TestFixture]
        public class AndTheEnumerableIsNull
        {
            [Test]
            public void ExceptionShouldBeThrown()
            {
                Assert.Throws<ArgumentNullException>(() => default(IEnumerable<TimeSpan>).Sum(null));
            }
        }

        [TestFixture]
        public class AndTheSelectorIsNull
        {
            [Test]
            public void ExceptionShouldBeThrown()
            {
                Assert.Throws<ArgumentNullException>(() => Substitute.For<IEnumerable<TimeSpan>>().Sum(null));
            }
        }

        [Test]
        [TestCase(new long[0])]
        [TestCase(new[] { 10L })]
        [TestCase(new[] { 100L, 1000L, 10000L })]
        public void SelectorShouldBeUsed(long[] values)
        {
            var count = 0;

            var _ = values.Sum(item =>
            {
                count++;
                return TimeSpan.FromTicks(item);
            });

            Assert.That(count, Is.EqualTo(values.Length));
        }

        [Test]
        [TestCase(new long[0])]
        [TestCase(new[] { 10L })]
        [TestCase(new[] { 100L, 1000L, 10000L })]
        public void SumShouldBeReturned(long[] values)
        {
            Assert.That(values.Sum(TimeSpan.FromTicks), Is.EqualTo(TimeSpan.FromTicks(values.Sum())));
        }
    }
}