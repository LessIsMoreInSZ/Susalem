<template>
  <el-dialog
    :modelValue="dialogVisible"
    :title="action === 'add' ? '新增角色' : '编辑角色'"
    width="30%"
    draggable
    :before-close="handleClose"
  >
    <el-form
      :model="roleFormData"
      label-width="80px"
      label-position="right"
      class="form"
      :rules="rules"
      ref="roleFormRef"
    >
      <el-form-item label="角色名称" prop="roleName">
        <el-input v-model="roleFormData.roleName" />
      </el-form-item>
      <!-- <el-form-item label="权限字符" prop="description">
        <el-input v-model="roleFormData.description" />
      </el-form-item> -->
      <el-form-item label="显示顺序" prop="roleSort">
        <el-input-number v-model="roleFormData.roleSort" :min="0" :max="10" controls-position="right" />
      </el-form-item>
      <el-form-item label="启用" prop="enable">
        <el-switch v-model="roleFormData.enable" />
      </el-form-item>
    </el-form>
    <div slot="footer" class="btn-footer">
      <el-button @click="handleCancel()">取消</el-button>
      <el-button type="primary" @click="onSubmit()">确定</el-button>
    </div>
  </el-dialog>
</template>

<script lang="ts" setup>
import { ref, reactive, getCurrentInstance, watch } from "vue"
import { ElMessageBox, ElMessage } from "element-plus"
import type { FormInstance, FormRules } from "element-plus"
import { addRoleApi, updateRoleDataApi } from "@/api/system/sys-role"
/**父组件值 */
const props = defineProps({
  dialogVisible: Boolean,
  action: String,
  rowInfo: Object
})
/**父组件方法 */
const emits = defineEmits(["CloseDialog", "resetGetRoleData"])
// 表单ref
const roleFormRef = ref()
// 表单数据绑定
const roleFormData = ref({
  id: "",
  roleName: "",
  roleSort: "" as any,
  enable: false
})
/** 表单校验*/
const rules = ref({
  roleName: [{ required: true, message: "请输入角色名称", trigger: "blur" }],
  roleSort: [{ required: true, message: "请输入显示顺序", trigger: "blur" }],
  enable: [{ required: true, message: "请选择是否启用", trigger: "blur" }]
})
/**弹框关闭按钮X掉触发*/
const handleClose = (done: any) => {
  ElMessageBox.confirm("确定关闭吗?", {
    confirmButtonText: "确定",
    cancelButtonText: "取消"
  })
    .then(() => {
      roleFormRef.value.resetFields()
      //   关闭对话框
      emits("CloseDialog")
      done()
    })
    .catch(() => {
      // catch error
    })
}
/**监听父组件传来的表格行数据 */
watch(
  () => props.rowInfo,
  (newValue) => {
    console.log("%c [ newValue ]-74", "font-size:13px; background:pink; color:#bf2c9f;", newValue)
    if (newValue != undefined || newValue != null) {
      roleFormData.value = {
        id: newValue.id,
        roleName: newValue.roleName,
        roleSort: newValue.roleSort,
        enable: newValue.enable
      }
    } else {
      roleFormData.value = {
        id: "",
        roleName: "",
        roleSort: "" as any,
        enable: false
      }
    }
  }
)
/**提交表单内容 */
const onSubmit = () => {
  // 表单内容验证，表单的ref属性拿到表单，使用el-api校验
  roleFormRef.value.validate(async (valid: FormInstance | undefined) => {
    if (valid) {
      const currentUser = localStorage.getItem("employeeNumber")
      if (props.action === "add") {
        console.log("jinruadd")
        let formAdd = reactive({
          roleName: roleFormData.value.roleName,
          roleSort: roleFormData.value.roleSort,
          enable: roleFormData.value.enable === true ? 1 : 0,
          createBy: currentUser
        })
        // await只会返回给他最近的async回调函数！！！否则报错
        const res: any = await addRoleApi(formAdd)
        // 如果成功，则清空form表单，并关闭弹框
        if (res) {
          //拿到form表单的ref，使用form自带的清空表单api，ep提供
          //需要注意的是：使用validate、resetFields，需要在表单内容上加prop绑定表单相应属性才会生效
          roleFormRef.value.resetFields()
          //   关闭对话框
          emits("CloseDialog")
          ElMessage({
            showClose: true,
            message: res.message,
            type: "success"
          })
          // 重新获取数据
          emits("resetGetRoleData")
        }
        // 编辑用户信息
      } else if (props.action === "edit") {
        let formEdit = reactive({
          id: roleFormData.value.id,
          roleName: roleFormData.value.roleName,
          roleSort: roleFormData.value.roleSort,
          enable: roleFormData.value.enable === true ? 1 : 0,
          updateBy: currentUser
        })
        const res: any = await updateRoleDataApi(formEdit)
        if (res) {
          roleFormRef.value.resetFields()
          //   关闭对话框
          emits("CloseDialog")
          ElMessage({
            showClose: true,
            message: res.message,
            type: "success"
          })
          // 重新获取数据
          emits("resetGetRoleData")
        }
      }
    } else {
      ElMessage.error("请填写完整信息")
    }
  })
}
// 表单取消时也需要重置表单内容
const handleCancel = () => {
  roleFormRef.value.resetFields()
  resetFormData()
  console.log("from", roleFormData.value)
  emits("CloseDialog")
}
// 清空表单数据，防止表单缓存与点击新增时回显数据
const resetFormData = () => {
  roleFormData.value = {
    id: "",
    roleName: "",
    roleSort: "0" as any,
    enable: false
  }
}
</script>

<style lang="scss" scoped>
// .form {
//   min-height: 250px;
// }
.btn-footer {
  display: flex;
  justify-content: end;
}
</style>
