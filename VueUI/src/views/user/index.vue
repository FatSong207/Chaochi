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

          <el-form-item label="角色">
            <el-select
              v-model="searchform.RoleId"
              clearable
              placeholder="請選擇"
            >
              <el-option
                v-for="item in selectRole"
                :key="item.Id"
                :label="item.FullName"
                :value="item.Id"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="用戶資訊：">
            <el-input
              v-model="searchform.Keywords"
              clearable
              placeholder="帳號/姓名/暱稱/電話"
            />
          </el-form-item>
          <el-form-item label="註冊日期：">
            <el-date-picker
              v-model="searchform.CreateTime"
              type="daterange"
              align="right"
              value-format="yyyy-MM-dd"
              unlink-panels
              range-separator="至"
              start-placeholder="開始日期"
              end-placeholder="結束日期"
              :picker-options="pickerOptions"
            />
          </el-form-item>
          <el-form-item>
            <el-button
              type="primary"
              @click="handleSearch()"
            >查詢</el-button>
          </el-form-item>
        </el-form>
      </el-card>
    </div>
    <el-card>
      <div class="list-btn-container">
        <el-button-group>
          <el-button
            v-hasPermi="['User/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['User/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >修改</el-button>
          <el-button
            v-hasPermi="['User/Enable']"
            type="info"
            icon="el-icon-video-pause"
            size="small"
            @click="setEnable('0')"
          >禁用</el-button>
          <el-button
            v-hasPermi="['User/Enable']"
            type="success"
            icon="el-icon-video-play"
            size="small"
            @click="setEnable('1')"
          >啟用</el-button>
          <el-button
            v-hasPermi="['User/DeleteSoft']"
            type="warning"
            icon="el-icon-delete"
            size="small"
            @click="deleteSoft('0')"
          >軟刪除</el-button>
          <el-button
            v-hasPermi="['User/Delete']"
            type="danger"
            icon="el-icon-delete"
            size="small"
            @click="deletePhysics()"
          >刪除</el-button>
          <el-button
            v-hasPermi="['User/ResetPassword']"
            type="default"
            icon="el-icon-refresh-right"
            size="small"
            @click="handleResetPassword()"
          >重置密碼</el-button>
          <el-button
            type="default"
            icon="el-icon-refresh"
            size="small"
            @click="loadTableData()"
          >更新</el-button>
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
        :default-sort="{prop: 'SortCode', order: 'descending'}"
        @select="handleSelectChange"
        @select-all="handleSelectAllChange"
        @sort-change="handleSortChange"
      >
        <el-table-column
          type="selection"
          width="30"
        />
        <el-table-column
          prop="Account"
          label="帳號"
          sortable="custom"
          width="230"
          fixed
        />
        <el-table-column
          prop="RealName"
          label="姓名"
          sortable="custom"
          width="180"
          fixed
        />
        <el-table-column
          prop="NickName"
          label="暱稱"
          sortable="custom"
          width="180"
          fixed
        />
        <el-table-column
          prop="Gender"
          label="性別"
          sortable="custom"
          width="90"
          align="center"
        >
          <template slot-scope="scope">
            {{ scope.row.Gender=== 1 ? '男' : '女' }}
          </template>
        </el-table-column>
        <el-table-column
          prop="Birthday"
          label="生日"
          sortable="custom"
          width="120"
          align="center"
          :formatter="dateformatter"
        />
        <el-table-column
          prop="MobilePhone"
          label="手機號碼"
          sortable="custom"
          width="120"
          align="center"
        />
        <el-table-column
          prop="DepartmentName"
          label="公司/單位"
          width="260"
          align="center"
        >
          <template slot-scope="scope">
            {{ scope.row.OrganizeName+"/"+ scope.row.DepartmentName }}
          </template>
        </el-table-column>
        <el-table-column
          prop="RoleName"
          label="角色"
          sortable="custom"
          width="280"
          align="center"
        />
        <el-table-column
          label="是否啟用"
          sortable="custom"
          width="90"
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
          width="90"
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
          label="添加時間"
          width="180"
          sortable
        />
        <el-table-column
          prop="LastModifyTime"
          label="更新時間"
          width="180"
          sortable
        >
          <template slot-scope="scope">
            {{ scope.row.LastModifyTime }}
          </template>
        </el-table-column>
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
      v-el-drag-dialog
      :title="editFormTitle"
      :visible.sync="dialogEditFormVisible"
      width="660px"
    >
      <el-form
        ref="editFrom"
        :inline="true"
        :model="editFrom"
        :rules="rules"
        class="demo-form-inline"
      >
        <el-form-item
          label="帳號"
          :label-width="formLabelWidth"
          prop="Account"
        >
          <el-input
            v-model="editFrom.Account"
            placeholder="請輸入帳號"
            autocomplete="off"
            :disabled="noEdit"
            clearable
          />
        </el-form-item>
        <el-form-item
          label="姓名"
          :label-width="formLabelWidth"
          prop="RealName"
        >
          <el-input
            v-model="editFrom.RealName"
            placeholder="請輸入姓名"
            autocomplete="off"
            :disabled="noEdit"
            clearable
          />
        </el-form-item>
        <el-form-item
          label="暱稱"
          :label-width="formLabelWidth"
          prop="NickName"
        >
          <el-input
            v-model="editFrom.NickName"
            placeholder="請輸入暱稱"
            autocomplete="off"
            clearable
          />
        </el-form-item>
        <el-form-item
          label="性別"
          :label-width="formLabelWidth"
          prop="Gender"
        >
          <el-radio-group v-model="editFrom.Gender">
            <el-radio :label="1">男</el-radio>
            <el-radio :label="0">女</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item
          label="手機號碼"
          :label-width="formLabelWidth"
          prop="MobilePhone"
        >
          <el-input
            v-model="editFrom.MobilePhone"
            placeholder="請輸入手機號碼"
            autocomplete="off"
            clearable
          />
        </el-form-item>
        <el-form-item
          label="Line ID"
          :label-width="formLabelWidth"
          prop="WeChat"
        >
          <el-input
            v-model="editFrom.WeChat"
            placeholder="請輸入Line ID"
            autocomplete="off"
            clearable
          />
        </el-form-item>
        <el-form-item
          label="Email"
          :label-width="formLabelWidth"
          prop="Email"
        >
          <el-input
            v-model="editFrom.Email"
            placeholder="請輸入Email @"
            autocomplete="off"
            clearable
          />
        </el-form-item>
        <el-form-item
          label="生日"
          :label-width="formLabelWidth"
          prop="Birthday"
        >
          <el-date-picker
            v-model="editFrom.Birthday"
            type="date"
            placeholder="選擇日期"
          />
        </el-form-item>
        <el-form-item
          label="選項"
          :label-width="formLabelWidth"
          prop=""
        >
          <el-checkbox v-model="editFrom.EnabledMark" :disabled="noEdit">啟用</el-checkbox>
          <el-checkbox v-model="editFrom.IsAdministrator" :disabled="noEdit">管理員</el-checkbox>
        </el-form-item>
        <el-form-item
          label="公司/單位"
          :label-width="formLabelWidth"
          prop="DepartmentId"
        >
          <el-cascader
            v-model="selectedOrganizeOptions"
            style="width:500px;"
            :options="selectOrganize"
            filterable
            :props="{label:'FullName',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }"
            clearable
            @change="handleSelectOrganizeChange"
          />
        </el-form-item>
        <el-form-item
          label="角色"
          :label-width="formLabelWidth"
          prop="RoleId"
        >
          <el-select
            v-model="editFrom.RoleId"
            style="width:500px"
            multiple
            clearable
            placeholder="請選擇"
          >
            <el-option
              v-for="item in selectRole"
              :key="item.Id"
              :label="item.FullName"
              :value="item.Id"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          label="備註"
          :label-width="formLabelWidth"
          prop="Description"
        >
          <el-input
            v-model="editFrom.Description"
            style="width:500px"
            placeholder=""
            autocomplete="off"
            clearable
          />
        </el-form-item>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button @click="dialogEditFormVisible = false">關 閉</el-button>
        <el-button
          type="primary"
          @click="saveEditForm()"
        >確 認</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getUserListWithPager,
  getUserDetail,
  saveUser,
  setUserEnable,
  deleteSoftUser,
  deleteUser,
  resetPassword
} from '@/api/security/userservice';
import * as fecha from 'element-ui/lib/utils/date';
import { getAllRoleList } from '@/api/security/roleservice';
import { getAllOrganizeTreeTable } from '@/api/security/organizeservice';

import elDragDialog from '@/directive/el-drag-dialog'; // base on element-ui

export default {
  name: 'User',
  directives: { elDragDialog },
  data() {
    return {
      noEdit: false,
      searchform: {
        RoleId: '',
        Keywords: '',
        CreateTime: ''
      },
      selectRole: [],
      selectedOrganizeOptions: '',
      selectOrganize: [],
      pickerOptions: {
        shortcuts: [
          {
            text: '今天',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime());
              picker.$emit('pick', [start, end]);
            }
          },
          {
            text: '昨天',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 1);
              picker.$emit('pick', [start, end]);
            }
          },
          {
            text: '最近兩天',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 2);
              picker.$emit('pick', [start, end]);
            }
          },
          {
            text: '最近三天',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 3);
              picker.$emit('pick', [start, end]);
            }
          },
          {
            text: '最近一周',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
              picker.$emit('pick', [start, end]);
            }
          },
          {
            text: '最近一個月',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
              picker.$emit('pick', [start, end]);
            }
          },
          {
            text: '最近兩個月',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 60);
              picker.$emit('pick', [start, end]);
            }
          }
        ]
      },
      loadBtnFunc: [],
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
        Account: [
          { required: true, message: '請輸入帳號', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 2 到 16 個字符', trigger: 'blur' }
        ],
        RealName: [
          { required: true, message: '請輸入姓名', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 2 到 50 個字符', trigger: 'blur' }
        ],
        DepartmentId: [
          { required: true, message: '請輸選擇所屬組織', trigger: 'blur' }
        ],
        RoleId: [
          { required: true, message: '請輸選擇崗位角色', trigger: 'blur' }
        ]
      },
      formLabelWidth: '80px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentSelected: []
    };
  },
  created() {
    this.pagination.currentPage = 1;
    this.InitDictItem();
    this.loadTableData();
  },
  methods: {
    /**
     * 初始化數據
     */
    InitDictItem() {
      getAllRoleList().then(res => {
        this.selectRole = res.ResData;
      });
      getAllOrganizeTreeTable().then(res => {
        this.selectOrganize = res.ResData;
      });
    },
    /**
     * 加載頁面table數據
     */
    loadTableData: function() {
      this.tableloading = true;
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.Keywords,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort,
        CreatorTime1:
          this.searchform.CreateTime !== ''
            ? this.searchform.CreateTime[0]
            : '',
        CreatorTime2:
          this.searchform.CreateTime !== ''
            ? this.searchform.CreateTime[1]
            : '',
        RoleId: this.searchform.RoleId
      };
      getUserListWithPager(seachdata).then(res => {
        this.tableData = res.ResData.Items;
        this.pagination.pageTotal = res.ResData.TotalItems;
        this.tableloading = false;
      });
    },
    /**
     * 點擊查詢
     */
    handleSearch: function() {
      this.pagination.currentPage = 1;
      this.loadTableData();
    },
    // 表單重置
    reset() {
      this.editFrom = {
        Account: '',
        RealName: '',
        NickName: '',
        Gender: 1,
        Birthday: '',
        MobilePhone: '',
        Email: '',
        WeChat: '',
        DepartmentId: '',
        RoleId: '',
        IsAdministrator: true,
        EnabledMark: true,
        Description: ''
      };
      this.selectedOrganizeOptions = '';
      this.noEdit = false;
      this.resetForm('editFrom');
    },
    /**
     * 新增、修改或查看明細信息（綁定顯示數據）     *
     */
    ShowEditOrViewDialog: function(view) {
      this.reset();
      if (view !== undefined) {
        if (
          this.currentSelected.length > 1 ||
          this.currentSelected.length === 0
        ) {
          this.$alert('請選擇一條數據進行編輯/修改', '提示');
        } else {
          this.currentId = this.currentSelected[0].Id;
          this.editFormTitle = '編輯';
          this.dialogEditFormVisible = true;
          this.noEdit = true
          this.bindEditInfo();
        }
      } else {
        this.editFormTitle = '新增';
        this.currentId = '';
        this.selectedOrganizeOptions = '';
        this.dialogEditFormVisible = true;
      }
    },
    bindEditInfo: function() {
      getUserDetail(this.currentId).then(res => {
        this.editFrom = res.ResData;
        this.editFrom.RoleId = res.ResData.RoleId.split(',');
        this.selectedOrganizeOptions = res.ResData.DepartmentId;
      });
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editFrom'].validate(valid => {
        if (valid) {
          const data = {
            Account: this.editFrom.Account,
            RealName: this.editFrom.RealName,
            NickName: this.editFrom.NickName,
            Gender: this.editFrom.Gender,
            Birthday: this.editFrom.Birthday,
            MobilePhone: this.editFrom.MobilePhone,
            Email: this.editFrom.Email,
            WeChat: this.editFrom.WeChat,
            DepartmentId: this.editFrom.DepartmentId,
            IsAdministrator: this.editFrom.IsAdministrator,
            EnabledMark: this.editFrom.EnabledMark,
            RoleId: this.editFrom.RoleId.join(','),
            Description: this.editFrom.Description,
            Id: this.currentId
          };

          var url = 'User/Insert';
          if (this.currentId !== '') {
            url = 'User/Update';
          }
          saveUser(data, url).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              });
              this.dialogEditFormVisible = false;
              this.currentSelected = '';
              this.selectedOrganizeOptions = '';
              this.loadTableData();
              this.InitDictItem();
            } else {
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              });
            }
          });
        } else {
          return false;
        }
      });
    },
    setEnable: function(val) {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示');
        return false;
      } else {
        var currentIds = [];
        this.currentSelected.forEach(element => {
          currentIds.push(element.Id);
        });
        const data = {
          Ids: currentIds,
          Flag: val
        };
        setUserEnable(data).then(res => {
          if (res.Success) {
            this.$message({
              message: '恭喜你，操作成功',
              type: 'success'
            });
            this.currentSelected = '';
            this.loadTableData();
          } else {
            this.$message({
              message: res.ErrMsg,
              type: 'error'
            });
          }
        });
      }
    },
    deleteSoft: function(val) {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示');
        return false;
      } else {
        var currentIds = [];
        this.currentSelected.forEach(element => {
          currentIds.push(element.Id);
        });
        const data = {
          Ids: currentIds,
          Flag: val
        };
        deleteSoftUser(data).then(res => {
          if (res.Success) {
            this.$message({
              message: '恭喜你，操作成功',
              type: 'success'
            });
            this.currentSelected = '';
            this.loadTableData();
          } else {
            this.$message({
              message: res.ErrMsg,
              type: 'error'
            });
          }
        });
      }
    },
    deletePhysics: function() {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示');
        return false;
      } else {
        var currentIds = [];
        this.currentSelected.forEach(element => {
          currentIds.push(element.Id);
        });

        this.$confirm('是否確認刪除所選的數據項?', '警告', {
          confirmButtonText: '確定',
          cancelButtonText: '取消',
          type: 'warning'
        })
          .then(function() {
            const data = {
              Ids: currentIds
            };
            return deleteUser(data);
          })
          .then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，刪除成功',
                type: 'success'
              });
              this.currentSelected = '';
              this.loadTableData();
            } else {
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              });
            }
          });
      }
    },
    /**
     * 當表格的排序條件發生變化的時候會觸發該事件
     */
    handleSortChange: function(column) {
      this.sortableData.sort = column.prop;
      if (column.order === 'ascending') {
        this.sortableData.order = 'asc';
      } else {
        this.sortableData.order = 'desc';
      }
      this.loadTableData();
    },
    /**
     * 當用戶手動勾選checkbox數據行事件
     */
    handleSelectChange: function(selection, row) {
      this.currentSelected = selection;
    },
    /**
     * 當用戶手動勾選全選checkbox事件
     */
    handleSelectAllChange: function(selection) {
      this.currentSelected = selection;
    },
    /**
     * 選擇每頁顯示數量
     */
    handleSizeChange(val) {
      this.pagination.pagesize = val;
      this.pagination.currentPage = 1;
      this.loadTableData();
    },
    /**
     * 選擇當頁面
     */
    handleCurrentChange(val) {
      this.pagination.currentPage = val;
      this.loadTableData();
    },

    /**
     *選擇組織
     */
    handleSelectOrganizeChange: function() {
      this.editFrom.DepartmentId = this.selectedOrganizeOptions;
    },
    dateformatter(row, column, cellValue, index) {
      var date = row[column.property];
      return cellValue ? fecha.format(new Date(date), 'yyyy-MM-dd') : '';
    },
    handleResetPassword: function(val) {
      if (
        this.currentSelected.length > 1 ||
        this.currentSelected.length === 0
      ) {
        this.$alert('請先選擇要操作的數據', '提示');
        return false;
      } else {
        const data = {
          userId: this.currentSelected[0].Id
        };
        resetPassword(data).then(res => {
          if (res.Success) {
            this.$alert('重置密碼成功，新密為' + res.ErrMsg, '提醒', {
              confirmButtonText: '確定',
              callback: action => {}
            });
            this.currentSelected = '';
            this.loadTableData();
          } else {
            this.$message({
              message: res.ErrMsg,
              type: 'error'
            });
          }
        });
      }
    }
  }
};
</script>
