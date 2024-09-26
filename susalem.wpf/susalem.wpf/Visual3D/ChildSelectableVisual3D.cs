using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace susalem.wpf.Visual3D
{
    public class ChildSelectableVisual3D : FileModelVisual3D
    {
        public Dictionary<string, Model3D> ChildrenDictionary { get; private set; }

        protected override void SourceChanged()
        {
            var ext = Path.GetExtension(Source);
            if (ext != null)
            {
                ext = ext.ToLower();
            }
            if (ext == ".3ds")
            {
                var r = new StudioMeshDictionaryReader(Dispatcher) { DefaultMaterial = DefaultMaterial, Freeze = false };
                (this.Visual3DModel, ChildrenDictionary) = r.Read(Source);
            }
            else
            {
                var importer = new ModelImporter { DefaultMaterial = this.DefaultMaterial };
                this.Visual3DModel = this.Source != null ? importer.Load(this.Source) : null;
            }
            this.OnModelLoaded();
        }
    }
}
