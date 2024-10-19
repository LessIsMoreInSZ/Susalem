<script setup lang="tsx">
import { ref } from 'vue';
import { NImage, NTime } from 'naive-ui';
import dayjs from 'dayjs'; // 确保安装了 dayjs
import { getGalleryPagelist } from '@/service/api';
import { $t } from '@/locales';
import { useAppStore } from '@/store/modules/app';
import { useTable } from '@/hooks/common/table';
const appStore = useAppStore();

// 抽屉开关
const active = ref(false);

const activedata = ref<sysVisLogPageRes | null>();
// 开关抽屉的方法
const activate = (row: sysVisLogPageRes) => {
  active.value = true;
  activedata.value = row;
  console.log(row);
};

const { columns, columnChecks, data, getData, loading, mobilePagination, searchParams, resetSearchParams } = useTable({
  apiFn: getGalleryPagelist,
  apiParams: {
    pageNo: 1,
    pageSize: 10
  },
  columns: () => [
    {
      key: 'imagesID',
      title: '编号',
      align: 'center',
      width: 100
    },
    {
      key: 'imagesURL',
      title: '图片',
      align: 'center',
      minWidth: 120,
      render: row => (
        <div>
          <NImage width="80" src={row.imagesURL} />
        </div>
      )
    },

    {
      key: 'type',
      title: '类型',
      align: 'center',
      width: 120
    },
    {
      key: 'imagesWide',
      title: '宽',
      align: 'center',
      minWidth: 100,
      ellipsis: {
        tooltip: true
      }
    },
    {
      key: 'imagesHeiget',
      title: '高',
      align: 'center',
      minWidth: 100,
      ellipsis: {
        tooltip: true
      }
    },
    {
      key: 'imagesSize',
      title: '大小',
      align: 'center',
      minWidth: 100,
      ellipsis: {
        tooltip: true
      }
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
</script>

<template>
  <div class="min-h-500px flex-col-stretch gap-16px overflow-hidden lt-sm:overflow-auto">
    <!-- <SysCustomerSearch v-model:model="searchParams" @reset="resetSearchParams" @search="getData" /> -->

    <NCard title="用户列表" :bordered="false" size="small" class="sm:flex-1-hidden card-wrapper">
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
          <template #header>图库列表</template>
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
