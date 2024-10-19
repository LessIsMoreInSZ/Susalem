<!--  -->
<script setup lang="ts">
import { ref } from 'vue';
// 导入 vue-stick 组件
// import VueStick from 'vue-stick';

const list = ref([
  {
    cover: 'https://gitee.com/Pridejoy/wallpaper.net/raw/master/doc/images/wx7560d334dd837f70.jpg',
    intro: '2018 年对于我来说是非...',
    title: '剧中人的2018年终总结'
  },
  {
    cover: 'https://image.meiye.art/pic_1700655652302',
    intro: '再次见到合肥，在历经五年的沪漂之后...',
    title: '再见 · 合肥'
  }
]);

// 用来修改状态、触发更新的函数
function loadMore() {
  list.value.push({
    cover: 'http://static.bh-lay.com/...yout.png',
    intro: '今天的分享主要围绕 可视化布局模块的一些...',
    title: 'added by scroll 可视化布局模块开发分享'
  });
}
</script>

<template>
  <div>
    123123
    <!--
 <VueStick :list="list" @on-scroll-end="loadMore">
      <template #default="scope">
        <div class="card">
          <img v-if="scope.data.cover" :src="scope.data.cover" />
          <h2>{{ scope.data.title }}</h2>
          <p>{{ scope.data.intro }}</p>
        </div>
      </template>
    </VueStick>
-->
  </div>
</template>

<style lang="scss" scoped>
.card_img {
  border-radius: 5px;
}
</style>
