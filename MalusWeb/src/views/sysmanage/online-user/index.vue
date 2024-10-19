<script setup lang="tsx">
import { NButton } from 'naive-ui';
import { onMounted, ref } from 'vue';
import { ForceOffline, SendMsgToOne, getOnlineUser } from '@/service/api';
import { $t } from '@/locales';
import { useAppStore } from '@/store/modules/app';
import { useTable } from '@/hooks/common/table';

const appStore = useAppStore();

// 抽屉开关
const active = ref(false);
const msg = ref(null);

const activedata = ref<sysLogPageRecord | null>();
// 开关抽屉的方法
const activate = (row: sysLogPageRecord) => {
  active.value = true;
  activedata.value = row;
};

const { data, columns, loading, mobilePagination, resetSearchParams } = useTable({
  apiFn: getOnlineUser,
  apiParams: {
    pageNo: 1,
    pageSize: 10
  },
  columns: () => [
    {
      key: 'userName',
      title: '账号',
      align: 'center',
      minWidth: 50
    },
    {
      key: 'realName',
      title: '用户名',
      align: 'center',
      width: 70
    },
    {
      key: 'ip',
      title: 'Ip',
      align: 'center',
      minWidth: 70
    },
    {
      key: 'os',
      title: '系统',
      align: 'center',
      minWidth: 70
    },
    {
      key: 'browser',
      title: '浏览器',
      align: 'center',
      minWidth: 70
    },
    {
      key: 'time',
      title: '在线时间',
      align: 'center',
      minWidth: 100
    },
    {
      key: 'operate',
      title: $t('common.operate'),
      align: 'center',
      width: 180,
      render: row => (
        <div class="flex-center gap-8px">
          <NButton type="primary" ghost size="small" onClick={() => forceuser(row)}>
            强制下线
          </NButton>
          <NButton type="primary" ghost size="small" onClick={() => activate(row)}>
            发送消息
          </NButton>
        </div>
      )
    }
  ]
});

const sendmsg = () => {
  SendMsgToOne({
    connectionId: activedata.value?.connectionId,
    msg: msg.value
  }).then(res => {
    if (res.data) {
      window.$message?.success('发送成功');
    }
  });
};

const forceuser = (row: sysLogPageRecord) => {
  ForceOffline({ connectionId: row.connectionId }).then(res => {
    // console.log('请求成功', res);
    if (res.data) {
      resetSearchParams();
    }
  });
};
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
    <NCard title="在线用户" :bordered="false" size="small" class="sm:flex-1-hidden card-wrapper">
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
          <template #header>发送消息</template>
          <NSpace vertical>
            <NInput v-model:value="msg" type="textarea" placeholder="基本的 Textarea"></NInput>
            <NButton type="primary" @click="sendmsg">发送</NButton>
          </NSpace>
        </NDrawerContent>
      </NDrawer>
    </div>
  </div>
</template>

<style scoped></style>
