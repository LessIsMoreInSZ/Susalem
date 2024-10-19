<script setup lang="tsx">
import { computed, reactive, watch } from 'vue';
import { useNaiveForm } from '@/hooks/common/form';
import { addConfig, upConfig } from '@/service/api';
import type { SysConfigRes } from '@/typings/res/sysconfig';
defineOptions({
  name: 'ConfigOperateDrawer'
});

interface Props {
  /** the type of operation */
  operateType: NaiveUI.TableOperateType;
  /** the edit row data */
  rowData?: SysConfigRes | null;
}

const props = defineProps<Props>();

interface Emits {
  (e: 'submitted'): void;
}

const emit = defineEmits<Emits>();

const visible = defineModel<boolean>('visible', {
  default: false
});

const { formRef, validate, restoreValidation } = useNaiveForm();

const title = computed(() => {
  const titles: Record<NaiveUI.TableOperateType, string> = {
    add: '新增配置',
    edit: '编辑配置'
  };
  return titles[props.operateType];
});

type Model = Pick<SysConfigRes | any, 'configID' | 'configKey' | 'configType' | 'configValue' | 'configdDescribe'>;

const model: Model = reactive(createDefaultModel());

function createDefaultModel(): Model {
  return {
    configID: 0,
    configKey: '',
    configType: '',
    configValue: '',
    configdDescribe: ''
  };
}

function handleUpdateModelWhenEdit() {
  if (props.operateType === 'add') {
    Object.assign(model, createDefaultModel());
    return;
  }

  if (props.operateType === 'edit' && props.rowData) {
    Object.assign(model, props.rowData);
  }
  // console.log('当前接收的数据', model, props);
}

function closeDrawer() {
  visible.value = false;
}

async function handleSubmit() {
  await validate();
  // request
  if (props.operateType === 'add') {
    // console.log('提交表单', model);
    addConfig(model).then(res => {
      // console.log('请求成功', res);
      if (res.data) {
        window.$message?.success('添加成功');
        closeDrawer();
        emit('submitted');
      }
    });
  }

  if (props.operateType === 'edit' && props.rowData) {
    upConfig(model).then(res => {
      // console.log('修改返回的状态', res);
      if (res.data) {
        window.$message?.success('修改成功');
        closeDrawer();
        emit('submitted');
      } else {
        window.$message?.error('修改失败');
      }
    });
  }
  // request
  console.log('提交的model', model);
}

watch(visible, () => {
  if (visible.value) {
    handleUpdateModelWhenEdit();
    restoreValidation();
  }
});
</script>

<template>
  <NDrawer v-model:show="visible" :title="title" display-directive="show" :width="360">
    <NDrawerContent :title="title" :native-scrollbar="false" closable>
      <NForm ref="formRef" :model="model">
        <NFormItem label="配置名称" path="configKey">
          <NInput v-model:value="model.configKey" :disabled="model.configID > 0" placeholder="请输入配置名称" />
        </NFormItem>
        <NFormItem label="配置类型" path="configValue">
          <NInput v-model:value="model.configType" placeholder="请输入配置参数" />
        </NFormItem>
        <NFormItem label="配置参数" path="configValue">
          <NInput v-model:value="model.configValue" placeholder="请输入配置参数" />
        </NFormItem>
        <NFormItem label="配置描述" path="configdDescribe">
          <NInput v-model:value="model.configdDescribe" type="textarea" placeholder="请输入配置描述" />
        </NFormItem>
      </NForm>
      <template #footer>
        <NSpace :size="16">
          <NButton @click="closeDrawer">{{ $t('common.cancel') }}</NButton>
          <NButton type="primary" @click="handleSubmit">{{ $t('common.confirm') }}</NButton>
        </NSpace>
      </template>
    </NDrawerContent>
  </NDrawer>
</template>

<style scoped></style>
