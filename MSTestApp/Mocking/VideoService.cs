
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSTestApp.Mocking
{
    
    class VideoService
    {

        private IFileReader _fileReader;
        private IVideoRepository _videoRepository;

        //construction injection (to pass either the real class or fake test class into)
        public VideoService(IFileReader fileReader = null, IVideoRepository videoRepository = null)
        {
            //If fileReader is null, iniate a new FileReader
            _fileReader = fileReader ?? new FileReader();
            _videoRepository = videoRepository ?? new VideoRepository();
        }

        public string ReadVideoTitle()
        {
            var str = _fileReader.Read("video.txt");
            var video = new Video();
            video = null;

            if (video == null)
                return "Error Parsing The Video";

            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {

            var videoIds = new List<int>();

            var videos = _videoRepository.GetUnprocessedVideos();

            foreach (var v in videos)
            {
                videoIds.Add(v.Id);
            }

            return String.Join(",", videoIds);

        }

    }

    public class VideoContext : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Video> Videos { get; set; }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }
}
