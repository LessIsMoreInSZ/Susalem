<script lang="ts" setup>
import { ref } from "vue"
import { ElMessage } from "element-plus"
import { useRouter } from "vue-router"
import { storeToRefs } from "pinia"
import { useAppStore } from "@/store/modules/app"
import { useSettingsStore } from "@/store/modules/settings"
import { useUserStore } from "@/store/modules/user"
import { useTagsViewStore } from "@/store/modules/tags-view"
import { User } from "@element-plus/icons-vue"
import Hamburger from "../Hamburger/index.vue"
import Breadcrumb from "../Breadcrumb/index.vue"
import Sidebar from "../Sidebar/index.vue"
import Notify from "@/components/Notify/index.vue"
import ThemeSwitch from "@/components/ThemeSwitch/index.vue"
import Screenfull from "@/components/Screenfull/index.vue"
import SearchMenu from "@/components/SearchMenu/index.vue"
import Language from "@/components/Language/index.vue"
import { useDevice } from "@/hooks/useDevice"
import { useLayoutMode } from "@/hooks/useLayoutMode"
import PersonalCenter from "@/components/PersonalCenter/index.vue"
import useSingnalR from "@/hooks/useSingnalR"

const { isMobile } = useDevice()
const { isTop } = useLayoutMode()
const router = useRouter()
const appStore = useAppStore()
const userStore = useUserStore()
const settingsStore = useSettingsStore()
const tagsViewStore = useTagsViewStore()
const { showNotify, showThemeSwitch, showScreenfull, showSearchMenu } = storeToRefs(settingsStore)

//
/** 切换侧边栏 */
const toggleSidebar = () => {
  appStore.toggleSidebar(false)
}

/** 登出 */
const logout = () => {
  useSingnalR.stopSignalR()
  userStore.logout()
  // // 清除tagsView
  // tagsViewStore.delAllCachedViews()
  // tagsViewStore.delAllVisitedViews()
  router.push("/login")
}

// #region 个人中心页面
const drawerOpen = ref(false)
const openPersonalCenter = () => {
  drawerOpen.value = true
}
// 关闭个人中心页面
const drawerClose = () => {
  drawerOpen.value = false
}

// 图片请求资源404提示
const errorHandler = () => {
  imageUrl.value = ""
  ElMessage.warning("用户头像资源丢失，请重新上传头像")
}
// 头像资源路径
const imageUrl = ref()
const updateImageUrl = (value: string) => {
  imageUrl.value = value
}
// #endregion
</script>

<template>
  <div class="navigation-bar">
    <Hamburger
      v-if="!isTop || isMobile"
      :is-active="appStore.sidebar.opened"
      class="hamburger"
      @toggle-click="toggleSidebar"
    />
    <Breadcrumb v-if="!isTop || isMobile" class="breadcrumb" />
    <Sidebar v-if="isTop && !isMobile" class="sidebar" />
    <div class="right-menu">
      <!-- <SearchMenu v-if="showSearchMenu" class="right-menu-item" />
      <Screenfull v-if="showScreenfull" class="right-menu-item" />
      <ThemeSwitch v-if="showThemeSwitch" class="right-menu-item" />
      <Notify v-if="showNotify" class="right-menu-item" /> -->
      <!-- <Language></Language> -->
      <el-dropdown class="right-menu-item">
        <div class="right-menu-avatar">
          <el-avatar v-if="imageUrl" :size="33">
            <img :src="imageUrl" alt="图片加载中" class="user" @error="errorHandler"
          /></el-avatar>
          <el-avatar v-else :icon="User" :size="33"></el-avatar>
          <span>{{ userStore.username }}</span>
        </div>
        <template #dropdown>
          <el-dropdown-menu>
            <el-dropdown-item @click="openPersonalCenter" icon="UserFilled">
              <span style="display: block">个人中心</span>
            </el-dropdown-item>
            <el-dropdown-item @click="logout" icon="SwitchButton">
              <span style="display: block">退出登录</span>
            </el-dropdown-item>
          </el-dropdown-menu>
        </template>
      </el-dropdown>
      <PersonalCenter
        :drawerOpen="drawerOpen"
        @drawerClose="drawerClose"
        @updateImageUrl="updateImageUrl"
      ></PersonalCenter>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.navigation-bar {
  height: var(--v3-navigationbar-height);
  overflow: hidden;
  background: var(--v3-header-bg-color);
  display: flex;
  justify-content: space-between;
  .hamburger {
    display: flex;
    align-items: center;
    height: 100%;
    padding: 0 15px;
    cursor: pointer;
  }
  .breadcrumb {
    flex: 1;
    // 参考 Bootstrap 的响应式设计将宽度设置为 576
    @media screen and (max-width: 576px) {
      display: none;
    }
  }
  .sidebar {
    flex: 1;
    // 设置 min-width 是为了让 Sidebar 里的 el-menu 宽度自适应
    min-width: 0px;
    :deep(.el-menu) {
      background-color: transparent;
    }
    :deep(.el-sub-menu) {
      &.is-active {
        .el-sub-menu__title {
          color: var(--el-menu-active-color) !important;
        }
      }
    }
  }
  .right-menu {
    margin-right: 10px;
    height: 100%;
    display: flex;
    align-items: center;
    color: #606266;
    .right-menu-item {
      padding: 0 10px;
      cursor: pointer;
      .right-menu-avatar {
        display: flex;
        align-items: center;
        .el-avatar {
          margin-right: 10px;
        }
        span {
          font-size: 17px;
        }
      }
    }
  }
}
</style>
