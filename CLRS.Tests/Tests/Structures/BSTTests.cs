using System.Collections.Generic;
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
        public void Insert_321KeysAndValues_ShouldBe123Tree()
        {
            //Arrange
            var expectedTree = new BinaryTreeNodeStub<string, int>(_value3.ToString(), _value3);
            expectedTree.WithLeftNode(_value1.ToString(), _value1)
                        .ToLeftNode()
                        .WithRightNode(_value2.ToString(), _value2);
            var actualTree = new BST<string, int>();

            //Act
            actualTree.Insert(_value3.ToString(), _value3);
            actualTree.Insert(_value1.ToString(), _value1);
            actualTree.Insert(_value2.ToString(), _value2);
            
            //Assert
            Assert.AreEqual(expectedTree, actualTree.Root);
        }

        [Test]
        public void Insert_231KeysAndValues_ShouldBe123Tree()
        {
            //Arrange
            var expectedTree = new BinaryTreeNodeStub<string, int>(_value2.ToString(), _value2);
            expectedTree.WithRightNode(_value3.ToString(), _value3)
                        .WithLeftNode(_value1.ToString(), _value1);
            var actualTree = new BST<string, int>();

            //Act
            actualTree.Insert(_value2.ToString(), _value2);
            actualTree.Insert(_value3.ToString(), _value3);
            actualTree.Insert(_value1.ToString(), _value1);

            //Assert
            Assert.AreEqual(expectedTree, actualTree.Root);
        }

        [Test]
        public void Delete_Key2From123Tree_ShouldBe13Tree()
        {
            //Arrange
            var expectedTree = new BinaryTreeNodeStub<string, int>(_value3.ToString(), _value3);
            expectedTree.WithLeftNode(_value1.ToString(), _value1);

            var actualTree = new BST<string, int>();
            actualTree.Insert(_value3.ToString(), _value3);
            actualTree.Insert(_value1.ToString(), _value1);
            actualTree.Insert(_value2.ToString(), _value2);

            //Act
            actualTree.Delete(_value2.ToString());

            //Assert
            Assert.AreEqual(expectedTree, actualTree.Root);
        }
        
        [Test]
        public void Delete_Key2RootFrom123Tree_ShouldBe13Tree()
        {
            //Arrange
            var expectedTree = new BinaryTreeNodeStub<string, int>(_value3.ToString(), _value3);
            expectedTree.WithLeftNode(_value1.ToString(), _value1);

            var actualTree = new BST<string, int>();
            actualTree.Insert(_value2.ToString(), _value2);
            actualTree.Insert(_value1.ToString(), _value1);
            actualTree.Insert(_value3.ToString(), _value3);

            //Act
            actualTree.Delete(_value2.ToString());

            //Assert
            Assert.AreEqual(expectedTree, actualTree.Root);
        }

        [Test]
        public void Reverse_WithTree123_ShouldBeTree321()
        {
            //Arrange
            var expectedTree = new BinaryTreeNodeStub<string, int>(_value2.ToString(), _value2);
            expectedTree.WithLeftNode(_value3.ToString(), _value3)
                        .WithRightNode(_value1.ToString(), _value1);
            var actualTree = new BST<string, int>();
            actualTree.Insert(_value2.ToString(), _value2);
            actualTree.Insert(_value3.ToString(), _value3);
            actualTree.Insert(_value1.ToString(), _value1);

            //Act
            actualTree.Reverse();

            //Assert
            Assert.AreEqual(expectedTree, actualTree.Root);
        }
    }
}
