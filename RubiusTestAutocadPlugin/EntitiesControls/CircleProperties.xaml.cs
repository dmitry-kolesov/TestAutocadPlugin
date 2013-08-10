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
        Point3D point1;
        public Point3D Center { get { return point1; } }
        Point3D point2;
        public Point3D Point2 { get { return point2; } }

        public CircleProperties()
        {
            InitializeComponent();
        }

        public CircleProperties(AcadCircle entity)
            : this()
        {
            Entity = entity;
            var coord = Entity.Center as double[];
            this.point1 = new Point3D(coord[0], coord[1], coord[2]);

            DataContext = this;
        }
    }
}
