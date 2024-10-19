<script setup lang="ts">
import { $t } from '@/locales';
import { useAuth } from '@/hooks/business/auth';

const { hasAuth } = useAuth();
</script>

<template>
  <NSpace vertical :size="16">
    <NCard title="权限校验" :bordered="false" size="small" segmented class="card-wrapper">
      <NDescriptions bordered :column="1">
        <NDescriptionsItem ions-item label="使用方法">
          <NSpace>
            <p>import { useAuth } from '@/hooks/business/auth';</p>
            <br />
            <p>const { hasAuth } = useAuth();</p>
            <br />
            <p>v-if="hasAuth('api:SysMenu:Add')"</p>
          </NSpace>
        </NDescriptionsItem>
      </NDescriptions>
    </NCard>
    <NCard
      :title="$t('page.function.toggleAuth.authHook')"
      :bordered="false"
      size="small"
      segmented
      class="card-wrapper"
    >
      <NSpace>
        <NButton v-if="hasAuth('api:SysMenu:Add')">拥有 api:SysMenu:Add 权限可见</NButton>

        <NButton v-if="hasAuth('api:SysMenu:Ad')">拥有api:SysMenu:Ad 权限可见</NButton>
      </NSpace>
    </NCard>
  </NSpace>
</template>

<style scoped></style>
