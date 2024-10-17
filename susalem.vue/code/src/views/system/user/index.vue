<template>
  <div class="app-container">
    <el-card class="searchTop" shadow="never">
      <div class="searForm">
        <el-form :inline="true" :model="searchFormInline" class="ef_1">
          <el-form-item label="工号:">
            <el-input
              v-model="searchFormInline.employeeNumber"
              placeholder="请输入工号检索"
              clearable
              prefix-icon="Search"
              class="ei_1"
              style="width: 190px"
            />
          </el-form-item>
          <el-form-item label="用户名:">
            <el-input
              v-model="searchFormInline.userName"
              placeholder="请输入用户名检索"
              clearable
              prefix-icon="Search"
              class="ei_1"
              style="width: 190px"
            />
          </el-form-item>
          <el-form-item label="用户名:">
            <!-- <el-input
                v-model="searchFormInline.enable"
                placeholder="请输入用户名检索"
                clearable
                prefix-icon="Search"
                class="ei_1"
                style="width: 190px"
              /> -->
            <el-select v-model="searchFormInline.enable" placeholder="请选择用户状态" clearable style="width: 170px">
              <el-option label="启用" value="true"></el-option>
              <el-option label="禁用" value="false"></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" :icon="Search" @click="handleSearch">搜索</el-button>
            <el-button :icon="Refresh" @click="resetSearch">重置</el-button>
          </el-form-item>
        </el-form>
      </div>
    </el-card>
    <el-card shadow="never">
      <div class="tableTop">
        <div class="buttonTop">
          <el-button type="primary" @click="handleAdd">+新增</el-button>
          <el-button type="danger" @click="handleDelList" style="padding: 5px">批量删除</el-button>
        </div>
      </div>
      <!-- 表格数据 -->
      <div class="table" ref="tableContanierRef">
        <el-table
          :data="userTableData"
          :fit="true"
          @selection-change="handleSelectionChange"
          :height="tableHeight"
          border
        >
          <el-table-column type="selection" width="40" />
          <el-table-column type="index" label="排序" align="center" width="100"></el-table-column>
          <el-table-column prop="employeeNumber" label="工号" align="center" width="120"></el-table-column>
          <el-table-column
            v-for="item in userTableLabel"
            align="center"
            :key="item.prop"
            :prop="item.prop"
            :label="item.label"
            show-overflow-tooltip
          />
          <el-table-column label="启用" align="center" width="90" prop="isEnable">
            <template #default="{ row }">
              <el-switch
                v-model="row.enable"
                inline-prompt
                inactive-color="#f56c6c"
                active-text="是"
                inactive-text="否"
                @change="changeUserStatus(row)"
              />
            </template>
          </el-table-column>
          <el-table-column fixed="right" align="center" label="操作" width="250">
            <!--Vue组件中定义一个插槽，row为当前行的数据，从父组件中拿到的，可以打印{{row}}查看 -->
            <!--如果#default="scope" scope为作用域对象，你可以拿到这个vue模版中的所有的对象和方法 -->
            <template #default="{ row }">
              <!-- {{ scope.row }} -->
              <el-button link type="primary" icon="Edit" @click="handleEdit(row)">编辑</el-button>
              <el-button link type="danger" icon="Delete" @click="handleUserRemove(row)">删除</el-button>
              <el-button link type="primary" icon="Refresh" @click="resetPassword(row)">重置密码</el-button>
            </template>
          </el-table-column>
        </el-table>
      </div>
      <div class="pager-foot">
        <!-- 分页 -->
        <el-pagination
          background
          :page-sizes="[30, 50, 100, 200]"
          layout="total, sizes, prev, pager, next, jumper"
          :total="userConfig.total"
          class="pager mt-5"
          :default-page-size="defaultPageSize"
          :current-page="userConfig.page"
          @size-change="handleSizeChange"
          @current-change="handleUserCurrentChange"
          v-if="userConfig.total != 0"
        />
      </div>
    </el-card>
  </div>
  <!--新增和编辑用户 dialogVisible：隐藏显示对话框视图 -->
  <el-dialog
    v-model="dialogVisible"
    :title="getAction.action === 'add' ? '新增用户' : '编辑用户'"
    :before-close="handleClose"
    class="dialog-form"
    draggable
    style="width: 40%"
  >
    <!-- form的prop和v-model双向绑定的要一致，否则会回显数据-->
    <el-form :model="formUserd" ref="userDialogForm" label-width="70px" label-position="right" :rules="rules">
      <el-row>
        <el-col :lg="12">
          <el-form-item label="用户名:" prop="userName">
            <!-- spellcheck关闭语法检查 -->
            <el-input v-model="formUserd.userName" spellcheck="false" placeholder="请输入用户名" clearable />
          </el-form-item>
        </el-col>
        <el-col :lg="12">
          <el-form-item v-if="getAction.action === 'add'" label="密码:" prop="password">
            <el-input v-model="formUserd.password" placeholder="请输入用户密码" clearable show-password />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :lg="12">
          <el-form-item label="工号:" prop="employeeNumber">
            <!-- spellcheck关闭语法检查 -->
            <el-input v-model="formUserd.employeeNumber" spellcheck="false" placeholder="请输入工号" clearable />
          </el-form-item>
        </el-col>
        <el-col :lg="12">
          <el-form-item label="角色:" prop="roleIds">
            <el-select v-model="formUserd.roleIds" style="width: 100%" placeholder="请选择">
              <el-option v-for="item in roleList" :label="item.roleName" :value="item.id" :key="item.id"></el-option>
            </el-select>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :lg="12">
          <el-form-item label="手机号:" prop="phoneNumber">
            <!-- spellcheck关闭语法检查 -->
            <el-input v-model="formUserd.phoneNumber" spellcheck="false" placeholder="请输入手机号" clearable />
          </el-form-item>
        </el-col>
        <el-col :lg="12">
          <el-form-item label="状态:" prop="enable">
            <el-select v-model="formUserd.enable" placeholder="请选择" clearable>
              <el-option label="正常" value="1" />
              <el-option label="禁用" value="0" />
            </el-select>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row class="dialog-foot">
        <el-form-item>
          <el-button @click="handleCancel">取消</el-button>
          <el-button type="primary" @click="onSubmit">确定</el-button>
        </el-form-item>
      </el-row>
    </el-form>
  </el-dialog>
</template>

<script setup lang="ts" name="user">
import { Md5 } from "ts-md5"
import { ref, reactive, onMounted, nextTick, onActivated, onBeforeUnmount } from "vue"
import { Search, Refresh } from "@element-plus/icons-vue"
import { ElMessage, ElMessageBox, FormInstance } from "element-plus"
import {
  getUserDataApi,
  addUserApi,
  updateUserDataApi,
  deleteUserApi,
  batchDelUserApi,
  changeStatusOrPasswordApi
} from "@/api/system/sys-user"
import { getRoleListApi, getRoleByIdApi } from "@/api/system/sys-role"
import { getPageSize, setPageSize } from "@/utils/cache/local-storage"
// #region 定义属性值
/**表格容器引用 */
const tableContanierRef = ref()
/**表格自适应高度 */
const tableHeight = ref()
/**默认页面的条数 */
const defaultPageSize = ref(10)
interface User {
  total: number
  page: number
  size: number
  userName: string
  employeeNumber: string
  enable: any
}
/**用户表格数据 */
const userTableData = ref()
/**对话框触发 */
const dialogVisible = ref<boolean>(false)
/**对话框title展示-复用 */
const getAction = reactive({
  action: ""
})
/**用户表格label */
const userTableLabel: any = reactive([
  {
    prop: "userName",
    label: "用户名"
  },
  {
    prop: "phoneNumber",
    label: "手机号"
  },
  {
    prop: "createTime",
    label: "创建时间"
  },
  {
    prop: "createBy",
    label: "创建人"
  },
  {
    prop: "updateBy",
    label: "修改人"
  }
])
/**用户分页查询配置 */
const userConfig = reactive<User>({
  total: 0,
  page: 1,
  size: 10,
  userName: "",
  employeeNumber: "",
  enable: ""
})
/**绑定搜索关键字 */
const searchFormInline = reactive({
  employeeNumber: "",
  userName: "",
  enable: ""
})
// #endregion

// #region 其他操作-生命周期、公共函数
/**封装获取pagesize */
const getCurrentPageSize = () => {
  return getPageSize("user-PageSize")
}
/**限制生命周期多次加载同一请求 */
const isMounted = ref(false)
onMounted(() => {
  isMounted.value = true
  const currentPageSize = getCurrentPageSize()
  if (currentPageSize) {
    defaultPageSize.value = currentPageSize
    userConfig.size = defaultPageSize.value
  }
  calculateTableHeight()
  window.addEventListener("resize", calculateTableHeight)
  getUserData(userConfig)
})
onActivated(() => {
  if (!isMounted.value) {
    calculateTableHeight()
    getUserData(userConfig)
  }
  isMounted.value = false
})

/**监听页面高度，计算设置表格高度*/
const calculateTableHeight = () => {
  nextTick(() => {
    if (tableContanierRef.value) {
      const windowHeight = window.innerHeight
      const tableTopHeight = tableContanierRef.value.offsetTop
      const height = windowHeight - tableTopHeight - 77
      tableHeight.value = height + "px"
    }
  })
}
/**组件销毁 */
onBeforeUnmount(() => {
  window.removeEventListener("resize", calculateTableHeight) // 组件销毁前移除事件监听
})
// #endregion

// #region 表格操作
/**可分配的角色数据信息 */
const roleList = ref()
/**保存批量删除的id */
let ids = reactive([]) as any
/**分页跳转 */
const handleUserCurrentChange = (val: number) => {
  userConfig.page = val
  getUserData(userConfig)
}
/** 改变每页条数数 */
const handleSizeChange = (val: number) => {
  userConfig.size = val
  getUserData(userConfig)
  setPageSize("user-PageSize", val)
}

/** 关键字搜索*/
const handleSearch = () => {
  userConfig.userName = searchFormInline.userName
  userConfig.employeeNumber = searchFormInline.employeeNumber
  userConfig.enable = searchFormInline.enable === "true" ? 1 : searchFormInline.enable === "false" ? 0 : ""
  // userConfig.page = 1
  getUserData(userConfig)
}

/**重置查询 */
const resetUserConfig = () => {
  defaultPageSize.value = getCurrentPageSize() || 10
  Object.assign(userConfig, {
    page: 1,
    userName: "",
    employeeNumber: "",
    enable: ""
  } as User)
}
/**重置搜索表单内容 */
const resetSearchFormInline = () => {
  Object.assign(searchFormInline, {
    userName: "",
    employeeNumber: "",
    enable: ""
  })
}
/** 重置搜索*/
const resetSearch = () => {
  resetSearchFormInline()
  resetUserConfig()
  getUserData(userConfig)
}

/**获取用户列表数据 */
const getUserData = async (userConfig: User) => {
  let res: any = await getUserDataApi(userConfig)
  console.log("%c [ userres ]-267", "font-size:13px; background:pink; color:#bf2c9f;", res)
  if (res) {
    // const rulesMap = {
    //   1: "管理员",
    //   2: "普通用户"
    // }
    userConfig.total = res.data.total
    if (res.data.dataList !== null && res.data.dataList.length !== 0) {
      userTableData.value = res.data.dataList.map((item: any) => {
        item.enable = item.enable === 1 ? true : false
        return item
      })
    }
  }
}
/** 重置密码*/
const resetPassword = (row: any) => {
  ElMessageBox.confirm("确定要重置该用户密码吗？默认：123456", "系统提示", {
    type: "warning",
    cancelButtonText: "取消",
    confirmButtonText: "确定"
  })
    .then(async () => {
      const password = await Md5.hashStr("123456").toString()
      const id = row.id
      //enable在这里不等于1或者0，后端进行校验
      const enable = 101
      let res = await changeStatusOrPasswordApi({
        id: id,
        enable: enable,
        password: password
      })
      if (res) {
        ElMessage({
          type: "success",
          message: `用户 "${row.userName}" 密码已重置"`
        })
        getUserData(userConfig)
      }
    })
    .catch(() => {})
}
/** 是否禁用或启用用户 */
const changeUserStatus = (row: any) => {
  let status = row.enable === true ? "启用" : "禁用"
  ElMessageBox.confirm(`确定要 "${status}"  用户 "${row.userName}" 吗?`, "系统提示", {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning"
  })
    .then(async () => {
      const id = row.id
      const enable = row.enable === true ? 1 : 0
      let res = await changeStatusOrPasswordApi({
        id: id,
        enable: enable,
        password: null
      })
      if (res) {
        ElMessage({
          type: "success",
          message: `用户 "${row.userName}" 状态已修改为 "${status}"`
        })
        getUserData(userConfig)
      }
    })
    .catch(() => {
      row.enable = row.enable === true ? false : true
    })
}

/** 新增用户*/
const handleAdd = async () => {
  resetFormUserd()
  getAction.action = "add"
  let res: any = await getRoleListApi()
  if (res) {
    roleList.value = res.data
  }
  dialogVisible.value = true
}

/** 编辑用户*/
const handleEdit = async (row: any) => {
  let userId = row.id
  // 获取角色列表
  let res: any = await getRoleByIdApi(userId)
  if (res) {
    getAction.action = "edit"
    roleList.value = res.data.roles
    dialogVisible.value = true
    //事件循环机制，异步执行，避免在新增按钮触发是还可以看到编辑的值显示
    nextTick(() => {
      // 浅拷贝
      Object.assign(formUserd, row)
      formUserd.enable = formUserd.enable ? "1" : "0"
      if (res.data.roleIds > 0) {
        formUserd.roleIds = res.data.roleIds
      } else {
        ElMessage.error("用戶角色被删除请重新选择")
      }
      //
      // res.data.roleIds.map((item: any) => {
      //   formUserd.roleIds = item
      // })
      console.log("%c [ formUserd ]-421", "font-size:13px; background:pink; color:#bf2c9f;", formUserd)
    })
  }
}
/** 删除用户*/
const handleUserRemove = (row: any) => {
  ElMessageBox.confirm("确定要删除吗", "提示", {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning"
  })
    .then(async () => {
      let res: any = await deleteUserApi({ id: row.id })
      console.log(res)
      if (res) {
        ElMessage({
          showClose: true,
          message: res.data,
          type: "success"
        })
        getUserData(userConfig)
      }
    })
    .catch(() => {
      // catch error
    })
}

/** 批量选中*/
const handleSelectionChange = (val: any) => {
  console.log("%c [ val ]-447", "font-size:13px; background:pink; color:#bf2c9f;", val)
  ids = []
  val.forEach((item: any) => {
    ids.push(item.id)
  })
}
/** 批量删除*/
const handleDelList = () => {
  ElMessageBox.confirm("确定要删除吗", "提示", {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning"
  })
    .then(async () => {
      if (ids.length !== 0) {
        console.log("%c [ ids ]-466", "font-size:13px; background:pink; color:#bf2c9f;", ids)
        let res: any = await batchDelUserApi(ids)
        if (res) {
          ElMessage({
            showClose: true,
            message: res.message,
            type: "success"
          })
          getUserData(userConfig)
          ids = []
        }
      } else {
        ElMessage({
          showClose: true,
          message: "请选择要删除的用户",
          type: "warning"
        })
      }
    })
    .catch((err) => {
      console.log(err)
    })
}
// #endregion

// #region 对话框-表单操作
/**对话框表单ref */
const userDialogForm = ref()
/**添加用户对话框表单数据接收绑定*/
const formUserd = reactive({
  id: "",
  employeeNumber: "",
  userName: "",
  phoneNumber: "",
  enable: "",
  // 默认密码
  password: "123456",
  roleIds: [] as any
})
/**重置表单数据，防止数据回显 */
const resetFormUserd = () => {
  Object.assign(formUserd, {
    id: "",
    employeeNumber: "",
    userName: "",
    phoneNumber: "",
    enable: "",
    // 默认密码
    password: "123456",
    roleIds: ""
  })
}
/**表单校验 */
const rules = reactive({
  employeeNumber: [{ required: true, message: "请输入工号", trigger: "blur" }],
  userName: [{ required: true, message: "请输入用户名", trigger: "blur" }],
  password: [{ required: true, message: "请输入密码", trigger: "blur" }],
  roleIds: [{ required: true, message: "请选择角色", trigger: "blur" }],
  enable: [{ required: true, message: "请选择状态", trigger: "blur" }],
  phoneNumber: [{ pattern: /^(?:(?:\+|00)86)?1\d{10}$/, message: "手机号格式不正确", trigger: "blur" }]
})
/** 取消对话框*/
const handleCancel = () => {
  dialogVisible.value = false
  formUserd.password = "123456"
  userDialogForm.value.resetFields()
}
/** 关闭对话框*/
const handleClose = (done: () => void) => {
  ElMessageBox.confirm("确定关闭吗?", {
    confirmButtonText: "确定",
    cancelButtonText: "取消"
  })
    .then(() => {
      formUserd.password = "123456"
      userDialogForm.value.resetFields()
      done()
    })
    .catch(() => {
      // catch error
    })
}
/** 提交保存用户信息*/
const onSubmit = () => {
  // 表单内容验证，表单的ref属性拿到表单，使用el-api校验
  userDialogForm.value.validate(async (valid: FormInstance | undefined) => {
    if (valid) {
      const currentUser = localStorage.getItem("employeeNumber")
      const userPassword = Md5.hashStr(formUserd.password).toString()
      if (getAction.action === "add") {
        console.log("%c [ formUserd ]-513", "font-size:13px; background:pink; color:#bf2c9f;", formUserd)
        // 新增用户密码加密
        let formUserdAdd = reactive({
          employeeNumber: formUserd.employeeNumber,
          userName: formUserd.userName,
          password: userPassword,
          roleIds: formUserd.roleIds,
          phoneNumber: formUserd.phoneNumber,
          enable: formUserd.enable,
          createBy: currentUser
        })
        // await只会返回给他最近的async回调函数！！！否则报错
        const res: any = await addUserApi(formUserdAdd)
        // 如果成功，则清空form表单，并关闭弹框
        if (res) {
          // 拿到form表单的ref，使用form自带的清空表单api，ep提供
          //需要注意的是：使用validate、resetFields，需要在表单内容上加prop绑定表单相应属性才会生效
          userDialogForm.value.resetFields()
          // 关闭弹框
          dialogVisible.value = false
          ElMessage({
            showClose: true,
            message: res.data,
            type: "success"
          })
          // 重新获取数据
          getUserData(userConfig)
        }
        // 编辑用户信息
      } else if (getAction.action === "edit") {
        const formUserdEdit = reactive({
          id: formUserd.id,
          employeeNumber: formUserd.employeeNumber,
          userName: formUserd.userName,
          password: userPassword,
          roleIds: formUserd.roleIds,
          phoneNumber: formUserd.phoneNumber,
          enable: formUserd.enable,
          updateBy: currentUser
        })
        const res: any = await updateUserDataApi(formUserdEdit)
        if (res) {
          userDialogForm.value.resetFields()
          dialogVisible.value = false
          ElMessage({
            showClose: true,
            message: res.data,
            type: "success"
          })
          // 重新获取数据
          getUserData(userConfig)
        }
      }
    } else {
      ElMessage.error("请填写完整信息")
    }
  })
}
// #endregion
</script>
<style lang="scss" scoped>
.searchTop {
  margin-bottom: 10px;
  :deep(.el-card__body) {
    padding-bottom: 2px;
  }
}
.tableTop {
  margin-bottom: 20px;
}
.table {
  display: flex;
}
.pager-foot {
  display: flex;
  justify-content: flex-end;
}
.dialog-foot {
  display: flex;
  justify-content: end;
}
</style>
