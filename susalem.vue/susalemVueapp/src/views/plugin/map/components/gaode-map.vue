<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { useScriptTag } from '@vueuse/core';
import { AMAP_SDK_URL } from '@/constants/map-sdk';

defineOptions({ name: 'GaodeMap' });

const { load } = useScriptTag(AMAP_SDK_URL);

const domRef = ref<HTMLDivElement>();

async function renderMap() {
  await load(true);
  if (!domRef.value) return;
  const map = new AMap.Map(domRef.value, {
    zoom: 11,
    center: [121.46917, 31.224361],
    viewMode: '3D'
  });
  map.getCenter();
}

onMounted(() => {
  renderMap();
});
</script>

<template>
  <div ref="domRef" class="h-full w-full"></div>
</template>

<style scoped></style>
