using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace susalem.wpf.Visual3D
{
    public class ChildSelectableVisual3D:FileModelVisual3D
    {
        public Model3DGroup Group =>(Model3DGroup) this.Visual3DModel;
    }
}
