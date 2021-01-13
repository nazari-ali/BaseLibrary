using BaseLibrary.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLibrary.Test.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        [TestCase("", true, false)]
        [TestCase("", false, false)]
        [TestCase(" ", true, false)]
        [TestCase(" ", false, true)]
        [TestCase("Test Value", true, true)]
        [TestCase("Test Value", false, true)]
        public void HasValue_WhenCalled_ReturnExpectedResult(string value, bool ignoreWhiteSpace, bool expectedResult)
        {
            // Act
            var result = value.HasValue(ignoreWhiteSpace);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("Test Value")]
        public void ToGuid_SendInvalidValue_ReturnDefaultValue(string value)
        {
            // Act
            var result = value.ToGuid();

            // Assert
            Assert.That(result, Is.EqualTo((Guid)default));
        }

        [Test]
        [TestCase("add4aa0e-4b7f-4fd7-a4b9-7deb75765ef5")]
        public void ToGuid_SendValidValue_Successed(string value)
        {
            // Act
            var result = value.ToGuid();

            // Assert
            Assert.That(result, Is.EqualTo(Guid.Parse(value)));
        }

        [Test]
        [TestCase("Test 123", "Test ۱۲۳")]
        [TestCase("0912345678", "۰۹۱۲۳۴۵۶۷۸")]
        public void ToPersianNumber_ConvertEnglishNumberToPersianNumber_Successed(string value, string expectedResult)
        {
            // Act
            var result = value.ToPersianNumber();

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("Test ۱۲۳", "Test 123")]
        [TestCase("۰۹۱۲۳۴۵۶۷۸", "0912345678")]
        public void ToEnglishNumber_ConvertPersianNumberToEnglishNumber_Successed(string value, string expectedResult)
        {
            // Act
            var result = value.ToEnglishNumber();

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("ﮐلاس اولي ھا", "کلاس اولی ها")]
        public void FixPersianChars_ReplaceCharPersianInsteadArabicChar_Successed(string value, string expectedResult)
        {
            // Act
            var result = value.FixPersianChars();

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("", null)]
        [TestCase("  ﮐلاس اولي ھا از تاريخ 27 آبان گل يادتون نره. ", "کلاس اولی ها از تاریخ ۲۷ آبان گل یادتون نره.")]
        public void CleanString_SetTrimAndFixPersianCharsAndToPersianNumberAndNullIfEmptyMethods_Successed(string value, string expectedResult)
        {
            // Act
            var result = value.CleanString();

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void NullIfEmpty_SendEmptyString_ReturnNull()
        {
            // Act
            var result = "".NullIfEmpty();

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}
