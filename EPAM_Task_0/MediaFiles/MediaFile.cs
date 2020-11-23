using EPAM_Task_0.MediaFiles.Enums;

namespace EPAM_Task_0.MediaFiles
{
    public abstract class MediaFile
    {
      
        private int _id;
        private string _name;
        private double _size;

        public int Id 
        {
            get { return _id; }
            private set 
            { 
                if (value < 0)
                {
                    _id = 0;
                }
                else
                {
                    _id = value;
                }
            }
        }

        public string Name
        {
            get { return _name; } 
            private set 
            {
                if (value == "")
                {
                    _name = "unknown file";
                }
                else
                {
                    _name = value;
                }
            } 
        }

        public double Size
        {
            get { return _size; }
            private set 
            {
                if (value <= 0)
                {
                    System.Console.WriteLine("Размер должен быть больше 0");
                    _size = 1;
                }
                else
                {
                    _size = value;
                }
            }
        }

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
        public abstract void Action();
    }
}
