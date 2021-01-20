using BaseLibrary.Test.Models;
using BaseLibrary.Tool.Extensions;
using BaseLibrary.Tool.Utilities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseLibrary.Test.Utilities
{
    [TestFixture]
    public class HttpClientToolsTests
    {
        private Dictionary<string, string> _headers;

        [SetUp]
        public void SetUp()
        {
            _headers = new Dictionary<string, string>
            {
                { "user-agent", ".net core" }
            };
        }

        [Test]
        [TestCase("https://api.github.com/users/zellwk/repos")]
        public async Task GetAsync_CallGetApiWithHeader_Successed(string url)
        {
            // Act
            var result = await HttpClientTools.GetAsync(url, _headers);

            // Assert
            var deSerializeResult = result.Deserialize<List<HttpClientUserResponse>>();
            Assert.That(deSerializeResult, Is.TypeOf(typeof(List<HttpClientUserResponse>)));
        }

        [Test]
        [TestCase("https://jsonplaceholder.typicode.com/posts")]
        public async Task GetAsync_CallGetApi_Successed(string url)
        {
            // Act
            var result = await HttpClientTools.GetAsync(url);

            // Assert
            var deSerializeResult = result.Deserialize<IEnumerable<HttpClientPostResponse>>();
            Assert.That(deSerializeResult, Is.TypeOf(typeof(List<HttpClientPostResponse>)));
        }

        [Test]
        [TestCase("https://api.github.com/users/zellwk/repos?sort=pushed")]
        public async Task GetAsync_CallGetApiWithHeaderAndParameter_Successed(string url)
        {
            // Act
            var result = await HttpClientTools.GetAsync(url, _headers);

            // Assert
            var deSerializeResult = result.Deserialize<List<HttpClientUserResponse>>();
            Assert.That(deSerializeResult, Is.TypeOf(typeof(List<HttpClientUserResponse>)));
        }

        [Test]
        [TestCase("https://jsonplaceholder.typicode.com/posts/1")]
        public async Task GetAsync_CallGetPostApiWithParameter_Successed(string url)
        {
            // Act
            var result = await HttpClientTools.GetAsync(url);

            // Assert
            var deSerializeResult = result.Deserialize<HttpClientPostResponse>();
            Assert.That(deSerializeResult, Is.TypeOf(typeof(HttpClientPostResponse)));
        }

        [Test]
        [TestCase("https://jsonplaceholder.typicode.com/posts/1/comments")]
        public async Task GetAsync_CallGetCommentApiWithParameter_Successed(string url)
        {
            // Act
            var result = await HttpClientTools.GetAsync(url);

            // Assert
            var deSerializeResult = result.Deserialize<List<HttpClientCommentResponse>>();
            Assert.That(deSerializeResult, Is.TypeOf(typeof(List<HttpClientCommentResponse>)));
        }

        [Test]
        [TestCase("https://jsonplaceholder.typicode.com/posts")]
        public async Task PostAsync_CallPostApi_Successed(string url)
        {
            // Arrange
            var data = new
            {
                title = "foo",
                body = "bar",
                userId = 1,
            };

            // Act
            var result = await HttpClientTools.PostAsync(url, data);

            // Assert
            var deSerializeResult = result.Deserialize<HttpClientPostResponse>();
            Assert.That(deSerializeResult.id, Is.EqualTo(101));
            Assert.That(deSerializeResult.userId, Is.EqualTo(1));
            Assert.That(deSerializeResult.title, Is.EqualTo("foo"));
        }

        [Test]
        [TestCase("https://jsonplaceholder.typicode.com/posts/1")]
        public async Task PutAsync_CallPutApi_Successed(string url)
        {
            // Arrange
            var data = new
            {
                id = 1,
                title = "foo 123",
                body = "bar 98",
                userId = 1,
            };

            // Act
            var result = await HttpClientTools.PutAsync(url, data);

            // Assert
            var deSerializeResult = result.Deserialize<HttpClientPostResponse>();
            Assert.That(deSerializeResult.id, Is.EqualTo(1));
            Assert.That(deSerializeResult.title, Is.EqualTo("foo 123"));
            Assert.That(deSerializeResult.body, Is.EqualTo("bar 98"));
        }

        [Test]
        [TestCase("https://jsonplaceholder.typicode.com/posts/1")]
        public async Task PatchAsync_CallPatchApi_Successed(string url)
        {
            // Arrange
            var data = new
            {
                body = "bar 123567",
            };

            // Act
            var result = await HttpClientTools.PatchAsync(url, data);

            // Assert
            var deSerializeResult = result.Deserialize<HttpClientPostResponse>();
            Assert.That(deSerializeResult.id, Is.EqualTo(1));
            Assert.That(deSerializeResult.title, Is.EqualTo("sunt aut facere repellat provident occaecati excepturi optio reprehenderit"));
            Assert.That(deSerializeResult.body, Is.EqualTo("bar 123567"));
        }

        [Test]
        [TestCase("https://jsonplaceholder.typicode.com/posts/1")]
        public async Task DeleteAsync_CallDeleteApi_Successed(string url)
        {
            // Act
            var result = await HttpClientTools.DeleteAsync(url);

            // Assert
            Assert.That(result, Is.EqualTo("{}"));
        }
    }
}
