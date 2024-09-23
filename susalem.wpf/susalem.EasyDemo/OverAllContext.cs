using HslCommunication.ModBus;
using susalem.EasyDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.EasyDemo
{
    public class OverAllContext
    {
        public static UserModel User { get; set; }


        //public static HslCommunication.ModBus.ModbusTcpServer modbusTcpServer { get; set; }

        /// <summary>
        /// 开锁信号
        /// </summary>
        public static ModbusTcpNet ModbusTcpLock { get; set; }
        /// <summary>
        /// 状态灯
        /// </summary>
        public static ModbusTcpNet ModbusTcpStatusLight { get; set; }

        /// <summary>
        /// 门反馈信号
        /// </summary>
        public static ModbusTcpNet ModbusTcpDoor { get; set; }

        /// <summary>
        /// 报警灯
        /// </summary>
        //public static ModbusTcpNet ModbusTcpAlarmLight { get; set; }

        /// <summary>
        /// 当前是否弹出报警弹窗
        /// </summary>
        public static bool IsWarn { get; set; }

        /// <summary>
        /// 当前是否弹出错误弹窗
        /// </summary>
        public static bool IsError { get; set; }
    }
}
