using System.Collections.Generic;
using System.IO.Ports;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using susalem.Communication;
using susalem.EasyDemo.Models;
using susalem.EasyDemo.Share;

namespace susalem.EasyDemo.ViewModels;

public partial class SerialComViewModel : ObservableObject
{
    private readonly SerialService _serialService;
    [ObservableProperty] private List<string> _comPortList;
    [ObservableProperty] private string? _selectedComPort;

    // 校验位
    public List<EnumItem<Parity>> ParitiesSource => EnumHelper.EnumConvertToList<Parity>();
    [ObservableProperty] private EnumItem<Parity>? _selectedParity;

    // 停止位
    public List<EnumItem<StopBits>> StopBitsSource => EnumHelper.EnumConvertToList<StopBits>();
    [ObservableProperty] private EnumItem<StopBits>? _selectedStopBits;

    // 波特率
    public List<int> BaudRates => new()
    {
        9600, 19200, 38400, 57600, 115200
    };

    [ObservableProperty] private int? _baudRate;

    // 数据位
    public List<int> DataBits => new() { 5, 6, 7, 8 };
    [ObservableProperty] private int _dataBit = 5;

    [ObservableProperty] private bool _isConnected;

    // 停止位

    // Design
    public SerialComViewModel()
    {
        _comPortList = new List<string>()
        {
            "COM1",
            "COM2",
        };
    }

    public SerialComViewModel(SerialService serialService)
    {
        _serialService = serialService;
        _comPortList = serialService.GetPortNames();
    }

    [RelayCommand]
    private async Task Connection()
    {
        IsConnected = !IsConnected;
    }
}