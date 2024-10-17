<template>
  <div class="icon-body">
    <el-input
      v-model="iconName"
      style="position: relative"
      clearable
      placeholder="请输入图标名称"
      @clear="filterIcons"
      @input="filterIcons"
    >
      <template #prefix>
        <el-icon class="el-input__icon">
          <search />
        </el-icon>
      </template>
      <template #suffix>
        <el-icon class="el-input__icon" @click="selectedIcon('')">
          <delete />
        </el-icon>
      </template>
    </el-input>
    <el-tabs v-model="activeName" @tab-change="handleTabChange">
      <el-tab-pane label="Svg-Icon" name="1">
        <div class="icon-list">
          <div
            class="icon-item mb10"
            v-for="(item, index) in allIcons.icons"
            :key="index"
            @click="selectedIcon(item, '')"
          >
            <svg-icon :name="item" style="height: 30px; width: 16px" />
            <div class="name">{{ item }}</div>
          </div>
        </div>
      </el-tab-pane>
      <el-tab-pane label="Element-UI Icons" name="2">
        <div class="icon-list">
          <div
            class="icon-item mb10"
            v-for="item of allIcons.elementIcons"
            :key="item"
            @click="selectedIcon(item, 'ele-')"
          >
            <!-- <component :is="item" style="height: 30px; width: 16px"></component> -->
            <svg-icon :name="'ele-' + item" style="height: 30px; width: 16px" />
            <div class="name">{{ item }}</div>
          </div>
        </div>
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<script lang="ts" setup>
import icons from "./requireIcons"
import * as elIcons from "@element-plus/icons-vue"
import { all } from "axios"
import { ref, reactive } from "vue"

const elementIcons = ref([]) as any
for (const key in elIcons) {
  elementIcons.value.push(key)
}
const iconName = ref("")
const iconList = ref(icons)
const activeName = ref("1")
const emit = defineEmits(["selectedSvgIcon"])

/**图标保存 */
const allIcons = reactive({
  icons: icons,
  elementIcons: elementIcons.value
})

/**图标搜索 */
function filterIcons() {
  if (activeName.value === "1") {
    allIcons.icons = icons
    if (iconName.value) {
      allIcons.icons = icons.filter((item: any) => item.indexOf(iconName.value) !== -1)
    }
  } else if (activeName.value === "2") {
    allIcons.elementIcons = elementIcons.value
    if (iconName.value) {
      allIcons.elementIcons = elementIcons.value.filter(
        (item: any) => item.toLowerCase().indexOf(iconName.value.toLowerCase()) !== -1
      )
    }
  }
}

function selectedIcon(name: any, prefix?: any) {
  const iconName = prefix != undefined ? prefix + name : name
  emit("selectedSvgIcon", iconName)
  document.body.click()
}

/**tab切换,触发图标查询 */
const handleTabChange = (val: any) => {
  if (iconName.value) {
    filterIcons()
  }
}

function reset() {
  iconName.value = ""
  allIcons.icons = icons
  allIcons.elementIcons = elementIcons.value
}

defineExpose({
  reset
})
</script>

<style lang="scss" scoped>
.icon-body {
  width: 100%;
  padding: 10px;
  .icon-list {
    overflow-y: scroll;
    display: grid;
    flex-wrap: wrap;
    height: 200px;
    grid-template-columns: repeat(5, 90px);

    .icon-item {
      cursor: pointer;
      text-align: center;
    }
  }
}
</style>
