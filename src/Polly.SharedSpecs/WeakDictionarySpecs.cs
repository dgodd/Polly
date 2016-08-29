using System;
using System.Linq;
using Xunit;

namespace Polly.Specs
{
    public class WeakDictionarySpecs
    {
        [Fact]
        public void Enumerable()
        {
            var dict = new Shared.WeakDictionary<string, object>();
            var fred = new object();
            dict.Add("fred", fred);

            var count = 0;
            foreach(var entry in dict.All())
            {
                Assert.Equal(entry.Key, "fred");
                count++;
            }
            Assert.Equal(count, 1);
        }

        [Fact]
        public void DoesNotHaveAStrongReference()
        {
            var dict = new Shared.WeakDictionary<string, object>();
            var fred = new object();
            dict.Add("fred", fred);

            Assert.Equal(dict.All().Count(), 1);

            fred = null;
            GC.Collect();

            var list = dict.All().ToArray();
            Assert.Equal(list.Count(), 0);
        }

        [Fact]
        public void CanReAddKeys()
        {
            var dict = new Shared.WeakDictionary<string, object>();
            var fred = new object();
            dict.Add("fred", fred);
            Assert.Equal(dict.All().Count(), 1);

            fred = null;
            GC.Collect();
            Assert.Equal(dict.All().Count(), 0);

            fred = new object();
            dict.Add("fred", fred);
            Assert.Equal(dict.All().Count(), 1);
        }
    }
}
