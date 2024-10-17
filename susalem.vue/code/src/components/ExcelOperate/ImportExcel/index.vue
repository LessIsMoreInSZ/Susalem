<template>
  <el-dialog :modelValue="excelDialogLeadVisible" title="导入Execl" :before-close="handleClose" width="30%">
    <el-upload
      class="upload-demo"
      drag
      action=""
      :auto-upload="false"
      accept=".xlsx,.xls"
      :show-file-list="false"
      style="margin-left: 10px"
      :on-change="handleExcelChange"
    >
      <el-icon class="el-icon--upload"><upload-filled /></el-icon>
      <div class="el-upload__text"><em>导入文件</em></div>
    </el-upload>
    <!-- 下载模版插槽 -->
    <div class="download-template">
      <slot name="downloadTemplate"> </slot>
    </div>
  </el-dialog>
</template>
<script lang="ts" setup>
import { ref } from "vue"
import { ElMessage } from "element-plus"
import type { UploadFile } from "element-plus"
import * as xlsx from "xlsx"
import { readFile } from "../../../utils/comUtils"
const props = defineProps({
  excelDialogLeadVisible: {
    type: Boolean
  }
})
const emit = defineEmits(["excelClose", "excelData"])
/**关闭对话框 */
const handleClose = (done: any) => {
  emit("excelClose")
}
/**解析excel表格并转成json格式发送 */
const handleExcelChange = async (uploadFile: UploadFile) => {
  if (!uploadFile.raw) return
  //读取文件转成二进制
  let dataBinary = await readFile(uploadFile.raw)
  // 使用插件解析
  let workBook = xlsx.read(dataBinary, { type: "binary" })
  console.log("%c [ workBook ]-85", "font-size:13px; background:pink; color:#bf2c9f;", workBook)
  workBook.SheetNames.forEach((SheetName: string) => {
    let workSheet = workBook.Sheets[SheetName]
    //按行读出表数据，并转换成json格式
    let sheetDataJson = xlsx.utils.sheet_to_json(workSheet, { header: 1 })
    //拿到第一行的表头，即字段名
    let headers = (sheetDataJson.shift() as Array<string>) || []
    //防止空表格
    if (headers.length !== 0) {
      let jsonData: any = []
      // 遍历数据行（1，hh，ll）
      sheetDataJson.forEach((row: any) => {
        let josnKeyValue: any = {}
        // 遍历表头，即字段名（id，name，age），拼接生成键值对对象，用于存储数据。
        headers.forEach((fieldKey: string, index: number) => {
          josnKeyValue[fieldKey] = row[index] || "" // 处理空值问题，避免出现undefined
        })
        jsonData.push(josnKeyValue) // 添加到jsonData数组中，数据为键值对对象。
      })
      emit("excelData", jsonData)
    }
  })
}
</script>
<style scoped></style>
