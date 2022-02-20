using System.IO;

#nullable enable
namespace SliceConsole
{
    class Program
    {

        private static readonly string VideoDirectoryPath = @"video";

        static void Main()
        {
            var movies = Directory.GetFiles(VideoDirectoryPath, "*.mp4", SearchOption.AllDirectories);
            var i = 0;
            foreach(var movie in movies)
            {
                System.Console.WriteLine($"処理中.. {++i}/{movies.Length}");
                var capture = new Capture() { SpanMsec = 500 };
                capture.FilePath = $"{movie}";
                var directory = capture.Start();
                if (directory is not null)
                {
                    File.Delete(movie);
                }

            }
        }
    }
}
