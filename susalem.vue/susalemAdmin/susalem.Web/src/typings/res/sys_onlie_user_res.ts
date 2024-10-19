// import { PaginatedData } from './page_res';

// 定义记录的接口

export interface SysOnlineUserRecord extends Api.Common.PaginatingQueryRecord {
  id: number;
  connectionId: string;
  userId: number;
  userName: string;
  realName: string;
  time: string;
  ip: string;
  browser: string;
  os: string;
}
