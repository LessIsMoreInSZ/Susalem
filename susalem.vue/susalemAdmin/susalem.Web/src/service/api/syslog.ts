import { request } from '../request';

/** 操作日志 */
export function getSysOplogPage(params?: any) {
  return request<any>({
    url: '/SysLog/OpPageList',
    method: 'get',
    params
  });
}

// 异常日志
export function getSysErrlogPage(params?: any) {
  return request<any>({
    url: '/SysLog/ErrPageList',
    method: 'get',
    params
  });
}

// 访问日志

export function getSysVislogPage(params?: any) {
  return request<any>({
    url: '/SysLog/VisPageList',
    method: 'get',
    params
  });
}
