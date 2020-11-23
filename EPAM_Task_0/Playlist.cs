using EPAM_Task_0.MediaFiles;
using System.Collections.Generic;


namespace EPAM_Task_0
{
    public class Playlist
    {
        private string _name;
        public string Name 
        {
            get { return _name; }
            private set
            {
                if (value == "")
                {
                    _name = "unknown";
                }
                else
                {
                    _name = value;
                }
            }
        }

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
