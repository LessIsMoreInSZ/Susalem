import { request } from '../request/';

// 必应壁纸分页
export function getBingWllpaperPagelist(params?: any) {
  return request<any>({
    url: '/BingWallpaper/PageList',
    method: 'get',
    params
  });
}
