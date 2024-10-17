<script lang="ts" setup>
import { useUserStore } from "@/store/modules/user"
import FullScreenContainer from "./components/full-screen-container.vue"
import Admin from "./components/Admin.vue"
import Editor from "./components/Editor.vue"

const userStore = useUserStore()
const isAdmin = userStore.roles.includes("超级管理员")
</script>

<template>
  <!-- <component :is="isAdmin ? Admin : Editor" /> -->
  <!-- <FullScreenContainer> -->
  <div id="dashboard"></div>
  <!-- </FullScreenContainer> -->
</template>
<style scoped lang="scss">
// #dashboard {
//   height: 100%;
//   width: 100%;
//   background-color: aqua;
//   background: url("../../assets/layouts/bg.png");
//   background-size: 100% 100%;
//   position: fixed;
//   overflow: hidden;
// }
</style>
