<template>
  <el-dropdown trigger="click" @command="changeLanguage">
    <!-- <i :class="'iconfont icon-zhongyingwen'" class="toolBar-icon"></i> -->
    <svg-icon name="icon-zhongyingwen2zhongwen" class="toolBar-icon"></svg-icon>

    <!-- <SvgIcon name="fullscreen" /> -->
    <template #dropdown>
      <el-dropdown-menu>
        <el-dropdown-item
          v-for="item in languageList"
          :key="item.value"
          :command="item.value"
          :disabled="language === item.value"
        >
          {{ item.label }}
        </el-dropdown-item>
      </el-dropdown-menu>
    </template>
  </el-dropdown>
</template>

<script setup lang="ts">
// //   import { useI18n } from "vue-i18n";
import { computed, ref } from "vue"

//   const i18n = useI18n();
//   const globalStore = useGlobalStore();
//   const language = computed(() => globalStore.language);
const language = ref("zh")

// 语言列表
const languageList = [
  { label: "简体中文", value: "zh" },
  { label: "English", value: "en" }
]

// 切换语言
const changeLanguage = (lang: string) => {
  //   i18n.locale.value = lang
  //   globalStore.setGlobalState("language", lang as LanguageType)
}
</script>
<style lang="scss" scoped>
.toolBar-icon {
  width: 18px;
  height: 18px;
  cursor: pointer;
}
.svg-icon {
  font-size: 20px;
  &:focus {
    outline: none;
  }
}
</style>
