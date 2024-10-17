<script lang="ts" setup>
// 在.env文件中定义
const VITE_APP_TITLE = import.meta.env.VITE_APP_TITLE
</script>

<template>
  <footer class="layout-footer">
    © 2024 Suzhou Harmontronics Automation Technology Co.,Ltd. All rights reserved. 苏ICP备16028560号
  </footer>
</template>

<style lang="scss" scoped>
.layout-footer {
  width: 100%;
  min-height: 50px;
  display: flex;
  align-items: center;
  justify-content: center;
  color: #c0c4cc;
}
</style>
