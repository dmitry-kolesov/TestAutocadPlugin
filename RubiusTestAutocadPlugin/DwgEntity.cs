using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.Interop.Common;

namespace RubiusTestAutocadPlugin
{
    public enum EntityType
    {
        Layer,
        Point,
        Line,
        Circle
    }

    public class DwgEntity
    {
        public string Name { get { return Type.ToString() + ((Type == EntityType.Layer) ? (Entity as AcadLayer).ObjectID : (Entity as AcadEntity).ObjectID); } }
        public string LayerName { get { return ((Type == EntityType.Layer) ? (Entity as AcadLayer).Name : (Entity as AcadEntity).Layer); } }

        public EntityType Type { get; set; }

        public object Entity { get; set; }

        public DwgEntity(object entity, EntityType type)
        {
            Entity = entity;
            Type = type;
        }
    }
}
