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
    /// Interaction logic for LayerProperties.xaml
    /// </summary>
    public partial class LayerProperties : UserControl
    {
        
        public AcadLayer Entity { get; set; }
        Color color;
        public Color Color { set { color = value; } get { return color; } }

        public LayerProperties()
        {
            InitializeComponent();
        }

        public LayerProperties(AcadLayer entity)
            : this()
        {
            Entity = entity;
            color = new Color() { R = (byte)Entity.TrueColor.Red, G = (byte)Entity.TrueColor.Green, B = (byte)Entity.TrueColor.Blue };
            DataContext = this;
        }
    }
}
