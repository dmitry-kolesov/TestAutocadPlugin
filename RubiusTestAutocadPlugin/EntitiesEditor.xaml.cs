using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Interop.Common;

using Application = Autodesk.AutoCAD.ApplicationServices.Application;

namespace RubiusTestAutocadPlugin
{
    /// <summary>
    /// Interaction logic for EntitiesEditor.xaml
    /// </summary>
    public partial class EntitiesEditor : Window
    {
        Document document;

        List<DwgEntity> entities = new List<DwgEntity>();

        public EntitiesEditor(Document _document)
        {
            InitializeComponent();
            document = _document;
            lvEntities.SelectionChanged += lvEntities_SelectionChanged;

            InitData();
        }

        void lvEntities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selected = e.AddedItems[0] as DwgEntity;
                propertyEditor.Init(selected);
            }
        }

        private void InitData()
        {
            GetEntities();

            lvEntities.ItemsSource = entities;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvEntities.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("LayerName");
            view.GroupDescriptions.Add(groupDescription);
        }

        public void GetEntities()
        {
            entities.Clear();
            Document dwg = Application.DocumentManager.MdiActiveDocument;

            AcadDocument acdDoc = (AcadDocument)dwg.AcadDocument;

            foreach (AcadEntity ent in acdDoc.ModelSpace)
            {
                if (ent is AcadPoint)
                {
                    entities.Add(new DwgEntity(ent, EntityType.Point));
                }
                else if (ent is AcadCircle)
                {
                    entities.Add(new DwgEntity(ent, EntityType.Circle));
                }
                else if (ent is AcadLine)
                {
                    entities.Add(new DwgEntity(ent, EntityType.Line));
                }
            }

            foreach (var layerEnt in acdDoc.Layers) // unfortunataly cant use linq because of acdDoc.Layers not allowed (not enumerable)
            {
                var layer = layerEnt as AcadLayer;
                entities.Add(new DwgEntity(layer, EntityType.Layer));
            }
        }

    }
}
