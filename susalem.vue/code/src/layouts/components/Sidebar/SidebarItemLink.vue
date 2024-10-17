<script lang="ts" setup>
import { isExternal } from "@/utils/validate"
import { useRouter } from "vue-router"

interface Props {
  to: string
}

const props = defineProps<Props>()
const router = useRouter()

const navigate = (event: Event) => {
  console.log("props.to")
  // 禁用默认url跳转
  event.preventDefault()
  router.push(props.to)
}
</script>

<template>
  <a v-if="isExternal(props.to)" :href="props.to" target="_blank" rel="noopener">
    <slot />
  </a>
  <!-- <router-link v-else :to="props.to" >
    <slot />
  </router-link> -->
  <!-- 使用span的点击事件来触发页面路由跳转，避免显示url -->
  <span v-else @click="navigate" class="custom-link">
    <slot />
  </span>
</template>
<style scoped>
/* 自定义这个元素是可点击的标签 */
.custom-link {
  cursor: pointer;
}
/* 禁用指针事件（鼠标悬停） */
.custom-link * {
  pointer-events: none;
}
</style>
