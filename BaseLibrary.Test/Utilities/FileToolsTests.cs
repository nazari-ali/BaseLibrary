using BaseLibrary.Test.Commons;
using BaseLibrary.Utilities;
using NUnit.Framework;
using System.IO;

namespace BaseLibrary.Test.Utilities
{
    [TestFixture]
    public class FileToolsTests : UtilitiesBaseTest
    {
        [TearDown]
        public void TearDown()
        {
            FileTools.RemoveDirectory(RootDirectory, true);
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        public void CreateDirectory_FailedToCreateDirectory_ReturnFalse(string path)
        {
            // Act
            var result = FileTools.CreateDirectory(path);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CreateDirectory_SuccessedToCreateDirectory_ReturnTrue()
        {
            // Act
            var result = FileTools.CreateDirectory(DirectoryTest);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void RemoveDirectory_FailedToRemoveDirectory_ReturnFalse()
        {
            // Act
            var result = FileTools.RemoveDirectory(DirectoryTestNotExist);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void RemoveDirectory_SuccessedToRemoveDirectory_ReturnTrue()
        {
            // Arrange
            FileTools.CreateDirectory(DirectoryTest);

            // Act
            var result = FileTools.RemoveDirectory(DirectoryTest, true);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CopyFilePathToPath_SuccessedCopyToDestinition_ReturnTrue()
        {
            // Arrange
            FileTools.RemoveFile(DestinitionFile);
            File.CreateText(FileTest).Close();

            // Act
            var result = FileTools.CopyFilePathToPath(FileTest, DestinitionFile);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CopyFilePathToPath_SuccessedCopyToDestinitionWithOverwrite_ReturnTrue()
        {
            // Arrange
            File.CreateText(FileTest).Close();

            // Act
            var result = FileTools.CopyFilePathToPath(FileTest, DestinitionFile, true);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CopyFilePathToPath_FailedCopyToDestinition_ReturnFalse()
        {
            // Arrange
            FileTools.RemoveFile(FileTest);

            // Act
            var result = FileTools.CopyFilePathToPath(FileTest, DestinitionFile);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void MoveFilePathToPath_SuccessedMoveToDestinition_ReturnTrue()
        {
            // Arrange
            FileTools.RemoveFile(DestinitionFile);
            File.CreateText(FileTest).Close();

            // Act
            var result = FileTools.MoveFilePathToPath(FileTest, DestinitionFile);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void MoveFilePathToPath_SuccessedMoveToDestinitionWithOverwrite_ReturnTrue()
        {
            // Arrange
            File.CreateText(FileTest).Close();

            // Act
            var result = FileTools.MoveFilePathToPath(FileTest, DestinitionFile, true);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void MoveFilePathToPath_FailedMoveToDestinition_ReturnFalse()
        {
            // Arrange
            FileTools.RemoveFile(FileTest);

            // Act
            var result = FileTools.MoveFilePathToPath(FileTest, DestinitionFile);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void RemoveFile_FailedToRemoveFile_ReturnFalse()
        {
            // Act
            var result = FileTools.RemoveFile(FileTestNotExist);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void RemoveFile_SuccessedToRemoveFile_ReturnTrue()
        {
            // Arrange
            File.CreateText(FileTest).Close();

            // Act
            var result = FileTools.RemoveFile(FileTest);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [TestCase("audio/aac", ".aac")]
        [TestCase("image/bmp", ".bmp")]
        [TestCase("text/csv", ".csv")]
        [TestCase("image/jpeg", ".jpg")]
        [TestCase("audio/mpeg", ".mp3")]
        [TestCase("video/mpeg", ".mpg")]
        [TestCase("video/mp4", ".mp4")]
        public void GetExtensionByContentType_WhenCalled_ReturnExpectedResult(string contentType, string expectedResult)
        {
            // Act
            var result = FileTools.GetExtensionByContentType(contentType);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("https://www.google.com/search?q=flower&safe=active&sxsrf=ALeKk02B_qo2o604LZysLNFotn20qH0GfQ:1609270721341&source=lnms&tbm=isch&sa=X&ved=2ahUKEwi_jsXN-PPtAhWpRhUIHeMNCBUQ_AUoAXoECBIQAw#imgrc=GQaYJR_Bgz8c3M", "text/html; charset=iso-8859-1")]
        [TestCase("https://static.honeykidsasia.com/wp-content/uploads/2019/05/mothers-day-flowers-768x549.jpg", "image/jpeg")]
        [TestCase("https://irsv.upmusics.com/99/Roozbeh%20Bemani%20-%20Alaaj%20(320).mp3", "audio/mpeg")]
        [TestCase("http://srv1.mihn.site/s7/archive/persian/s/Shabnam%20Jaleh/video/480/Shabnam%20Jaleh%20-%20Adat%20Nadaram%20[MihanMusic]%20480.mp4", "application/octet-stream")]
        [TestCase("http://dl.pop-music.ir/music/1399/Azar/Babak%20Radmanesh%20-%20Chaharbagh%20720.mp4", "video/mp4")]
        public void GetContentType_WhenCalled_ReturnExpectedResult(string url, string expectedResult)
        {
            // Act
            var result = FileTools.GetContentType(url);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("https://static.honeykidsasia.com/wp-content/uploads/2019/05/mothers-day-flowers-768x549.jpg", ".jpg")]
        [TestCase("https://irsv.upmusics.com/99/Roozbeh%20Bemani%20-%20Alaaj%20(320).mp3", ".mp3")]
        [TestCase("http://dl.pop-music.ir/music/1399/Azar/Babak%20Radmanesh%20-%20Chaharbagh%20720.mp4", ".mp4")]
        public void GetExtensionByUrle_WhenCalled_ReturnExpectedResult(string url, string expectedResult)
        {
            // Act
            var result = FileTools.GetExtensionByUrl(url);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void DirectoryCopy_NotFoundDirectory_ThrowDirectoryNotFoundException()
        {
            // Arrange
            FileTools.RemoveDirectory(OriginalDirectory, true);

            // Act, Assert
            Assert.That(() => FileTools.DirectoryCopy(OriginalDirectory, DestinitionDirectory, true), Throws.Exception.TypeOf<DirectoryNotFoundException>());
        }

        [Test]
        public void DirectoryCopy_CopyAllDirectoryAndAllFileFromOriginalDirectory_Successed()
        {
            // Arrange
            FileTools.RemoveDirectory(DestinitionDirectory, true);

            File.CreateText($"{OriginalDirectory}/file test 1.txt").Close();
            File.CreateText($"{OriginalDirectory}/file test 2.txt").Close();
            File.CreateText($"{OriginalDirectory}/file test 3.txt").Close();

            FileTools.CreateDirectory($"{OriginalDirectory}/sub directory 1/");
            File.CreateText($"{OriginalDirectory}/sub directory 1/file test 4.txt").Close();
            File.CreateText($"{OriginalDirectory}/sub directory 1/file test 5.txt").Close();

            FileTools.CreateDirectory($"{OriginalDirectory}/sub directory 2/");
            File.CreateText($"{OriginalDirectory}/sub directory 2/file test 6.txt").Close();

            // Act
            FileTools.DirectoryCopy(OriginalDirectory, DestinitionDirectory, true);

            // Assert
            Assert.That(Directory.Exists(DestinitionDirectory), Is.True);
            Assert.That(File.Exists($"{OriginalDirectory}/file test 2.txt"), Is.True);
            Assert.That(Directory.Exists($"{OriginalDirectory}/sub directory 2/"), Is.True);
        }
    }
}
