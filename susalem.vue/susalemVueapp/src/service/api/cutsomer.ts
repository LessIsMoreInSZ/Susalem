import { request } from '../request/';

// 用户分页
export function getCustomerPagelist(params?: any) {
  return request<any>({
    url: '/Customer/PageList',
    method: 'get',
    params
  });
}
