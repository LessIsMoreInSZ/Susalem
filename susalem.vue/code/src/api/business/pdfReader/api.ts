import axios from "axios"
import { defineComponent } from "vue"
import { ElMessage } from "element-plus"
import { type ResultHelper } from "./interface"
import * as XLSX from "xlsx"
import { request } from "./service"

 
//获取PDF列表
export async function GetPdfNames(): Promise<ResultHelper> {
  return request({
    url: "/api/Pdf/GetPdfNames",
    method: "get"
    // data
  })
}  

//上传
export async function UploadPDF(par: any) {
  let formData = new FormData()
  formData.append("file", par)
  return request({
    url: "/api/Pdf/AddingPdfFile",
    method: "post",
    headers: {
      "Content-Type": "multipart/form-data"
    },
    data: formData
  })
} 
export async function GetPdfByName(par: any): Promise<ResultHelper> {
  return request({
    url: "/api/Pdf/GetPdfByName",
    method: "get",
    responseType: "arraybuffer",
    params: {
      name: par
    }
  })
}
export async function DeletePdfByName(par: any): Promise<ResultHelper> {
  return request({
    url: "/api/Pdf/DeletePdfByName",
    method: "post",
    data: par
  })
}