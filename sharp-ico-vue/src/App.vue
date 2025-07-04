<script setup>
import SharpIco from './components/SharpIco.vue'
import { VERSION, notifications } from '../notifications.js'
import {ElMessageBox, ElNotification} from 'element-plus';
import ThemeToggle from './components/ThemeToggle.vue';
import { useDark } from '@vueuse/core';

const isDark = useDark();
// 检查版本更新
function checkVersionUpdate() {
  ElNotification(notifications.welcome);
  const lastVersion = localStorage.getItem('app_version')

  if (lastVersion !== VERSION) {
    // 版本不一致时显示提示
    localStorage.setItem('app_version', VERSION);

    // 如果是首次访问（无lastVersion）则不提示更新
    if (lastVersion) {
      showUpdateNotification();
    }
  }
}

// 更新提示函数
function showUpdateNotification() {
  ElMessageBox.confirm('检测到新版本，点击按钮更新页面？', '提示', {
    cancelButtonText: '稍后',
    type: 'warning',
    showClose: false,
    showCancelButton: false,
    confirmButtonText: '更新'
  }).then(() => { window.location.reload(); });
}

// 应用启动时检查
checkVersionUpdate();
</script>

<template>
  <div :class="{ dark: isDark }">
    <ThemeToggle />
    <SharpIco />
  </div>
</template>


<style>
@import './assets/styles/theme.css';
</style>