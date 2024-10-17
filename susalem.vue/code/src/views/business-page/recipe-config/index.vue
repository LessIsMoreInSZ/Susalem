<template>
  <div>
    <el-row>
      <el-col :span="4">
        <div class="recipeConfig-sidebar">
          <el-card shadow="never" style="height: 100%">
            <div class="head-container">
              <el-input v-model="recipeType" placeholder="请输入配方种类" clearable prefix-icon="Search" />
            </div>
            <div class="head-tree">
              <el-scrollbar class="scro-bar" height="550px">
                <el-tree
                  :data="recipeTypeTree"
                  :props="defaultProps"
                  :filter-node-method="filterNode"
                  ref="ecipeTypeTreeRef"
                  node-key="recipeTypeId"
                  highlight-current
                  @node-click="handleClick"
                />
              </el-scrollbar>
            </div>
          </el-card>
        </div>
      </el-col>
      <el-col :span="20">
        <div class="app-container">
          <el-card class="searchTop" shadow="never">
            <div class="searchForm">
              <el-form :inline="true" :model="searchFormInline">
                <el-form-item label="参数名称:">
                  <el-input
                    v-model="searchFormInline.parameterId"
                    placeholder="请输入参数名称检索"
                    clearable
                    prefix-icon="Search"
                    style="width: 190px"
                  />
                </el-form-item>
                <!-- <el-form-item label="参数类型:">
              <el-select
                v-model="searchFormInline.dataType"
                placeholder="请选择参数类型"
                clearable
                filterable
                style="width: 190px"
              >
                <el-option-group v-for="group in dataTypeOptions" :key="group.label" :label="group.label">
                  <el-option v-for="item in group.options" :key="item.value" :label="item.label" :value="item.value" />
                </el-option-group>
              </el-select>
            </el-form-item> -->
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
                <el-table-column type="index" label="排序" align="center" width="80px"></el-table-column>
                <el-table-column
                  prop="parameterId"
                  label="参数名称"
                  align="center"
                  show-overflow-tooltip
                ></el-table-column>
                <el-table-column
                  prop="parameterName"
                  label="参数描述"
                  align="center"
                  show-overflow-tooltip
                ></el-table-column>
                <el-table-column
                  prop="dataType"
                  label="参数值类型"
                  align="center"
                  show-overflow-tooltip
                ></el-table-column>
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
      </el-col>
    </el-row>
    <!--新增和编辑用户 dialogVisible：隐藏显示对话框视图 -->
    <el-dialog
      v-model="dialogVisible"
      :title="getAction.action === 'add' ? '新增配方项' : '编辑配方项'"
      :before-close="handleClose"
      class="currentDialog"
      draggable
      style="width: 27%"
    >
      <!-- form的prop和v-model双向绑定的要一致，否则会回显数据-->
      <el-form
        class="dialog-form"
        :model="currentDialogForm"
        ref="currentDialogFormRef"
        label-width="100px"
        label-position="right"
        :rules="currentDialogFormRules"
      >
        <el-row>
          <el-col :span="24">
            <el-form-item label="参数名称:" prop="parameterId">
              <el-input
                v-model="currentDialogForm.parameterId"
                placeholder="请输入参数名称"
                :disabled="getAction.action == 'edit'"
                clearable
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="参数值类型:" prop="dataType">
              <el-select v-model="currentDialogForm.dataType" placeholder="请选择参数值类型" clearable filterable>
                <el-option-group v-for="group in dataTypeOptions" :key="group.label" :label="group.label">
                  <el-option v-for="item in group.options" :key="item.value" :label="item.label" :value="item.value" />
                </el-option-group>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="参数描述:" prop="parameterName">
              <el-input v-model="currentDialogForm.parameterName" placeholder="请输入参数描述" clearable />
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

<script setup lang="ts" name="recipeConfig">
import { ref, reactive, onMounted, nextTick, watch, onActivated, onBeforeUnmount } from "vue"
import { Search, Refresh, Chicken } from "@element-plus/icons-vue"
import { ElMessage, ElMessageBox, FormInstance } from "element-plus"
import {
  getRecipeConfigDataApi,
  deleteRecipeConfigApi,
  batchDelRecipeConfigApi,
  addRecipeConfigApi,
  updateRecipeConfigApi
} from "@/api/business/bus-recipeConfig"
import { getAllRecipeTypeDataApi } from "@/api/business/bus-recipeType"
import { getPageSize, setPageSize } from "@/utils/cache/local-storage"
// #region 定义属性值
/**表格容器引用 */
const tableContanierRef = ref()
/**表格自适应高度 */
const tableHeight = ref()
/**默认页面的条数 */
const defaultPageSize = ref(30)
/**侧边栏tree属性绑定 */
const defaultProps = reactive({
  value: "recipeTypeId",
  label: "recipeTypeId",
  children: "children"
})
/**侧边栏搜索框绑定值 */
const recipeType = ref()
/**侧边栏配方类型选择 */
const recipeTypeTree = ref()
/**侧边栏列表ref */
const ecipeTypeTreeRef = ref()

/**参数类型分组 */
const dataTypeOptions = [
  {
    label: "整型类型",
    options: [
      {
        value: "Int16",
        label: "Int16"
      },
      {
        value: "Int32",
        label: "Int32"
      },
      {
        value: "Int64",
        label: "Int64"
      },
      {
        value: "UInt16",
        label: "UInt16"
      },
      {
        value: "UInt32",
        label: "UInt32"
      },
      {
        value: "UInt64",
        label: "UInt64"
      }
    ]
  },
  {
    label: "浮点类型",
    options: [
      {
        value: "Float",
        label: "Float"
      },
      {
        value: "Double",
        label: "Double"
      }
    ]
  },
  {
    label: "字节类型",
    options: [
      {
        value: "Byte",
        label: "Byte"
      }
      // {
      //   value: "SByte",
      //   label: "SByte"
      // }
    ]
  },
  {
    label: "字符串类型",
    options: [
      {
        value: "String",
        label: "String"
      }
      // {
      //   value: "WString",
      //   label: "WString"
      // }
    ]
  },
  {
    label: "其他类型",
    options: [
      {
        value: "Bool",
        label: "Bool"
      }
      // {
      //   value: "DateTime",
      //   label: "DateTime"
      // }
    ]
  }
]
/**分页配置 */
const config = reactive({
  total: 0,
  page: 1,
  size: 30,
  //进行搜索的关键词
  parameterId: "",
  dataType: "",
  recipeTypeId: ""
})
/**搜索绑定 */
const searchFormInline = reactive({
  parameterId: "",
  dataType: "",
  recipeTypeId: ""
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
  return getPageSize("recipeConfig-PageSize")
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
  getAllRcipeTypeData()
})
/**页面缓存被激活 */
onActivated(() => {
  if (!isMounted.value) {
    calculateTableHeight() // 页面加载时计算表格高度
    window.addEventListener("resize", calculateTableHeight) // 监听窗口大小改变事件
    getTableData(config)
    getAllRcipeTypeData()
  }
  isMounted.value = false
})

/**监听页面高度，计算设置表格高度*/
const calculateTableHeight = () => {
  nextTick(() => {
    if (tableContanierRef.value) {
      const windowHeight = window.innerHeight // 获取窗口高度
      const tableOffsetTop = tableContanierRef.value.offsetTop // 获取表格容器距离页面顶部的距离
      const height = windowHeight - tableOffsetTop - 160 // 20 是为了留一些空间
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
    recipeTypeTree.value = res.data.dataList
  })
}

// #region Table主页面相关操作
/**侧边栏搜索 */
watch(recipeType, (val: any) => {
  ecipeTypeTreeRef.value.filter(val)
})
/**侧边栏搜索过滤节点 */
const filterNode = (value: string, data: any) => {
  if (!value) return true
  return data.recipeTypeId.toLocaleLowerCase().includes(value.toLocaleLowerCase())
}
/**侧边栏选择配方种类跳转查询 */
const handleClick = (data: any) => {
  config.recipeTypeId = data.recipeTypeId
  config.page = 1
  getTableData(config)
}

/** 关键字搜索*/
const handleSearch = () => {
  config.parameterId = searchFormInline.parameterId
  config.dataType = searchFormInline.dataType
  // config.page = 1
  getTableData(config)
}
/**重置查询 */
const resetConfig = () => {
  defaultPageSize.value = getCurrentPageSize() || 30
  Object.assign(config, {
    page: 1,
    parameterId: "",
    dataType: "",
    recipeTypeId: ""
  })
}
/**重置搜索表单内容 */
const resetSearchFormInline = () => {
  Object.assign(searchFormInline, {
    parameterId: "",
    dataType: "",
    recipeTypeId: ""
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
  setPageSize("recipeConfig-PageSize", val)
}

/**获取表格数据 */
const getTableData = async (config: any) => {
  let res: any = await getRecipeConfigDataApi(config)
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
      let res: any = await deleteRecipeConfigApi({ id: row.id })
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
    batchRemoveDatas.push(item.id)
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
        let res: any = await batchDelRecipeConfigApi(batchRemoveDatas)
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
  parameterId: "",
  dataType: "",
  parameterName: "",
  recipeTypeId: ""
})
/**对话框表单校验规则 */
const currentDialogFormRules = reactive({
  parameterId: [
    {
      required: true,
      message: "请输入参数名称",
      trigger: "blur"
    }
  ],
  parameterName: [
    {
      required: true,
      message: "请输入参数描述",
      trigger: "blur"
    }
  ],
  dataType: [
    {
      required: true,
      message: "请选择参数值类型",
      trigger: "change"
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
    parameterId: "",
    dataType: "",
    parameterName: "",
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
        const { parameterId, parameterName, dataType, recipeTypeId } = currentDialogForm
        let addCurrentDialogForm = {
          parameterId: parameterId,
          parameterName: parameterName,
          dataType: dataType,
          recipeTypeId: recipeTypeId
        }
        let res: any = await addRecipeConfigApi(addCurrentDialogForm)
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
        let res: any = await updateRecipeConfigApi(editCurrentDialogForm)
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
.recipeConfig-sidebar {
  // display: flex;
  // flex-direction: column;
  // height: 100%;
  // padding: 10px;
  padding-left: 10px;
  padding-top: 10px;
  height: calc(100% - 10px);
  .head-tree {
    margin-top: 10px;
    :deep(.el-tree-node__expand-icon) {
      padding: 0px;
    }
    height: calc(100% - 20px);
  }
}

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
