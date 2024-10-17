<template>
  <div>
    <div class="app-container">
      <el-card class="searchTop" shadow="never">
        <div class="searchForm">
          <el-form :inline="true" :model="searchFormInline">
            <el-form-item label="配方号:">
              <el-input
                v-model="searchFormInline.recipeId"
                placeholder="请输入配方号检索"
                clearable
                prefix-icon="Search"
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
        <div class="table" ref="tableContanierRef">
          <el-table
            :data="currentTableData"
            :fit="true"
            @selection-change="handleSelectionChange"
            :header-cell-style="{ textAlign: 'center' }"
            :height="tableHeight"
            border
          >
            <el-table-column type="selection" width="40" />
            <el-table-column type="index" label="排序" align="center" width="120px"></el-table-column>
            <el-table-column prop="recipeId" label="配方号" align="center"></el-table-column>
            <el-table-column prop="recipeName" label="配方名称" align="center"></el-table-column>
            <el-table-column prop="recipeType.recipeTypeId" label="配方种类" align="center"></el-table-column>
            <el-table-column fixed="right" align="center" width="150px" label="操作">
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
            :default-page-size="defaultPageSize"
            layout="total, sizes, prev, pager, next, jumper"
            :total="config.total"
            :current-page="config.page"
            class="pager mt-5"
            @size-change="handleSizeChange"
            @current-change="handleCurrentChange"
            v-if="config.total > 0"
          />
        </div>
      </el-card>
    </div>
    <!--新增和编辑用户 dialogVisible：隐藏显示对话框视图 -->
    <el-dialog
      v-model="dialogVisible"
      :title="getAction.action === 'add' ? '新增配方' : '编辑配方'"
      :before-close="handleClose"
      class="currentDialog"
      draggable
      style="width: 25%"
    >
      <!-- form的prop和v-model双向绑定的要一致，否则会回显数据-->
      <el-form
        class="dialog-form"
        :model="currentDialogForm"
        ref="currentDialogFormRef"
        label-position="right"
        label-width="90px"
        :rules="currentDialogFormRules"
      >
        <el-row>
          <el-col :span="24">
            <el-form-item label="配方号:" prop="recipeId">
              <el-input
                v-model="currentDialogForm.recipeId"
                placeholder="请输入配方号"
                :disabled="getAction.action == 'edit'"
                clearable
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="配方名称:" prop="recipeName">
              <el-input v-model="currentDialogForm.recipeName" placeholder="请输入配方名称" clearable />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="配方种类:" prop="recipeTypeId">
              <el-select v-model="currentDialogForm.recipeTypeId" placeholder="请选择配方种类" clearable filterable>
                <el-option
                  v-for="item in recipeTypeOptions"
                  :key="item.recipeTypeId"
                  :label="item.recipeTypeId"
                  :value="item.recipeTypeId"
                />
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
  </div>
</template>

<script setup lang="ts" name="recipe">
import { ref, reactive, onMounted, nextTick, onActivated, onBeforeUnmount } from "vue"
import { Search, Refresh } from "@element-plus/icons-vue"
import { ElMessage, ElMessageBox, FormInstance } from "element-plus"
import {
  getRecipeDataApi,
  deleteRecipeApi,
  batchDelRecipeApi,
  addRecipeApi,
  updateRecipeApi
} from "@/api/business/bus-recipe"
import { getAllRecipeTypeDataApi } from "@/api/business/bus-recipeType"
import { getPageSize, setPageSize } from "@/utils/cache/local-storage"
// #region 定义属性值
/**表格容器引用 */
const tableContanierRef = ref()
/**表格自适应高度 */
const tableHeight = ref()
/**默认页面的条数 */
const defaultPageSize = ref(30)
/**分页配置 */
const config = reactive({
  total: 0,
  page: 1,
  size: 30,
  //进行搜索的关键词
  recipeId: ""
})
/**搜索绑定 */
const searchFormInline = reactive({
  recipeId: ""
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
/**对话框配方种类选择 */
const recipeTypeOptions = ref()
// #endregion

// #region 其他操作-生命周期、公共函数
/**封装获取pagesize */
const getCurrentPageSize = () => {
  return getPageSize("recipe-PageSize")
}
/**限制生命周期多次加载同一请求 */
const isMounted = ref(false)
/** 页面初始化*/
onMounted(() => {
  isMounted.value = true
  const currentPageSize = getCurrentPageSize()
  if (currentPageSize) {
    defaultPageSize.value = currentPageSize
    config.size = defaultPageSize.value
  }
  calculateTableHeight() // 页面加载时计算表格高度
  window.addEventListener("resize", calculateTableHeight) // 监听窗口大小改变事件
  getTableData(config)
})
/**页面缓存被激活 */
onActivated(() => {
  if (!isMounted.value) {
    calculateTableHeight() // 页面加载时计算表格高度
    window.addEventListener("resize", calculateTableHeight) // 监听窗口大小改变事件
    getTableData(config)
  }
  isMounted.value = false
})

/**监听页面高度，计算设置表格高度*/
const calculateTableHeight = () => {
  nextTick(() => {
    if (tableContanierRef.value) {
      const windowHeight = window.innerHeight // 获取窗口高度
      const tableOffsetTop = tableContanierRef.value.offsetTop // 获取表格容器距离页面顶部的距离
      const height = windowHeight - tableOffsetTop - 80 // 20 是为了留一些空间
      tableHeight.value = height + "px" // 设置表格高度
    }
  })
}
/**组件销毁 */
onBeforeUnmount(() => {
  window.removeEventListener("resize", calculateTableHeight) // 组件销毁前移除事件监听
})
// #endregion

/**获取所有配方种类数据 */
const getAllRcipeTypeData = () => {
  getAllRecipeTypeDataApi().then((res: any) => {
    recipeTypeOptions.value = res.data.dataList
  })
}

// #region Table主页面相关操作
/** 关键字搜索*/
const handleSearch = () => {
  config.recipeId = searchFormInline.recipeId
  // config.page = 1
  getTableData(config)
}
/**重置查询 */
const resetConfig = () => {
  defaultPageSize.value = getCurrentPageSize() || 30
  Object.assign(config, {
    page: 1,
    recipeId: ""
  })
}
/**重置搜索表单内容 */
const resetSearchFormInline = () => {
  Object.assign(searchFormInline, {
    recipeId: ""
  })
}
/** 重置搜索*/
const resetSearch = () => {
  resetSearchFormInline()
  resetConfig()
  getTableData(config)
}

/**分页跳转 */
const handleCurrentChange = (val: number) => {
  config.page = val
  getTableData(config)
}
/** 改变每页条数 */
const handleSizeChange = (val: number) => {
  config.size = val
  getTableData(config)
  setPageSize("recipe-PageSize", val)
}

/**获取表格数据 */
const getTableData = async (config: any) => {
  let res: any = await getRecipeDataApi(config)
  if (res) {
    config.total = res.data.total
    currentTableData.value = res.data.dataList
  }
}

/** 新增按钮操作*/
const handleAdd = async () => {
  resetCurrentDialogForm()
  getAction.action = "add"
  dialogVisible.value = true
  getAllRcipeTypeData()
}
/** 编辑按钮操作*/
const handleEdit = async (row: any) => {
  getAction.action = "edit"
  dialogVisible.value = true
  getAllRcipeTypeData()
  nextTick(() => {
    Object.assign(currentDialogForm, row)
    currentDialogForm.recipeTypeId = row.recipeType.recipeTypeId
  })
}
/** 删除按钮操作*/
const handleRemove = (row: any) => {
  console.log("%c [ row ]-216", "font-size:13px; background:pink; color:#bf2c9f;", row)
  ElMessageBox.confirm("确定要删除吗", "提示", {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning"
  })
    .then(async () => {
      let res: any = await deleteRecipeApi({ id: row.recipeId })
      if (res) {
        ElMessage({
          showClose: true,
          message: res.message,
          type: "success"
        })
        getTableData(config)
      }
    })
    .catch(() => {
      // catch error
    })
}
/** 批量选中*/
const handleSelectionChange = (val: any) => {
  console.log("%c [ val ]-447", "font-size:13px; background:pink; color:#bf2c9f;", val)
  batchRemoveDatas = []
  val.forEach((item: any) => {
    // 批量选中删除的主键值：唯一
    batchRemoveDatas.push(item.recipeId)
  })
}
/** 批量删除操作*/
const handleDelList = () => {
  ElMessageBox.confirm("确定要删除吗", "提示", {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning"
  })
    .then(async () => {
      if (batchRemoveDatas.length !== 0) {
        console.log("%c [ ids ]-466", "font-size:13px; background:pink; color:#bf2c9f;", batchRemoveDatas)
        let res: any = await batchDelRecipeApi(batchRemoveDatas)
        if (res) {
          ElMessage({
            showClose: true,
            message: res.message,
            type: "success"
          })
          getTableData(config)
          batchRemoveDatas = []
        }
      } else {
        ElMessage({
          showClose: true,
          message: "请选择要删除的数据",
          type: "warning"
        })
      }
    })
    .catch((err) => {
      console.log(err)
    })
}
// #endregion

// #region 对话框属性值和相关操作
/**对话框表单进行数据双向绑定 */
const currentDialogForm = reactive({
  recipeId: "",
  recipeName: "",
  recipeTypeId: ""
})
/**对话框表单校验规则 */
/**对话框表单校验规则 */
const currentDialogFormRules = reactive({
  recipeId: [
    {
      required: true,
      message: "请输入配方号",
      trigger: "blur"
    }
  ],
  recipeName: [
    {
      required: true,
      message: "请输入配方名称",
      trigger: "blur"
    }
  ],
  recipeTypeId: [
    {
      required: true,
      message: "请选择配方种类",
      trigger: "change"
    }
  ]
})

/**重置对话框表单数据，防止数据回显 */
const resetCurrentDialogForm = () => {
  Object.assign(currentDialogForm, {
    // 重置表单数据
    recipeId: "",
    recipeName: "",
    recipeTypeId: ""
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
      // 验证通过，发送新增或修改请求
      if (getAction.action == "add") {
        const { recipeId, recipeName, recipeTypeId } = currentDialogForm
        let addCurrentDialogForm = {
          recipeId: recipeId,
          recipeName: recipeName,
          recipeTypeId: recipeTypeId
        }
        let res: any = await addRecipeApi(addCurrentDialogForm)
        if (res) {
          ElMessage({
            type: "success",
            message: res.message
          })
          currentDialogFormRef.value.resetFields()
          dialogVisible.value = false
          getTableData(config)
        }
      } else {
        let editCurrentDialogForm = { ...currentDialogForm }
        let res: any = await updateRecipeApi(editCurrentDialogForm)
        if (res) {
          ElMessage({
            type: "success",
            message: res.message
          })
          currentDialogFormRef.value.resetFields()
          dialogVisible.value = false
          getTableData(config)
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
.add-container {
  padding: 20px;
}
.searchTop {
  margin-bottom: 10px;
  // :deep(.el-card__body) {
  //   padding-bottom: 2px;
  // }
}
.tableTop {
  margin-bottom: 20px;
}
.table {
  display: flex;
  margin-bottom: 0px;
}
.pager-foot {
  display: flex;
  justify-content: flex-end;
}
.dialog-foot {
  display: flex;
  justify-content: end;
}
.form_description {
  :deep(.el-textarea__inner) {
    height: 100px;
  }
}
.dialog-form {
  margin-top: 20px;
}
</style>
