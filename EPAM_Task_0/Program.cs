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
            MediaPlayer player = new MediaPlayer();
            player.Play(new Playlist());
            player.Play(new Image());

            
        }
    }
}
