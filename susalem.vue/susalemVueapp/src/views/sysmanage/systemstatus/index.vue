<script setup lang="ts">
import { computed, onMounted, ref } from 'vue';
import { useAppStore } from '@/store/modules/app';
import { getSysData } from '@/service/api';

const appStore = useAppStore();

const column = computed(() => (appStore.isMobile ? 1 : 2));

const sysdata = ref<SystemInfo | null>();
const isLoading = ref(true);
onMounted(async () => {
  try {
    sysdata.value = (await getSysData()).data;
  } catch (error) {
    console.error('Failed to fetch system data:', error);
    // 可以在这里添加更多的错误处理逻辑
  } finally {
    isLoading.value = false;
  }
});
</script>

<template>
  <div>
    <NSpace vertical :size="16">
      <NCard title="服务器信息" :bordered="false" size="small" segmented class="card-wrapper">
        <NDescriptions label-placement="left" bordered size="small" :column="column">
          <NDescriptionsItem label="服务器名称">
            <NSkeleton v-if="isLoading" :sharp="false" class="card-primary" />
            <a v-else class="text-primary" rel="noopener noreferrer">{{ sysdata?.machineName }}</a>
          </NDescriptionsItem>
          <NDescriptionsItem label="服务器IP">
            <a class="text-primary" rel="noopener noreferrer">{{ sysdata?.ip }}</a>
          </NDescriptionsItem>
          <NDescriptionsItem label="系统名称">
            <a class="text-primary" rel="noopener noreferrer">{{ sysdata?.osName }}</a>
          </NDescriptionsItem>
          <NDescriptionsItem label="系统架构">
            <a class="text-primary" rel="noopener noreferrer">{{ sysdata?.osArchitecture }}</a>
          </NDescriptionsItem>
          <NDescriptionsItem label=".Net版本">
            <a class="text-primary" rel="noopener noreferrer">{{ sysdata?.doNetName }}</a>
          </NDescriptionsItem>
          <NDescriptionsItem label="	CPU数量">
            <a class="text-primary" rel="noopener noreferrer">{{ sysdata?.cpuCount }}</a>
          </NDescriptionsItem>
          <NDescriptionsItem label="服务开始运行时间">
            <a class="text-primary" rel="noopener noreferrer">{{ sysdata?.startTime }}</a>
          </NDescriptionsItem>
          <NDescriptionsItem label="	服务运行时间">
            <a class="text-primary" rel="noopener noreferrer">{{ sysdata?.runTime }}</a>
          </NDescriptionsItem>
        </NDescriptions>
      </NCard>
      <NCard title="CPU/RAM信息" :bordered="false" size="small" segmented class="card-wrapper">
        <NFlex justify="space-around" class="flex-container">
          <div class="flex-item">
            <NProgress type="circle" :percentage="10" />
            <span>CPU使用率</span>
          </div>
          <div class="flex-item">
            <NProgress type="circle" :percentage="34" />
            <span>RAM使用率</span>
          </div>
          <div class="flex-item">
            <NProgress type="circle" :percentage="sysdata?.memoryInfo.appRAMRate" />
            <span>应用RAM使用率</span>
          </div>
        </NFlex>
      </NCard>
      <NCard title="磁盘信息" :bordered="false" size="small" segmented class="card-wrapper">
        <!-- 骨架屏 -->
        <NSkeleton v-if="isLoading" :sharp="false" class="card-primary" />
        <NFlex v-else justify="space-around" class="flex-container">
          <div v-for="(item, index) in sysdata?.diskInfo" :key="index" class="flex-item">
            <NProgress type="circle" :percentage="item.availablePercent" />
            <span>{{ item.diskName }}</span>
          </div>
        </NFlex>
      </NCard>
    </NSpace>
  </div>
</template>

<style scoped>
.flex-container {
  flex-direction: column; /* 垂直排列 */
  align-items: center; /* 水平居中 */
}

.flex-item {
  display: flex; /* 启用Flexbox布局 */
  flex-direction: column; /* 子元素垂直排列 */
  align-items: center; /* 子元素水平居中 */
  justify-content: flex-end; /* 子元素在交叉轴方向上对齐到容器底部 */
  margin: 20px; /* 根据需要添加间距 */
}
</style>
