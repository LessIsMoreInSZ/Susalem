import { resolve } from "path"

/**异步延迟间隔 */
export function delay(timeout = 0) {
  return new Promise((resolve) => {
    let timer = setTimeout(() => {
      clearTimeout(timer)
      resolve(true)
    }, timeout)
  })
}
/**读取文件并解析成二进制数据 */
export function readFile(file: any) {
  return new Promise((resolve) => {
    let reader = new FileReader()
    reader.readAsBinaryString(file)
    reader.onload = (ev: any) => {
      resolve(ev.target.result)
    }
  })
}
/** 下载文件 */
const baseApi = import.meta.env.VITE_BASE_API
export const downloadFile = (url: string, fileName?: string) => {
  return new Promise((resolve, reject) => {
    const a = document.createElement("a")
    a.href = baseApi + "/" + url // 这里使用的是相对路径，需要根据实际情况修改为正确的文件路径或URL。
    // 设置下载文件的默认名称
    let fileNameValue = fileName ? fileName + ".xlsx" : "template.xlsx"
    a.setAttribute("download", fileNameValue)
    a.style.display = "none"

    // 监听下载完成事件
    const handleDownloadComplete = () => {
      document.body.removeChild(a)
      resolve(true)
    }

    // 监听下载失败事件
    const handleDownloadError = (error: any) => {
      document.body.removeChild(a)
      reject(error)
    }

    a.addEventListener("click", handleDownloadComplete)
    a.addEventListener("load", handleDownloadComplete)
    a.addEventListener("error", handleDownloadError)

    document.body.appendChild(a)
    a.click()
  })
}
