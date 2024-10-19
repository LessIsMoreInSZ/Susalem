<script setup lang="tsx">
import { onMounted, ref } from 'vue';
import { getSysOplogPage } from '@/service/api';
import { $t } from '@/locales';
import { useAppStore } from '@/store/modules/app';
import { useTable } from '@/hooks/common/table';
import SysLogSearch from '../modules/syslog-search.vue';
const appStore = useAppStore();

// 抽屉开关
const active = ref(false);

const activedata = ref<any>();
// 开关抽屉的方法
const activate = (row: any) => {
  active.value = true;
  activedata.value = row;
};

const { columns, columnChecks, data, getData, loading, mobilePagination, searchParams, resetSearchParams } = useTable({
  apiFn: getSysOplogPage,
  apiParams: {
    pageNo: 1,
    pageSize: 10
  },
  columns: () => [
    {
      key: 'id',
      title: '序号',
      align: 'center',
      minWidth: 150
    },
    {
      key: 'logType',
      title: '类型',
      align: 'center',
      minWidth: 150
    },
    {
      key: 'message',
      title: '详情',
      align: 'center',
      minWidth: 100,
      ellipsis: {
        tooltip: true
      }
    },
    {
      key: 'logDateTime',
      title: '日志时间',
      align: 'center',
      minWidth: 100
    },
    {
      key: 'operate',
      title: $t('common.operate'),
      align: 'center',
      width: 130,
      render: row => (
        <div class="flex-center gap-8px">
          <NButton type="primary" ghost size="small" onClick={() => activate(row)}>
            详情
          </NButton>
        </div>
      )
    }
  ]
});

onMounted(async () => {
  try {
    // console.log('res');
  } catch (error) {
    console.error('Failed to fetch system data:', error);
    // 可以在这里添加更多的错误处理逻辑
  }
});
</script>

<template>
  <div class="min-h-500px flex-col-stretch gap-16px overflow-hidden lt-sm:overflow-auto">
    <SysLogSearch v-model:model="searchParams" @reset="resetSearchParams" @search="getData" />

    <NCard title="日志列表" :bordered="false" size="small" class="sm:flex-1-hidden card-wrapper">
      <template #header-extra>
        <NButton size="small" @click="getData">
          <template #icon>
            <icon-mdi-refresh class="text-icon" :class="{ 'animate-spin': loading }" />
          </template>
          刷新
        </NButton>
      </template>
      <NDataTable
        :columns="columns"
        :data="data"
        size="small"
        :flex-height="!appStore.isMobile"
        :scroll-x="962"
        :loading="loading"
        remote
        :row-key="row => row.id"
        :pagination="mobilePagination"
        class="sm:h-full"
      />
    </NCard>
    <div>
      <NDrawer v-model:show="active" :width="502">
        <NDrawerContent>
          <template #header>日志详情</template>
          <NList hoverable clickable>
            <div v-if="activedata">
              <div v-for="([key, value], index) in Object.entries(activedata)" :key="index">
                <NListItem>
                  <NThing title="" content-style="margin-top: 10px;">
                    <template #description>
                      <NSpace size="small" style="margin-top: 4px">
                        <NTag :bordered="false" type="info" size="small">{{ key }}</NTag>
                      </NSpace>
                    </template>
                    {{ value }}
                  </NThing>
                </NListItem>
              </div>
            </div>
          </NList>
        </NDrawerContent>
      </NDrawer>
    </div>
  </div>
</template>

<style scoped></style>
