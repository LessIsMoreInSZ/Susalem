import { request } from '../request/';

// 用户分页
export function getGalleryPagelist(params?: any) {
  return request<any>({
    url: '/Gallery/PageList',
    method: 'get',
    params
  });
}

// 删除指定的key;
export function deleteGallery(id: number) {
  return request<boolean>({
    url: `/Gallery/DeleteKey?key=${id}`,
    method: 'post'
  });
}
