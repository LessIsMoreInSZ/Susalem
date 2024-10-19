<script setup lang="tsx">
import { NButton } from 'naive-ui';
import { ref } from 'vue';
import VueJsonPretty from 'vue-json-pretty';
import { getSysErrlogPage } from '@/service/api';
import { $t } from '@/locales';
import { useAppStore } from '@/store/modules/app';
import { useTable } from '@/hooks/common/table';
import 'vue-json-pretty/lib/styles.css';
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
  apiFn: getSysErrlogPage,
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
      key: 'exceptionType',
      title: '异常类型',
      align: 'center',
      minWidth: 150
    },
    {
      key: 'actionName',
      title: '异常方法',
      align: 'center',
      width: 150
    },
    {
      key: 'logDateTime',
      title: '异常时间',
      align: 'center',
      minWidth: 150,
      ellipsis: {
        tooltip: true
      }
    },
    {
      key: 'operate',
      title: $t('common.operate'),
      align: 'center',
      width: 150,
      render: row => (
        <div class="flex-center gap-8px">
          <NButton type="primary" ghost size="small" onClick={() => activate(row.message)}>
            详情
          </NButton>
        </div>
      )
    }
  ]
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
              <NListItem>
                <NThing title="" content-style="margin-top: 10px;">
                  <template #description>
                    <NSpace size="small" style="margin-top: 4px">
                      <NTag :bordered="false" type="info" size="small">详情</NTag>
                    </NSpace>
                  </template>
                  <div>
                    {{ activedata }}
                    <!--
 <VueJsonPretty
                      show-length
                      show-icon
                      virtual
                      show-line-number
                      show-select-controller
                      :data=""
                    ></VueJsonPretty>
-->
                  </div>
                </NThing>
              </NListItem>
            </div>
          </NList>
        </NDrawerContent>
      </NDrawer>
    </div>
  </div>
</template>

<style scoped></style>
