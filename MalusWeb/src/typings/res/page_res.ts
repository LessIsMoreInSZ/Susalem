// 定义分页返回
export interface PageData<T> {
  records: T[];
  total: number;
  pageSize: number;
  currentPage: number;
}
