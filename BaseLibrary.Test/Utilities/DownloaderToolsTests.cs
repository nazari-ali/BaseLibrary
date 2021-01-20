using BaseLibrary.Test.Commons;
using BaseLibrary.Tool.Utilities;
using NUnit.Framework;
using System;
using System.Net;

namespace BaseLibrary.Test.Utilities
{
    [TestFixture]
    public class DownloaderToolsTests : UtilitiesBaseTest
    {
        [Test]
        [TestCase("")]
        [TestCase(" ")]
        public void Downloader_SendInvalidArgument_ThrowArgumentNullException(string url)
        {
            // Act, Assert
            Assert.That(() => DownloadTools.Download(url, DownloadDirectory), Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]
        [TestCase("https://static.honeykidsasia.com/wp-content/uploads/2019/05/mothers-day-flowers-768x549.jpg")]
        [TestCase("https://irsv.upmusics.com/99/Roozbeh%20Bemani%20-%20Alaaj%20(320).mp3")]
        [TestCase("http://dl.pop-music.ir/music/1399/Azar/Babak%20Radmanesh%20-%20Chaharbagh%20720.mp4")]
        [TestCase("https://aspb1.cdn.asset.aparat.com/aparat-video/a8677934a0e6a32291b41a5eb83ce8ab11179169-360p.mp4?wmsAuthSign=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0b2tlbiI6IjEwN2RlMTM2Nzk5NjgzM2I0MWI2M2M3ZDM2MzlmNmYxIiwiZXhwIjoxNjA5MzAxNTY4LCJpc3MiOiJTYWJhIElkZWEgR1NJRyJ9.5r6Aud0Y9wdCFC_vmsudRjUcps25VPLpcQ4OncHqpNw")]
        public void Downloader_SuccessedDownload_ReturnPath(string url)
        {
            // Act
            var result = DownloadTools.Download(url, DownloadDirectory);

            // Asert
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        [TestCase("https://irsv.upmusics.com/99/Roozbeh%20Bemani%20-%20Alaaj%20(320)")]
        [TestCase("https://irsv.upmusics.com/99/Roozbeh%20Bemani%20-%20Alaa.mp3")]
        public void Downloader_FailedDownload_ThrowFileNotFoundException(string url)
        {
            // Act, Assert
            Assert.That(() => DownloadTools.Download(url, DownloadDirectory), Throws.Exception.TypeOf<WebException>());
        }
    }
}
