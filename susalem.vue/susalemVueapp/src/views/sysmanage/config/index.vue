<script setup lang="tsx">
import { deleteConfig, getConfigPage } from '@/service/api';
import { $t } from '@/locales';
import { useAppStore } from '@/store/modules/app';
import { useTable, useTableOperate } from '@/hooks/common/table';
import ConfigOperateDrawer from './modules/config-operate-drawer.vue';

const appStore = useAppStore();

const { data, getData, columns, loading, pagination, mobilePagination } = useTable({
  apiFn: getConfigPage,
  apiParams: {
    pageNo: 1,
    pageSize: 10
  },
  columns: () => [
    {
      key: 'configID',
      title: '序号',
      align: 'center',
      width: 50,
      minWidth: 100
    },
    {
      key: 'configKey',
      title: '配置名称',
      align: 'center',
      width: 100,
      minWidth: 200,
      ellipsis: {
        tooltip: true
      }
    },
    {
      key: 'configType',
      title: '配置类型',
      align: 'center',
      width: 100,
      minWidth: 200,
      ellipsis: {
        tooltip: true
      }
    },
    {
      key: 'configValue',
      title: '配置参数',
      align: 'center',
      width: 100,
      minWidth: 200
    },
    {
      key: 'configdDescribe',
      title: '配置描述',
      align: 'center',
      width: 100,
      minWidth: 200,
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
          <NButton type="primary" ghost size="small" onClick={() => edit(row)}>
            {$t('common.edit')}
          </NButton>
          <NPopconfirm onPositiveClick={() => handleDelete(row.configID)}>
            {{
              default: () => $t('common.confirmDelete'),
              trigger: () => (
                <NButton type="error" ghost size="small">
                  {$t('common.delete')}
                </NButton>
              )
            }}
          </NPopconfirm>
        </div>
      )
    }
  ]
});

const {
  drawerVisible,
  handleAdd,
  editingData,
  handleEditAny,
  operateType
  // closeDrawer
} = useTableOperate(data, getData);
function edit(rowData: any) {
  handleEditAny(rowData);
  // console.log('editingData', editingData);
}

function handleDelete(configID: number) {
  // console.log(id);
  deleteConfig(configID).then(res => {
    if (res.data) {
      window.$message?.success('删除成功');
      getData();
    } else {
      window.$message?.success('删除成功');
    }
  });
}
</script>

<template>
  <div class="min-h-500px flex-col-stretch gap-16px overflow-hidden lt-sm:overflow-auto">
    <NCard title="配置管理" :bordered="false" size="small" class="sm:flex-1-hidden card-wrapper">
      <template #header-extra>
        <TableHeaderOperation :loading="loading" is-hidden disabled-delete @add="handleAdd" @refresh="getData" />
      </template>
      <NDataTable
        :columns="columns"
        :data="data"
        size="small"
        :flex-height="!appStore.isMobile"
        :scroll-x="962"
        :loading="loading"
        remote
        :row-key="row => row.configID"
        :pagination="mobilePagination"
        class="sm:h-full"
      />
    </NCard>
    <div>
      <ConfigOperateDrawer
        v-model:visible="drawerVisible"
        :operate-type="operateType"
        :row-data="editingData"
        @submitted="getData"
      ></ConfigOperateDrawer>
    </div>
  </div>
</template>

<style scoped></style>
