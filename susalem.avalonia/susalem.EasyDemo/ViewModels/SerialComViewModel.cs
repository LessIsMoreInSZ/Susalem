using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using susalem.Communication;
using susalem.Communication.Args;
using susalem.Communication.Interface;
using susalem.Communication.Share;
using susalem.EasyDemo.Models;
using susalem.EasyDemo.Share;

namespace susalem.EasyDemo.ViewModels;
/// <summary>
/// todo 发太快时消息显示控制
/// </summary>
public partial class SerialComViewModel : ObservableObject
{
    private readonly SerialService _serialService;
    [ObservableProperty] private List<string> _comPortList;
    [ObservableProperty] private string? _selectedComPort;

    // 校验位
    public List<EnumItem<Parity>> ParitiesSource { get; } = EnumHelper.EnumConvertToList<Parity>();
    [ObservableProperty] private EnumItem<Parity>? _selectedParity;

    // 停止位
    public List<EnumItem<StopBits>> StopBitsSource { get; } = EnumHelper.EnumConvertToList<StopBits>();
    [ObservableProperty] private EnumItem<StopBits>? _selectedStopBits;

    // 波特率
    public List<int> BaudRates { get; } = new()
    {
        9600, 19200, 38400, 57600, 115200
    };

    [ObservableProperty] private int? _baudRate = 9600;

    // 数据位
    public List<int> DataBits { get; } = new() { 5, 6, 7, 8 };
    public IUiLogger UiLogger { get; set; }
    public INotify Notify { get; set; }

    [ObservableProperty] private int _dataBit = 5;

    [ObservableProperty] private bool _isConnected;
    [ObservableProperty] private bool _receiveIsHex = true;
    [ObservableProperty] private bool _sendIsHex = true;
    [ObservableProperty] private string _sendString;


    // Design
    public SerialComViewModel()
    {
        _comPortList = new List<string>()
        {
            "COM1",
            "COM2",
        };
        SelectedParity = ParitiesSource.FirstOrDefault(it => it.Value == Parity.None);
        SelectedStopBits = StopBitsSource.FirstOrDefault(it => it.Value == StopBits.One);
        SelectedComPort = ComPortList.FirstOrDefault();
    }

    public SerialComViewModel(SerialService serialService)
    {
        _serialService = serialService;
        _comPortList = serialService.GetPortNames();
        SelectedParity = ParitiesSource.FirstOrDefault(it => it.Value == Parity.None);
        SelectedStopBits = StopBitsSource.FirstOrDefault(it => it.Value == StopBits.One);
        SelectedComPort = ComPortList.FirstOrDefault();
    }

    [RelayCommand]
    private async Task Connection()
    {
        try
        {
            if (IsConnected)
            {
                _serialService.Receive -= OnSerialReceive;
                _serialService.Dispose();
            }
            else
            {
                _serialService.PortName = SelectedComPort;
                _serialService.Parity = SelectedParity!.Value;
                _serialService.BaudRate = BaudRate!.Value;
                _serialService.DataBits = DataBit;
                _serialService.StopBits = SelectedStopBits!.Value;
                _serialService.Receive += OnSerialReceive;
                _serialService.Connect();
            }

            IsConnected = !IsConnected;
        }
        catch (Exception e)
        {
            // todo
            Notify.Error(e.Message);
        }
    }

    private void OnSerialReceive(object? sender, ReceiveArgs e)
    {
        var buffer = e.Data;
        UiLogger.Info($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] Receive");
        UiLogger.Message($" {(ReceiveIsHex ? buffer.ToHexString() : buffer.ToUtf8Str())}", "#2B2BFF");
    }

    [RelayCommand]
    private void Send()
    {
        try
        {
            if (!IsConnected || string.IsNullOrEmpty(SendString))
            {
                Notify.Warn("串口未连接或发送内容为空");
                return;
            }

            byte[] sendBuffer;
            if (SendIsHex)
            {
                sendBuffer = SendString.HexStringToArray();
            }
            else
            {
                sendBuffer = Encoding.UTF8.GetBytes(SendString);
            }

            ((ICommunication)_serialService).Send(sendBuffer);
            UiLogger.Info($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] Send");
            UiLogger.Success($"{(SendIsHex ? sendBuffer.ToHexString() : sendBuffer.ToUtf8Str())}");
        }
        catch (Exception e)
        {
            Notify.Error(e.Message);
        }
    }
}