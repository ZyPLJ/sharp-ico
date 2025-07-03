<template>
  <div class="ico-info-section" v-if="icoInfo">
    <el-collapse v-model="activeNames">
      <el-collapse-item name="1">
        <template #title>
          <div class="info-header">
            <el-icon><InfoFilled /></el-icon>
            <span>ICO文件信息</span>
            <el-tag size="small" type="primary" class="image-count-tag">
              {{ icoInfo.imageCount }}个图像
            </el-tag>
          </div>
        </template>

        <div class="ico-info-content">
          <div class="file-header">
            <el-icon><Document /></el-icon>
            <span>正在检查ICO文件信息</span>
          </div>

          <el-scrollbar height="350px">
            <div class="images-list">
              <el-card
                  v-for="(img, index) in icoInfo.images"
                  :key="index"
                  class="image-info-card"
                  :class="{ 'has-warning': img.warning }"
              >
                <div class="image-info-header">
                  <span class="image-number">第{{ index + 1 }}张图像</span>
                  <el-tag size="small" :type="getTagType(img)">
                    {{ img.width }}x{{ img.height }}
                  </el-tag>
                </div>

                <div class="image-details">
                  <div class="detail-item">
                    <span class="label">色深:</span>
                    <span>{{ img.bpp }}bpp</span>
                  </div>
                  <div class="detail-item">
                    <span class="label">大小:</span>
                    <span>{{ formatBytes(img.size) }}</span>
                  </div>
                  <div class="detail-item">
                    <span class="label">偏移:</span>
                    <span>{{ img.offset }}</span>
                  </div>
                </div>

                <div v-if="img.warning" class="warning-text">
                  <el-icon><Warning /></el-icon>
                  <span>{{ img.warning }}</span>
                </div>
              </el-card>
            </div>
          </el-scrollbar>
        </div>
      </el-collapse-item>
    </el-collapse>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { InfoFilled, Document, Warning } from '@element-plus/icons-vue';

// 这里假设icoInfo会通过props传入
// 实际使用时替换为您的数据源
const props = defineProps({
  icoInfo: {
    type: Object,
    default: null
  }
});

const activeNames = ref(['1']);

// 根据尺寸确定标签类型
const getTagType = (img) => {
  if (img.width <= 32) return 'info';
  if (img.width <= 128) return 'success';
  if (img.width <= 512) return 'warning';
  return 'danger';
};

// 格式化字节数
const formatBytes = (bytes) => {
  if (bytes < 1024) return bytes + '字节';
  return (bytes / 1024).toFixed(2) + 'KB';
};
</script>

<style scoped>
.ico-info-section {
  margin-top: 20px;
  border-top: 1px dashed #ebeef5;
  padding-top: 20px;
}

.info-header {
  display: flex;
  align-items: center;
  gap: 8px;
  font-weight: 500;
  color: #303133;
}

.image-count-tag {
  margin-left: 12px;
}

.file-header {
  display: flex;
  align-items: center;
  gap: 8px;
  margin-bottom: 16px;
  padding: 8px 0;
  font-size: 15px;
  color: #606266;
  border-bottom: 1px solid #f0f0f0;
}

.images-list {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
  gap: 16px;
  padding: 8px 0;
}

.image-info-card {
  border-radius: 6px;
  transition: all 0.3s;
}

.image-info-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 6px 16px rgba(0, 0, 0, 0.1);
}

.has-warning {
  border: 1px solid #e6a23c;
  background-color: rgba(255, 236, 217, 0.1);
}

.image-info-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 12px;
}

.image-number {
  font-weight: 500;
}

.image-details {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 10px;
  margin-bottom: 10px;
}

.detail-item {
  display: flex;
  flex-direction: column;
  gap: 4px;
}

.label {
  font-size: 12px;
  color: #909399;
}

.warning-text {
  display: flex;
  align-items: center;
  gap: 6px;
  margin-top: 10px;
  padding: 8px;
  border-radius: 4px;
  background-color: #fef0f0;
  color: #f56c6c;
  font-size: 13px;
}

@media (max-width: 768px) {
  .images-list {
    grid-template-columns: 1fr;
  }

  .image-details {
    grid-template-columns: 1fr 1fr;
  }
}
</style>