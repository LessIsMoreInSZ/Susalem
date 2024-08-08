<!-- <script setup lang="ts">
import HelloWorld from './components/HelloWorld.vue'
</script>

<template>
  <div>
    <a href="https://vitejs.dev" target="_blank">
      <img src="/vite.svg" class="logo" alt="Vite logo" />
    </a>
    <a href="https://vuejs.org/" target="_blank">
      <img src="./assets/vue.svg" class="logo vue" alt="Vue logo" />
    </a>
  </div>
  <HelloWorld msg="Vite + Vue" />
</template>

<style scoped>
.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
  transition: filter 300ms;
}
.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}
.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style> -->

<template>
  <!-- 搜索栏 -->
  <div style="margin: 10px 0">
      <el-input v-model="name" style="width:300px;" placeholder="请输入名称" />
      <el-input v-model="address" placeholder="请输入地址" style="width:300px;margin-left:10px;" />
      <el-button type="primary" style="margin-left:5px;" @click="load">
          <el-icon style="vertical-align: middle">
              <Search />
          </el-icon> <span style="vertical-align: middle"> 搜索 </span>
      </el-button>
      <el-button type="warning" style="margin-left:5px;" @click="reset">
          <el-icon style="vertical-align: middle">
              <RefreshLeft />
          </el-icon> <span style="vertical-align: middle"> 重置 </span>
      </el-button>
  </div>
  <!-- 新增栏菜单 -->
  <div style="margin: 10px 0">
      <el-button type="success" @click="handleAdd">
          <el-icon style="vertical-align: middle">
              <Plus />
          </el-icon> <span style="vertical-align: middle"> 新增 </span>
      </el-button>
      <!-- <el-upload :show-file-list="false" style="display: inline-block; position: relative; top: 3px;margin-left:5px;"
          :action='`http://127.0.0.1/user/import`' :on-success="handleImportSuccess"
          :headers="{ Authorization: token }">
          <el-button type="primary">
              <el-icon style="vertical-align: middle">
                  <Top />
              </el-icon> <span style="vertical-align: middle"> 导入 </span>
          </el-button>
      </el-upload> -->
      <el-button type="primary">
              <el-icon style="vertical-align: middle">
                  <Top />
              </el-icon> <span style="vertical-align: middle"> 导入 </span>
          </el-button>
      <el-button type="primary" @click="exportData" style="margin-left:5px;">
          <el-icon style="vertical-align: middle">
              <Top />
          </el-icon> <span style="vertical-align: middle"> 导出 </span>
      </el-button>
      <el-popconfirm title="您确定删除吗？" @confirm="confirmDelBatch">
          <template #reference>
              <el-button type="danger" style="margin-left: 5px">
                  <el-icon style="vertical-align: middle">
                      <Remove />
                  </el-icon> <span style="vertical-align: middle"> 批量删除 </span>
              </el-button>
          </template>
      </el-popconfirm>
  </div>
  <!-- 表格栏 -->
  <div style="margin: 10px 0">
      <el-table :data="currentTableData"  :fit="true" border @selection-change="handleSelectionChange">
          <el-table-column type="selection" width="55" />
          <el-table-column prop="id" label="ID"></el-table-column>
          <el-table-column prop="recipeName" label="配方名"></el-table-column>
          <el-table-column prop="level" label="Level"></el-table-column>
          <el-table-column prop="state" label="State"></el-table-column>
          <!-- <el-table-column label="操作" width="180">
              <template #default="scope">
                  <el-button type="primary" @click="handleEdit(scope.row)">编辑</el-button>
                  <el-popconfirm title="您确定删除吗？" @confirm="del(scope.row.id)">
                      <template #reference>
                          <el-button type="danger">删除</el-button>
                      </template>
                  </el-popconfirm>
              </template>
          </el-table-column> -->
      </el-table>
  </div>
  <!-- 分页 -->
  <div style="margin: 10px 0">
      <el-pagination @current-change="load" @size-change="load" v-model:current-page="pageNum"
          v-model:page-size="pageSize" background :page-sizes="[2, 5, 10, 20]"
          layout="total, sizes, prev, pager, next, jumper" :total="total" />
  </div>
  <!-- 新增 -->
  <el-dialog v-model="dialogFormVisible" title="新增用户信息" width="40%">
      <el-form ref="ruleFormRef" :rules="rules" :model="state.form" label-width="80px" style="padding: 0 20px"
          status-icon>
          <!-- <el-form-item prop="id" label="ID">
              <el-input v-model="state.form.id" autocomplete="off" />
          </el-form-item> -->
          <el-form-item prop="text" label="标题">
              <el-input v-model="state.form.text" autocomplete="off" />
          </el-form-item>
      </el-form>
      <template #footer>
          <span class="dialog-footer">
              <el-button @click="dialogFormVisible = false">取消</el-button>
              <el-button type="primary" @click="save">
                  保存
              </el-button>
          </span>
      </template>
  </el-dialog>
  <!-- 新增end -->
  <!-- 修改 -->
  <el-dialog v-model="dialogFormVisibleUpdate" title="新增用户信息" width="40%">
      <el-form ref="ruleFormRef" :rules="rules" :model="state.form" label-width="80px" style="padding: 0 20px"
          status-icon>
          <el-form-item prop="id" label="ID">
              <el-input v-model="state.form.id" autocomplete="off" disable />
          </el-form-item>
          <el-form-item prop="text" label="配方名">
              <el-input v-model="state.form.RecipeName" autocomplete="off" />
          </el-form-item>
          <el-form-item prop="text" label="Level">
              <el-input v-model="state.form.Level" autocomplete="off" />
          </el-form-item>
          <el-form-item prop="text" label="State">
              <el-input v-model="state.form.State" autocomplete="off" />
          </el-form-item>
      </el-form>
      <template #footer>
          <span class="dialog-footer">
              <el-button @click="dialogFormVisibleUpdate = false">取消</el-button>
              <el-button type="primary" @click="saveUpdate">
                  保存
              </el-button>
          </span>
      </template>
  </el-dialog>
  <!-- 修改end -->
</template>
<script setup>
import { nextTick, reactive, ref ,onMounted} from "vue";
// 引入通知
import { ElMessage } from "element-plus";
import axios from "axios"
// import { findByPage } from "../../api/emo.js";

const name = ref('')
const address = ref('')
const pageNum = ref(1)
const pageSize = ref(10)
const total = ref(0)

/**当前表格数据 */
const currentTableData = ref()

// 定义表格数据，请求后台方法
const state = reactive({
  tableData: [],
  form: {}
})
/** 页面初始化*/
onMounted(() => {
  load()
})

const load = () => {
    //axios.get('https://localhost:7268/Recipe/GetAllRecipes')
   axios.get('https://localhost:7268/Recipe/GetAllRecipes')
   .then(res => {
    // console.log(res.data)
    //    console.log(res.data.data.dataList)
      currentTableData.value = res.data.data.dataList.$values
       console.log(res.data.data.dataList)
       console.log(currentTableData.value)
     // state.tableData = res.data
    //   state.tableData = res.page.list
    //   state.tableData = res.page.list
    //   total.value = res.page.totalCount
  })

}
// 调用 load方法拿到后台数据
load()


// 重置搜索框方法
const reset = () => {
  name.value = ''
  address.value = ''
  load()
}
// -------------------------------------------------------------------------------------------------------------------------------------------
// 控制新增弹框显示隐藏
const dialogFormVisible = ref(false)

// 新增方法
const handleAdd = () => {
  dialogFormVisible.value = true
  ruleFormRef.value.resetFields()
  state.form = {}
}
// 添加效验规则
const rules = reactive({
  id: [
      { required: true, message: '请输入id', trigger: 'blur' },
      { min: 3, max: 20, message: '长度在3-20之间', trigger: 'blur' },
  ],
  text: [
      { required: true, message: '请输入text', trigger: 'blur' },
  ],
})
const ruleFormRef = ref()
// 新增保存方法
const save = () => {
  ruleFormRef.value.validate(valid => {   // valid就是校验的结果
      if (valid) {
        axios.post('https://localhost:7268/Recipe/AddRecipe', state.form).then(res => {
              console.log(res)
              if (res.code === 200) {
                  ElMessage.success('保存成功')
                  dialogFormVisible.value = false
                  load()  // 刷新表格数据
              } else {
                  ElMessage.error(res.msg)
              }
          })
      }
  })
}
// -------------------------------------------------------------------------------------------------------------------------------------------
// 修改
const dialogFormVisibleUpdate = ref(false)
// 编辑触发方法
const handleEdit = (raw) => {
  dialogFormVisibleUpdate.value = true
  nextTick(() => {
      ruleFormRef.value.resetFields()
      state.form = JSON.parse(JSON.stringify(raw))
  })
}
// 修改保存方法
const saveUpdate = () => {
  // ruleFormRef.value.validate(valid => {   // valid就是校验的结果
  //     if (valid) {
  //     }
  // })
  axios.request.post('https://localhost:7268/emo/updateById', state.form).then(res => {
      console.log(res)
      if (res.code === 200) {
          ElMessage.success('保存成功')
          dialogFormVisibleUpdate.value = false
          load()  // 刷新表格数据
      } else {
          ElMessage.error(res.msg)
      }
  })
}
// -------------------------------------------------------------------------------------------------------------------------------------------
// 删除
const del = (id) => {
  axios.request.delete('/emo/del/' + id).then(res => {
      if (res.code === 200) {
          ElMessage.success('操作成功')
          load()  // 刷新表格数据
      } else {
          ElMessage.error(res.msg)
      }
  })
}
// -------------------------------------------------------------------------------------------------------------------------------------------
// 批量删除
const multipleSelection = ref([])

const handleSelectionChange = (val) => {
  multipleSelection.value = val
}
// 批量删除方法
const confirmDelBatch = () => {
  if (!multipleSelection.value || !multipleSelection.value.length) {
      ElMessage.warning("请选择数据")
      return
  }
  const idArr = multipleSelection.value.map(v => v.id)
  axios.request.post('/user/del/batch', idArr).then(res => {
      if (res.code === '200') {
          ElMessage.success('操作成功')
          load()  // 刷新表格数据
      } else {
          ElMessage.error(res.msg)
      }
  })
}
// -------------------------------------------------------------------------------------------------------------------------------------------
// 导出接口
const exportData = () => {
  //   window.open(`http://${config.serverUrl}/user/export`)
  window.open(`https://localhost:7268/Recipe/GetAllRecipes`)
}
</script>

<style>
th {
  background-color: aliceblue !important;
}
</style>
