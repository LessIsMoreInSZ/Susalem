import { request } from '../request';

/** get role list */
export function getRoleUserMenu(params?: any) {
  return request<number[]>({
    url: '/SysRoleMenu/Get',
    method: 'get',
    params
  });
}

export function setRoleUserMenu(data?: any) {
  return request<ResRolePermission[]>({
    url: '/SysRoleMenu/Set',
    method: 'post',
    data
  });
}
