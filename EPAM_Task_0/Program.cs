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
            m.AddPlaylist("new 1", list);

            m.PlayPlaylist(m.playlists[1]);
            m.AddMediaToPlaylist(m.playlists[0], new Image());
            Console.WriteLine();
            Console.WriteLine("Hello World!");
        }
    }
}
