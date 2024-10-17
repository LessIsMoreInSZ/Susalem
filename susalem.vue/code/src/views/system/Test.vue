<template>
  <div>
    <div>Test</div>
    <svg-icon name="button" color="red" style="width: 100px; height: 100px" />
    <svg-icon name="chaxun" color="red" style="width: 100px; height: 100px" />
    <svg-icon name="shouquan" color="red" style="width: 100px; height: 100px" />
    <div>
      <el-input v-model="input" style="width: 30%"></el-input>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref } from "vue"
const input = ref("")
</script>

<style></style>
