using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.wpf.Interfaces
{
   public interface ISplitPaneService
    {
        bool IsPaneOpen { get; set; }

        void Register(SplitView splitView);

        void Navigate(string page);
    }
}
