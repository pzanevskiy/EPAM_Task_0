using EPAM_Task_0.MediaFiles;
using System.Collections.Generic;

namespace EPAM_Task_0
{
    public class MediaLibrary
    {
        private MediaPlayer mediaPlayer;

        public List<Playlist> playlists;

        public MediaLibrary()
        {
            playlists = new List<Playlist>();
            playlists.Add(new Playlist());
            mediaPlayer = new MediaPlayer();
        }
        public void AddPlaylist(string name, List<MediaFile> mediaFiles)
        {
            Playlist playlist = new Playlist();
            playlist.RenamePlaylist(name);
            playlist.AddMedia(mediaFiles);
            playlists.Add(playlist);
        }

        public void AddPlaylist(Playlist playlist)
        {
            playlists.Add(playlist);
        }

        public void RemovePlaylist(Playlist playlist)
        {
            playlists.Remove(playlist);
        }

        

        public void AddMediaToPlaylist(Playlist playlist, MediaFile mediaFile)
        {
            playlist.AddMedia(mediaFile);
        }

    }
}
