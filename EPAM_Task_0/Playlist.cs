using EPAM_Task_0.MediaFiles;
using System.Collections.Generic;


namespace EPAM_Task_0
{
    public class Playlist
    {
        public string Name { get; private set; }

        public List<MediaFile> MediaFiles { get; private set; }

        public Playlist()
        {
            this.Name = "untitled";
            this.MediaFiles = new List<MediaFile>();
        }

        public void RenamePlaylist(string newName)
        {
            this.Name = newName;
        }

        public void AddMedia(List<MediaFile> mediaFiles)
        {
            MediaFiles.AddRange(mediaFiles);
        }
        public void AddMedia(MediaFile mediaFile)
        {
            MediaFiles.Add(mediaFile);
        }

        public void RemoveMedia(MediaFile mediaFile)
        {
            MediaFiles.Remove(mediaFile);
        }

        public MediaFile SearchMediaFile(MediaFile mediaFile)
        {
            foreach (var item in MediaFiles)
            {
                if (item.Id == mediaFile.Id)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
