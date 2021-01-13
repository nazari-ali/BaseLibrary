using BaseLibrary.Test.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BaseLibrary.Test.Commons
{
    public class BaseTest
    {
        protected Mock<DbContext> DbContext;
        protected List<Artist> Artists { get; set; }

        [SetUp]
        public void SetUp()
        {
            Artists = new List<Artist>
            {
                new Artist
                {
                    FullName = "Shadmehr Aghili",
                    Gender = Gender.Male,
                    IsCertificate = false,
                    IsActive = true,
                    DateAdd = DateTime.UtcNow
                },
                new Artist
                {
                    FullName = "Abi",
                    Gender = Gender.Male,
                    IsCertificate = false,
                    IsActive = true,
                    DateAdd = DateTime.UtcNow
                },
                new Artist
                {
                    FullName = "Mohsen Yegane",
                    Gender = Gender.Male,
                    IsCertificate = true,
                    IsActive = true,
                    DateAdd = DateTime.UtcNow
                },
                new Artist
                {
                    FullName = "Farzad Farzin",
                    Gender = Gender.Male,
                    IsCertificate = true,
                    IsActive = true,
                    DateAdd = DateTime.UtcNow
                },
                new Artist
                {
                    FullName = "Nikita",
                    Gender = Gender.Female,
                    IsCertificate = false,
                    IsActive = true,
                    DateAdd = DateTime.UtcNow
                }
            };

            DbContext = GetMockDbContext();

            DbContext.Setup(x => x.Set<Artist>())
                .ReturnsDbSet(Artists);
        }

        private Mock<DbContext> GetMockDbContext()
        {
            var options = new DbContextOptionsBuilder<DbContext>()
                .UseInMemoryDatabase(databaseName: "FakeConnectionString")
                .Options;

            return new Mock<DbContext>(options);
        }
    }
}
