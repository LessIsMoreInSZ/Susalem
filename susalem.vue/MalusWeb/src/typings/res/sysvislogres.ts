interface sysVisLogPageRes {
  pageNo: number;
  pageSize: number;
  totalPage: number;
  totalRows: number;
  records: sysLogPageRecord[];
}

interface sysLogPageRecord {
  id: string;
  name: string;
  success: boolean;
  message: string | null;
  ip: string;
  location: string;
  browser: string;
  os: string;
  url: string;
  className: string;
  methodName: string;
  reqMethod: string;
  param: string;
  result: string;
  elapsedTime: number;
  opTime: string;
  account: any;
}
