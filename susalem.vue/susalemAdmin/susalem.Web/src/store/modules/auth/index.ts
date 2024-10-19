import { computed, reactive, ref } from 'vue';
import { defineStore } from 'pinia';
import { useLoading } from '@sa/hooks';
import { SetupStoreId } from '@/enum';
import { useRouterPush } from '@/hooks/common/router';
import { getTokenUserInfo, reqLogin } from '@/service/api';
import { localStg } from '@/utils/storage';
import { $t } from '@/locales';
import { useRouteStore } from '../route';
import { clearAuthStorage, getToken, getUserInfo } from './shared';

export const useAuthStore = defineStore(SetupStoreId.Auth, () => {
  const routeStore = useRouteStore();
  const { route, toLogin, redirectFromLogin } = useRouterPush(false);
  const { loading: loginLoading, startLoading, endLoading } = useLoading();

  const token = ref(getToken());

  let userInfo: Api.Auth.UserInfo = reactive(getUserInfo());

  /** 是静态路由中的超级角色 */
  const isStaticSuper = computed(() => {
    const { VITE_AUTH_ROUTE_MODE, VITE_STATIC_SUPER_ROLE } = import.meta.env;

    return false;
    return VITE_AUTH_ROUTE_MODE === 'static' && userInfo.roles.includes(VITE_STATIC_SUPER_ROLE);
  });

  /** 是登录名 */
  const isLogin = computed(() => Boolean(token.value));

  /** 重置身份验证存储 */
  async function resetStore() {
    const authStore = useAuthStore();

    clearAuthStorage();

    authStore.$reset();

    if (!route.value.meta.constant) {
      await toLogin();
    }

    routeStore.resetStore();
  }

  /**
   * 登录
   *
   * @param userName User name
   * @param password Password
   * @param [redirect=true] 登录后是否重定向。默认值为`true`. Default is `true`
   */
  async function login(userName: string, password: string, redirect = true) {
    startLoading();
    await reqLogin(userName, password).then(async res => {
      if (res.data) {
        const pass = await getUserInfoByToken(res.data.token);
        if (pass) {
          await routeStore.initAuthRoute();
          if (redirect) {
            await redirectFromLogin();
          }
          if (routeStore.isInitAuthRoute) {
            window.$notification?.success({
              title: $t('page.login.common.loginSuccess'),
              content: $t('page.login.common.welcomeBack', { userName: userInfo.userName }),
              duration: 4500
            });
          }
        }
      } else {
        resetStore();
      }
    });

    endLoading();
  }

  async function getUserInfoByToken(loginToken: string) {
    // 1. stored in the localStorage, the later requests need it in headers
    localStg.set('token', loginToken);
    // localStg.set('refreshToken', loginToken.refreshToken);
    const userInfores = await getTokenUserInfo();
    if (userInfores.data) {
      // 2. store user info
      localStg.set('userInfo', userInfores.data);
      userInfo = userInfores.data;
      // 3. update store
      // console.log('更新用户信息', userInfores.data);
      Object.assign(userInfo, userInfores.data);

      return true;
    }

    return false;
  }
  return {
    token,
    userInfo,
    isStaticSuper,
    isLogin,
    loginLoading,
    resetStore,
    login
  };
});
