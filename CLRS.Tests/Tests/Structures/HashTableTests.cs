using System.Collections.Generic;
using System.Linq;
using CLRS.Core.Structures;
using CLRS.Tests.Generators;
using CLRS.Tests.Stubs;
using NUnit.Framework;

namespace CLRS.Tests.Tests.Structures
{
    [TestFixture(Category = "Data Structures")]
    public class HashTableTests
    {
        [Test]
        public void Insert_OneValueToEmptyHashTable_AddedOneValue()
        {
            //Arrange
            var key1 = (IntStub) 1;
            var val1 = "val1";
            var hashTable = new HashTable<IntStub, string>();

            //Act
            hashTable.Insert(key1, val1);

            //Assert
            Assert.AreEqual(1, hashTable.Count);
            Assert.AreEqual(val1, hashTable[key1]);
        }
        
        [Test]
        public void Insert_FiveValuesToEmptyHashTable_AddedFiveValues()
        {
            //Arrange
            var numbers = IntStubGenerator.GetFirst100Numbers();
            var hashTable = new HashTable<IntStub, string>();
            var expectedValues = IntStubGenerator.GetFirst100Numbers().Take(5).Select(number => number.ToString());

            //Act
            foreach (var number in numbers.Take(5)) 
                hashTable.Insert(number, number.ToString());

            //Assert
            var actualValues = new List<string>();
            foreach (var number in numbers.Take(5))
                actualValues.Add((string)hashTable[number]);
            Assert.AreEqual(5, hashTable.Count);
            CollectionAssert.AreEqual(expectedValues, actualValues);
        }
        
        [Test]
        public void Insert_TwoStringValuesToEmptyHashTable_AddedTwoValues()
        {
            //Arrange
            var key1 = "Ключик 8493448";
            var value1 = "Значение №8493448";
            var key2 = "Ключик 8495";
            var value2 = "Значение №8498";
            var hashTable = new HashTable<string, string>();

            //Act
            hashTable.Insert(key1, value1);
            hashTable.Insert(key2, value2);

            //Assert
            Assert.AreEqual(2, hashTable.Count);
            Assert.AreEqual(value1, hashTable[key1]);
            Assert.AreEqual(value2, hashTable[key2]);
        }
        
        [Test]
        public void Insert_TwoValuesWithSameHashCode_AddsAllNumbersWithCollision()
        {
            //Arrange
            var hashTable = new HashTable<SameHashCodeIntStub, string>();
            var one = (SameHashCodeIntStub) 1;
            var two = (SameHashCodeIntStub) 2;

            //Act
            hashTable.Insert(one, "1");
            hashTable.Insert(two, "2");

            //Assert
            Assert.AreEqual("1", hashTable[one].ToString());
            Assert.AreEqual("2", hashTable[two].ToString());
        }

        [Test]
        public void Remove_WithThereIsOneEntity_RemoveEntity()
        {
            //Arrange
            var key = (IntStub)1;
            var value = "Значение №93448";
            var hashTable = new HashTable<IntStub, string>();
            hashTable.Insert(key, value);

            //Act
            hashTable.Remove(key);

            //Assert
            Assert.AreEqual(0, hashTable.Count);
            Assert.AreEqual(null, hashTable[key]);
        }

        [Test]
        public void Remove_ExistNumberWithSameHashCode_RemoveNumberWithCollision()
        {
            //Arrange
            var hashTable = new HashTable<SameHashCodeIntStub, string>();
            var one = (SameHashCodeIntStub) 1;
            var two = (SameHashCodeIntStub) 2;
            hashTable.Insert(one, "1");
            hashTable.Insert(two, "2");

            //Act
            hashTable.Remove(two);

            //Assert
            Assert.AreEqual("1", hashTable[one].ToString());
            Assert.AreEqual(null, hashTable[two]);
        }
    }
}
