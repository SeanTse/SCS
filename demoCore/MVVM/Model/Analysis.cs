using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

namespace demoCore.MVVM.Model
{
    public class Analysis
    {
        #region member
        Landmark[] landmarks;
        #endregion
        #region Constructor
        public Analysis(string name)
        {
            CephalometricId = CephalometricName = name;
        }
        public Analysis(string id, string name)
        {
            CephalometricId = id;
            CephalometricName = name;
        }
        #endregion
        string CephalometricName { get; set; }
        string CephalometricId { get; set; }

        public void LoadFromXml(string xml_file)
        {
            var fullPath = "D:\\vs_projects\\SCS\\demoCore\\Resources\\Analysis\\" + xml_file;
            FileStream fs = File.OpenRead(fullPath);
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Landmark[]), new XmlRootAttribute("Landmarks"));
                landmarks = (Landmark[])serializer.Deserialize(sr);
            }
        }
        public int LandmarkCount { get { return landmarks.Length; } }
        //public abstract void Calculate();
    }
}
