using EPAM_Task_0.MediaFiles;
using System;
using System.Collections.Generic;

namespace EPAM_Task_0

{
    class Program
    {
        static void Main(string[] args)
        {
            MediaLibrary m = new MediaLibrary();
            List<MediaFile> list = new List<MediaFile>
            {
                new Audio(),
                new Video(),
                new Video(),
                new Image()
            };
            MediaFile media = new Image(-1, "", -1, 1,5);
            media.Action();
            Console.WriteLine();
            Console.WriteLine("Hello World!");
        }
    }
}
