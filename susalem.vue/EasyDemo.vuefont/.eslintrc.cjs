import globals from 'globals'
import pluginJs from '@eslint/js'
import tseslint from 'typescript-eslint'
import pluginVue from 'eslint-plugin-vue'
import configPrettier from 'eslint-config-prettier'
import pluginPrettier from 'eslint-plugin-prettier'
import parser from '@typescript-eslint/parser'

export default [
  pluginJs.configs.recommended,
  ...tseslint.configs.recommended,
  ...pluginVue.configs['flat/essential'],
  {
    files: ['**/*.ts?(x)'],
    ignores: ['**/dist/**', '**/node_modules/**'],
    languageOptions: {
      parser: parser,
      globals: {
        ...globals.commonjs,
        ...globals.browser,
        ...globals.es2021,
        ...globals.node
      }
    },
    // 🟡 recommended.plugins: ['prettier']
    plugins: {
      prettier: pluginPrettier
    },
    rules: {
      // 🟡 recommended.extends: ['prettier']
      ...configPrettier.rules,
      // 🟡 recommended.rules: { ... }
      ...pluginPrettier.configs.recommended.rules,

      // 🟡 一些自己的自定义 rules
      'prettier/prettier': 'warn',
      '@typescript-eslint/no-unused-vars': 'off',
      'no-unused-vars': 'off',

      // eslint（https://eslint.bootcss.com/docs/rules/）
      'no-var': 'error', // 要求使用 let 或 const 而不是 var
      'no-multiple-empty-lines': ['warn', { max: 1 }], // 不允许多个空行
      'no-unexpected-multiline': 'error', // 禁止空余的多行
      'no-useless-escape': 'off', // 禁止不必要的转义字符

      // typeScript (https://typescript-eslint.io/rules)
      '@typescript-eslint/prefer-ts-expect-error': 'error', // 禁止使用 @ts-ignore
      '@typescript-eslint/no-explicit-any': 'off', // 禁止使用 any 类型
      '@typescript-eslint/no-non-null-assertion': 'off',
      '@typescript-eslint/no-namespace': 'off', // 禁止使用自定义 TypeScript 模块和命名空间。
      '@typescript-eslint/semi': 'off',

      // eslint-plugin-vue (https://eslint.vuejs.org/rules/)
      'vue/multi-word-component-names': 'off', // 要求组件名称始终为 “-” 链接的单词
      'vue/script-setup-uses-vars': 'error', // 防止<script setup>使用的变量<template>被标记为未使用
      'vue/no-mutating-props': 'off', // 不允许组件 prop的改变
      'vue/attribute-hyphenation': 'off' //
    }
  }
]
