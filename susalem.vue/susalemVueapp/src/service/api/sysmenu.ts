import { request } from '../request';

/** 角色分页 */
export function getMenuTreeList() {
  return request<Api.SystemManage.MenuList>({
    url: 'SysMenu/MenuTreeList',
    method: 'get'
  });
}

/** 添加菜单 */
export function addMenu(data?: any) {
  return request<boolean>({
    url: '/SysMenu/Add',
    method: 'post',
    data
  });
}

/** 更新菜单 */
export function updateMenu(data?: any) {
  return request<boolean>({
    url: '/SysMenu/Update',
    method: 'post',
    data
  });
}

/** 删除菜单 */
export function deleteMenu(params: any) {
  return request<boolean>({
    url: '/SysMenu/Delete',
    method: 'get',
    params
  });
}
