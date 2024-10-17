<template>
  <el-dialog
    :modelValue="exportExcelDialogVisible"
    class="currentDialog"
    draggable
    :before-close="handleExcelClose"
    style="width: 27%"
  >
    <template #header>
      <div class="dialog-header">
        <span>导出Excel</span>
        <el-divider direction="vertical" />
      </div>
    </template>
    <!-- form的prop和v-model双向绑定的要一致，否则会回显数据-->
    <div>
      <el-form
        class="dialog-form"
        :model="exportExcelDialogForm"
        ref="exportExcelDialogFormRef"
        label-width="75px"
        label-position="top"
        :rules="exportExcelDialogFormRules"
      >
        <el-row>
          <el-col :span="24">
            <el-form-item label="SN:" prop="sn">
              <el-input v-model="exportExcelDialogForm.sn" placeholder="请输入SN"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="时间范围:" prop="time">
              <el-date-picker
                v-model="exportExcelDialogForm.rangeTime"
                type="datetimerange"
                start-placeholder="开始时间"
                range-separator="To"
                end-placeholder="结束时间"
                value-format="YYYY-MM-DD HH:mm:ss"
              />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
    </div>
    <template #footer>
      <div class="dialog-foot">
        <el-button @click="handleExportCancel">取消</el-button>
        <el-button type="primary" @click="onExportSubmit">确定</el-button>
      </div>
    </template>
  </el-dialog>
</template>
<script lang="ts" setup>
import { ref, reactive } from "vue"
import { ElMessage, FormInstance } from "element-plus"
const props = defineProps({
  exportExcelDialogVisible: {
    type: Boolean
  }
})

const emit = defineEmits(["handleExcelClose"])
const exportExcelDialogForm = reactive({
  sn: "",
  rangeTime: ""
})

const exportExcelDialogFormRef = ref()
const exportExcelDialogFormRules = reactive({
  sn: [
    {
      required: true,
      message: "请输入SN查询",
      trigger: "blur"
    }
  ]
})

const handleExcelClose = () => {
  refreshForm()
  emit("handleExcelClose")
}

const handleExportCancel = () => {
  refreshForm()
  emit("handleExcelClose")
}
const onExportSubmit = () => {
  exportExcelDialogFormRef.value.validate((vaild: FormInstance | undefined) => {
    if (vaild) {
      console.log("导出Excel", exportExcelDialogForm)
      emit("handleExcelClose")
    } else {
      ElMessage.error("请填写完整")
    }
  })
}

const refreshForm = () => {
  exportExcelDialogFormRef.value.resetFields()
  Object.assign(exportExcelDialogForm, {
    sn: "",
    rangeTime: ""
  })
}
</script>
<style scoped>
.dialog-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  font-size: 17px;
  font-weight: 500;
  font-family: "PingFang SC";
}
</style>
