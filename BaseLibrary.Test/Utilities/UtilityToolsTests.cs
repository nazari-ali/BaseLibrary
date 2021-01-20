using BaseLibrary.Tool.Utilities;
using NUnit.Framework;

namespace BaseLibrary.Test.Utilities
{
    [TestFixture]
    public class UtilityToolsTests
    {
        [Test]
        [TestCase(4)]
        [TestCase(6)]
        public void GetRandomCode_WhenCall_ReturnSuccess(int numberOfCode)
        {
            // Act
            var result = UtilityTools.GetRandomCode(numberOfCode);

            // Assert
            Assert.That(result.ToString(), Has.Length.EqualTo(numberOfCode));
        }
    }
}
