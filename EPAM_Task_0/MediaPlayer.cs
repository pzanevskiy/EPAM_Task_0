using EPAM_Task_0.MediaFiles;
using System;

namespace EPAM_Task_0
{
    public class MediaPlayer
    {
        public void Play(MediaFile mediaFile)
        {
            Console.WriteLine("plays");
        }

        public void Play(Playlist playlist)
        {
            foreach (var item in playlist.MediaFiles)
            {
                Play(item);
            }
        }
    }
}
