using EPAM_Task_0.MediaFiles.Enums;
using System;

namespace EPAM_Task_0.MediaFiles
{
    public class Video : MediaFile
    {
        public double Duration { get; private set; }

        public string Quality { get; private set; }

        public Video()
        {
            this.MediaType = MediaType.Video;
        }

        public Video(int id, string name, double size, double duration, string quality) : base(id, name, size)
        {
            this.MediaType = MediaType.Audio;
            this.Duration = duration;
            this.Quality = quality;
        }

        protected override void Info()
        {
            Console.WriteLine("This is video");
        }

        public override void Action()
        {
            Info();
            Console.WriteLine($"{Name} play with quality - {Quality}, duration - {Duration}");
        }
    }
}
