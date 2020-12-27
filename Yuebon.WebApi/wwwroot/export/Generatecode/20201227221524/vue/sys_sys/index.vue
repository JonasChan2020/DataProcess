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
          <el-form-item label="名称：">
            <el-input v-model="searchform.keywords" clearable placeholder="名称" />
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="handleSearch()">查询</el-button>
          </el-form-item>
        </el-form>
      </el-card>
    </div>
    <el-card>
      <div class="list-btn-container">
        <el-button-group>
          <slot v-for="itemf in loadBtnFunc">
            <el-button
              v-if="itemf.FullName==='新增'"
              type="primary"
              icon="el-icon-plus"
              size="small"
              @click="ShowEditOrViewDialog()"
            >新增</el-button>
            <el-button
              v-if="itemf.FullName==='修改'"
              type="primary"
              icon="el-icon-edit"
              class="el-button-modify"
              size="small"
              @click="ShowEditOrViewDialog('edit')"
            >修改</el-button>
            <el-button
              v-if="itemf.FullName=='禁用'"
              type="info"
              icon="el-icon-video-pause"
              size="small"
              @click="setEnable('0')"
            >禁用</el-button>
            <el-button
              v-if="itemf.FullName=='启用'"
              type="success"
              icon="el-icon-video-play"
              size="small"
              @click="setEnable('1')"
            >启用</el-button>
            <el-button
              v-if="itemf.FullName==='软删除'"
              type="warning"
              icon="el-icon-delete"
              size="small"
              @click="deleteSoft('0')"
            >软删除</el-button>
            <el-button
              v-if="itemf.FullName==='删除'"
              type="danger"
              icon="el-icon-delete"
              size="small"
              @click="deletePhysics()"
            >删除</el-button>
          </slot>
          <el-button type="default" icon="el-icon-refresh" size="small" @click="loadTableData()">刷新</el-button>
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
                <el-table-column prop="Syscode" label="系统编码" sortable="custom" width="120" />
        <el-table-column prop="Sysname" label="系统名称" sortable="custom" width="120" />
        <el-table-column prop="Sysclassify" label="系统分类" sortable="custom" width="120" />
        <el-table-column prop="Sysdesc" label="系统描述" sortable="custom" width="120" />
        <el-table-column prop="Orgid" label="所属组织" sortable="custom" width="120" />
        <el-table-column prop="Dsort" label="排序" sortable="custom" width="120" />
        <el-table-column prop="State" label="状态" sortable="custom" width="120" />
        <el-table-column prop="Cuid" label="创建人" sortable="custom" width="120" />
        <el-table-column prop="Ctime" label="创建时间" sortable="custom" width="120" />
        <el-table-column prop="Uuid" label="更新人" sortable="custom" width="120" />
        <el-table-column prop="Utime" label="更新时间" sortable="custom" width="120" />

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
      :title="editFormTitle+'{TableNameDesc}'"
      :visible.sync="dialogEditFormVisible"
      width="640px"
    >
      <el-form ref="editFrom" :model="editFrom" :rules="rules">
                <el-form-item label="系统编码" :label-width="formLabelWidth" prop="Syscode">
          <el-input v-model="editFrom.Syscode" placeholder="请输入系统编码" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="系统名称" :label-width="formLabelWidth" prop="Sysname">
          <el-input v-model="editFrom.Sysname" placeholder="请输入系统名称" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="系统分类" :label-width="formLabelWidth" prop="Sysclassify">
          <el-input v-model="editFrom.Sysclassify" placeholder="请输入系统分类" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="系统描述" :label-width="formLabelWidth" prop="Sysdesc">
          <el-input v-model="editFrom.Sysdesc" placeholder="请输入系统描述" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="所属组织" :label-width="formLabelWidth" prop="Orgid">
          <el-input v-model="editFrom.Orgid" placeholder="请输入所属组织" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="排序" :label-width="formLabelWidth" prop="Dsort">
          <el-input v-model="editFrom.Dsort" placeholder="请输入排序" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="状态" :label-width="formLabelWidth" prop="State">
          <el-input v-model="editFrom.State" placeholder="请输入状态" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="创建人" :label-width="formLabelWidth" prop="Cuid">
          <el-input v-model="editFrom.Cuid" placeholder="请输入创建人" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="创建时间" :label-width="formLabelWidth" prop="Ctime">
          <el-input v-model="editFrom.Ctime" placeholder="请输入创建时间" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="更新人" :label-width="formLabelWidth" prop="Uuid">
          <el-input v-model="editFrom.Uuid" placeholder="请输入更新人" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="更新时间" :label-width="formLabelWidth" prop="Utime">
          <el-input v-model="editFrom.Utime" placeholder="请输入更新时间" autocomplete="off" clearable />
        </el-form-item>

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogEditFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="saveEditForm()">确 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>

import { getSys_sysListWithPager, getSys_sysDetail,
  saveSys_sys, setSys_sysEnable, deleteSoftSys_sys,
  deleteSys_sys } from '@/api/sys_sys/sys_sys'

export default {
  data() {
    return {
      searchform: {
        keywords: ''
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
      editFrom: {
                Syscode: '',
        Sysname: '',
        Sysclassify: '',
        Sysdesc: '',
        Orgid: '',
        Dsort: '',
        State: '',
        Cuid: '',
        Ctime: '',
        Uuid: '',
        Utime: '',

      },
      rules: {
        
      },
      formLabelWidth: '80px',
      currentId: '', // 当前操作对象的ID值，主要用于修改
      currentSelected: []
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
     * 初始化数据
     */
    InitDictItem() {
    },
    /**
     * 加载页面table数据
     */
    loadTableData: function() {
      this.tableloading = true
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.keywords,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      }
      getSys_sysListWithPager(seachdata).then(res => {
        this.tableData = res.ResData.Items
        this.pagination.pageTotal = res.ResData.TotalItems
        this.tableloading = false
      })
    },
    /**
     * 点击查询
     */
    handleSearch: function() {
      this.pagination.currentPage = 1
      this.loadTableData()
    },

    /**
     * 新增、修改或查看明细信息（绑定显示数据）     *
     */
    ShowEditOrViewDialog: function(view) {
      if (view !== undefined) {
        if (this.currentSelected.length > 1 || this.currentSelected.length === 0) {
          this.$alert('请选择一条数据进行编辑/修改', '提示')
        } else {
          this.currentId = this.currentSelected[0].Id
          this.editFormTitle = '编辑'
          this.dialogEditFormVisible = true
          this.bindEditInfo()
        }
      } else {
        this.editFormTitle = '新增'
        this.currentId = ''
        this.dialogEditFormVisible = true
      }
    },
    bindEditInfo: function() {
      getSys_sysDetail(this.currentId).then(res => {
               this.editFrom.Syscode = res.ResData.Syscode
        this.editFrom.Sysname = res.ResData.Sysname
        this.editFrom.Sysclassify = res.ResData.Sysclassify
        this.editFrom.Sysdesc = res.ResData.Sysdesc
        this.editFrom.Orgid = res.ResData.Orgid
        this.editFrom.Dsort = res.ResData.Dsort
        this.editFrom.State = res.ResData.State
        this.editFrom.Cuid = res.ResData.Cuid
        this.editFrom.Ctime = res.ResData.Ctime
        this.editFrom.Uuid = res.ResData.Uuid
        this.editFrom.Utime = res.ResData.Utime

      })
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          const data = {
                   'Syscode':this.editFrom.Syscode,
        'Sysname':this.editFrom.Sysname,
        'Sysclassify':this.editFrom.Sysclassify,
        'Sysdesc':this.editFrom.Sysdesc,
        'Orgid':this.editFrom.Orgid,
        'Dsort':this.editFrom.Dsort,
        'State':this.editFrom.State,
        'Cuid':this.editFrom.Cuid,
        'Ctime':this.editFrom.Ctime,
        'Uuid':this.editFrom.Uuid,
        'Utime':this.editFrom.Utime,

            'Id': this.currentId
          }
          saveSys_sys(data).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
              this.dialogEditFormVisible = false
              this.currentSelected = ''
              this.$refs['editFrom'].resetFields()
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
        this.$alert('请先选择要操作的数据', '提示')
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
        setSys_sysEnable(data).then(res => {
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
        this.$alert('请先选择要操作的数据', '提示')
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
        deleteSoftSys_sys(data).then(res => {
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
        this.$alert('请先选择要操作的数据', '提示')
        return false
      } else {
        var currentIds = []
        this.currentSelected.forEach(element => {
          currentIds.push(element.Id)
        })
        const data = {
          Ids: currentIds
        }
        deleteSys_sys(data).then(res => {
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
    /**
     * 当表格的排序条件发生变化的时候会触发该事件
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
     * 当用户手动勾选checkbox数据行事件
     */
    handleSelectChange: function(selection, row) {
      this.currentSelected = selection
    },
    /**
     * 当用户手动勾选全选checkbox事件
     */
    handleSelectAllChange: function(selection) {
      this.currentSelected = selection
    },
    /**
     * 选择每页显示数量
     */
    handleSizeChange(val) {
      this.pagination.pagesize = val
      this.pagination.currentPage = 1
      this.loadTableData()
    },
    /**
     * 选择当页面
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
