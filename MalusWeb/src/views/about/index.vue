<script setup lang="ts">
import { computed } from 'vue';
import { $t } from '@/locales';
import { useAppStore } from '@/store/modules/app';
import pkg from '~/package.json';
import dotnetjson from '~/dotnetneget.json';

const appStore = useAppStore();

const column = computed(() => (appStore.isMobile ? 1 : 2));

interface PkgJson {
  name: string;
  version: string;
  dependencies: PkgVersionInfo[];
  devDependencies: PkgVersionInfo[];
}

interface PkgVersionInfo {
  name: string;
  version: string;
}

const { name, version, dependencies, devDependencies } = pkg;
const { packages } = dotnetjson;

console.log('packages', packages);

function transformVersionData(tuple: [string, string]): PkgVersionInfo {
  const [$name, $version] = tuple;
  return {
    name: $name,
    version: $version
  };
}

const pkgJson: PkgJson = {
  name,
  version,
  dependencies: Object.entries(dependencies).map(item => transformVersionData(item)),
  devDependencies: Object.entries(devDependencies).map(item => transformVersionData(item))
};

const latestBuildTime = BUILD_TIME;
</script>

<template>
  <NSpace vertical :size="16">
    <NCard :title="$t('page.about.title')" :bordered="false" size="small" segmented class="card-wrapper">
      <!-- <NDivider /> -->
      <NH3 style="text-align: center">MalusAdmin</NH3>
      <div>
        <P>Malus是海棠的意思，顾名思义，海棠后台管理系统，读音与【马卢斯】相近，也可称作为马卢斯后台管理系统。</P>
        <br />
        <P>
          基于NET Core | NET7/8 & Sqlsugar | Vue3 | vite4 | TypeScript | NaiveUI
          开发的前后端分离式权限管理系统,采用最原生最简洁的方式来实现, 前端清新优雅高颜值，后端
          结构清晰，优雅易懂，功能强大,提供快速开发的解决方案。
        </P>
        <br />
        <P>后端：基于 .NET7 和 sqlsugar ，集成常用组件，从0到1搭建。</P>
        <br />
        <P>前端：基于 Soybean Admin 做适配，主技术栈：Vue3、NaiveUI 版本</P>
        <br />
        <p>极简的项目依赖，简洁清爽的目录结构，代码注释方便上手</p>
        <br />
      </div>
      <div>
        <ul>
          <li>
            公众号: （持续分享编程干货和有趣的知识）
            <a
              href="https://mp.weixin.qq.com/s/7zwCvOroFsBwKNmmZR17RA"
              class="text-primary"
              target="_blank"
              rel="noopener noreferrer"
            >
              Netshare
            </a>
          </li>
          <li>
            开源地址：
            <a
              href="https://gitee.com/Pridejoy/MalusAdmin"
              class="text-primary"
              target="_blank"
              rel="noopener noreferrer"
            >
              https://gitee.com/Pridejoy/MalusAdmin
            </a>
          </li>
          <li>
            后端文档：
            <a href="https://www.dotnetshare.com/" class="text-primary" target="_blank" rel="noopener noreferrer">
              https://www.dotnetshare.com/
            </a>
          </li>
          <li>
            前端文档：
            <a href="https://docs.soybeanjs.cn/zh/" class="text-primary" target="_blank" rel="noopener noreferrer">
              https://docs.soybeanjs.cn
            </a>
          </li>
          <li>
            Naive UI 文档：
            <a
              href="https://www.naiveui.com/zh-CN/os-theme/components/button"
              class="text-primary"
              target="_blank"
              rel="noopener noreferrer"
            >
              https://www.naiveui.com
            </a>
          </li>
          <li>
            SqlSugar 文档：
            <a href="https://www.donet5.com" class="text-primary" target="_blank" rel="noopener noreferrer">
              https://www.donet5.com
            </a>
          </li>
        </ul>
        <br />
      </div>
    </NCard>
    <NCard :title="$t('page.about.projectInfo.title')" :bordered="false" size="small" segmented class="card-wrapper">
      <NDescriptions label-placement="left" bordered size="small" :column="column">
        <NDescriptionsItem :label="$t('page.about.projectInfo.version')">
          <NTag type="primary">{{ pkgJson.version }}</NTag>
        </NDescriptionsItem>
        <NDescriptionsItem :label="$t('page.about.projectInfo.latestBuildTime')">
          <NTag type="primary">{{ latestBuildTime }}</NTag>
        </NDescriptionsItem>
        <NDescriptionsItem :label="$t('page.about.projectInfo.githubLink')">
          <a
            class="text-primary"
            href="https://github.com/pridejoy/MalusAdmin"
            target="_blank"
            rel="noopener noreferrer"
          >
            https://github.com/pridejoy/MalusAdmin
          </a>
        </NDescriptionsItem>
        <NDescriptionsItem label="Gitee">
          <a
            class="text-primary"
            href="https://gitee.com/Pridejoy/MalusAdmin"
            target="_blank"
            rel="noopener noreferrer"
          >
            https://gitee.com/Pridejoy/MalusAdmin
          </a>
        </NDescriptionsItem>
      </NDescriptions>
    </NCard>

    <NCard title="后端依赖" :bordered="false" size="small" segmented class="card-wrapper">
      <NDescriptions label-placement="left" bordered size="small" :column="column">
        <NDescriptionsItem v-for="item in packages" :key="item.name" :label="item.name">
          {{ item.version }}
        </NDescriptionsItem>
      </NDescriptions>
    </NCard>

    <NCard :title="$t('page.about.prdDep')" :bordered="false" size="small" segmented class="card-wrapper">
      <NDescriptions label-placement="left" bordered size="small" :column="column">
        <NDescriptionsItem v-for="item in pkgJson.dependencies" :key="item.name" :label="item.name">
          {{ item.version }}
        </NDescriptionsItem>
      </NDescriptions>
    </NCard>
    <NCard :title="$t('page.about.devDep')" :bordered="false" size="small" segmented class="card-wrapper">
      <NDescriptions label-placement="left" bordered size="small" :column="column">
        <NDescriptionsItem v-for="item in pkgJson.devDependencies" :key="item.name" :label="item.name">
          {{ item.version }}
        </NDescriptionsItem>
      </NDescriptions>
    </NCard>
  </NSpace>
</template>

<style scoped></style>
