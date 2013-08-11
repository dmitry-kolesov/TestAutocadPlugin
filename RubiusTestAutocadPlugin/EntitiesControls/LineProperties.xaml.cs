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
    /// Interaction logic for LineProperties.xaml
    /// </summary>
    public partial class LineProperties : UserControl
    {
        public AcadLine Entity { get; set; }
        Point3D point1;
        public Point3D Point1 { get { return point1; } set { point1 = value; } }
        Point3D point2;
        public Point3D Point2 { get { return point2; } set { point2 = value; } }

        public double Point1X { get { return point1.X; } set { point1.X = value; } }
        public double Point1Y { get { return point1.Y; } set { point1.Y = value; } }
        public double Point1Z { get { return point1.Z; } set { point1.Z = value; } }

        public double Point2X { get { return point2.X; } set { point2.X = value; } }
        public double Point2Y { get { return point2.Y; } set { point2.Y = value; } }
        public double Point2Z { get { return point2.Z; } set { point2.Z = value; } }

        public LineProperties()
        {
            InitializeComponent();
        }

        public LineProperties(AcadLine entity)
            : this()
        {
            Entity = entity;
            var coord = Entity.StartPoint as double[];
            this.point1 = new Point3D(coord[0], coord[1], coord[2]);

            coord = Entity.EndPoint as double[];
            this.point2 = new Point3D(coord[0], coord[1], coord[2]);

            DataContext = this;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            Entity.StartPoint = new double[] { point1.X, point1.Y, point1.Z};
            Entity.EndPoint = new double[] { point2.X, point2.Y, point2.Z };
        }

        //private void pointTbx_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    double res;
        //    if (double.TryParse((e.Source as TextBox).Text, out res))
        //        e.Handled = true;
        //    else
        //        e.Source = e.OriginalSource;
        //    e.Handled = false;
        //}

        //private void point1XTbx_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        //{
        //    ;
        //}
    }
}
