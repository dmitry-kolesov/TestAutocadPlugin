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
using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Interop.Common;

namespace RubiusTestAutocadPlugin
{
    /// <summary>
    /// Interaction logic for CircleProperties.xaml
    /// </summary>
    public partial class CircleProperties : UserControl
    {
        public AcadCircle Entity { get; set; }
        Point3D center;
        public Point3D Center { get { return center; } }

        public double CenterX { get { return center.X; } set { center.X = value; } }
        public double CenterY { get { return center.Y; } set { center.Y = value; } }
        public double CenterZ { get { return center.Z; } set { center.Z = value; } }

        public CircleProperties()
        {
            InitializeComponent();
        }

        public CircleProperties(AcadCircle entity)
            : this()
        {
            Entity = entity;
            var coord = Entity.Center as double[];
            this.center = new Point3D(coord[0], coord[1], coord[2]);

            DataContext = this;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {

            Entity.Center = new double[] { center.X, center.Y, center.Z };
            var radius = 0.0;
            if (double.TryParse(radiusTbx.Text, out radius))
            {
                Entity.Radius = radius;
            }
        }
    }
}
