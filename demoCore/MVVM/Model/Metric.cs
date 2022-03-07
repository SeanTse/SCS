using System.Windows;
using System.Xml.Serialization;

namespace demoCore.MVVM.Model
{
    public class Landmark
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Symbol { get; set; }
        public Point Cooridinate { get; set; }
    }

    class Measure
    {
        string Id { get; set; }
        string Name { get; set; }
        string Description { set; get; }
        double Value { get; set; }
        string Unit { get; set; }
        string Reference { get; set; }
    }
}
