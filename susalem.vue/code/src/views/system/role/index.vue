<template>
  <div class="app-container">
    <el-card class="searchTop" shadow="never">
      <div class="user-header">
        <el-form :inline="true" :model="roleFormInline" class="ef_1">
          <el-form-item label="角色名:">
            <el-input
              v-model="roleFormInline.keywords"
              placeholder="请输入角色名检索"
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
      <div class="table" ref="tableContanierRef">
        <el-table
          :data="RoleTableData"
          :fit="true"
          @selection-change="handleSelectionChange"
          :height="tableHeight"
          border
        >
          <el-table-column type="selection" width="40" />
          <el-table-column type="index" label="角色编号" align="center" width="100"></el-table-column>
          <el-table-column label="角色名称" prop="roleName" width="125px" align="center"></el-table-column>
          <el-table-column label="显示顺序" prop="roleSort" width="125px" align="center"></el-table-column>
          <el-table-column label="创建时间" prop="createTime" align="center" :show-overflow-tooltip="true">
          </el-table-column>
          <el-table-column label="创建人" :show-overflow-tooltip="true" prop="createBy" align="center">
          </el-table-column>
          <el-table-column label="修改人" :show-overflow-tooltip="true" prop="updateBy" align="center">
          </el-table-column>
          <el-table-column label="启用" prop="enable" align="center" width="70">
            <template #default="{ row }">
              <el-switch
                v-model="row.enable"
                inline-prompt
                inactive-color="#f56c6c"
                active-text="是"
                inactive-text="否"
                @change="changeRoleStatus(row)"
              />
            </template>
          </el-table-column>
          <el-table-column fixed="right" align="center" label="操作" width="250">
            <template #default="scope">
              <el-button
                link
                type="primary"
                icon="Edit"
                v-if="scope.row.roleName != '超级管理员'"
                @click="handleEdit(scope.row)"
                v-hasPermi="['system:role:edit']"
                >编辑</el-button
              >
              <el-button
                link
                type="danger"
                icon="Delete"
                v-if="scope.row.roleName != '超级管理员'"
                @click="handleRemove(scope.row)"
                v-hasPermi="['system:role:remove']"
                >删除</el-button
              >
              <el-button
                link
                type="primary"
                icon="CircleCheck"
                v-if="scope.row.roleName != '超级管理员'"
                @click="handleMenuDataScope(scope.row)"
                v-hasPermi="['system:role:edit']"
                >菜单权限</el-button
              >
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
          :default-page-size="defaultPageSize"
          :current-page="config.page"
          class="pager mt-5"
          @size-change="handleSizeChange"
          @current-change="handleCurrentPageChange"
          v-if="config.total != 0"
        />
      </div>
    </el-card>
  </div>

  <!-- 角色菜单弹框 -->
  <Dialog
    :dialogVisible="dialogVisible"
    :action="getAction.action"
    :rowInfo="rowInfo"
    @CloseDialog="CloseDialog"
    @resetGetRoleData="resetGetRoleData"
  ></Dialog>

  <!--菜单权限Dialog -->
  <el-dialog title="角色权限分配" v-model="showRoleScope" width="500px" :before-close="handleClose">
    <el-form :model="form" label-width="80px" ref="roleMenuForm">
      <el-form-item label="菜单搜索">
        <el-input placeholder="请输入关键字进行过滤" v-model="searchText"></el-input>
      </el-form-item>
      <!-- <el-form-item label="权限字符">
          <el-input v-model="form.roleKey" :disabled="true"></el-input>
        </el-form-item> -->
      <el-form-item label="菜单权限">
        <el-checkbox v-model="form.menuExpand" @change="handleCheckedTreeExpand($event, 'menu')">展开/折叠</el-checkbox>
        <el-checkbox v-model="form.menuNodeAll" @change="handleCheckedTreeNodeAll($event, 'menu')"
          >全选/全不选</el-checkbox
        >
        <el-checkbox v-model="form.menuCheckStrictly" @change="handleCheckedTreeConnect($event, 'menu')"
          >父子联动</el-checkbox
        >
        <el-tree
          class="tree-border"
          :data="menuOptions"
          show-checkbox
          ref="menu"
          node-key="id"
          :check-strictly="!form.menuCheckStrictly"
          empty-text="加载中，请稍后"
          :filter-node-method="menuFilterNode"
          :props="defaultProps"
          :default-expand-all="true"
        ></el-tree>
      </el-form-item>
    </el-form>
    <div slot="footer" class="dialog-footer">
      <el-button @click="roleMenuCancel()">取 消</el-button>
      <el-button type="primary" @click="submitRoleScope()">保存</el-button>
    </div>
  </el-dialog>
</template>
<script lang="ts" setup name="role">
import { ElMessage, ElMessageBox } from "element-plus"
import { Search, Refresh } from "@element-plus/icons-vue"
import Dialog from "./components/Dialog.vue"
import { nextTick, onMounted, reactive, ref, watch, onActivated, onBeforeUnmount } from "vue"
import {
  batchDelRoleApi,
  changeRoleEnableApi,
  deleteRoleApi,
  getRoleDataApi,
  updataRoleMenuScopeApi
} from "@/api/system/sys-role"
import { getMenuTreeSelectApi } from "@/api/system/sys-menu"
import { getPageSize, setPageSize } from "@/utils/cache/local-storage"
// #region 定义属性值
/**表格容器引用 */
const tableContanierRef = ref()
/**表格自适应高度 */
const tableHeight = ref()
/**默认页面的条数 */
const defaultPageSize = ref(10)
/**查询角色、分页、搜索角色 */
const config = reactive({
  total: 0,
  page: 1,
  size: 10,
  roleName: ""
})
/**双向绑定，为了关键词搜索 */
const roleFormInline = reactive({
  keywords: ""
})
/** 获取menu的引用*/
const menu = ref()
/**树形组件默认绑定的props */
const defaultProps = {
  id: "id",
  children: "children",
  label: "title"
}
// 复用模态框,新增和编辑
const getAction = reactive({
  action: "add"
})
// 表格数据
const RoleTableData = ref()
// #endregion

// #region 其他操作-生命周期、公共函数
/**封装获取pagesize */
const getCurrentPageSize = () => {
  return getPageSize("role-PageSize")
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
  calculateTableHeight()
  window.addEventListener("resize", calculateTableHeight)
  getRoleData(config)
})
/**页面缓存被激活 */
onActivated(() => {
  if (!isMounted.value) {
    calculateTableHeight()
    window.addEventListener("resize", calculateTableHeight)
    getRoleData(config)
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
// #region 表格操作
/**传给子组件的数据 */
const rowInfo = ref()
/** 分页大小*/
const handleSizeChange = (val: number) => {
  config.size = val
  getRoleData(config)
  setPageSize("role-PageSize", val)
}
// 页面跳转，触发分页条件查询
const handleCurrentPageChange = (page: any) => {
  config.page = page
  getRoleData(config)
}
// 关键词搜索
const handleSearch = () => {
  config.roleName = roleFormInline.keywords
  // config.page = 1
  getRoleData(config)
}
/**重置查询 */
const resetConfig = () => {
  defaultPageSize.value = getCurrentPageSize() || 10
  Object.assign(config, {
    page: 1,
    roleName: ""
  })
}
/**重置搜索表单内容 */
const resetRoleFormInline = () => {
  Object.assign(roleFormInline, {
    keywords: ""
  })
}
/**重置搜索 */
const resetSearch = () => {
  resetRoleFormInline()
  resetConfig()
  getRoleData(config)
}
// 用于子组件重置查询表格
const resetGetRoleData = () => {
  getRoleData(config)
}
// 获取角色数据
const getRoleData = async (config: any) => {
  let res: any = await getRoleDataApi(config)
  if (res) {
    console.log("%c [ roleres ]-206", "font-size:13px; background:pink; color:#bf2c9f;", res)
    config.total = res.data.total
    if (res.data.dataList !== null && res.data.dataList.length !== 0) {
      RoleTableData.value = res.data.dataList.map((item: any) => {
        item.enable = item.enable === 1 ? true : false
        return item
      })
    }
  }
}
//改变角色状态（是否禁用）
const changeRoleStatus = (row: any) => {
  let status = row.enable === true ? "启用" : "禁用"
  ElMessageBox.confirm(`确定要 "${status}"  角色 "${row.roleName}" 吗?`, "系统提示", {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning"
  })
    .then(async () => {
      const id = row.id
      const enable = row.enable === true ? 1 : 0
      let res = await changeRoleEnableApi({
        id: id,
        enable: enable
      })
      if (res) {
        ElMessage({
          type: "success",
          message: `用户 "${row.name}" 状态已修改为 "${status}"`
        })
        getRoleData(config)
      }
    })
    .catch(() => {
      row.enable = row.enable === true ? false : true
    })
}

// 新增按钮
const handleAdd = () => {
  getAction.action = "add"
  dialogVisible.value = true
}
// 编辑按钮
const handleEdit = (row: any) => {
  getAction.action = "edit"
  // 将值设置为 null 以触发 watcher
  rowInfo.value = null
  // 使用 $nextTick 确保在下一个 DOM 更新周期再设置正确的值
  nextTick(() => {
    rowInfo.value = row
  })
  dialogVisible.value = true
}

// 子组件弹框关闭
const CloseDialog = () => {
  dialogVisible.value = false
}

// 删除菜单
const handleRemove = (row: any) => {
  ElMessageBox.confirm("确定要删除吗", "提示", {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning"
  })
    .then(async () => {
      // 删除操作
      let res: any = await deleteRoleApi({ id: row.id })
      if (res) {
        ElMessage({
          showClose: true,
          message: res.message,
          type: "success"
        })
        getRoleData(config)
      }
    })
    .catch(() => {
      // catch error
    })
}
let ids = reactive([]) as any
// 批量选中
const handleSelectionChange = (val: any) => {
  ids = []
  val.forEach((item: any) => {
    ids.push(item.id)
  })
}
// 批量删除
const handleDelList = () => {
  ElMessageBox.confirm("确定要删除吗", "提示", {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning"
  })
    .then(async () => {
      if (ids.length !== 0) {
        let res: any = await batchDelRoleApi(ids)
        if (res) {
          ElMessage({
            showClose: true,
            message: res.message,
            type: "success"
          })
          getRoleData(config)
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
    .catch((err) => {})
}
// #endregion

// #region 对话框-表单操作
const dialogVisible = ref()
// 菜单筛选过滤
const searchText = ref()
// 根据角色ID查询菜单树结构-树形数据
const menuOptions = ref()
/**角色菜单权限对话框是否展示 */
const showRoleScope = ref()
/**菜单权限表单ref */
const roleMenuForm = ref()
/**菜单权限表单 */
const form = reactive({
  roleId: "",
  roleName: "",
  roleKey: "",
  menuCheckStrictly: true,
  menuExpand: true,
  menuNodeAll: false
})

// /**重置表单 */
const reset = () => {
  form.roleId = ""
  form.roleName = ""
  form.roleKey = ""
  form.menuCheckStrictly = true
  form.menuExpand = true
  form.menuNodeAll = false
  menuOptions.value = []
}

/**对话框点击x关闭 */
const handleClose = (done: () => void) => {
  ElMessageBox.confirm("确定关闭吗?", {
    confirmButtonText: "确定",
    cancelButtonText: "取消"
  })
    .then(() => {
      reset()
      searchText.value = ""
      roleMenuForm.value.resetFields()
      done()
    })
    .catch(() => {
      // catch error
    })
}
// 树形控件获取菜单数据信息
const handleMenuDataScope = async (row: any) => {
  reset()
  showRoleScope.value = true
  const roleId = row.id
  const roleMenu = await getRoleMenuTreeselect(roleId)
  // 选中当前角色拥有的菜单权限
  if (roleMenu) {
    const checkedKeys = roleMenu.data.checkKeys
    const uniqueCheckedKeys = Array.from(new Set(checkedKeys))
    checkedKeys.forEach((v: any) => {
      nextTick(() => {
        // proxy?.$refs.menu.setChecked(v, true, false)
        menu.value.setChecked(v, true, false)
      })
    })
  }

  form.roleId = row.id
  form.roleKey = row.description
}

// 取消按钮
const roleMenuCancel = () => {
  showRoleScope.value = false
  searchText.value = ""
  reset()
  roleMenuForm.value.resetFields()
}

/** 根据角色ID查询菜单树结构 */
const getRoleMenuTreeselect = async (roleId: any) => {
  let res: any = await getMenuTreeSelectApi(roleId)
  if (res) {
    menuOptions.value = res.data.menus
  }
  return res
}

/** 监听过滤*/
watch(searchText, (newValue) => {
  // proxy.$refs.menu.filter(newValue)
  menu.value.filter(newValue)
})
// 菜单筛选
const menuFilterNode = (value: any, data: any) => {
  if (!value) return true
  return data.title.indexOf(value) !== -1
}

// 树权限（全选/全不选）
const handleCheckedTreeNodeAll = (value: any, type: any) => {
  if (type == "menu") {
    menu.value.setCheckedNodes(value ? menuOptions.value : [])
  }
}
// 树权限（父子联动）
const handleCheckedTreeConnect = (value: any, type: any) => {
  if (type == "menu") {
    form.menuCheckStrictly = value
  }
}
// 树权限（展开/折叠）
const handleCheckedTreeExpand = (value: any, type: any) => {
  if (type == "menu") {
    const treeList = menuOptions.value
    for (let i = 0; i < treeList.length; i++) {
      menu.value.store.nodesMap[treeList[i].id].expanded = value
    }
  }
}
/**提交菜单权限表单*/
const submitRoleScope = async () => {
  if (form.roleId != undefined) {
    let checkedKeys = menu.value.getCheckedKeys()
    let halfCheckedKeys = menu.value.getHalfCheckedKeys()
    checkedKeys.unshift.apply(checkedKeys, halfCheckedKeys)
    let params = {
      roleId: form.roleId,
      menuIds: checkedKeys
    }
    let res: any = await updataRoleMenuScopeApi(params)
    if (res) {
      ElMessage({
        type: "success",
        message: res.message
      })
      showRoleScope.value = false
      reset()
      roleMenuForm.value.resetFields()
    }
  }
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
  margin-bottom: 0px;
}
.pager-foot {
  display: flex;
  justify-content: flex-end;
}
.user-header {
  display: flex;
  // justify-content: space-between;
  // padding-bottom: -5px;
  // padding-top: -10px;
}

// .el-select {
//   width: 196px;
//   margin-right: 20px;
// }

// 输入框宽度限制
.input_add {
  width: 196px;
  margin-right: 20px;
  &:hover {
    width: 196px;
  }
  &:focus {
    width: 196px;
  }
}
.end {
  justify-content: end;
  margin-bottom: 0px;
}
// .el-dropdown-link {
//   cursor: pointer;
//   align-items: center;
//   color: #409eff;
//   border: none;
//   outline: none;
// }

.tree-border {
  margin-top: 5px;
  border: 1px solid #e5e6e7;
  background: #ffffff none;
  border-radius: 4px;
  width: 380px;
}

.dialog-footer {
  display: flex;
  justify-content: end;
}
</style>
