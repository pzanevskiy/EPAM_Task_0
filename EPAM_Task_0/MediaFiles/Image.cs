using EPAM_Task_0.MediaFiles.Enums;
using System;


namespace EPAM_Task_0.MediaFiles
{
    public class Image : MediaFile
    {
        public int Height { get; private set; }

        public int Width { get; private set; }

        public Image()
        {
            this.MediaType = MediaType.Image;
        }

        public Image(int id, string name, double size, int height, int wight) : base(id, name, size)
        {
            this.MediaType = MediaType.Audio;
            this.Height = height;
            this.Width = wight;
        }

        public override void Info()
        {
            Console.WriteLine("This is image");
        }
        public override void Action()
        {
            Console.WriteLine($"{Name} properties: height - {Height}, width - {Width}");
        }
    }
}
