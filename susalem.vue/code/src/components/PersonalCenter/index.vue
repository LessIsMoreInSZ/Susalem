<template>
  <!-- 个人信息页抽屉 -->
  <el-drawer :modelValue="drawerOpen" :direction="direction" size="25%" :before-close="handleClose">
    <template #header>
      <h2 class="header-title">个人中心</h2>
    </template>
    <template #default>
      <el-form :inline="false" :model="personInfo" :rules="rules" ref="personForm">
        <el-form-item style="margin-bottom: 10px" label="请上传头像："></el-form-item>
        <!-- @change="handleChange" -->
        <el-upload
          class="avatar-uploader"
          :show-file-list="false"
          :on-success="handleAvatarSuccess"
          :before-upload="beforeAvatarUpload"
          :http-request="uploadImage"
        >
          <img v-if="imageUrl" :src="imageUrl" class="avatar" @error="handleImageError" />
          <el-icon v-else class="avatar-uploader-icon"><Plus /></el-icon>
        </el-upload>
        <el-form-item label="工&nbsp&nbsp&nbsp号:" prop="rule" style="margin-top: 13px; margin-left: 9px" class="efi">
          <el-input v-model="personInfo.employeeNumber" placeholder="工号" disabled />
        </el-form-item>
        <el-form-item label="权&nbsp&nbsp&nbsp限:" prop="rule" style="margin-top: 13px; margin-left: 9px" class="efi">
          <el-input v-model="personInfo.roleName" placeholder="等级" disabled />
        </el-form-item>

        <el-form-item label="用户名:" prop="userName" class="efi">
          <el-input class="ei_1" v-model="personInfo.userName" placeholder="用户名" clearable />
        </el-form-item>
        <el-form-item label="电&nbsp&nbsp&nbsp话:" prop="phoneNumber" class="efi">
          <el-input v-model="personInfo.phoneNumber" placeholder="电话" clearable />
        </el-form-item>
        <el-form-item label="旧密码:" prop="oldPassword" class="efi" style="margin-left: 9px">
          <el-input v-model="personInfo.oldPassword" placeholder="请输入旧密码" clearable show-password />
        </el-form-item>
        <el-form-item label="新密码:" prop="newPassword" class="efi" style="margin-left: 9px">
          <el-input v-model="personInfo.newPassword" placeholder="请输入想要修改的密码" clearable show-password />
        </el-form-item>
      </el-form>
      <div class="personal-footer">
        <el-button @click="cancelClick">取消</el-button>
        <el-button type="primary" @click="confirmClick">提交</el-button>
      </div>
    </template>
    <!-- <template #footer>
      <div>
        <el-button @click="cancelClick">取消</el-button>
        <el-button type="primary" @click="confirmClick">提交</el-button>
      </div>
    </template> -->
  </el-drawer>
</template>
<script lang="ts" setup>
import { ref, reactive, onMounted } from "vue"
import { ElMessage } from "element-plus"
import { uploadImageApi, getPersonInfoApi, updatePersonanlInfoApi } from "@/api/system/sys-user"
import type { FormInstance } from "element-plus"
import { Md5 } from "ts-md5"
import { useUserStore } from "@/store/modules/user"
import { useRouter } from "vue-router"
const router = useRouter()
const baseApi = import.meta.env.VITE_BASE_API
const props = defineProps({
  drawerOpen: Boolean
})

const emit = defineEmits(["drawerClose", "updateImageUrl"])
/**	Drawer 打开的方向 */
const direction: any = ref("rtl")
// 图片展示路径
const imageUrl = ref()
// 个人信息绑定
let personInfo = ref({
  id: "",
  roleName: "",
  userName: "",
  oldPassword: "",
  newPassword: "",
  phoneNumber: "",
  employeeNumber: ""
})

// form表单ref绑定
const personForm = ref()

// 表单校验
const rules = reactive({
  userName: [
    {
      required: true,
      message: "请输入用户名",
      trigger: "blur"
    }
  ],
  oldPassword: [
    {
      min: 6,
      max: 20,
      message: "旧密码长度应在6-20之间",
      trigger: "blur"
    }
  ],
  newPassword: [
    {
      min: 6,
      max: 20,
      message: "密码长度在6-20之间",
      trigger: "blur"
    },
    {
      pattern: /^(?=.*[a-zA-Z])(?=.*[0-9]).*$/,
      message: "密码必须包含字母和数字",
      trigger: "blur"
    },
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          personInfo.value.oldPassword != undefined &&
          personInfo.value.oldPassword != "" &&
          personInfo.value.oldPassword != null &&
          value != undefined &&
          value != "" &&
          value != null
        ) {
          if (value === personInfo.value.oldPassword) {
            callback(new Error("新密码不能与旧密码相同"))
          } else {
            callback()
          }
        } else {
          callback()
        }
      },
      trigger: "blur"
    },
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          (personInfo.value.oldPassword === undefined || personInfo.value.oldPassword === "") &&
          value != undefined &&
          value != "" &&
          value != null
        ) {
          callback(new Error("请输入旧密码"))
        } else {
          callback()
        }
      },
      trigger: "blur"
    }
  ],
  phoneNumber: [
    {
      required: true,
      message: "请输入手机号",
      trigger: "blur"
    },
    { required: true, pattern: /^(?:(?:\+|00)86)?1\d{10}$/, message: "手机号格式不正确", trigger: "blur" }
  ]
})
let employeeNumber = localStorage.getItem("employeeNumber") as string
// 图片上传限制
const beforeAvatarUpload = (file: any) => {
  const types = ["image/jpeg", "image/png"]
  const isType = types.includes(file.type)
  const isLt2M = file.size / 1024 / 1024 < 2
  if (!isType) {
    ElMessage.error("请上传 JPG或PNG 图片格式!")
  }
  if (!isLt2M) {
    ElMessage.error("上传图片大小不能超过 2MB!")
  }
  return isType && isLt2M
}
// 图片上传请求
const uploadImage = async (options: any) => {
  console.log(options)
  let formData = new FormData()
  formData.append("file", options.file)
  formData.append("employeeNumber", employeeNumber)
  const res: any = await uploadImageApi(formData)
  if (res && res.data.path.length > 0) {
    imageUrl.value = baseApi + "/" + res.data.path
    emit("updateImageUrl", imageUrl.value)
    console.log(imageUrl.value)
  }
}
// 图片资源丢失
const handleImageError = () => {
  imageUrl.value = ""
  // ElMessage.warning("图片资源丢失,请重新上传头像试试")
}
const handleAvatarSuccess = (res: any) => {
  // console.log(res)
}
//   点击x号关闭个人信息页
const handleClose = () => {
  emit("drawerClose")
  personForm.value.resetFields()
}

// 加载获取用户信息
onMounted(() => {
  getPersonalInfo()
})

const getPersonalInfo = () => {
  getPersonInfoApi({ employeeNumber: employeeNumber }).then((element: any) => {
    element.data.password = ""
    personInfo.value = element.data
    // 如果返回图片路径在拼接路径
    if (element.data.headPortrait !== null) {
      imageUrl.value = baseApi + "/" + element.data.headPortrait
      // imageUrl.value = element.data.headPortrait
      emit("updateImageUrl", imageUrl.value)
    }
    if (useUserStore().username !== element.data.userName) {
      useUserStore().username = element.data.userName
    }
  })
}
// 取消关闭
const cancelClick = () => {
  emit("drawerClose")
  personForm.value.resetFields()
}
// 提交表单
const confirmClick = () => {
  personForm.value.validate(async (valid: boolean) => {
    if (valid) {
      let oldPersonPassword = null
      let newPersonPassword = validatePassword(personInfo.value.oldPassword, personInfo.value.newPassword)
      if (newPersonPassword !== null) {
        oldPersonPassword = Md5.hashStr(personInfo.value.oldPassword).toString()
      }
      const { id, roleName, userName, newPassword, phoneNumber } = personInfo.value
      let personalInfoData = {
        id,
        roleName,
        userName,
        newPassword: newPersonPassword,
        oldPassword: oldPersonPassword,
        phoneNumber
      }
      let res: any = await updatePersonanlInfoApi(personalInfoData)
      if (res) {
        ElMessage.success(res.message)
        personForm.value.resetFields()
        emit("drawerClose")
        getPersonalInfo()
      }
    }
  })
}
// 判断用户是否更改密码
const validatePassword = (oldPassword: string, newPassword: string) => {
  console.log("%c [ oldPassword ]-212", "font-size:13px; background:pink; color:#bf2c9f;", oldPassword)
  if (newPassword && newPassword !== "") {
    if (oldPassword === "" || oldPassword === null || oldPassword === undefined) {
      ElMessage.error("请输入旧密码进行修改")
      return null
    }
    return Md5.hashStr(newPassword).toString()
  } else {
    return null
  }
}
</script>
<style lang="scss" scoped>
:deep(.avatar-uploader) {
  margin-left: 65px;
}
.avatar-uploader .avatar {
  width: 180px;
  height: 180px;
  display: block;
}
.personal-footer {
  display: flex;
  justify-content: end;
  margin-top: 30px;
}
.header-title {
  font-weight: bold;
  margin-bottom: 0px;
}
</style>
<style>
/* :deep(.el-drawer__header) {
  margin-bottom: 15px !important;
} */
.avatar-uploader .el-upload {
  border: 1px dashed var(--el-border-color);
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
  transition: var(--el-transition-duration-fast);
}

.avatar-uploader .el-upload:hover {
  border-color: var(--el-color-primary);
}

.el-icon.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  text-align: center;
}
</style>
