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
        public void Pause(MediaFile mediaFile)
        {

        }

        public void Pause(Playlist playlist)
        {

        }

        public void Stop(MediaFile mediaFile)
        {

        }

        public void Stop(Playlist playlist)
        {

        }
        public void Next(MediaFile mediaFile)
        {

        }

        public void Next(Playlist playlist)
        {

        }

        public void Previous(MediaFile mediaFile)
        {

        }

        public void Previous(Playlist playlist)
        {

        }
    }
}
