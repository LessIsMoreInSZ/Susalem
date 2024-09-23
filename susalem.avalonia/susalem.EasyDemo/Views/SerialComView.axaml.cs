using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using susalem.Communication;
using susalem.EasyDemo.ViewModels;

namespace susalem.EasyDemo.Views;

public partial class SerialComView : UserControl
{
    public SerialComView()
    {
        InitializeComponent();
        DataContext = new SerialComViewModel(new SerialService());
    }
}