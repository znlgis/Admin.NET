<template>
    <el-card v-bind="$attrs">
        <template #header>
            <div class="cardpro-header">
                <div v-if="$slots.prefix || prefixIcon" class="cardpro-header-prefix">
                    <slot v-if="$slots.prefix" name="prefix" />
                    <el-icon v-else-if="prefixIcon">
                        <component :is="prefixIcon" />
                    </el-icon>
                </div>
                <div :class="['cardpro-header-title', sizeClass]">
                    <span>{{ props.title }}</span>
                </div>
                <div v-if="$slots.suffix || suffix" class="cardpro-header-suffix">
                    <slot v-if="$slots.suffix" name="suffix" />
                    <span v-else-if="props.suffix">{{ props.suffix }}</span>
                </div>
            </div>
        </template>
        
        <template v-for="(value, name) in $slots" #[name]="scopedData">
            <slot :name="name" v-bind="scopedData"></slot>
        </template>
    </el-card>
</template>

<script setup lang="ts" name="CardPro">
import { computed } from 'vue';
import { storeToRefs } from 'pinia';
import { useSizeProp } from 'element-plus'
import { useThemeConfig } from '/@/stores/themeConfig';

const storesThemeConfig = useThemeConfig();
const { themeConfig } = storeToRefs(storesThemeConfig);

const props = defineProps({
    title: { type: String },
    prefixIcon: { type: String },
    suffix: { type: String },
    size: useSizeProp
});

// 获取全局组件大小
const getGlobalComponentSize = computed(() => {
	return themeConfig.value.globalComponentSize;
});

const sizeClass = computed(() => {
    if(props.size) return  "cardpro-" + props.size;
    else return "cardpro-" + getGlobalComponentSize.value;
});
</script>

<style lang="scss" scoped>
.cardpro-header {
    display: flex;
    align-items: center;

    &-prefix {
        height: 100%;
        margin-right: 5px;
        display: flex;
        align-items: center;
    }
    &-title { flex: 1 }
    &-suffix {
        height: 100%;
        margin-left: 5px;
    }
}

.cardpro-small {
    padding: 10px 0;
}
.cardpro-default {
    padding: 15px 0;
}
.cardpro-large {
    padding: 20px 0;
}

:deep(.el-card__header) {
    padding: 0 20px;
}
</style>
