using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Threading;
using AvaloniaEdit;
using AvaloniaEdit.Document;
using AvaloniaEdit.Rendering;
using susalem.Communication.Interface;
using susalem.EasyDemo.Share;

namespace susalem.EasyDemo.Controls;

public class TextEdit : TextEditor, IUiLogger
{
    readonly LineColorTransformer _lineColorTransformer = new LineColorTransformer();

    protected override Type StyleKeyOverride => typeof(TextEditor);

    private Channel<(string, string)> _channel = Channel.CreateUnbounded<(string, string)>(new UnboundedChannelOptions
    {
        AllowSynchronousContinuations = false,
        SingleReader = true,
        SingleWriter = false
    });

    private CancellationTokenSource _cts = new();

    public TextEdit()
    {
        HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
        VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        TextArea.TextView.LineTransformers.Add(_lineColorTransformer);
        IsReadOnly = true;

        Task.Run(WriteMessage, _cts.Token);
    }

    private async Task? WriteMessage()
    {
        await foreach (var valueTuple in _channel.Reader.ReadAllAsync())
        {
            var lineList = valueTuple.Item1.Split(Environment.NewLine).ToList();
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                foreach (var item in lineList)
                {
                    AppendText(item + Environment.NewLine);
                    _lineColorTransformer.AddLineColor(Document.LineCount - 1, valueTuple.Item2);
                }

                // todo 下方有空白，计算滚动条位置
                ScrollToEnd();
            });
        }
    }

    public void Message(string message, string color)
    {
        _channel.Writer.TryWrite((message, color));
    }

    public void Info(string message)
    {
        Message(message, "#808080");
    }

    public void Success(string message)
    {
        Message(message, "#1BD66C");
    }

    public void Warning(string message)
    {
        Message(message, "#FFCE44");
    }

    public void Error(string message)
    {
        Message(message, "#E30519");
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        _cts.Cancel();
        _cts.Dispose();
        base.OnUnloaded(e);
    }
}

public class LineColorTransformer : DocumentColorizingTransformer
{
    private Dictionary<int, string> _lineColorDict = new();

    public void AddLineColor(int line, string color)
    {
        _lineColorDict[line] = color;
    }

    protected override void ColorizeLine(DocumentLine line)
    {
        if (_lineColorDict.TryGetValue(line.LineNumber, out var color) && !string.IsNullOrEmpty(color))
        {
            ChangeLinePart(line.Offset, line.EndOffset,
                sp => { sp.TextRunProperties.SetForegroundBrush(BrushHelper.Parse(color)); });
        }
    }
}