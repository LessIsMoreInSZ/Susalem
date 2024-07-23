using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace susalem.EasyDemo.ViewModels
{
    internal class AlarmRecordViewModel:BindableBase
    {
        private List<TestModel>? testModel=new List<TestModel>()
        {
            new TestModel(){Index="1",Name="1",Remark="1"},
            new TestModel(){Index="1",Name="1",Remark="1"},
            new TestModel(){Index="1",Name="1",Remark="1"},
            new TestModel(){Index="1",Name="1",Remark="1"},
            new TestModel(){Index="1",Name="1",Remark="1"},
            new TestModel(){Index="1",Name="1",Remark="1"}
        };

        public List<TestModel>? DataList
        {
            get { return testModel; }
            set { testModel = value; RaisePropertyChanged(); }
        }
    }

    class TestModel: BindableBase
    {
        private string? index;

        public string? Index
        {
            get { return index; }
            set { index = value; RaisePropertyChanged(); }
        }

        private string? name;

        public string? Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(); }
        }

        private string? remark;

        public string? Remark
        {
            get { return remark; }
            set { remark = value; RaisePropertyChanged(); }
        }

    }
}
