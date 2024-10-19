export interface SysConfigRes extends Api.Common.PaginatingQueryRecord {
  configID: number;
  configKey: string;
  configType: string;
  configdDescribe: number;
  isDeleted: string;
}
