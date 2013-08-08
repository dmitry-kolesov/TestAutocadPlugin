using System;
using System.Collections.Generic;
using System.Linq;
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

namespace RubiusTestAutocadPlugin
{
    /// <summary>
    /// Interaction logic for EntitiesEditor.xaml
    /// </summary>
    public partial class EntitiesEditor : Window
    {
        Document document;
        public EntitiesEditor(Document _document)
        {
            InitializeComponent();
            document = _document;
        }

        private void reloadBtn_Click(object sender, RoutedEventArgs e)
        {
            var dict = GetDbEntities(document);
            treeView.ItemsSource = dict;
            if (dict.Count > 0)
            {
                var el = dict.FirstOrDefault();
                MessageBox.Show(el.Key + "__" + el.Value);
                
                AcadDocument acdDoc = (Autodesk.AutoCAD.Interop.AcadDocument)dwg.AcadDocument;

                foreach (AcadEntity Ent in acdDoc.ModelSpace)
                {
                    Autodesk.AutoCAD.Interop.Common.AcadPoint pt = Ent as Autodesk.AutoCAD.Interop.Common.AcadPoint;
                    if (pt != null)
                    {
                        double[] coord = pt.Coordinates as double[];
                        dwg.Editor.WriteMessage("\nPoint location: {0}, {1}, {2}", coord[0], coord[1], coord[2]);
                    }
                }
            }
        }

        private Dictionary<string, List<ObjectId>> GetDbEntities(Document _document)
        {
            Database db = _document.Database;
            Dictionary<string, List<ObjectId>> dict = new Dictionary<string, List<ObjectId>>();
            using (Transaction t = db.TransactionManager.StartTransaction())
            {
                for (long i = db.BlockTableId.Handle.Value; i < db.Handseed.Value; i++)
                {
                    ObjectId id = ObjectId.Null;
                    Handle h = new Handle(i); 
                    if (db.TryGetObjectId(h, out id))
                    {
                        string type = id.ObjectClass.Name;
                        if (!dict.Keys.Contains(type))
                            dict.Add(type, new List<ObjectId>());
                        dict[type].Add(id);
                    }
                }
                t.Commit();
            }
            return dict;
        }
    }
}
