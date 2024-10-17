import { ElLoading, ElLoadingService } from "element-plus"

interface loadingOptions {
  //绑定class元素，实现局部加载
  target?: HTMLElement | string
  // 是否锁定屏幕滚动
  lock?: boolean
  // 加载文案
  text?: string
  // 遮罩层颜色
  background?: string
  // 是否全屏遮罩层
  fullscreen?: boolean
}
// 默认设置
const defaultOptions: loadingOptions = {
  //锁定屏幕滚动
  lock: true,
  text: "正在加载...",
  background: "rgba(0, 0, 0, 0.1)",
  fullscreen: true
}
// 定义加载组件实例
let loadingInstance: any | undefined
// 打开加载组件(可单独调用)
const showLoading = (options?: any) => {
  loadingInstance = ElLoading.service(options ? options : defaultOptions)
}
// 关闭加载组件(可单独调用)
const closeLoading = () => {
  if (loadingInstance) {
    loadingInstance.close()
  }
}
/**
 *导出加载组件模块使用
 * @param {请求函数} 传入异步请求函数
 * @param {加载组件配置参数} options
 * @returns
 */
export const loadModule = async (asyncFn: () => Promise<any>, options: loadingOptions = {}): Promise<any> => {
  let mergeOptions = { ...defaultOptions, ...options }

  try {
    // 打开加载模块
    showLoading(mergeOptions)
    // 执行函数获取返回值
    const result = await asyncFn()
    const isPromise = result instanceof Promise
    // 同步请求
    if (!isPromise) {
      closeLoading()
      return result
    }
    // 异步请求处理，成功关闭加载返回响应值
    return result
      .then((res) => {
        closeLoading()
        return res
      })
      .catch((err) => {
        closeLoading()
        throw err
      })
  } catch (error) {
    closeLoading()
    throw error
  }
}

export function useLoading() {
  return { loadModule, showLoading, closeLoading }
}
