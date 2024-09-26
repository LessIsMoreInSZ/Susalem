using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
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
            if(viewport is null)
            {
                viewport= vp.Viewport;
                PointSelectionCommand = new PointSelectionCommand(viewport, OnModelSelected);
                viewport.InputBindings.Add(new MouseBinding(PointSelectionCommand, new MouseGesture(MouseAction.LeftClick)));

            }

        }

        private void OnModelSelected(object? sender, ModelsSelectedEventArgs e)
        {
            if (SelectedModel is not null)
            {
                if (SelectedModel is GeometryModel3D model)
                {
                    model.Material =null;
                }
            }
           
            SelectedModel =e.SelectedModels.FirstOrDefault();
            if (SelectedModel is GeometryModel3D model3d)
            {
                model3d.Material = Materials.Yellow;
            }
            //MessageBox.Show(SelectedModel.GetName());
        }

        public PointSelectionCommand PointSelectionCommand { get; private set; }

        public ThreeDViewModel()
        {

        }
    }
}
