using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using susalem.Communication;
using susalem.EasyDemo.Compoents;
using susalem.EasyDemo.ViewModels;

namespace susalem.EasyDemo.Views;

public partial class SerialComView : UserControl
{
    private SerialComViewModel _viewModel = new SerialComViewModel(new SerialService());

    public SerialComView()
    {
        InitializeComponent();
        _viewModel.UiLogger = TextEdit;
        _viewModel.Notify = new Notify(new(TopLevel.GetTopLevel(this)));
        DataContext = _viewModel;
    }
}