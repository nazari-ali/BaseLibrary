using BaseLibrary.Utilities;
using NUnit.Framework;
using System;

namespace BaseLibrary.Test.Commons
{
    public class UtilitiesBaseTest
    {
        protected string BaseDirectory => $"{AppContext.BaseDirectory}";
        protected string RootDirectory => $"{BaseDirectory}/root/";
        protected string OriginalDirectory => $"{RootDirectory}original/";
        protected string FileTest => $"{OriginalDirectory}file.txt";
        protected string FileTestNotExist => $"{OriginalDirectory}file not exist.txt";
        protected string DirectoryTest => $"{OriginalDirectory}directory/";
        protected string DirectoryTestNotExist => $"{OriginalDirectory}directory not exist/";
        protected string DestinitionDirectory => $"{RootDirectory}destinition/";
        protected string DestinitionFile => $"{DestinitionDirectory}file 1.txt";
        protected string DownloadDirectory => $"{DestinitionDirectory}download/";

        [SetUp]
        public void SetUp()
        {
            FileTools.CreateDirectory(RootDirectory);
            FileTools.CreateDirectory(OriginalDirectory);
            FileTools.CreateDirectory(DestinitionDirectory);
            FileTools.CreateDirectory(DownloadDirectory);
        }
    }
}
