<!-- 组件使用文档： https://gitee.com/zuohuaijun/Admin.NET/pulls/1559  -->
<script setup lang="ts">
import { reactive, watch, PropType } from 'vue';
import { DictItem } from '/@/types/global';
import { useUserInfo } from '/@/stores/userInfo';

const emit = defineEmits(['update:modelValue']);
const dictList = useUserInfo().dictList;
const props = defineProps({
	modelValue: {
		type: [String, Number, Boolean, Array, null],
		default: null,
		required: true,
	},
	code: {
		type: String,
		required: true,
	},
	propLabel: {
		type: String,
		default: 'label',
	},
	propValue: {
		type: String,
		default: 'value',
	},
	onItemFilter: {
		type: Function,
		default: (dict: any): boolean => dict,
	},
	onItemFormatter: {
		type: Function as PropType<(dict: any) => string | undefined | null>,
		default: () => undefined,
	},
	renderAs: {
		type: String,
		default: 'tag',
		validator(value: string) {
			return ['tag', 'select', 'radio', 'checkbox'].includes(value);
		},
	},
	multiple: {
		type: Boolean,
		default: false,
	},
});

const state = reactive({
	dict: {} as DictItem | DictItem[] | undefined,
	dictData: [] as DictItem[],
	value: null as any,
});

const setDictValue = (value: any) => {
	state.value = value;
	state.dictData = dictList[props.code]?.filter(props.onItemFilter) ?? [];

	if (Array.isArray(value)) {
		state.dict = state.dictData.filter((x: any) => value.includes(x[props.propValue]));
		if (state.dict) {
			state.dict.forEach((item: any) => {
				if (!['success', 'warning', 'info', 'primary', 'danger'].includes(item.tagType ?? '')) {
					item.tagType = 'primary';
				}
			});
		}
	} else {
		state.dict = state.dictData.find((x: any) => x[props.propValue] == state.value);
		if (state.dict && !['success', 'warning', 'info', 'primary', 'danger'].includes((state.dict as DictItem).tagType ?? '')) {
			(state.dict as DictItem).tagType = 'primary';
		}
	}
};

watch(
	() => props.modelValue,
	(newValue) => setDictValue(newValue?.toString()),
	{ immediate: true }
);
</script>

<template>
	<!-- 渲染标签 -->
	<template v-if="props.renderAs === 'tag'">
		<template v-if="Array.isArray(state.dict)">
			<el-tag v-for="(item, index) in state.dict" :key="index" v-bind="$attrs" :type="item.tagType" :style="item.styleSetting" :class="item.classSetting" class="mr-1">
				{{ onItemFormatter(item) ?? item[props.propLabel] }}
			</el-tag>
		</template>
		<template v-else>
			<el-tag v-if="state.dict" v-bind="$attrs" :type="state.dict.tagType" :style="state.dict.styleSetting" :class="state.dict.classSetting">
				{{ onItemFormatter(state.dict) ?? state.dict[props.propLabel] }}
			</el-tag>
			<span v-else>{{ state.value }}</span>
		</template>
	</template>
	<!-- 渲染选择器 -->
	<template v-if="props.renderAs === 'select'">
		<el-select v-model="state.value" v-bind="$attrs" :multiple="props.multiple" @change="(newValue: any) => emit('update:modelValue', newValue)">
			<el-option v-for="(item, index) in state.dictData" :key="index" :label="onItemFormatter(item) ?? item[props.propLabel]" :value="item[props.propValue]" />
		</el-select>
	</template>
	<!-- 渲染复选框（多选） -->
	<template v-if="props.renderAs === 'checkbox'">
		<el-checkbox-group v-model="state.value" v-bind="$attrs" @change="(newValue: any) => emit('update:modelValue', newValue)">
			<el-checkbox v-for="(item, index) in state.dictData" :key="index" :label="item[props.propValue]">
				{{ onItemFormatter(item) ?? item[props.propLabel] }}
			</el-checkbox>
		</el-checkbox-group>
	</template>
	<!-- 渲染单选框 -->
	<template v-if="props.renderAs === 'radio'">
		<el-radio-group v-model="state.value" v-bind="$attrs" @change="(newValue: any) => emit('update:modelValue', newValue)">
			<el-radio v-for="(item, index) in state.dictData" :key="index" :value="item[props.propValue]">
				{{ onItemFormatter(item) ?? item[props.propLabel] }}
			</el-radio>
		</el-radio-group>
	</template>
</template>

<style scoped lang="scss"></style>
