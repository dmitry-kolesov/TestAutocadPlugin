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
using Autodesk.AutoCAD.Colors;
using Color = System.Windows.Media.Color;

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
        // should use this one for not saving value directly to entity
        //String layerName;
        //public String LayerName { set { layerName = value; } get { return layerName; } }
        //bool layerOn;
        //public bool LayerOn { set { layerOn = value; } get { return layerOn; } }

        public LayerProperties()
        {
            InitializeComponent();
        }

        public LayerProperties(AcadLayer entity)
            : this()
        {
            Entity = entity;
            color = new Color() { A = 255, R = (byte)Entity.TrueColor.Red, G = (byte)Entity.TrueColor.Green, B = (byte)Entity.TrueColor.Blue };
            DataContext = this;
        }

        // should use this one for not saving value directly to entity
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            Entity.Name = nameTbx.Text;
            Entity.LayerOn = visibilityChkbx.IsChecked ?? false;
            AcadAcCmColor acCol = Entity.TrueColor;
            acCol.ColorMethod = AcColorMethod.acColorMethodByRGB;
            acCol.SetRGB(color.R, color.G, color.B);
            Entity.TrueColor = acCol;
            acCol = null;
        }
    }
}
