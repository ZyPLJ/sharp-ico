<!-- src/components/ThemeToggle.vue -->
<template>
  <div class="theme-toggle-container">
    <button
        class="theme-toggle"
        @click="toggleDark()"
        :class="{ 'dark': isDark }"
        aria-label="Toggle theme"
    >
      <span class="theme-toggle-icons">
        <span class="sun-icon">☀️</span>
        <span class="moon-icon">🌙</span>
      </span>
    </button>
  </div>
</template>

<script setup>
import { useDark, useToggle } from '@vueuse/core'

const isDark = useDark({
  selector: 'body',
  attribute: 'class',
  valueDark: 'dark',
  valueLight: 'light'
})

const toggleDark = useToggle(isDark)
</script>

<style scoped>
.theme-toggle-container {
  position: fixed;
  top: 20px;
  right: 20px;
  z-index: 999;
}

.theme-toggle {
  --size: 2.5rem;
  --icon-size: calc(var(--size) * 0.5);
  --transition-duration: 0.3s;
  --transition-easing: cubic-bezier(0.25, 0, 0.3, 1);

  position: relative;
  width: var(--size);
  height: var(--size);
  border-radius: 50%;
  background: var(--color-background);
  border: 1px solid var(--color-border);
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  cursor: pointer;
  overflow: hidden;
  transition:
      background var(--transition-duration) var(--transition-easing),
      box-shadow var(--transition-duration) var(--transition-easing);
}

.theme-toggle:hover {
  box-shadow: 0 3px 8px rgba(0, 0, 0, 0.15);
}

.theme-toggle-icons {
  position: relative;
  display: block;
  width: 100%;
  height: 100%;
}

.sun-icon,
.moon-icon {
  position: absolute;
  top: 50%;
  left: 50%;
  font-size: var(--icon-size);
  line-height: 1;
  transform: translate(-50%, -50%);
  transition:
      opacity var(--transition-duration) var(--transition-easing),
      transform var(--transition-duration) var(--transition-easing);
}

.sun-icon {
  opacity: 1;
  transform: translate(-50%, -50%) rotate(0deg);
}

.moon-icon {
  opacity: 0;
  transform: translate(-50%, -50%) rotate(90deg);
}

.dark .sun-icon {
  opacity: 0;
  transform: translate(-50%, -50%) rotate(-90deg);
}

.dark .moon-icon {
  opacity: 1;
  transform: translate(-50%, -50%) rotate(0deg);
}

/* 添加旋转动画 */
.theme-toggle:active .sun-icon {
  transform: translate(-50%, -50%) rotate(360deg);
}

.theme-toggle:active .moon-icon {
  transform: translate(-50%, -50%) rotate(360deg);
}
</style>