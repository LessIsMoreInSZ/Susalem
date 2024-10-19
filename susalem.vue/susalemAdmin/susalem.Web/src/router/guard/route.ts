import type {
  LocationQueryRaw,
  NavigationGuardNext,
  RouteLocationNormalized,
  RouteLocationRaw,
  Router
} from 'vue-router';
import type { RouteKey, RoutePath } from '@elegant-router/types';
import { useAuthStore } from '@/store/modules/auth';
import { useRouteStore } from '@/store/modules/route';
import { localStg } from '@/utils/storage';

/**
 * create route guard
 *
 * @param router router instance
 */
export function createRouteGuard(router: Router) {
  router.beforeEach(async (to, from, next) => {
    const location = await initRoute(to);

    if (location) {
      next(location);
      return;
    }

    const authStore = useAuthStore();

    const rootRoute: RouteKey = 'root';
    const loginRoute: RouteKey = 'login';
    const noAuthorizationRoute: RouteKey = '403';
    // debugger;
    const isLogin = Boolean(localStg.get('token'));
    const needLogin = !to.meta.constant;
    const routeRoles = to.meta.roles || [];
    // console.log(authStore.userInfo);
    // const hasRole = authStore.userInfo.roles.some(role => routeRoles.includes(role));
    const hasRole = true;
    const hasAuth = authStore.isStaticSuper || !routeRoles.length || hasRole;

    const routeSwitches: CommonType.StrategicPattern[] = [
      // 如果登录时是登录路由，则切换到根页面
      {
        condition: isLogin && to.name === loginRoute,
        callback: () => {
          next({ name: rootRoute });
        }
      },
      // 如果is是常量路由，则允许它直接访问
      {
        condition: !needLogin,
        callback: () => {
          handleRouteSwitch(to, from, next);
        }
      },
      // 如果路由需要登录，但用户未登录，则切换到登录页面
      {
        condition: !isLogin && needLogin,
        callback: () => {
          next({ name: loginRoute, query: { redirect: to.fullPath } });
        }
      },
      // 如果用户已登录并具有授权，则允许其访问
      {
        condition: isLogin && needLogin && hasAuth,
        callback: () => {
          handleRouteSwitch(to, from, next);
        }
      },
      // 如果用户已登录但没有授权，则切换到403页
      {
        condition: isLogin && needLogin && !hasAuth,
        callback: () => {
          next({ name: noAuthorizationRoute });
        }
      }
    ];

    routeSwitches.some(({ condition, callback }) => {
      if (condition) {
        callback();
      }

      return condition;
    });
  });
}

/**
 * initialize route
 *
 * @param to to route
 */
async function initRoute(to: RouteLocationNormalized): Promise<RouteLocationRaw | null> {
  const routeStore = useRouteStore();

  const notFoundRoute: RouteKey = 'not-found';
  const isNotFoundRoute = to.name === notFoundRoute;

  // if the constant route is not initialized, then initialize the constant route
  if (!routeStore.isInitConstantRoute) {
    await routeStore.initConstantRoute();

    // the route is captured by the "not-found" route because the constant route is not initialized
    // after the constant route is initialized, redirect to the original route
    if (isNotFoundRoute) {
      const path = to.fullPath;

      const location: RouteLocationRaw = {
        path,
        replace: true,
        query: to.query,
        hash: to.hash
      };

      return location;
    }
  }

  // if the route is the constant route but is not the "not-found" route, then it is allowed to access.
  if (to.meta.constant && !isNotFoundRoute) {
    return null;
  }

  // the auth route is initialized
  // it is not the "not-found" route, then it is allowed to access
  if (routeStore.isInitAuthRoute && !isNotFoundRoute) {
    return null;
  }
  // it is captured by the "not-found" route, then check whether the route exists
  if (routeStore.isInitAuthRoute && isNotFoundRoute) {
    const exist = await routeStore.getIsAuthRouteExist(to.path as RoutePath);
    const noPermissionRoute: RouteKey = '403';

    if (exist) {
      const location: RouteLocationRaw = {
        name: noPermissionRoute
      };

      return location;
    }

    return null;
  }

  // 如果身份验证路由未初始化，则初始化身份验证路由
  const isLogin = Boolean(localStg.get('token'));
  // 初始化身份验证路由需要用户登录，如果没有，则重定向到登录页面
  if (!isLogin) {
    const loginRoute: RouteKey = 'login';
    const redirect = to.fullPath;

    const query: LocationQueryRaw = to.name !== loginRoute ? { redirect } : {};

    const location: RouteLocationRaw = {
      name: loginRoute,
      query
    };

    return location;
  }

  // 初始化身份验证路由
  await routeStore.initAuthRoute();

  // the route is captured by the "not-found" route because the auth route is not initialized
  // after the auth route is initialized, redirect to the original route
  if (isNotFoundRoute) {
    const rootRoute: RouteKey = 'root';
    const path = to.redirectedFrom?.name === rootRoute ? '/' : to.fullPath;

    const location: RouteLocationRaw = {
      path,
      replace: true,
      query: to.query,
      hash: to.hash
    };

    return location;
  }

  return null;
}

function handleRouteSwitch(to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext) {
  // route with href
  if (to.meta.href) {
    window.open(to.meta.href, '_blank');

    next({ path: from.fullPath, replace: true, query: from.query, hash: to.hash });

    return;
  }

  next();
}
