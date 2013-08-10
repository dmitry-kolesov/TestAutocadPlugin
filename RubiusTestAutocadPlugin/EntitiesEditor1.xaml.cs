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
        
        Dictionary<string, List<AcadPoint>> pointsDict = new Dictionary<string, List<AcadPoint>>();
        Dictionary<string, List<AcadLine>> linesDict = new Dictionary<string, List<AcadLine>>();
        Dictionary<string, List<AcadCircle>> circlesDict = new Dictionary<string, List<AcadCircle>>();

         List<AcadPoint> points = new List<AcadPoint>();
         List<AcadLine> lines = new List<AcadLine>();
         List<AcadCircle> circles = new List<AcadCircle>();

        List<AcadLayer> layers = new List<AcadLayer>();

        public EntitiesEditor(Document _document)
        {
            InitializeComponent();
            document = _document;
        }

        private void reloadBtn_Click(object sender, RoutedEventArgs e)
        {
            //var dict = GetDbEntities(document);
            GetEntities();

            pointsCmbx.ItemsSource = points;
            linesCmbx.ItemsSource = lines;
            circlesCmbx.ItemsSource = circles;
            layersCmbx.ItemsSource = layers;

            lvEntities.ItemsSource = entities;
            //treeView.ItemsSource = dict;
            //if (dict.Count > 0)
            {
                //var el = dict.FirstOrDefault();
                //MessageBox.Show(el.Key + "__" + el.Value);
                
            }
        }

        public void GetEntities()
        {
            Document dwg = Application.DocumentManager.MdiActiveDocument;

            AcadDocument acdDoc = (AcadDocument)dwg.AcadDocument;

            foreach (AcadEntity ent in acdDoc.ModelSpace)
            {
                if (ent is AcadPoint)
                {
                    var pt = ent as AcadPoint;
                    //var layer = pt.Layer;
                    //if (!points.Keys.Contains(layer))
                    //    points.Add(layer, new List<AcadPoint>());
                    //points[layer].Add(pt);
                    points.Add(pt);
                    entities.Add(new DwgEntity(ent, EntityType.Point));
                }
                else if (ent is AcadCircle)
                {
                    var circle = ent as AcadCircle;
                    //var layer = circle.Layer;
                    //if (!circles.Keys.Contains(layer))
                    //    circles.Add(layer, new List<AcadCircle>());
                    //circles[layer].Add(circle);
                    circles.Add(circle);
                    entities.Add(new DwgEntity(ent, EntityType.Circle));
                }
                else if (ent is AcadLine)
                {
                    var line = ent as AcadLine;
                    //var layer = line.Layer;
                    //if (!lines.Keys.Contains(layer))
                    //    lines.Add(layer, new List<AcadLine>());
                    //lines[layer].Add(line);
                    lines.Add(line);
                    entities.Add(new DwgEntity(ent, EntityType.Line));
                }
                //else if (ent is Autodesk.AutoCAD.Interop.Common.AcadLayer)
                //{
                //    var layer = ent as Autodesk.AutoCAD.Interop.Common.AcadLayer;
                //}
                //else if (ent is Autodesk.AutoCAD.Interop.Common.AcadLayers)
                //{
                //    Autodesk.AutoCAD.Interop.Common.AcadLayers layers = ent as Autodesk.AutoCAD.Interop.Common.AcadLayers;
                //}
            }

            foreach (var layerEnt in acdDoc.Layers) // unfortunataly cant use linq because of acdDoc.Layers not allowed (not enumerable)
            {
                var layer = layerEnt as AcadLayer;
                layers.Add(layer);
                if (layerEnt is AcadEntity) 
                    entities.Add(new DwgEntity(layerEnt as AcadEntity, EntityType.Layer));
            }
        }

        //private Dictionary<string, List<ObjectId>> GetDbEntities(Document _document)
        //{
        //    Database db = _document.Database;
        //    Dictionary<string, List<ObjectId>> dict = new Dictionary<string, List<ObjectId>>();
        //    using (Transaction t = db.TransactionManager.StartTransaction())
        //    {
        //        for (long i = db.BlockTableId.Handle.Value; i < db.Handseed.Value; i++)
        //        {
        //            ObjectId id = ObjectId.Null;
        //            Handle h = new Handle(i); 
        //            if (db.TryGetObjectId(h, out id))
        //            {
        //                string type = id.ObjectClass.Name;
        //                if (!dict.Keys.Contains(type))
        //                    dict.Add(type, new List<ObjectId>());
        //                dict[type].Add(id);
        //            }
        //        }
        //        t.Commit();
        //    }
        //    return dict;
        //}
    }
}
