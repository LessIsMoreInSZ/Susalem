<script lang="ts" setup>
import { useTagsViewStore } from "@/store/modules/tags-view"
import { useSettingsStore } from "@/store/modules/settings"
import Footer from "./Footer/index.vue"
import { watch } from "vue"

const tagsViewStore = useTagsViewStore()
const settingsStore = useSettingsStore()
</script>
<!-- key 采用 route.path 和 route.fullPath 有着不同的效果，大多数时候 path 更通用 -->
<template>
  <section class="app-main">
    <div class="app-scrollbar">
      <router-view v-slot="{ Component, route }">
        <transition name="el-fade-in" mode="out-in">
          <div>
            <!-- 想要使用这一种,需要给每个组件设置name,安装vite-plugin-vue-setup-extend-plus插件 -->
            <keep-alive :include="tagsViewStore.cachedViews">
              <component :is="Component" :key="route.path" class="app-container-grow" />
            </keep-alive>
          </div>
        </transition>
      </router-view>
      <!-- 页脚 -->
      <!-- <Footer v-if="settingsStore.showFooter" /> -->
    </div>
    <!-- 返回顶部 -->
    <el-backtop />
    <!-- 返回顶部（固定 Header 情况下） -->
    <el-backtop target=".app-scrollbar" />
  </section>
</template>

<style lang="scss" scoped>
@import "@/styles/mixins.scss";

.app-main {
  width: 100%;
  background-color: var(--v3-body-bg-color);
  display: flex;
}

.app-scrollbar {
  flex-grow: 1;
  overflow: auto;
  @extend %scrollbar;
  display: flex;
  flex-direction: column;
  .app-container-grow {
    flex-grow: 1;
  }
}
</style>
