using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelixToolkit.Wpf;
using susalem.wpf.Visual3D;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace susalem.wpf.ViewModels
{
    public partial class ThreeDViewModel : ObservableObject
    {
        [ObservableProperty]
        private List<Model3D> selectedModels;

        [ObservableProperty]
        Model3D selectedModel;

        Viewport3D viewport;

        [RelayCommand]

        void Loaded(HelixViewport3D vp)
        {
            if (viewport is null)
            {
                viewport = vp.Viewport;
                foreach(var item in viewport.Children)
                {
                    //foreach (var child in item.)
                    //{
                    //}

                    _3d = item as ChildSelectableVisual3D;
                    if(_3d!=null)
                    {
                        //for
                    }
                    Console.WriteLine(item);
                }
                PointSelectionCommand = new PointSelectionCommand(viewport, OnModelSelected);
                viewport.InputBindings.Add(new MouseBinding(PointSelectionCommand, new MouseGesture(MouseAction.LeftClick)));

            }

        }

        ChildSelectableVisual3D _3d;

        private void OnModelSelected(object? sender, ModelsSelectedEventArgs e)
        {
            if (SelectedModel is not null)
            {
                if (SelectedModel is GeometryModel3D model)
                {
                    model.Material = null;
                }
            }

            SelectedModel = e.SelectedModels.FirstOrDefault();
            Model3D model3D = e.SelectedModels.FirstOrDefault() as Model3D;
            if (SelectedModel is GeometryModel3D model3d)
            {
                model3d.Material = Materials.Yellow;
            }

            Console.WriteLine(_3d);
            foreach(var item in _3d.ChildrenDictionary)
            {
                GeometryModel3D geo3d = item.Value as GeometryModel3D;
                if(geo3d.Material== Materials.Yellow)
                {
                    Console.WriteLine(item.Key);
                    Console.WriteLine("666");

                }
            }
            var SelectedModels = e.SelectedModels;
            //foreach (var model in SelectedModels)
            //{
            //    Debug.Write(model.GetName());
            //}




            //MessageBox.Show(SelectedModel.GetName());
        }

        public PointSelectionCommand PointSelectionCommand { get; private set; }

        public ThreeDViewModel()
        {

        }
    }
}
