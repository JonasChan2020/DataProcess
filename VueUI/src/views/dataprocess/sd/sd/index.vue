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
          <el-button
            v-hasPermi="['Sd_sysdb/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['Sd_sysdb/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >修改</el-button>
          <el-button
            v-hasPermi="['Sd_sysdb/Enable']"
            type="info"
            icon="el-icon-video-pause"
            size="small"
            @click="setEnable('0')"
          >禁用</el-button>
          <el-button
            v-hasPermi="['Sd_sysdb/Enable']"
            type="success"
            icon="el-icon-video-play"
            size="small"
            @click="setEnable('1')"
          >启用</el-button>
          <el-button
            v-hasPermi="['Sd_sysdb/DeleteSoft']"
            type="warning"
            icon="el-icon-delete"
            size="small"
            @click="deleteSoft('0')"
          >软删除</el-button>
          <el-button
            v-hasPermi="['Sd_sysdb/Delete']"
            type="danger"
            icon="el-icon-delete"
            size="small"
            @click="deletePhysics()"
          >删除</el-button>
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
        <el-table-column prop="SdName" label="名称" sortable="custom" width="120" />
        <el-table-column prop="Sdtype" label="类型" sortable="custom" width="120" />
        <el-table-column prop="Classify_id" label="分类" sortable="custom" width="260" align="center">
          <template slot-scope="scope">
            {{ scope.row.Classify_Name }}
          </template>
        </el-table-column>
        <el-table-column prop="dbName" label="库名称" sortable="custom" width="120" />
        <el-table-column prop="Description" label="描述" sortable="custom" width="120" />
        <el-table-column prop="Sys_Name" label="所属系统" sortable="custom" width="120" />
        <el-table-column label="是否主库" sortable="custom" width="120" prop="Is_maindb" align="center">
          <template slot-scope="scope">
            <el-tag :type="scope.row.Is_maindb === true ? 'success' : 'info'" disable-transitions>{{ scope.row.Is_maindb === true ? "是" : "否" }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="SortCode" label="排序字段" sortable="custom" width="90" align="center" />
        <el-table-column label="是否启用" sortable="custom" width="120" prop="EnabledMark" align="center">
          <template slot-scope="scope">
            <el-tag :type="scope.row.EnabledMark === true ? 'success' : 'info'" disable-transitions>{{ scope.row.EnabledMark === true ? "启用" : "禁用" }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="操作" sortable="custom" width="120" align="center">
          <template slot-scope="scope">
            <el-button
              type="primary"
              icon="el-icon-plus"
              size="small"
              @click="UpdateDbContents(scope.row.Id)"
            >更新结构信息</el-button>
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
      :title="editFormTitle+'连接'"
      :visible.sync="dialogEditFormVisible"
      width="640px"
    >
      <el-form ref="editFrom" :model="editFrom" :rules="rules" class="demo-form-inline">
        <el-form-item label="连接名称" :label-width="formLabelWidth" prop="SdName">
          <el-input v-model="editFrom.SdName" placeholder="请输入连接名称" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="数据库类型" :label-width="formLabelWidth" prop="Sdtype">
          <el-input v-model="editFrom.Sdtype" placeholder="请输入数据库类型" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="连接分类" :label-width="formLabelWidth" prop="Classify_id">
          <el-cascader v-model="selectedclass" style="width:500px;" :options="selectclasses" filterable :props="{label:'Dtname',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }" clearable @change="handleSelectClassChange" />
        </el-form-item>
        <el-form-item label="描述" :label-width="formLabelWidth" prop="Description">
          <el-input v-model="editFrom.Description" placeholder="请输入描述" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="主机地址" :label-width="formLabelWidth" prop="HostAddress">
          <el-input v-model="editFrom.HostAddress" placeholder="请输入主机地址" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="端口" :label-width="formLabelWidth" prop="Port">
          <el-input v-model="editFrom.Port" placeholder="请输入端口" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="数据库名" :label-width="formLabelWidth" prop="dbName">
          <el-input v-model="editFrom.dbName" placeholder="请输入数据库名" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="用户名" :label-width="formLabelWidth" prop="UserId">
          <el-input v-model="editFrom.UserId" placeholder="请输入用户名" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="密码" :label-width="formLabelWidth" prop="Password">
          <el-input v-model="editFrom.Password" placeholder="请输入密码" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="排序" :label-width="formLabelWidth" prop="SortCode">
          <el-input v-model.number="editFrom.SortCode" placeholder="请输入排序,默认为99" autocomplete="off" clearable />
        </el-form-item>

        <el-form-item label="选项" :label-width="formLabelWidth" prop="">
          <el-checkbox v-model="editFrom.Is_maindb">是否为主数据库</el-checkbox>
        </el-form-item>

        <el-form-item label="选项" :label-width="formLabelWidth" prop="">
          <el-checkbox v-model="editFrom.EnabledMark">启用</el-checkbox>
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

import { getSd_sysdbListWithPager, getSd_sysdbDetail,
  saveSd_sysdb, setSd_sysdbEnable, deleteSoftSd_sysdb,
  deleteSd_sysdb, UpdateDbContents
} from '@/api/dataprocess/sd_sysdb'
import {
  getAllSdClassifyTreeTable
} from '@/api/dataprocess/sd_classify'

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
      selectedclass: '',
      selectclasses: [],
      dialogEditFormVisible: false,
      editFormTitle: '',
      editFrom: {
        Classify_id: '',
        Description: '',
        EnabledMark: '1',
        Sddesc: '',
        SdName: '',
        Sdtype: '',
        HostAddress: '',
        Port: '',
        dbName: '',
        UserId: '',
        Password: '',
        Is_maindb: '',
        SortCode: ''

      },
      rules: {

      },
      formLabelWidth: '110px',
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
      getAllSdClassifyTreeTable().then(res => {
        this.selectclasses = res.ResData
      })
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

      getSd_sysdbListWithPager(seachdata).then(res => {
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
        this.selectedclass = ''
        this.dialogEditFormVisible = true
      }
    },
    bindEditInfo: function() {
      getSd_sysdbDetail(this.currentId).then(res => {
        this.editFrom.Classify_id = res.ResData.Classify_id
        this.editFrom.Description = res.ResData.Description
        this.editFrom.EnabledMark = res.ResData.EnabledMark
        this.editFrom.Sddesc = res.ResData.Sddesc
        this.editFrom.SdName = res.ResData.SdName
        this.editFrom.Sdtype = res.ResData.Sdtype
        this.editFrom.HostAddress = res.ResData.HostAddress
        this.editFrom.Port = res.ResData.Port
        this.editFrom.dbName = res.ResData.dbName
        this.editFrom.UserId = res.ResData.UserId
        this.editFrom.Password = res.ResData.Password
        this.editFrom.Is_maindb = res.ResData.Is_maindb
        this.editFrom.SortCode = res.ResData.SortCode
        this.selectedclass = res.ResData.Classify_id
      })
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          const data = {
            'Classify_id': this.editFrom.Classify_id,
            'Description': this.editFrom.Description,
            'EnabledMark': this.editFrom.EnabledMark,
            'Sddesc': this.editFrom.Sddesc,
            'SdName': this.editFrom.SdName,
            'Sdtype': this.editFrom.Sdtype,
            'HostAddress': this.editFrom.HostAddress,
            'Port': this.editFrom.Port,
            'dbName': this.editFrom.dbName,
            'UserId': this.editFrom.UserId,
            'Password': this.editFrom.Password,
            'Is_maindb': this.editFrom.Is_maindb,
            'SortCode': this.editFrom.SortCode,
            'Id': this.currentId
          }
          var url = 'Sd_sysdb/Insert'
          if (this.currentId !== '') {
            url = 'Sd_sysdb/Update?id=' + this.currentId
          }
          saveSd_sysdb(data, url).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
              this.dialogEditFormVisible = false
              this.currentSelected = ''
              this.selectedclass = ''
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
        setSd_sysdbEnable(data).then(res => {
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
        deleteSoftSd_sysdb(data).then(res => {
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
        deleteSd_sysdb(data).then(res => {
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
     * 更新数据库结构信息
     * @param dbid 数据库ID
     */
    UpdateDbContents: function(dbid) {
      UpdateDbContents(dbid).then(res => {
        if (res.Success) {
          this.$message({
            message: '恭喜你，操作成功',
            type: 'success'
          })
        }
      })
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
*选择分类
*/
    handleSelectClassChange: function() {
      this.editFrom.Classify_id = this.selectedclass
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
