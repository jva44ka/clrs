using System.Collections.Generic;
using System.Linq;
using CLRS.Core.Structures;
using CLRS.Tests.Generators;
using CLRS.Tests.Stubs;
using NUnit.Framework;

namespace CLRS.Tests.Tests.Structures
{
    [TestFixture(Category = "Data Structures")]
    public class BSTTests
    {
        private readonly int _value1 = 1;
        private readonly int _value2 = 2;
        private readonly int _value3 = 3;

        [Test]
        public void Add321()
        {
            //Arrange
            var actualTree = new BST<string, int>();
            actualTree.Insert(_value3.ToString(), _value3);
            actualTree.Insert(_value1.ToString(), _value1);
            actualTree.Insert(_value2.ToString(), _value2);

            var expectedTree = new BinaryTreeNodeStub<string, int>(_value3.ToString(), _value3);
            expectedTree.WithLeftNode(_value1.ToString(), _value1)
                        .ToLeftNode()
                        .WithRightNode(_value2.ToString(), _value2);

            //Assert
            Assert.AreEqual(expectedTree, actualTree.Root);
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
            CollectionAssert.AreEqual(new List<int> { 1, 2, 3 }, tree.InOrderTreeWalk());
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
            CollectionAssert.AreEqual(new List<int> { 1, 2, 3 }, tree.InOrderTreeWalk());

            tree.Delete(value2.ToString());
            CollectionAssert.AreEqual(new List<int> { 1, 3 }, tree.InOrderTreeWalk());
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
            CollectionAssert.AreEqual(new List<int> { 1, 2, 3 }, tree.InOrderTreeWalk());

            tree.Reverse();
            CollectionAssert.AreEqual(new List<int> { 3, 2, 1 }, tree.InOrderTreeWalk());
        }

        [Test]
        public void ReverseBigTree()
        {
            var values = new List<int> { 2, 1, 3, 4, 5, 6, 7, 9 };

            var tree = new BST<string, int>();

            foreach (var value in values)
                tree.Insert(value.ToString(), value);

            CollectionAssert.AreEqual(values.OrderBy(x => x), tree.InOrderTreeWalk());

            tree.Reverse();
            CollectionAssert.AreEqual(values.OrderByDescending(x => x), tree.InOrderTreeWalk());
        }
    }
}
