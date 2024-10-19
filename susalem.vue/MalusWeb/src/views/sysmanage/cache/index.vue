<!--  -->
<script setup lang="tsx">
import { ref } from 'vue';
import type { TreeOption, TreeOverrideNodeClickBehavior } from 'naive-ui';
import VueJsonPretty from 'vue-json-pretty';
import 'vue-json-pretty/lib/styles.css';
import { getAllKeys, getKeys } from '@/service/api';

// const loading = ref(false);
const data = ref();
const info = ref();
const showstr = ref();

getAllCacheData();

function getAllCacheData() {
  getAllKeys().then(res => {
    data.value = buildTree(res.data);
    // console.log('data', data.value);
  });
}

const override: TreeOverrideNodeClickBehavior = ({ option }) => {
  if (option.children) {
    // console.log('toggleExpand');
    return 'toggleExpand';
  }
  showstr.value = option.value;
  getKeys(option.value).then(res => {
    info.value = res.data;
  });
  return 'default';
};
function buildTree(keyList: Array<string>): TreeOption[] {
  const cacheData: TreeOption[] = [];

  // 遍历数组，构建树
  // eslint-disable-next-line no-plusplus
  for (let i = 0; i < keyList.length; i++) {
    const keyNames = keyList[i].split(':');
    const pName = keyNames[0];
    const childName = keyNames[1] || '';

    // 查找或创建父节点
    let parentNode = cacheData.find((x: TreeOption) => x.label === pName);
    if (!parentNode) {
      parentNode = {
        label: pName,
        value: pName,
        children: []
      };
      cacheData.push(parentNode);
    }

    // 如果存在子节点名称，创建子节点
    if (childName) {
      const childNode: TreeOption = {
        label: childName,
        value: keyList[i],
        children: []
      };
      parentNode.children.push(childNode);
    }
  }

  // 移除没有子节点的children属性;
  const cleanTree = (nodes: TreeOption[]): TreeOption[] => {
    return nodes.map(node => ({
      ...node,
      children: node.children.length ? cleanTree(node.children) : undefined
    }));
  };

  return cleanTree(cacheData);
}
</script>

<template>
  <div class="min-h-500px flex-col-stretch gap-16px overflow-hidden lt-sm:overflow-auto">
    <NRow gutter="24">
      <NCol :span="7">
        <NCard
          title="缓存列表"
          :bordered="false"
          size="small"
          class="sm:flex-1-hidden card-wrapper"
          :segmented="{
            content: true,
            footer: 'soft'
          }"
        >
          <template #header-extra>
            <div class="button-container">
              <NButton size="small" @click="getAllCacheData">
                <template #icon>
                  <icon-mdi-refresh class="text-icon" />
                </template>
                刷新
              </NButton>

              <NButton size="small" ghost type="error">
                <template #icon>
                  <icon-ic-round-delete class="text-icon" />
                </template>
                删除
              </NButton>
            </div>
          </template>

          <NTree
            block-line
            :data="data"
            key-field="label"
            label-field="value"
            :override-default-node-click-behavior="override"
            children-field="children"
            selectable
          />
        </NCard>
      </NCol>
      <NCol :span="17">
        <NCard
          :title="`缓存数据:[${showstr}]`"
          :bordered="false"
          size="small"
          class="sm:flex-1-hidden card-wrapper"
          :segmented="{
            content: true,
            footer: 'soft'
          }"
        >
          <div>
            <VueJsonPretty
              show-length
              show-icon
              virtual
              show-line-number
              show-select-controller
              style="padding-bottom: 60px"
              :data="info"
            ></VueJsonPretty>
          </div>
        </NCard>
      </NCol>
    </NRow>
  </div>
</template>

<style lang="scss" scoped>
.button-container {
  display: flex;
  gap: 8px; /* 或者您想要的任何间距 */
}
</style>
