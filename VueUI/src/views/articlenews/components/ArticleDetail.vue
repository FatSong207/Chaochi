<template>
  <div>
    <el-form ref="editFrom" :model="editFrom" :rules="rules">
      <el-row>
        <el-col :span="12">
          <el-form-item label="文章標題" :label-width="formLabelWidth" prop="Title">
            <slot v-if="showType==='show'">{{ editFrom.Title }}</slot>
            <el-input v-else v-model="editFrom.Title" placeholder="請輸入文章標題" autocomplete="off" clearable />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="副標題" :label-width="formLabelWidth" prop="SubTitle">
            <slot v-if="showType==='show'">{{ editFrom.SubTitle }}</slot>
            <el-input v-else v-model="editFrom.SubTitle" placeholder="請輸入文章副標題" autocomplete="off" clearable />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="文章分類" :label-width="formLabelWidth" prop="CategoryId">
            <slot v-if="showType==='show'">{{ editFrom.CategoryName }}</slot>
            <el-cascader
              v-else
              v-model="selectedCategoryOptions"
              style="width: 380px"
              :options="selectCategory"
              filterable
              :props="{label: 'Title',
                       value: 'Id',
                       children: 'Children',
                       emitPath: false,
                       checkStrictly: true,
                       expandTrigger: 'hover',
              }"
              clearable
              @change="handleSelectCategoryChange"
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="外網" :label-width="formLabelWidth" prop="LinkUrl">
            <slot v-if="showType==='show'">{{ editFrom.LinkUrl }}</slot>
            <el-input v-else v-model="editFrom.LinkUrl" placeholder="請輸入外網網址" autocomplete="off" clearable />
          </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-form-item label="詳情" :label-width="formLabelWidth" prop="Description">
            <div v-if="showType==='show'" v-html="editFrom.Description" />
            <Tinymce v-else ref="editorDescription" v-model="editFrom.Description" :height="400" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="排序" :label-width="formLabelWidth" prop="SortCode">
            <slot v-if="showType==='show'">{{ editFrom.SortCode }}</slot>
            <el-input v-else v-model="editFrom.SortCode" placeholder="請輸入排序" autocomplete="off" clearable />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="屬性" :label-width="formLabelWidth" prop="">
            <el-tag v-if="showType==='show' &&editFrom.IsTop">置頂</el-tag>
            <slot v-else-if="showType==='show' &&!editFrom.IsTop" />
            <el-checkbox v-else v-model="editFrom.IsTop">置頂</el-checkbox>
            <el-tag v-if="showType==='show' &&editFrom.IsHot">熱門</el-tag>
            <el-tag v-else-if="showType==='show' &&!editFrom.IsHot" />
            <el-checkbox v-else v-model="editFrom.IsHot">熱門</el-checkbox>
            <el-tag v-if="showType==='show' &&editFrom.IsRed">推薦</el-tag>
            <slot v-else-if="showType==='show' &&!editFrom.IsRed" />
            <el-checkbox v-else v-model="editFrom.IsRed">推薦</el-checkbox>
            <el-tag v-if="showType==='show' &&editFrom.IsNew">最新</el-tag>
            <slot v-else-if="showType==='show' &&!editFrom.IsNew" />
            <el-checkbox v-else v-model="editFrom.IsNew">最新</el-checkbox>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="是否發布" :label-width="formLabelWidth" prop="EnabledMark">
            <slot v-if="showType==='show' &&editFrom.EnabledMark">是</slot>
            <slot v-else-if="showType==='show' &&!editFrom.EnabledMark">否</slot>
            <el-radio-group v-else v-model="editFrom.EnabledMark">
              <el-radio :label="true">是</el-radio>
              <el-radio :label="false">否</el-radio>
            </el-radio-group>
          </el-form-item>
        </el-col>
      </el-row>
    </el-form>
    <div v-if="showType!=='show'" class="yuebon-page-footer">
      <el-button @click="reset">重置</el-button>
      <el-button v-preventReClick type="primary" @click="saveEditForm">存 檔</el-button>
    </div>
  </div>
</template>

<script>

import { getArticlenewsDetail, saveArticlenews } from '@/api/cms/articlenews'
import { GetAllCategoryTreeTable } from '@/api/cms/articlecategory'
import Tinymce from '@/components/Tinymce'
export default {
  name: 'ArticleDetail',
  components: {
    Tinymce
  },
  data() {
    return {
      editFrom: {},
      rules: {
        CategoryId: [
          { required: true, message: '請輸入文章分類', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 2 到 50 個字符', trigger: 'blur' }
        ],
        Title: [
          { required: true, message: '請輸入文章標題', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 2 到 50 個字符', trigger: 'blur' }
        ]
      },
      selectedCategoryOptions: '',
      selectCategory: [],
      formLabelWidth: '80px',
      currentId: '', // 當前操作對象的ID值，主要用于修改、查看
      showType: 'edit' // 操作類型編輯、新增、查看
    }
  },
  created() {
    this.InitDictItem()
    this.reset()
  },
  methods: {
    /**
     * 初始化數據
     */
    InitDictItem() {
      GetAllCategoryTreeTable().then(res => {
        this.selectCategory = res.ResData
      })
      if (this.$route.params && this.$route.params.id && this.$route.params.id !== 'null') {
        this.currentId = this.$route.params.id
        this.showType = this.$route.params.showtype
        this.bindEditInfo()
      }
    },

    /**
     * 選擇分類
     */
    handleSelectCategoryChange: function() {
      this.editFrom.CategoryId = this.selectedCategoryOptions
    },

    // 表單重置
    reset() {
      if (!this.currentId) {
        this.editFrom = {
          CategoryId: '',
          Title: '',
          Description: '',
          SortCode: 99,
          EnabledMark: true
        }
        this.selectedCategoryOptions = ''
        this.resetForm('editFrom')
      } else {
        this.bindEditInfo()
      }
    },
    bindEditInfo: function() {
      getArticlenewsDetail(this.currentId).then(res => {
        this.editFrom = res.ResData
        this.$refs.editorDescription.setContent(res.ResData.Description)
        this.selectedCategoryOptions = res.ResData.CategoryId
      })
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          var url = 'Articlenews/Insert'
          if (this.currentId !== '') {
            url = 'Articlenews/Update'
          }
          saveArticlenews(this.editFrom, url).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
              this.currentSelected = ''
              this.$store.state.tagsView.visitedViews.splice(this.$store.state.tagsView.visitedViews.findIndex(item => item.path === this.$route.path), 1)
              this.$router.push(this.$store.state.tagsView.visitedViews[this.$store.state.tagsView.visitedViews.length - 1].path)
            } else {
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              })
            }
          })
        } else {
          return false
        }
      })
    }
  }
}
</script>
<style lang="scss" scoped>
.yuebon-page-footer{
    padding: 10px 20px 20px;
    text-align: right;
    -webkit-box-sizing: border-box;
    box-sizing: border-box;
    & button{
        margin-right: 10px;
    }
}
</style>
