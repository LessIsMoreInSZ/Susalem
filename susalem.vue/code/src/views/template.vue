<template>
  <div>
    <div class="app-container">
      <el-card class="searchTop" shadow="never">
        <div>
          <el-form :inline="true" :model="searchFormInline" class="ef_1">
            <el-form-item label="工厂名:">
              <el-input
                v-model="searchFormInline.workshopName"
                placeholder="请输入工号检索"
                clearable
                prefix-icon="Search"
                class="ei_1"
                style="width: 190px"
              />
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
        <div class="table">
          <el-table :data="currentTableData" :fit="true" @selection-change="handleSelectionChange">
            <el-table-column type="selection" width="30" />
            <el-table-column type="index" label="用户编号" align="center" width="120px"></el-table-column>
            <el-table-column prop="employeeNumber" label="工号" align="center"></el-table-column>
            <el-table-column fixed="right" align="center" label="操作">
              <!--Vue组件中定义一个插槽，row为当前行的数据，从父组件中拿到的，可以打印{{row}}查看 -->
              <!--如果#default="scope" scope为作用域对象，你可以拿到这个vue模版中的所有的对象和方法 -->
              <template #default="{ row }">
                <!-- {{ scope.row }} -->
                <el-button link type="primary" icon="Edit" @click="handleEdit(row)">编辑</el-button>
                <el-button link type="danger" icon="Delete" @click="handleRemove(row)">删除</el-button>
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
            :total="config.total"
            class="pager mt-5"
            @size-change="handleSizeChange"
            @current-change="handleUserCurrentChange"
          />
        </div>
      </el-card>
    </div>
    <!--新增和编辑用户 dialogVisible：隐藏显示对话框视图 -->
    <el-dialog
      v-model="dialogVisible"
      :title="getAction.action === 'add' ? '新增工厂数据' : '编辑工厂数据'"
      :before-close="handleClose"
      class="dialog-form"
      draggable
      style="width: 30%"
    >
      <!-- form的prop和v-model双向绑定的要一致，否则会回显数据-->
      <el-form
        :model="currentDialogForm"
        ref="currentDialogFormRef"
        label-width="70px"
        label-position="right"
        :rules="currentDialogFormRules"
      >
        <el-row class="dialog-foot">
          <el-form-item>
            <el-button @click="handleCancel">取消</el-button>
            <el-button type="primary" @click="onSubmit">确定</el-button>
          </el-form-item>
        </el-row>
      </el-form>
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from "vue"
import { Search, Refresh } from "@element-plus/icons-vue"
import { ElMessage, ElMessageBox, FormInstance } from "element-plus"

// #region 定义属性值
/**分页配置 */
const config = reactive({
  total: 0,
  page: 1,
  size: 10,
  //进行搜索的关键词
  workshopName: ""
})
/**搜索绑定 */
const searchFormInline = reactive({
  workshopName: ""
})
/**当前表格数据 */
const currentTableData = ref()
/**保存批量删除的数据 */
let batchRemoveDatas = reactive([]) as any
/**对话框开关触发值 */
const dialogVisible = ref(false)
/**对话框标题值，复用对话框add或者edit */
const getAction = reactive({
  action: ""
})
/**对话框表单绑定的ref */
const currentDialogFormRef = ref()
// #endregion

onMounted(() => {
  getTableData(config)
})

// #region Table主页面相关操作
/** 关键字搜索*/
const handleSearch = () => {
  config.workshopName = searchFormInline.workshopName
  getTableData(config)
}
/**重置查询 */
const resetConfig = () => {
  Object.assign(config, {
    total: 0,
    page: 1,
    size: 10,
    workshopName: ""
  })
}
/**重置搜索表单内容 */
const resetSearchFormInline = () => {
  Object.assign(searchFormInline, {
    workshopName: ""
  })
}
/** 重置搜索*/
const resetSearch = () => {
  resetSearchFormInline()
  resetConfig()
  getTableData(config)
}

/**分页跳转 */
const handleUserCurrentChange = (val: number) => {
  config.page = val
}
/** 改变每页条数 */
const handleSizeChange = (val: number) => {}

/**获取表格数据 */
const getTableData = (config: any) => {}

/** 新增按钮操作*/
const handleAdd = async () => {
  resetCurrentDialogForm()
  getAction.action = "add"
  //   let res: any = await getRoleListApi()
  //   if (res) {
  //     roleList.value = res.data
  //   }
  dialogVisible.value = true
}
/** 编辑按钮操作*/
const handleEdit = async (row: any) => {
  //   let userId = row.id
  //   // 获取角色列表
  //   let res: any = await getRoleByIdApi(userId)
  //   if (res) {
  //     getAction.action = "edit"
  //     roleList.value = res.data.roles
  //     dialogVisible.value = true
  //     //事件循环机制，异步执行，避免在新增按钮触发是还可以看到编辑的值显示
  //     nextTick(() => {
  //       // 浅拷贝
  //       Object.assign(formUserd, row)
  //       formUserd.enable = formUserd.enable ? "1" : "0"
  //       formUserd.roleIds = res.data.roleIds
  //       // res.data.roleIds.map((item: any) => {
  //       //   formUserd.roleIds = item
  //       // })
  //       console.log("%c [ formUserd ]-421", "font-size:13px; background:pink; color:#bf2c9f;", formUserd)
  //     })
  //   }
}
/** 删除按钮操作*/
const handleRemove = (row: any) => {
  //   ElMessageBox.confirm("确定要删除吗", "提示", {
  //     confirmButtonText: "确定",
  //     cancelButtonText: "取消",
  //     type: "warning"
  //   })
  //     .then(async () => {
  //       let res: any = await deleteUserApi({ id: row.id })
  //       console.log(res)
  //       if (res) {
  //         ElMessage({
  //           showClose: true,
  //           message: res.data,
  //           type: "success"
  //         })
  //         getUserData(userConfig)
  //       }
  //     })
  //     .catch(() => {
  //       // catch error
  //     })
}
/** 批量选中*/
const handleSelectionChange = (val: any) => {
  console.log("%c [ val ]-447", "font-size:13px; background:pink; color:#bf2c9f;", val)
  batchRemoveDatas = []
  val.forEach((item: any) => {
    // 批量选中删除的主键值：唯一
    batchRemoveDatas.push(item.id)
  })
}
/** 批量删除操作*/
const handleDelList = () => {
  //   ElMessageBox.confirm("确定要删除吗", "提示", {
  //     confirmButtonText: "确定",
  //     cancelButtonText: "取消",
  //     type: "warning"
  //   })
  //     .then(async () => {
  //       if (ids.length !== 0) {
  //         console.log("%c [ ids ]-466", "font-size:13px; background:pink; color:#bf2c9f;", ids)
  //         let res: any = await batchDelUserApi(ids)
  //         if (res) {
  //           ElMessage({
  //             showClose: true,
  //             message: res.message,
  //             type: "success"
  //           })
  //           getUserData(userConfig)
  //           ids = []
  //         }
  //       } else {
  //         ElMessage({
  //           showClose: true,
  //           message: "请选择要删除的用户",
  //           type: "warning"
  //         })
  //       }
  //     })
  //     .catch((err) => {
  //       console.log(err)
  //     })
}
// #endregion

// #region 对话框属性值和相关操作
/**对话框表单进行数据双向绑定 */
const currentDialogForm = reactive({})
/**对话框表单校验规则 */
const currentDialogFormRules = reactive({})

/**重置表单数据，防止数据回显 */
const resetCurrentDialogForm = () => {
  Object.assign(currentDialogForm, {
    // 重置表单数据
  })
}
/** 点击x号关闭对话框*/
const handleClose = (done: () => void) => {
  ElMessageBox.confirm("确定关闭吗?", {
    confirmButtonText: "确定",
    cancelButtonText: "取消"
  })
    .then(() => {
      // 确定关闭后需要的操作处理
      currentDialogFormRef.value.resetFields()
      done()
    })
    .catch(() => {
      // catch error
    })
}

/** 点击取消按钮关闭对话框*/
const handleCancel = () => {
  dialogVisible.value = false
  currentDialogFormRef.value.resetFields()
}

/** 对话框提交保存数据*/
const onSubmit = () => {
  // 表单内容验证，表单的ref属性拿到表单，使用el-api校验
  currentDialogFormRef.value.validate(async (valid: FormInstance | undefined) => {
    if (valid) {
    } else {
      ElMessage.error("请填写完整信息")
    }
  })
}
// #endregion
</script>
<style lang="scss" scoped>
.add-container {
  padding: 20px;
}
.searchTop {
  margin-bottom: 20px;
  :deep(.el-card__body) {
    padding-bottom: 2px;
  }
}
.tableTop {
  margin-bottom: 20px;
}
.table {
  display: flex;
  margin-bottom: 10px;
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
