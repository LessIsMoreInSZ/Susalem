using System;

namespace susalem.EasyDemo.Models;

/// <summary>
/// 下拉框枚举选择类
/// </summary>
/// <typeparam name="T"></typeparam>
public class EnumItem<T> where T : struct, Enum
{
    public string? Name { get; set; }

    public T Value { get; set; }
}