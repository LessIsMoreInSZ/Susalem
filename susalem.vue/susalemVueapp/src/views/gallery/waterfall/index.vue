<script setup lang="ts">
import { onMounted, onUnmounted, ref } from 'vue';
import { NCard, NImage } from 'naive-ui';
import { getGalleryPagelist } from '@/service/api';

interface GalleryItem {
  imagesURL: string;
}

const imageList = ref<GalleryItem[]>([]);
const apiParams = ref({
  pageNo: 1,
  pageSize: 20
});
const contentRef = ref<HTMLElement | null>(null);

// 加载更多图片
async function addimg() {
  try {
    const res = await getGalleryPagelist(apiParams.value);
    if (Array.isArray(res.data?.records) && res.data?.records.length > 0) {
      imageList.value = imageList.value.concat(res.data?.records);
      apiParams.value.pageNo += 1;
    }
  } catch (error) {
    console.error('Error fetching more images:', error);
  }
}

// 使用 IntersectionObserver 监听底部
const observer = new IntersectionObserver(
  entries => {
    entries.forEach(entry => {
      if (entry.isIntersecting) {
        addimg();
      }
    });
  },
  {
    root: null, // 视口作为参照
    rootMargin: '0px',
    threshold: 1.0 // 100% 可见时触发
  }
);

onMounted(() => {
  const target = document.createElement('div');
  target.classList.add('observer-target');
  contentRef.value?.appendChild(target);

  observer.observe(target);
  addimg(); // 初始加载
});

onUnmounted(() => {
  observer.disconnect();
  if (contentRef.value) {
    const target = contentRef.value.querySelector('(observer-target)');
    if (target) target.remove();
  }
});
</script>

<template>
  <div id="content" ref="contentRef">
    <div class="masonry-grid">
      <div v-for="(item, index) in imageList" :key="index" class="masonry-grid-item">
        <NCard>
          <NImage :src="item.imagesURL" alt="" height="auto" width="100%" />
        </NCard>
      </div>
      <div v-if="apiParams.pageNo > 1" class="observer-target"></div>
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
