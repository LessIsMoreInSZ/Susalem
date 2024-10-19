<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { useScriptTag } from '@vueuse/core';
import { BAIDU_MAP_SDK_URL } from '@/constants/map-sdk';

defineOptions({ name: 'BaiduMap' });

window.HOST_TYPE = '2';

const { load } = useScriptTag(BAIDU_MAP_SDK_URL);

const domRef = ref<HTMLDivElement>();

async function renderMap() {
  await load(true);
  if (!domRef.value) return;
  const map = new BMap.Map(domRef.value);
  const point = new BMap.Point(121.46917, 31.224361);
  map.centerAndZoom(point, 15);
  map.enableScrollWheelZoom();
}

onMounted(() => {
  renderMap();
});
</script>

<template>
  <div ref="domRef" class="h-full w-full"></div>
</template>

<style scoped></style>
