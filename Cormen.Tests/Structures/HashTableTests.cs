using Cormen.Core.Structures;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cormen.Tests.Structures
{
    [TestFixture(Category = "Structures")]
    public class HashTableTests
    {
        [Test]
        public void HashTableAdd()
        {
            var key = "Ключик 8495485";
            var value = "Значение №8493448";

            var hashTable = new HashTable<string, string>();
            Assert.AreEqual(0, hashTable.Count);
            Assert.AreEqual(null, hashTable[key]);

            hashTable.Add(key, value);
            Assert.AreEqual(1, hashTable.Count);
            Assert.AreEqual(value, hashTable[key]);
        }

        [Test]
        public void HashTableAddAndRemove()
        {
            var key = "Ключик 85485";
            var value = "Значение №93448";

            var hashTable = new HashTable<string, string>();
            Assert.AreEqual(0, hashTable.Count);
            Assert.AreEqual(null, hashTable[key]);

            hashTable.Add(key, value);
            Assert.AreEqual(1, hashTable.Count);
            Assert.AreEqual(value, hashTable[key]);

            hashTable.Remove(key);
            Assert.AreEqual(0, hashTable.Count);
            Assert.AreEqual(null, hashTable[key]);
        }
    }
}
