using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using Autodesk.AutoCAD.Interop.Common;

namespace RubiusTestAutocadPlugin
{
    /// <summary>
    /// Interaction logic for PointProperties.xaml
    /// </summary>
    public partial class PointProperties : UserControl
    {
        public AcadPoint Entity { get; set; }
        Point3D point;
        public Point3D Point { get { return point; } set { point = value; } }

        public double PointX { get { return point.X; } set { point.X = value; } }
        public double PointY { get { return point.Y; } set { point.Y = value; } }
        public double PointZ { get { return point.Z; } set { point.Z = value; } }

        public PointProperties()
        {
            InitializeComponent();
        }

        public PointProperties(AcadPoint entity) : this()
        {
            Entity = entity;
            var coord = Entity.Coordinates as double[];
            this.point = new Point3D(coord[0], coord[1], coord[2]);

            DataContext = this;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            Entity.Coordinates = new double[] { point.X, point.Y, point.Z };
        }
    }
}
