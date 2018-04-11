
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace MSTestApp.Mocking
{
    [TestFixture]
    class VideoServiceTests
    {
        private VideoService _videoService;
        private Mock<IFileReader> _mockFileReader;
        private Mock<IVideoRepository> _videoRepository;

        [SetUp]
        public void SetUp()
        {
            //using Moq to create fake / mock objects for external dependencies
            //Google moq document and use the GitHub Wiki
            _mockFileReader = new Mock<IFileReader>();
            _videoRepository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_mockFileReader.Object, _videoRepository.Object);
        }

        //Example test using interfaces to inject a dependency. A fake class is made to avoid looking at an external file
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _mockFileReader.Setup(fr => fr.Read("video.txt")).Returns("");
            
            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnEmptyString()
        {

            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));

        }

        [Test]
        public void GetUnprocessedVideosAsCsv_FewUnprocessedVideos_ReturnAString()
        {

            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>
            {
                new Video{Id = 1},
                new Video{Id = 2},
                new Video{Id = 3}
            });

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));

        }

    }
}
