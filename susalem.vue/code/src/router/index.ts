import { type RouteRecordRaw, createRouter } from "vue-router"
import { history, flatMultiLevelRoutes } from "./helper"
import routeSettings from "@/config/route"

const Layouts = () => import("@/layouts/index.vue")

/**
 * 常驻路由
 * 除了 redirect/403/404/login 等隐藏页面，其他页面建议设置 Name 属性
 */
export const constantRoutes: RouteRecordRaw[] = [
  {
    path: "/redirect",
    component: Layouts,
    meta: {
      hidden: true
    },
    children: [
      {
        path: ":path(.*)",
        component: () => import("@/views/redirect/index.vue")
      }
    ]
  },
  {
    path: "/403",
    component: () => import("@/views/error-page/403.vue"),
    meta: {
      hidden: true
    }
  },
  {
    path: "/404",
    component: () => import("@/views/error-page/404.vue"),
    meta: {
      hidden: true
    },
    alias: "/:pathMatch(.*)*"
  },
  {
    path: "/login",
    component: () => import("@/views/login/index.vue"),
    meta: {
      hidden: true
    }
  },
  // {
  //   path: "/",
  //   name: "main",
  //   component: () => import("@/views/dashboard/index.vue"),
  //   // name: "Dashboard",
  //   meta: {
  //     title: "首页",
  //     svgIcon: "dashboard",
  //     affix: true
  //   }
  // }
  {
    path: "/",
    name: "main",
    component: Layouts,
    redirect: "/dashboard",
    children: [
      {
        path: "dashboard",
        component: () => import("@/views/dashboard/index.vue"),
        name: "Dashboard",
        meta: {
          title: "首页",
          elIcon: "dashboard",
          affix: true
        }
      }
    ]
  }
  // #region feichu
  // {
  //   path: "/unocss",
  //   component: Layouts,
  //   redirect: "/unocss/index",
  //   children: [
  //     {
  //       path: "index",
  //       component: () => import("@/views/unocss/index.vue"),
  //       name: "UnoCSS",
  //       meta: {
  //         title: "UnoCSS",
  //         svgIcon: "unocss"
  //       }
  //     }
  //   ]
  // },
  // {
  //   path: "/link",
  //   meta: {
  //     title: "外链",
  //     svgIcon: "link"
  //   },
  //   children: [
  //     {
  //       path: "https://juejin.cn/post/7089377403717287972",
  //       component: () => {},
  //       name: "Link1",
  //       meta: {
  //         title: "中文文档"
  //       }
  //     },
  //     {
  //       path: "https://juejin.cn/column/7207659644487139387",
  //       component: () => {},
  //       name: "Link2",
  //       meta: {
  //         title: "新手教程"
  //       }
  //     }
  //   ]
  // },
  // {
  //   path: "/table",
  //   component: Layouts,
  //   redirect: "/table/element-plus",
  //   name: "Table",
  //   meta: {
  //     title: "系统管理",
  //     elIcon: "Grid"
  //   },
  //   children: [
  //     {
  //       path: "/element-plus",
  //       component: () => import("@/views//table/element-plus/index.vue"),
  //       name: "ElementPlus",
  //       meta: {
  //         title: "ep",
  //         keepAlive: true
  //       }
  //     }
  //   ]
  // }
  // {
  //   path: "/hook-demo",
  //   component: Layouts,
  //   redirect: "/hook-demo/use-fetch-select",
  //   name: "HookDemo",
  //   meta: {
  //     title: "Hook 示例",
  //     elIcon: "Menu",
  //     alwaysShow: true
  //   },
  //   children: [
  //     {
  //       path: "use-fetch-select",
  //       component: () => import("@/views/hook-demo/use-fetch-select.vue"),
  //       name: "UseFetchSelect",
  //       meta: {
  //         title: "useFetchSelect"
  //       }
  //     },
  //     {
  //       path: "use-fullscreen-loading",
  //       component: () => import("@/views/hook-demo/use-fullscreen-loading.vue"),
  //       name: "UseFullscreenLoading",
  //       meta: {
  //         title: "useFullscreenLoading"
  //       }
  //     },
  //     {
  //       path: "use-watermark",
  //       component: () => import("@/views/hook-demo/use-watermark.vue"),
  //       name: "UseWatermark",
  //       meta: {
  //         title: "useWatermark"
  //       }
  //     }
  //   ]
  // }
  // #endregion
]

/**
 * 动态路由
 * 用来放置有权限 (Roles 属性) 的路由
 * 必须带有 Name 属性
 */
// export const dynamicRoutes: RouteRecordRaw[] = [
//   {
//     path: "/table",
//     component: Layouts,
//     redirect: "/table/element-plus",
//     name: "Table",
//     meta: {
//       title: "系统管理",
//       elIcon: "Grid"
//     },
//     children: [
//       {
//         path: "/user",
//         component: () => import("@/views/system/user/index.vue"),
//         name: "user",
//         meta: {
//           title: "用户管理",
//           keepAlive: true
//         }
//       },
//       {
//         path: "/role",
//         component: () => import("@/views/system/role/index.vue"),
//         name: "Role",
//         meta: {
//           title: "角色管理",
//           keepAlive: true
//         }
//       },
//       {
//         path: "/menu",
//         component: () => import("@/views/system/menu/index.vue"),
//         name: "Menu",
//         meta: {
//           title: "菜单管理",
//           keepAlive: true
//         }
//       },
//       {
//         path: "/element-plus",
//         component: () => import("@/views//table/element-plus/index.vue"),
//         name: "ElementPlus",
//         meta: {
//           title: "ep",
//           keepAlive: true
//         }
//       }
//     ]
//   }
// ]

const router = createRouter({
  history,
  routes: routeSettings.thirdLevelRouteCache ? flatMultiLevelRoutes(constantRoutes) : constantRoutes
})

/** 重置路由 */
export function resetRouter() {
  // 注意：所有动态路由路由必须带有 Name 属性，否则可能会不能完全重置干净
  try {
    router.getRoutes().forEach((route) => {
      const { name, meta } = route
      if (name && meta.roles?.length) {
        router.hasRoute(name) && router.removeRoute(name)
      }
    })
  } catch {
    // 强制刷新浏览器也行，只是交互体验不是很好
    window.location.reload()
  }
}

export default router
