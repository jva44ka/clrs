using CLRS.Core.Structures;
using CLRS.Tests.Generators;
using NUnit.Framework;

namespace CLRS.Tests.Tests.Structures
{
    [TestFixture(Category = "Data Structures")]
    public class RBTTests
    {
        private readonly int _value1 = 1;
        private readonly int _value2 = 2;
        private readonly int _value3 = 3;

        [Test]
        public void Insert_312KeysAndValues_ShouldBeBalanced123Tree()
        {
            //Arrange
            var actualTree = new RBT<string, int>();
            var expectedTree = BinaryTreeStubGenerator.New(_value2.ToString(), _value2)
                                                      .WithLeftNode(_value1.ToString(), _value1)
                                                      .WithRightNode(_value3.ToString(), _value3)
                                                      .WithEnrichNilNodes();

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
            var actualTree = new RBT<string, int>();
            var expectedTree = BinaryTreeStubGenerator.New(_value2.ToString(), _value2)
                                                      .WithLeftNode(_value1.ToString(), _value1)
                                                      .WithRightNode(_value3.ToString(), _value3)
                                                      .WithEnrichNilNodes();

            //Act
            actualTree.Insert(_value2.ToString(), _value2);
            actualTree.Insert(_value3.ToString(), _value3);
            actualTree.Insert(_value1.ToString(), _value1);

            //Assert
            Assert.AreEqual(expectedTree, actualTree.Root);
        }

        [Test]
        public void Insert_321KeysAndValues_ShouldBeBalanced123Tree()
        {
            //Arrange
            var actualTree = new RBT<string, int>();
            var expectedTree = BinaryTreeStubGenerator.New(_value2.ToString(), _value2)
                                                      .WithLeftNode(_value1.ToString(), _value1)
                                                      .WithRightNode(_value3.ToString(), _value3)
                                                      .WithEnrichNilNodes();

            //Act
            actualTree.Insert(_value3.ToString(), _value3);
            actualTree.Insert(_value2.ToString(), _value2);
            actualTree.Insert(_value1.ToString(), _value1);

            //Assert
            Assert.AreEqual(expectedTree, actualTree.Root);
        }

        [Test]
        public void Delete_ByKey3With123Tree_ShouldBe12Tree()
        {
            //Arrange
            var actualTree = new RBT<string, int>();
            actualTree.Insert(_value3.ToString(), _value3);
            actualTree.Insert(_value1.ToString(), _value1);
            actualTree.Insert(_value2.ToString(), _value2);
            var expectedTree = BinaryTreeStubGenerator.New(_value2.ToString(), _value2)
                                                      .WithLeftNode(_value1.ToString(), _value1)
                                                      .WithEnrichNilNodes();

            //Act
            actualTree.Delete(_value3.ToString());

            //Assert
            Assert.AreEqual(expectedTree, actualTree.Root);
        }
    }
}
