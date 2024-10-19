import type { Router } from 'vue-router';
import { createRouteGuard } from './route';
import { createProgressGuard } from './progress';
import { createDocumentTitleGuard } from './title';

/**
 * Router guard createProgressGuard用于监听页面加载进度， createRouteGuard用于拦截和处理路由跳转 ， createDocumentTitleGuard用于动态设置文档标题。
 *
 * @param router - Router instance
 */
export function createRouterGuard(router: Router) {
  // 用于监听页面加载进度
  createProgressGuard(router);
  // 用于拦截和处理路由跳转
  createRouteGuard(router);
  // 用于动态设置文档标题
  createDocumentTitleGuard(router);
}
