using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
//using Autodesk.AutoCAD.Geometry;

namespace RubiusTestAutocadPlugin
{
    public class PluginInitializer : IExtensionApplication 
    {
        private Document document;

        [CommandMethod("RUN_EDITOR")]
        public void Start()
        {
            document = Application.DocumentManager.MdiActiveDocument;
            document.Editor.WriteMessage("Привет из Autocad плагина");
            //GetDbEntities(document);
            EntitiesEditor form = new EntitiesEditor(document);
            form.Show();
        }


        public void Initialize()
        {
            var editor = Application.DocumentManager.MdiActiveDocument.Editor;
            editor.WriteMessage("Инициализация плагина.." + Environment.NewLine);
        }

        public void Terminate()
        {

        }
    }
}
