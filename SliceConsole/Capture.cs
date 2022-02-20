using System;
using System.IO;
using OpenCvSharp;
using OpenCvSharp.Extensions;

#nullable enable
namespace SliceConsole
{
    class Capture
    {

        public string? FilePath { get; set; }

        public int SpanMsec { get; set; }

        public string? Start()
        {

            if (String.IsNullOrEmpty(FilePath))
            {
                return null;
            }

            using (var capture = new VideoCapture(FilePath))
            {

                var fileName = Path.GetFileName(FilePath).Replace(".mp4", String.Empty);
                var directoryName = $"{Path.GetDirectoryName(FilePath)}/{fileName}";
                Directory.CreateDirectory(directoryName);

                var frameCount = capture.Get(VideoCaptureProperties.FrameCount);
                var fps = capture.Get(VideoCaptureProperties.Fps);
                var videoLengthMsec = (int)Math.Floor((frameCount / fps) * 1000);

                var mat = new Mat();
                for (var currentMsec = 0; currentMsec < videoLengthMsec; currentMsec += SpanMsec)
                {
                    capture.PosMsec = currentMsec;
                    capture.Read(mat);
                    if (mat.Empty())
                    {
                        break;
                    }
                    BitmapConverter.ToBitmap(mat).Save($@"{directoryName}/{fileName}_{currentMsec / 100}.png");
                }

                return directoryName;

            }

        }
    }
}
