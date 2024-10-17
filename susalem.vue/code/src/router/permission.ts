import router from "@/router"
import { useUserStoreHook } from "@/store/modules/user"
import { usePermissionStoreHook } from "@/store/modules/permission"
import { ElMessage } from "element-plus"
import { setRouteChange } from "@/hooks/useRouteListener"
import { useTitle } from "@/hooks/useTitle"
import { getToken } from "@/utils/cache/cookies"
import routeSettings from "@/config/route"
import isWhiteList from "@/config/white-list"
import NProgress from "nprogress"
import "nprogress/nprogress.css"
//import Layout from '@/layouts/index.vue'
import { getDynamicRoutesApi } from "@/api/login"
import { RouteRecordRaw } from "vue-router"
const modules = import.meta.glob("@/views/**/**.vue")
const modules1 = import.meta.glob("@/layouts/**.vue")
const Layouts = () => import("@/layouts/index.vue")
const { setTitle } = useTitle()
NProgress.configure({ showSpinner: false })

router.beforeEach(async (to, _from, next) => {
  NProgress.start()
  const userStore = useUserStoreHook()
  const permissionStore = usePermissionStoreHook()
  const token = getToken()

  // || token === "undefined"
  // 判断该用户是否已经登录
  if (!token) {
    // 如果在免登录的白名单中，则直接进入
    if (isWhiteList(to)) return next()
    // 其他没有访问权限的页面将被重定向到登录页面
    return next("/login")
  }

  // 如果已经登录，并准备进入 Login 页面，则重定向到主页
  if (to.path === "/login") {
    return next({ path: "/" })
  }

  // 如果用户已经获得其权限角色
  if (userStore.roles.length !== 0) return next()

  // 否则要重新获取权限角色
  try {
    await userStore.getInfo({ employeeNumber: localStorage.getItem("employeeNumber") })
    // 注意：角色必须是一个数组！ 例如: ["admin"] 或 ["developer", "editor"]
    const roles = userStore.roles

    //获取后端路由，并传递给dynamicRoutes
    const { data } = await getDynamicRoutesApi()
    permissionStore.setDynamicRoutes(filterDynamicRoutes(data))

    // 生成可访问的 Routes
    routeSettings.dynamic ? permissionStore.setRoutes(roles) : permissionStore.setAllRoutes()
    // 将 "有访问权限的动态路由" 添加到 Router 中
    permissionStore.addRoutes.forEach((route) => router.addRoute(route))

    console.log("const+dynamic routes", router.getRoutes())
    // 确保添加路由已完成
    // 设置 replace: true, 因此导航将不会留下历史记录
    next({ ...to, replace: true })
  } catch (err: any) {
    // 过程中发生任何错误，都直接重置 Token，并重定向到登录页面
    userStore.resetToken()
    ElMessage.error(err.message || "路由守卫过程发生错误")
    next("/login")
  }
})

router.afterEach((to) => {
  // 设置路由跳转
  setRouteChange(to)
  setTitle(to.meta.title)
  NProgress.done()
})

interface RoutesItem {
  path: string
  name: string
  redirect: string
  meta: {
    title: string
    elIcon: string
    roles: string[]
    alwaysShow: boolean
    keepAlive: boolean
  }
  component: object | null
  children?: Object[] | null
}

/**对动态路由进行处理 */
const filterDynamicRoutes = (data: any) => {
  const res: any[] = []
  data.forEach((item: any) => {
    let e_new: RoutesItem = {
      path: item.path,
      name: item.name,
      redirect: item.redirect,
      meta: {
        title: item.meta?.title,
        elIcon: item.meta?.elIcon,
        roles: item.meta?.roles,
        alwaysShow: item.meta?.alwaysShow,
        keepAlive: item.meta?.keepAlive === 1 ? true : false
      },
      component: null
    }
    if (item.component === "Layouts") {
      e_new.component = Layouts
    } else {
      e_new.component = modules[`/src/views${item.component}.vue`]
    }
    // if (item.children.length === 0) {
    //   item.children = null
    // }
    if (item.children && item.children != null) {
      const child = filterDynamicRoutes(item.children)
      e_new = { ...e_new, children: child }
    }
    res.push(e_new)
  })
  console.log("%c [ res ]-124", "font-size:13px; background:pink; color:#bf2c9f;", res)
  return res
}
