<template>
  <div class="app-container">
    <div class="filter-container">
      <el-card>
        <el-form
          ref="searchform"
          :inline="true"
          :model="searchform"
          class="demo-form-inline"
          size="small"
        >
          <el-form-item label="業務單據名稱：">
            <el-input v-model="searchform.name" clearable placeholder="業務單據名稱" />
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="handleSearch()">查詢</el-button>
          </el-form-item>
        </el-form>
      </el-card>
    </div>
    <el-card>
      <div class="list-btn-container">
        <el-button-group>
          <el-button v-hasPermi="['Sequence/Add']" type="primary" icon="el-icon-plus" size="small" @click="ShowEditOrViewDialog()">新增</el-button>
          <el-button v-hasPermi="['Sequence/Edit']" type="primary" icon="el-icon-edit" class="el-button-modify" size="small" @click="ShowEditOrViewDialog('edit')">修改</el-button>
          <el-button v-hasPermi="['Sequence/Enable']" type="info" icon="el-icon-video-pause" size="small" @click="setEnable('0')">禁用</el-button>
          <el-button v-hasPermi="['Sequence/Enable']" type="success" icon="el-icon-video-play" size="small" @click="setEnable('1')">啟用</el-button>
          <el-button v-hasPermi="['Sequence/DeleteSoft']" type="warning" icon="el-icon-delete" size="small" @click="deleteSoft('0')">刪除暫存</el-button>
          <el-button v-hasPermi="['Sequence/Delete']" type="danger" icon="el-icon-delete" size="small" @click="deletePhysics()">刪除</el-button>
          <el-button type="default" icon="el-icon-refresh" size="small" @click="loadTableData()">更新</el-button>
        </el-button-group>
      </div>
      <el-table
        ref="gridtable"
        v-loading="tableloading"
        :data="tableData"
        border
        stripe
        highlight-current-row
        style="width: 100%"
        :default-sort="{prop: 'SortCode', order: 'ascending'}"
        @select="handleSelectChange"
        @select-all="handleSelectAllChange"
        @sort-change="handleSortChange"
      >
        <el-table-column type="selection" width="30" />
        <el-table-column prop="SequenceName" label="業務單據名稱" sortable="custom" width="180" />
        <el-table-column prop="SequenceDelimiter" label="分隔符號" sortable="custom" width="90" />
        <el-table-column prop="SequenceReset" label="重置規則" sortable="custom" width="120">
          <template slot-scope="scope">
            <el-tag v-if="scope.row.SequenceReset==='D'">每天</el-tag>
            <el-tag v-else-if="scope.row.SequenceReset==='M'">每月</el-tag>
            <el-tag v-else-if="scope.row.SequenceReset==='Y'">每年</el-tag>
            <el-tag v-else />
          </template>
        </el-table-column>
        <el-table-column prop="Step" label="步長" sortable="custom" width="80" />
        <el-table-column prop="CurrentNo" label="當前數值" sortable="custom" width="90" />
        <el-table-column prop="CurrentCode" label="當前編號" sortable="custom" width="200" />
        <el-table-column prop="CurrentReset" label="當前重置依賴" sortable="custom" width="150" />
        <el-table-column prop="Description" label="說明" sortable="custom" width="280" />
        <el-table-column
          label="是否啟用"
          sortable="custom"
          width="120"
          prop="EnabledMark"
          align="center"
        >
          <template slot-scope="scope">
            <el-tag
              :type="scope.row.EnabledMark === true ? 'success' : 'info'"
              disable-transitions
            >{{ scope.row.EnabledMark===true?'啟用':'禁用' }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column
          label="是否刪除"
          sortable="custom"
          width="120"
          prop="DeleteMark"
          align="center"
        >
          <template slot-scope="scope">
            <el-tag
              :type="scope.row.DeleteMark === true ? 'danger' : 'success'"
              disable-transitions
            >{{ scope.row.DeleteMark===true?'已刪除':'否' }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column
          prop="CreatorTime"
          label="建立時間"
          sortable
          width="160"
        />
        <el-table-column
          prop="LastModifyTime"
          label="更新時間"
          sortable
          width="160"
        />
      </el-table>
      <div class="pagination-container">
        <el-pagination
          background
          :current-page="pagination.currentPage"
          :page-sizes="[5,10,20,50,100, 200, 300, 400]"
          :page-size="pagination.pagesize"
          layout="total, sizes, prev, pager, next, jumper"
          :total="pagination.pageTotal"
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
        />
      </div>
    </el-card>
    <el-dialog
      ref="dialogEditForm"
      :title="editFormTitle"
      :visible.sync="dialogEditFormVisible"
      width="640px"
    >
      <el-form ref="editFrom" :model="editFrom" :rules="rules">
        <el-form-item label="單據名稱" :label-width="formLabelWidth" prop="SequenceName">
          <el-input v-model="editFrom.SequenceName" placeholder="請輸入業務單據名稱" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="分隔符號" :label-width="formLabelWidth" prop="SequenceDelimiter">
          <el-input v-model="editFrom.SequenceDelimiter" placeholder="請輸入分隔符號" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="重置規則" :label-width="formLabelWidth" prop="SequenceReset">
          <el-select v-model="editFrom.SequenceReset" clearable placeholder="請選重置規則">
            <el-option
              v-for="item in selectSequenceReset"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            />
          </el-select>
        </el-form-item>
        <el-form-item label="步長" :label-width="formLabelWidth" prop="Step">
          <el-input v-model="editFrom.Step" placeholder="請輸入步長" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="當前數值" :label-width="formLabelWidth" prop="CurrentNo">
          <el-input v-model="editFrom.CurrentNo" placeholder="請輸入當前數值" autocomplete="off" readonly="readonly" />
        </el-form-item>
        <el-form-item label="當前編號" :label-width="formLabelWidth" prop="CurrentCode">
          <el-input v-model="editFrom.CurrentCode" placeholder="請輸入當前編號" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="是否啟用" :label-width="formLabelWidth" prop="EnabledMark">
          <el-radio-group v-model="editFrom.EnabledMark">
            <el-radio :label="true">是</el-radio>
            <el-radio :label="false">否</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="說明" :label-width="formLabelWidth" prop="Description">
          <el-input v-model="editFrom.Description" autocomplete="off" clearable />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogEditFormVisible = false">關 閉</el-button>
        <el-button type="primary" @click="saveEditForm()">確 認</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>

import { getSequenceListWithPager, getSequenceDetail,
  saveSequence, setSequenceEnable, deleteSoftSequence,
  deleteSequence } from '@/api/security/sequence'

export default {
  name: 'Sequence',
  data() {
    return {
      searchform: {
        name: ''
      },
      loadBtnFunc: [],
      selectedOrganizeOptions: '',
      selectOrganize: [],
      selectSequenceType: [],
      tableData: [],
      tableloading: true,
      pagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      sortableData: {
        order: 'desc',
        sort: 'CreatorTime'
      },
      dialogEditFormVisible: false,
      editFormTitle: '',
      editFrom: {},
      rules: {
        SequenceName: [
          { required: true, message: '請輸入業務單據名稱', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 2 到 50 個字符', trigger: 'blur' }
        ]
      },
      formLabelWidth: '80px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentSelected: [],
      selectSequenceReset: [{
        value: 'D',
        label: '每天'
      }, {
        value: 'M',
        label: '每月'
      }, {
        value: 'Y',
        label: '每年'
      }
      ]
    }
  },
  created() {
    this.pagination.currentPage = 1
    this.InitDictItem()
    this.loadTableData()
    this.loadBtnFunc = JSON.parse(localStorage.getItem('yueboncurrentfuns'))
  },
  methods: {
    /**
     * 初始化數據
     */
    InitDictItem() {
    },
    /**
     * 加載頁面table數據
     */
    loadTableData: function() {
      this.tableloading = true
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.name,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      }
      getSequenceListWithPager(seachdata).then(res => {
        this.tableData = res.ResData.Items
        this.pagination.pageTotal = res.ResData.TotalItems
        this.tableloading = false
      })
    },
    /**
     * 點擊查詢
     */
    handleSearch: function() {
      this.pagination.currentPage = 1
      this.loadTableData()
    },
    // 表單重置
    reset() {
      if (!this.currentId) {
        this.editFrom = {
          SequenceName: '',
          SequenceDelimiter: '',
          SequenceReset: '',
          Step: 1,
          CurrentNo: 0,
          CurrentCode: '',
          CurrentReset: '',
          Description: '',
          EnabledMark: true
        }
        this.resetForm('editFrom')
      } else {
        this.bindEditInfo()
      }
    },

    /**
     * 新增、修改或查看明細信息（綁定顯示數據）     *
     */
    ShowEditOrViewDialog: function(view) {
      if (view !== undefined) {
        if (this.currentSelected.length > 1 || this.currentSelected.length === 0) {
          this.$alert('請選擇一條數據進行編輯/修改', '提示')
        } else {
          this.currentId = this.currentSelected[0].Id
          this.editFormTitle = '編輯'
          this.dialogEditFormVisible = true
        }
      } else {
        this.editFormTitle = '新增'
        this.currentId = ''
        this.dialogEditFormVisible = true
      }
      this.reset()
    },
    bindEditInfo: function() {
      getSequenceDetail(this.currentId).then(res => {
        this.editFrom = res.ResData
      })
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          const data = {
            'SequenceName': this.editFrom.SequenceName,
            'SequenceDelimiter': this.editFrom.SequenceDelimiter,
            'SequenceReset': this.editFrom.SequenceReset,
            'Step': this.editFrom.Step,
            'CurrentNo': this.editFrom.CurrentNo,
            'CurrentCode': this.editFrom.CurrentCode,
            'CurrentReset': this.editFrom.CurrentReset,
            'EnabledMark': this.editFrom.EnabledMark,
            'Description': this.editFrom.Description,
            'Id': this.currentId
          }

          var url = 'Sequence/Insert'
          if (this.currentId !== '') {
            url = 'Sequence/Update'
          }
          saveSequence(data, url).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
              this.dialogEditFormVisible = false
              this.currentSelected = ''
              this.reset()
              this.loadTableData()
              this.InitDictItem()
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
    },
    setEnable: function(val) {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示')
        return false
      } else {
        var currentIds = []
        this.currentSelected.forEach(element => {
          currentIds.push(element.Id)
        })
        const data = {
          Ids: currentIds,
          Flag: val
        }
        setSequenceEnable(data).then(res => {
          if (res.Success) {
            this.$message({
              message: '恭喜你，操作成功',
              type: 'success'
            })
            this.currentSelected = ''
            this.loadTableData()
          } else {
            this.$message({
              message: res.ErrMsg,
              type: 'error'
            })
          }
        })
      }
    },
    deleteSoft: function(val) {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示')
        return false
      } else {
        var currentIds = []
        this.currentSelected.forEach(element => {
          currentIds.push(element.Id)
        })
        const data = {
          Ids: currentIds,
          Flag: val
        }
        deleteSoftSequence(data).then(res => {
          if (res.Success) {
            this.$message({
              message: '恭喜你，操作成功',
              type: 'success'
            })
            this.currentSelected = ''
            this.loadTableData()
          } else {
            this.$message({
              message: res.ErrMsg,
              type: 'error'
            })
          }
        })
      }
    },
    deletePhysics: function() {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示')
        return false
      } else {
        var currentIds = []
        this.currentSelected.forEach(element => {
          currentIds.push(element.Id)
        })
        this.$confirm('是否確認刪除所選的數據項?', '警告', {
          confirmButtonText: '確定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(function() {
          const data = {
            Ids: currentIds
          }
          return deleteSequence(data)
        }).then(res => {
          if (res.Success) {
            this.$message({
              message: '恭喜你，刪除成功',
              type: 'success'
            })
            this.currentSelected = ''
            this.loadTableData()
          } else {
            this.$message({
              message: res.ErrMsg,
              type: 'error'
            })
          }
        })
      }
    },
    /**
     * 當表格的排序條件發生變化的時候會觸發該事件
     */
    handleSortChange: function(column) {
      this.sortableData.sort = column.prop
      if (column.order === 'ascending') {
        this.sortableData.order = 'asc'
      } else {
        this.sortableData.order = 'desc'
      }
      this.loadTableData()
    },
    /**
     * 當用戶手動勾選checkbox數據行事件
     */
    handleSelectChange: function(selection, row) {
      this.currentSelected = selection
    },
    /**
     * 當用戶手動勾選全選checkbox事件
     */
    handleSelectAllChange: function(selection) {
      this.currentSelected = selection
    },
    /**
     * 選擇每頁顯示數量
     */
    handleSizeChange(val) {
      this.pagination.pagesize = val
      this.pagination.currentPage = 1
      this.loadTableData()
    },
    /**
     * 選擇當頁面
     */
    handleCurrentChange(val) {
      this.pagination.currentPage = val
      this.loadTableData()
    }
  }
}
</script>

<style>
</style>
