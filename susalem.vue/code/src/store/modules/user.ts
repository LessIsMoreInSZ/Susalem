import { ref } from "vue"
import store from "@/store"
import { defineStore } from "pinia"
import { useTagsViewStore } from "./tags-view"
import { useSettingsStore } from "./settings"
import { getToken, removeToken, setToken } from "@/utils/cache/cookies"
import { resetRouter } from "@/router"
import { loginApi, getUserInfoApi } from "@/api/login"
import { type LoginRequestData } from "@/api/login/types/login"
import routeSettings from "@/config/route"
import router from "@/router/index"
import { resolve } from "path"
import { removeAllPageSize } from "@/utils/cache/local-storage"

export const useUserStore = defineStore("user", () => {
  const token = ref<string>(getToken() || "")
  const roles = ref<string[]>([])
  const username = ref<string>("")
  const employeeNumber = ref<string>("")

  const tagsViewStore = useTagsViewStore()
  const settingsStore = useSettingsStore()

  //const modules = import.meta.glob('../view/*/*/*.vue')
  //const modules = import.meta.glob('/src/*/*.vue')

  /** 登录 */
  const login = async ({ employeeNumber, password }: LoginRequestData) => {
    const { data } = await loginApi({ employeeNumber, password })
    setToken(data.token)
    token.value = data.token
  }
  /** 获取用户详情 */
  const getInfo = async (employeeNumber: any) => {
    const { data } = await getUserInfoApi(employeeNumber)
    console.log(5, data)

    //getRoutes()

    username.value = data.username
    // 验证返回的 roles 是否为一个非空数组，否则塞入一个没有任何作用的默认角色，防止路由守卫逻辑进入无限循环
    roles.value = data.roles?.length > 0 ? data.roles : routeSettings.defaultRoles
  }

  //自己写的
  // const getRoutes = async () => {
  //   console.log("getRoutes")

  //   const { routelist } = await getDynamicRoutesApi()
  // }

  /** 模拟角色变化 */
  const changeRoles = async (role: string) => {
    const newToken = "token-" + role
    token.value = newToken
    setToken(newToken)
    // 用刷新页面代替重新登录
    window.location.reload()
  }
  /** 登出 */
  const logout = () => {
    removeToken()
    token.value = ""
    roles.value = []
    resetRouter()
    _resetTagsView()
    removeAllPageSize(getAllPageSize())
  }
  /** 重置 Token */
  const resetToken = () => {
    removeToken()
    token.value = ""
    roles.value = []
  }
  /** 重置 Visited Views 和 Cached Views */
  const _resetTagsView = () => {
    // if (!settingsStore.cacheTagsView) {
    tagsViewStore.delAllVisitedViews()
    tagsViewStore.delAllCachedViews()
  }
  /**获取所有页面的条数 */
  const getAllPageSize = () => {
    let pageKeys = []
    for (let i = 0; i < localStorage.length; i++) {
      const pageSizeKey = localStorage.key(i)
      console.log("%c [ pageSizeKey ]-87", "font-size:13px; background:pink; color:#bf2c9f;", pageSizeKey)
      if (pageSizeKey?.includes("-PageSize")) {
        pageKeys.push(pageSizeKey)
      }
    }
    return pageKeys
  }
  return { token, roles, username, employeeNumber, login, getInfo, changeRoles, logout, resetToken }
})

/** 在 setup 外使用 */
export function useUserStoreHook() {
  return useUserStore(store)
}
