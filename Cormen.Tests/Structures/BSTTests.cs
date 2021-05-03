using Cormen.Core.Structures;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cormen.Tests.Structures
{
    [TestFixture(Category = "Data Structures")]
    public class BSTTests
    {
        [Test]
        public void Add()
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
    }
}
