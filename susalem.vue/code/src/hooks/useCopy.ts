import { ElMessage } from "element-plus"
import useClipboard from "vue-clipboard3"

async function copyModule(row?: any, column?: any, cell?: any, event?: any) {
  const copyValue = event.target.innerText
  if (copyValue === undefined || copyValue.length < 1) {
    return
  }
  const { toClipboard } = useClipboard()
  try {
    let res = await toClipboard(copyValue)
    if (res) {
      ElMessage({
        message: "复制成功",
        type: "success"
      })
    }
  } catch (error) {
    ElMessage({
      message: "复制失败,请手动复制",
      type: "error"
    })
  }
}

export function useCopy() {
  return { copyModule }
}
