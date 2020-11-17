using EPAM_Task_0.MediaFiles.Enums;
using System;

namespace EPAM_Task_0.MediaFiles
{
    public class Audio : MediaFile
    {
        public string Singer { get; private set; }

        public string Genre { get; private set; }

        public double Duration { get; private set; }

        public Audio()
        {
            this.MediaType = MediaType.Audio;
        }

        public Audio(int id, string name, double size, string singer, string genre, double duration) : base(id, name, size)
        {
            this.MediaType = MediaType.Audio;
            this.Singer = singer;
            this.Genre = genre;
            this.Duration = duration;
        }

        public override void Info()
        {
            Console.WriteLine("This is audio");
        }
    }
}
