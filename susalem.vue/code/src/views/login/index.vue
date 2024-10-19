<script lang="ts" setup>
import { reactive, ref } from "vue"
import { useRouter } from "vue-router"
import { useUserStore } from "@/store/modules/user"
import { ElMessage, type FormInstance, type FormRules } from "element-plus"
import { User, Lock, Key, Picture, Loading } from "@element-plus/icons-vue"
import { type LoginRequestData } from "@/api/login/types/login"
import ThemeSwitch from "@/components/ThemeSwitch/index.vue"
import { Md5 } from "ts-md5"

const router = useRouter()

/** 登录表单元素的引用 */
const loginFormRef = ref<FormInstance | null>(null)

/** 登录按钮 Loading */
const loading = ref(false)

/** 登录表单数据 */
const loginFormData: LoginRequestData = reactive({
  employeeNumber: "",
  password: ""
})
/** 登录表单校验规则 */
const loginFormRules: FormRules = {
  employeeNumber: [{ required: true, message: "请输入用户名", trigger: "blur" }],
  password: [
    { required: true, message: "请输入密码", trigger: "blur" },
    { min: 6, max: 16, message: "长度在 6 到 16 个字符", trigger: "blur" }
  ]
}
/**双击图片快速登录 */
const fastLogin = () => {
  loading.value = true
  let fastLoginData = {
    employeeNumber: "admin",
    password: Md5.hashStr("123456").toString()
  }
  localStorage.setItem("employeeNumber", fastLoginData.employeeNumber)
  useUserStore()
    .login(fastLoginData)
    .then(() => {
      router.push({ path: "/" })
    })
    .catch(() => {
      console.log("error")
      loginFormData.password = ""
    })
    .finally(() => {
      loading.value = false
    })
}
/** 登录逻辑 */
const handleLogin = () => {
  loginFormRef.value?.validate((valid: boolean, fields) => {
    if (valid) {
      loading.value = true
      let submitLoginFormData = {
        userNameOrEmailAddress: loginFormData.employeeNumber,
        password: Md5.hashStr(loginFormData.password).toString()
      }
      // useUserStore().employeeNumber = submitLoginFormData.employeeNumber
      localStorage.setItem("userNameOrEmailAddress", submitLoginFormData.userNameOrEmailAddress)
      useUserStore()
        .login(submitLoginFormData)
        .then(() => {
          console.log("const route", router.getRoutes())
          router.push({ path: "/" })
        })
        .catch(() => {
          console.log("error")
          loginFormData.password = ""
        })
        .finally(() => {
          loading.value = false
        })
    } else {
      ElMessage.error("请填写正确信息")
      // console.error("表单校验不通过", fields)
    }
  })
}
</script>

<template>
  <div class="login-container">
    <!-- 主题组件 -->
    <!-- <ThemeSwitch class="theme-switch" /> -->
    <!-- 动画组件-->
    <!-- <Owl :close-eyes="isFocus" /> -->
    <div class="login-card">
      <div class="title">
        <img src="@/assets/layouts/jccLogo.jpg" @dblclick="fastLogin" />
      </div>
      <div class="content">
        <el-form ref="loginFormRef" :model="loginFormData" :rules="loginFormRules" @keyup.enter="handleLogin">
          <el-form-item prop="employeeNumber">
            <el-input
              v-model.trim="loginFormData.employeeNumber"
              placeholder="用户名"
              type="text"
              tabindex="1"
              :prefix-icon="User"
              size="large"
            />
          </el-form-item>
          <el-form-item prop="password">
            <el-input
              v-model.trim="loginFormData.password"
              placeholder="密码"
              type="password"
              tabindex="2"
              :prefix-icon="Lock"
              size="large"
              show-password
            />
          </el-form-item>
          <el-button :loading="loading" type="primary" size="large" @click.prevent="handleLogin">登 录</el-button>
        </el-form>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.login-container {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 100%;
  min-height: 100%;
  .theme-switch {
    position: fixed;
    top: 5%;
    right: 5%;
    cursor: pointer;
  }
  .login-card {
    width: 480px;
    max-width: 90%;
    border-radius: 20px;
    box-shadow: 0 0 10px #dcdfe6;
    background-color: #fff;
    overflow: hidden;
    .title {
      display: flex;
      justify-content: center;
      align-items: center;
      height: 150px;
      img {
        height: 100%;
      }
    }
    .content {
      padding: 20px 50px 50px 50px;
      :deep(.el-input-group__append) {
        padding: 0;
        overflow: hidden;
        .el-image {
          width: 100px;
          height: 40px;
          border-left: 0px;
          user-select: none;
          cursor: pointer;
          text-align: center;
        }
      }
      .el-button {
        width: 100%;
        margin-top: 10px;
      }
    }
  }
}
</style>
