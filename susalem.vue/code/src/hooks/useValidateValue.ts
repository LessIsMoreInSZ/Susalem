import * as CustomValid from "../utils/validate"

/**
 * 校验参数值类型
 * @param currentDataType
 * @param value
 * @param callback
 */
const validateValueType = (currentDataType: string, value: any, callback: any) => {
  switch (true) {
    case currentDataType === "Float" || currentDataType === "Double":
      !CustomValid.isFloat(value) ? callback(new Error("请输入正确的参数值类型")) : callback()
      break
    case currentDataType.startsWith("Int"):
      !CustomValid.isInt(value) ? callback(new Error("请输入正确的参数值类型")) : callback()
      break
    case currentDataType.startsWith("UInt"):
      !CustomValid.isUInt(value) ? callback(new Error("请输入正确的参数值类型")) : callback()
      break
    case currentDataType.startsWith("Bool"):
      !CustomValid.isBoolean(value) ? callback(new Error("请输入正确的参数值类型")) : callback()
      break
    case currentDataType.toLocaleLowerCase().includes("string"):
      !CustomValid.isString(value) ? callback(new Error("请输入正确的参数值类型")) : callback()
      break
    case currentDataType.startsWith("Byte"):
      !CustomValid.isByte(value) ? callback(new Error("请输入正确的参数值类型")) : callback()
      break
    case currentDataType.startsWith("SByte"):
      !CustomValid.isSByte(value) ? callback(new Error("请输入正确的参数值类型")) : callback()
      break
    default:
      callback(new Error("未知类型，请重新输入"))
      break
  }
}

export function useValidateValue() {
  return { validateValueType }
}
