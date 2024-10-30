using AvalonDock;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.ConditionalDraw;
using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Prism.Regions;
using SkiaSharp;
using susalem.wpf.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.wpf.ViewModels
{
    public partial class DigitalBoardViewModel : ObservableRecipient, INavigationAware
    {
        DockingManager _dockingManager;

        [ObservableProperty]
        ObservableCollection<UserData> userDatas;
        public Axis TrendYAxis { get; set; }

        [ObservableProperty]
        ISeries[] trendSeries;

        [ObservableProperty]
        ISeries[] relativeSeries;

        [ObservableProperty]
        ISeries[] averageSeries;

        [ObservableProperty]
        ISeries[] todaySeries;

        [ObservableProperty]
        DateTime datetime=DateTime.Now;
        [ObservableProperty]
        string title = "Didital Board";
        [RelayCommand]
        void Loaded(DockingManager dm)
        {
            _dockingManager = dm;
        }
        [RelayCommand]
        void AddNewData()
        {
            UserDatas.Add(new() { Datetime = DateTime.Now, No = UserDatas.Count });
        }
        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            InitData();
        }
        void InitData()
        {
            UserDatas = new(Enumerable.Range(1, 20).Select(n => new UserData
            {
                No = n,
                Datetime = DateTime.Now.AddMinutes(-Random.Shared.NextDouble() * 5000),
                Username = $"User {n}",
                State = n % 2 == 0 ? "Online" : "Offline"
            }));

            TrendSeries = [
                new LineSeries<int>(){
                    Name="In",
                    Values=new  ObservableCollection<int>{100,500,300,300,200 }
                },
                new LineSeries<int>(){
                    Name="Out",
                    Values=new  ObservableCollection<int>{700,200,300,400,800}
                }];

            RelativeSeries = [ new PolarLineSeries<double>
            {
                Values = new ObservableCollection<double> { 15, 14, 13, 12, 11, 10 },
                DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30)),
                GeometrySize = 15,
                DataLabelsSize = 8,
                DataLabelsPosition = PolarLabelsPosition.Middle,
                DataLabelsRotation = LiveCharts.CotangentAngle,
                IsClosed = true
            }];
            var averageS = new RowSeries<TodayInfo>
            {
                Values = new ObservableCollection<TodayInfo>{
                        new(){Name="Public",Value=40,Paint=new(SKColors.Aqua)},
                        new(){Name="Students",Value=120,Paint=new(SKColors.Khaki)},
                        new(){Name="Teachers",Value=160,Paint=new(SKColors.BlueViolet)},
                        new(){Name="Enterprices",Value=80,Paint=new(SKColors.DarkGreen)},},
                //DataLabelsPaint = new SolidColorPaint(new SKColor(245, 245, 245)),
                //DataLabelsPosition = DataLabelsPosition.End,
                //DataLabelsTranslate = new(-1, 0),
                //DataLabelsFormatter = point => $"{point.Model!.Name} {point.Coordinate.PrimaryValue}",
                //MaxBarWidth = 50,
                //Padding = 10,
            }.OnPointMeasured(point =>
            {
                if (point.Visual is null) return;
                point.Visual.Fill = point.Model!.Paint;
            });
            AverageSeries = [averageS];
        }
        public class TodayInfo: ObservableValue
        {
            public string Name { get; set; }
            public SolidColorPaint Paint { get; set; }

        }
    }
}
