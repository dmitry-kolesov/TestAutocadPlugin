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
using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Interop.Common;

namespace RubiusTestAutocadPlugin
{
    /// <summary>
    /// Interaction logic for PropertyEditorFactory.xaml
    /// </summary>
    public partial class PropertyEditorFactory : UserControl
    {
        public PropertyEditorFactory()
        {
            InitializeComponent();
        }

        public void Init(DwgEntity entity)
        {
            mainGrid.Children.Clear();
            UserControl resultControl = null;
            switch (entity.Type)
            {
                case EntityType.Layer:
                    resultControl = new LayerProperties(entity.Entity as AcadLayer);
                    break;
                case EntityType.Line:
                    resultControl = new LineProperties(entity.Entity as AcadLine);
                    break;
                case EntityType.Circle:
                    resultControl = new CircleProperties(entity.Entity as AcadCircle);
                    break;
                case EntityType.Point:
                    resultControl = new PointProperties(entity.Entity as AcadPoint);
                    break;
            }
            mainGrid.Children.Add(resultControl);
        }
    }
}
