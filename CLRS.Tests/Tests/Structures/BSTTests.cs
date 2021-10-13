using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using CLRS.Core.Structures;

namespace Cormen.Tests.Structures
{
    [TestFixture(Category = "Data Structures")]
    public class BSTTests
    {
        [Test]
        public void Add321()
        {
            var value1 = 1;
            var value2 = 2;
            var value3 = 3;

            var tree = new BST<string, int>();
            
            tree.Insert(value3.ToString(), value3);
            tree.Insert(value1.ToString(), value1);
            tree.Insert(value2.ToString(), value2);
            CollectionAssert.AreEqual(new List<int> { 1, 2, 3 }, tree.IncoderTreeWalk());
        }

        [Test]
        public void Add231()
        {
            var value1 = 1;
            var value2 = 2;
            var value3 = 3;

            var tree = new BST<string, int>();

            tree.Insert(value2.ToString(), value2);
            tree.Insert(value3.ToString(), value3);
            tree.Insert(value1.ToString(), value1);
            CollectionAssert.AreEqual(new List<int> { 1, 2, 3 }, tree.IncoderTreeWalk());
        }

        [Test]
        public void AddAndRemove()
        {
            var value1 = 1;
            var value2 = 2;
            var value3 = 3;

            var tree = new BST<string, int>();

            tree.Insert(value3.ToString(), value3);
            tree.Insert(value1.ToString(), value1);
            tree.Insert(value2.ToString(), value2);
            CollectionAssert.AreEqual(new List<int> { 1, 2, 3 }, tree.IncoderTreeWalk());

            tree.Delete(value2.ToString());
            CollectionAssert.AreEqual(new List<int> { 1, 3 }, tree.IncoderTreeWalk());
        }

        [Test]
        public void ReverseTo321()
        {
            var value1 = 1;
            var value2 = 2;
            var value3 = 3;

            var tree = new BST<string, int>();

            tree.Insert(value2.ToString(), value2);
            tree.Insert(value3.ToString(), value3);
            tree.Insert(value1.ToString(), value1);
            CollectionAssert.AreEqual(new List<int> { 1, 2, 3 }, tree.IncoderTreeWalk());

            tree.Reverse();
            CollectionAssert.AreEqual(new List<int> { 3, 2, 1 }, tree.IncoderTreeWalk());
        }

        [Test]
        public void ReverseBigTree()
        {
            var values = new List<int> { 2, 1, 3, 4, 5, 6, 7, 9 };

            var tree = new BST<string, int>();

            foreach (var value in values)
                tree.Insert(value.ToString(), value);

            CollectionAssert.AreEqual(values.OrderBy(x => x), tree.IncoderTreeWalk());
            var beforeReverseWalkedTree = tree.IncoderTreeWalk();

            tree.Reverse();
            CollectionAssert.AreEqual(values.OrderByDescending(x => x), tree.IncoderTreeWalk());
        }
    }
}
