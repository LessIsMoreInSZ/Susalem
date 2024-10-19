import { request } from '../request';

/** get user routes */
export function getAllKeys() {
  return request<any>({
    url: '/SysCache/GetAllKeys',
    method: 'get'
  });
}

export function getKeys(params?: any) {
  return request<any>({
    url: `/SysCache/GetKeys?key=${params}`,
    method: 'get'
  });
}

// 删除指定的key;
export function deleteKey() {
  return request<boolean>({
    url: '/SysCache/DeleteKey?key=',
    method: 'post'
  });
}
