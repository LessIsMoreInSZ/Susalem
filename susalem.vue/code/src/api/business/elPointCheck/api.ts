import axios from "axios"
import { defineComponent } from "vue"
import { ElMessage } from "element-plus"
import { type ResultHelper } from "./interface"
import * as XLSX from "xlsx"
import { request } from "./service"

export async function testPic(): Promise<ResultHelper> {
  return request({
    url: "/api/Excel/aaa",
    method: "post"
    // data
  })
}
//获取excel模板列表
export async function GetTemplateList(): Promise<ResultHelper> {
  return request({
    url: "/api/Excel/GetExcelTemplates",
    method: "get"
    // data
  })
} //获取excel模板列表
export async function GetRecordList(par: any): Promise<ResultHelper> {
  return request({
    url: "/api/Excel/GetExcelRecords",
    method: "get",
    params: {
      file: par
    }
  })
}

//上传
export async function UploadTemplate(par: any) {
  let formData = new FormData()
  formData.append("file", par)
  return request({
    url: "/api/Excel/AddingExcelTemplates",
    method: "post",
    headers: {
      "Content-Type": "multipart/form-data"
    },
    data: formData
  })
}
//删除
export async function DeleteExcelTemplates(par: any) {
  return request({
    url: "/api/Excel/DeleteExcelTemplates",
    method: "post",
    data: par
  })
}export async function DeleteExcelRecords(par: any) {
  return request({
    url: "/api/Excel/DeleteExcelRecords",
    method: "post",
    data: par
  })
}

export async function GetExcelByName(par: string): Promise<ResultHelper> {
  return request({
    url: "/api/Excel/GetFileByName",
    method: "get",
    responseType: "blob",
    params: {
      name: par
    }
  })
}
//GetRecordByName
export async function GetRecordByName(par: string): Promise<ResultHelper> {
  return request({
    url: "/api/Excel/GetRecordByName",
    method: "get",
    responseType: "blob",
    params: {
      name: par
    }
  })
} 
export async function SaveRecord(params: any): Promise<ResultHelper> {
  return request({
    url: "/api/Excel/SaveRecord",
    method: "post",
    data: params
  })
}
export async function GetFileSizeByName(params: any): Promise<ResultHelper> {
  return request({
    url: "/api/Excel/GetFileSizeByName",
    method: "get",
    params: {
      name: params
    }
  })
}