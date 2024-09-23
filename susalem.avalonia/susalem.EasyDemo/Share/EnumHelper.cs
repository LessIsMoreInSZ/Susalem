using System;
using System.Collections.Generic;
using System.Linq;
using susalem.EasyDemo.Models;

namespace susalem.EasyDemo.Share;

public static class EnumHelper
{
    /// <summary>
    /// 将枚举转换成可绑定的列表
    /// </summary>
    public static List<EnumItem<T>> EnumConvertToList<T>() where T : struct, Enum
    {
        var list = new List<EnumItem<T>>();
        var values = Enum.GetValues<T>();
        foreach (var value in values)
        {
            var name = Enum.GetName(typeof(T), value);
            list.Add(new EnumItem<T>
            {
                Name = name,
                Value = value
            });
        }

        return list;
    }
}