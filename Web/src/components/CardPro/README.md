# `CardPro` 说明

`CardPro`卡片组件，基于`ElCard`二次封装，增加了大小、标题、图标等属性，可以直接写属性值实现，无需像官方版本一样通过`solt`实现。组件继承了官方版的所有属性、插槽，你也可以按官方版本一样的使用。

---

## 如何使用

### 基本用法

卡片标题、内容以及图标，通过 `prefix-icon` 属性设置卡片图标。与官方版本不同的时，`CardPro` 通过 `title`
来设置标题，如果你使用 `header` 来设置，它将覆盖 `CardPro` 组件的特有属性，与官方版本完全一致。

```html
<template>
    <card-pro title="标题" prefix-icon="ele-DocumentCopy">
        <!-- 你的内容 -->
    </card-pro>
</template>
<script lang="ts" setup>
import CardPro from '/@/components/CardPro/index.vue';
</script>
```

### 后缀

通过 `suffix` 属性设置卡片右上角文本，你也可以通过 `solt` 来实现自定义内容

```html
<template>
    <card-pro title="标题" suffix="more >>>">
        <!-- 你的内容 -->
    </card-pro>
</template>
<script lang="ts" setup>
import CardPro from '/@/components/CardPro/index.vue';
</script>
```

使用  `solt` 方式来自定义后缀内容，你可以在其中添加按钮、链接、图标、输入框等

```html
<template>
    <card-pro title="标题">
        <template #suffix>
           <el-button type="primary" icon="ele-Refresh" round plain @click="reset">刷新</el-button>
        </template>

        <!-- 你的内容 -->
    </card-pro>
</template>
<script lang="ts" setup>
import CardPro from '/@/components/CardPro/index.vue';
</script>
```

### 不同尺寸

使用 `size` 属性配置尺寸，可使用 `large`、`default` 和 `small`

```html
<template>
    <card-pro title="标题" size="small">
        <!-- 你的内容 -->
    </card-pro>
</template>
<script lang="ts" setup>
import CardPro from '/@/components/CardPro/index.vue';
</script>
```

---

最新更新于 2025.10.10
