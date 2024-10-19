<script setup lang="ts">
import { ref } from 'vue';
import { NCard } from 'naive-ui';

const imageList = ref([
  { src: 'https://image.meiye.art/pic_1700634840507' },
  { src: 'https://image.meiye.art/pic_1629358896940pFVTf3STAomHWstneQnay' },
  { src: 'https://image.meiye.art/pic_W4bSnMDhxm4M3ymuZX0ht' },
  { src: 'https://image.meiye.art/pic_Thf5O8Zv_aY2yQ77posrd' }
  // ...更多图片
]);

function addimg() {
  for (let index = 0; index < 100; index++) {
    imageList.value.push({ src: 'https://image.meiye.art/pic_W4bSnMDhxm4M3ymuZX0ht' });
  }
}

addimg();
</script>

<template>
  <div id="content">
    <div class="masonry-grid">
      <div v-for="(item, index) in imageList" :key="index" class="masonry-grid-item">
        <NCard>
          <template #header>
            {{ index }}
          </template>
          <NImage :src="item.src" alt="" height="auto" width="100%" />
        </NCard>
      </div>
    </div>
  </div>
</template>

<style scoped>
.masonry-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  gap: 16px;
  width: 100%;
}

.masonry-grid-item {
  break-inside: avoid;
}

img {
  width: 100%;
  height: auto;
}
</style>
