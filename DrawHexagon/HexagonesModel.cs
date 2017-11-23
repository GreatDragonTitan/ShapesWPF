namespace DrawHexagon
{
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Xml.Serialization;

    public class HexagonesModel
    {
        public ObservableCollection<HexagonModel> Shapes;

        public HexagonesModel()
        {
            Shapes = new ObservableCollection<HexagonModel>();
        }

        public void Serialize(string _path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<HexagonModel>));

            using (FileStream fs = new FileStream(_path, FileMode.Create))
            {
                formatter.Serialize(fs, Shapes);
            }
        }

        public void Deserialize(string _path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<HexagonModel>));

            using (FileStream fs = new FileStream(_path, FileMode.OpenOrCreate))
            {
                Shapes = (ObservableCollection<HexagonModel>)formatter.Deserialize(fs);
            }
        }
    }
}
