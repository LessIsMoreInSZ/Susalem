<script setup lang="ts"></script>

<template>
  <div class="min-h-500px flex-col-stretch gap-16px overflow-hidden lt-sm:overflow-auto">
    <NCard title="个人信息" style="margin-bottom: 16px">
      <NTabs type="line" animated>
        <NTabPane name="oasis" tab="信息修改"></NTabPane>
        <NTabPane name="the beatles" tab="密码修改"></NTabPane>
      </NTabs>
    </NCard>
  </div>
</template>

<style scoped></style>
