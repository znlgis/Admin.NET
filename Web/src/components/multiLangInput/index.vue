<template>
    <div class="multi-lang-input">
        <el-input v-model="props.modelValue" :placeholder="`请输入 ${currentLangLabel}`" clearable>
            <template #append>
                <el-button @click="openDialog" circle>
                    <template #icon>
                        <i class="iconfont icon-diqiu1"></i>
                    </template>
                </el-button>
            </template>
        </el-input>

        <el-dialog v-model="dialogVisible" title="多语言设置" width="600px">
            <el-form ref="ruleFormRef" label-width="auto">
                <el-row :gutter="35">
                    <el-col v-for="lang in languages" :key="lang.code" :span="24">
                        <el-form-item :label="lang.label">
                            <el-input v-model="multiLangValue[lang.code]" :placeholder="`请输入: ${lang.label}`"
                                clearable />
                        </el-form-item>
                    </el-col>
                </el-row>
            </el-form>

            <template #footer>
                <el-button @click="aiTranslation">AI翻译</el-button>
                <el-button @click="closeDialog">关闭</el-button>
                <el-button type="primary" @click="confirmDialog">确认修改</el-button>
            </template>
        </el-dialog>
    </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, reactive } from 'vue';
import { useLangStore } from '/@/stores/useLangStore';
import { Local } from '/@/utils/storage';
import { getAPI } from '/@/utils/axios-utils';
import { SysLangTextApi } from '/@/api-services/api';
import { ElMessage } from 'element-plus';

const ruleFormRef = ref();

const fetchMultiLang = async () => {
    const result = await getAPI(SysLangTextApi).apiSysLangTextListPost({ entityName: props.entityName, entityId: props.entityId, fieldName: props.fieldName, pageSize: 200 }).then(res => res.data.result)
    return result ?? [];
};


const props = defineProps<{
    modelValue: string;
    entityName: string;
    entityId: string;
    fieldName: string;
}>();

// 全局语言
const langStore = useLangStore();
const languages = ref<any>([] as any);

// 当前语言（可根据用户设置或浏览器设置）
const currentLang = ref('zh_CN');
const activeLang = ref('zh_CN');

// 是否弹框
const dialogVisible = ref(false);

// 多语言对象
const multiLangValue = ref<Record<string, string>>({});

// 当前语言显示 Label
const currentLangLabel = computed(() => {
    return (
        languages.value.find((l: { code: string; }) => l.code === currentLang.value)?.Label || currentLang.value
    );
});

// 初始化语言
onMounted(async () => {
    if (langStore.languages.length === 0) {
        await langStore.loadLanguages();
    }
    const themeConfig = Local.get('themeConfig');
    const globalI18n = themeConfig?.globalI18n;
    if (globalI18n) {
        const matched = langStore.languages.find(l => l.code === globalI18n);
        const langCode = matched?.code || 'zh_CN';
        currentLang.value = langCode;
        activeLang.value = langCode;
    }
    languages.value = langStore.languages;

    if (languages.value.length > 0) {
        currentLang.value = languages.value[0].code;
        activeLang.value = languages.value[0].code;
    }
});
const aiTranslation = async () => {
    languages.value.forEach(async (element: { code: string | number; value: string | number; }) => {
        if (element.code == currentLang.value) {
            return;
        }
        multiLangValue.value[element.code] = '正在翻译...';
        try {
            const text = await getAPI(SysLangTextApi).apiSysLangTextAiTranslateTextPost({ originalText: props.modelValue, targetLang: element.value }).then(res => res.data.result);
            if (text) {
                multiLangValue.value[element.code] = text;
            } else {
                multiLangValue.value[element.code] = '';
            }
        } catch (e: any) {
            multiLangValue.value[element.code] = '';
            ElMessage.warning(e.message);
        }
    });
}

// 打开对话框（点击按钮）
const openDialog = async () => {
    multiLangValue.value = {};
    const res = await fetchMultiLang();
    multiLangValue.value[currentLang.value] = props.modelValue;
    res.forEach((element: { langCode: string | number; content: string; }) => {
        multiLangValue.value[element.langCode] = element.content;
    });
    dialogVisible.value = true;
    ruleFormRef.value?.resetFields();
};

// 关闭对话框（只是关闭）
const closeDialog = () => {
    dialogVisible.value = false;
    multiLangValue.value = {};
    ruleFormRef.value?.resetFields();
};

// 确认按钮（更新 + 关闭）
const confirmDialog = async () => {
    const langItems = Object.entries(multiLangValue.value)
        .filter(([_, content]) => content && content.trim() !== '')
        .map(([code, content]) => ({
            EntityName: props.entityName,
            EntityId: props.entityId,
            FieldName: props.fieldName,
            LangCode: code,
            Content: content,
        }));

    if (langItems.length === 0) {
        ElMessage.warning('请输入至少一条多语言内容！');
        return;
    }

    try {
        await getAPI(SysLangTextApi).apiSysLangTextBatchSavePost(langItems);
        ElMessage.success('保存成功！');
        dialogVisible.value = false;
    } catch (err) {
        console.error(err);
        ElMessage.error('保存失败！');
    }
    dialogVisible.value = false;
    ruleFormRef.value?.resetFields();
};
</script>

<style scoped>
.multi-lang-input {
    width: 100%;
}
</style>
