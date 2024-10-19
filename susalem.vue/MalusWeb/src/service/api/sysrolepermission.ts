import { request } from '../request';

/** get role list */
export function getSysRoleAllPermission(params?: any) {
  return request<ResRolePermission[]>({
    url: '/SysRolePermission/GetAllButen',
    method: 'get',
    params
  });
}

// 获取用户的权限按钮
export function getRoleButen(params?: any) {
  return request<any>({
    url: '/SysRolePermission/GetRoleButen',
    method: 'get',
    params
  });
}

// 添加用户的权限按钮
export function addUserButtonPermiss(data?: any) {
  return request<string[]>({
    url: '/SysRolePermission/AddUserButtonPermiss',
    method: 'post',
    data
  });
}
