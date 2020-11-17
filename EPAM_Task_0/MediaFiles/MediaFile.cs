using EPAM_Task_0.MediaFiles.Enums;

namespace EPAM_Task_0.MediaFiles
{
    public abstract class MediaFile
    {

        public int Id { get; set; }

        public string Name { get; private set; }

        public double Size { get; private set; }

        protected MediaType MediaType { get; set; }


        public MediaFile()
        {

        }

        public MediaFile(int id, string name, double size)
        {
            this.Id = id;
            this.Name = name;
            this.Size = size;
        }

        public abstract void Info();
    }
}
