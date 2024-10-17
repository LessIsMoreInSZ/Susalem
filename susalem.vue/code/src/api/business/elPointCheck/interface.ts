// 班表接口
export interface ResultHelper {
   code:number,
   message:string,
   data:any
}

export interface Cell{
   rowIndex:number//行名，数字
   colIndex:string//列名，ABCDEFG
   content:string//内容
}
export interface SaveSheets{
   sheetName:string
   cells:Array<Cell>
}
export interface SaveExcel{
   fileName:string
   sheets:Array<SaveSheets>
}

export interface CellValue{
   rowIndex:number
   colIndex:string
}