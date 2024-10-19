<script setup lang="ts">
import { computed, ref, shallowRef, watch } from 'vue';
import { $t } from '@/locales';
import { getMenuTreeList, getRoleUserMenu, setRoleUserMenu } from '@/service/api';

defineOptions({
  name: 'MenuAuthModal'
});

interface Props {
  /** the roleId */
  roleId: number;
}

const props = defineProps<Props>();

const visible = defineModel<boolean>('visible', {
  default: false
});

function closeModal() {
  visible.value = false;
}

const title = computed(() => $t('common.edit') + $t('page.manage.role.menuAuth'));

const tree = shallowRef<Api.SystemManage.Menu[]>([]);

async function getTree() {
  await getMenuTreeList().then(res => {
    if (res.data?.records) {
      tree.value = res.data.records;
    }
  });
}

const checks = shallowRef<number[]>([]);

async function getChecks() {
  // request
  await getRoleUserMenu({ roleId: props.roleId }).then(res => {
    if (res.data) {
      checks.value = res.data;
    }
  });
}

function handleSubmit() {
  // request
  setRoleUserMenu({ roleId: props.roleId, menuId: checks.value }).then(res => {
    if (res.data) {
      window.$message?.success?.($t('common.modifySuccess'));

      closeModal();
    }
  });
}

// 父子联动
const cascadeactive = ref<boolean>(false);
const updateCascadeActive = (value: boolean) => {
  cascadeactive.value = value;
};
// 全部展开 是否
const expandallactive = ref<boolean>(false);
const expandallactivehandle = (value: boolean) => {
  expandallactive.value = value;
};
// 是否全选
const checkallactive = ref<boolean>(false);
const checkallactivehandle = (value: boolean) => {
  checkallactive.value = value;
  if (value) {
    checks.value = tree.value.map(getAllIds).flat();
  } else {
    checks.value = [];
  }
};

// 获取所有子节点
function getAllIds(node: any) {
  // 基础情况：如果节点是 null 或者 undefined，返回一个空数组
  if (!node) return [];

  // 将当前节点的 id 添加到结果数组中
  const ids = [node.id];

  // 如果节点有子节点，递归地调用 getAllIds 并扩展结果数组
  if (node.children && node.children.length > 0) {
    node.children.forEach(child => {
      ids.push(...getAllIds(child));
    });
  }

  // 返回包含当前节点 id 和所有子节点 id 的数组
  return ids;
}
function init() {
  getTree();
  // getPages();
  getChecks();
}

watch(visible, val => {
  if (val) {
    init();
  }
});
</script>

<template>
  <NModal v-model:show="visible" :title="title" preset="card" class="w-480px">
    <div class="flex-y-center gap-16px pb-12px">
      <div>展开/折叠</div>
      <!-- <NSelect :value="home" :options="pageSelectOptions" size="small" class="w-160px" @update:value="updateHome" /> -->
      <NSwitch v-model:value="expandallactive" @update:value="expandallactivehandle" />
      <div>全选/全不选</div>
      <NSwitch v-model:value="checkallactive" @update:value="checkallactivehandle" />
      <div>父子连动</div>
      <NSwitch v-model:value="cascadeactive" @update:value="updateCascadeActive" />
    </div>

    <NTree
      v-model:checked-keys="checks"
      :data="tree"
      key-field="id"
      label-field="menuName"
      :cascade="cascadeactive"
      :default-expand-all="expandallactive"
      checkable
      expand-on-click
      virtual-scroll
      block-line
      class="h-280px"
    />
    <template #footer>
      <NSpace justify="end">
        <NButton size="small" class="mt-16px" @click="closeModal">
          {{ $t('common.cancel') }}
        </NButton>
        <NButton type="primary" size="small" class="mt-16px" @click="handleSubmit">
          {{ $t('common.confirm') }}
        </NButton>
      </NSpace>
    </template>
  </NModal>
</template>

<style scoped></style>
