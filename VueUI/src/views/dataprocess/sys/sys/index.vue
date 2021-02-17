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
            v-hasPermi="['Sys_sys/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['Sys_sys/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >修改</el-button>
          <el-button
            v-hasPermi="['Sys_sys/Enable']"
            type="info"
            icon="el-icon-video-pause"
            size="small"
            @click="setEnable('0')"
          >禁用</el-button>
          <el-button
            v-hasPermi="['Sys_sys/Enable']"
            type="success"
            icon="el-icon-video-play"
            size="small"
            @click="setEnable('1')"
          >启用</el-button>
          <el-button
            v-hasPermi="['Sys_sys/DeleteSoft']"
            type="warning"
            icon="el-icon-delete"
            size="small"
            @click="deleteSoft('0')"
          >软删除</el-button>
          <el-button type="default" icon="el-icon-refresh" size="small" @click="loadTableData()">刷新</el-button>
          <el-button
            v-hasPermi="['Sys_sys/SitMainDb']"
            type="primary"
            icon="el-icon-edit"
            size="small"
            @click="chosemaindb()"
          >选择主数据库</el-button>
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
        <el-table-column prop="Classify_id" label="系统分类" sortable="custom" width="260" align="center">
          <template slot-scope="scope">
            {{ scope.row.Classify_Name }}
          </template>
        </el-table-column>
        <el-table-column prop="MdbId" label="主库" sortable="custom" width="260" align="center">
          <template slot-scope="scope">
            {{ scope.row.MdbName }}
          </template>
        </el-table-column>
        <el-table-column prop="Description" label="描述" sortable="custom" width="120" />
        <el-table-column prop="SortCode" label="排序字段" sortable="custom" width="90" align="center" />
        <el-table-column label="是否启用" sortable="custom" width="120" prop="EnabledMark" align="center">
          <template slot-scope="scope">
            <el-tag :type="scope.row.EnabledMark === true ? 'success' : 'info'" disable-transitions>{{ scope.row.EnabledMark === true ? "启用" : "禁用" }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="选择" sortable="custom" width="120" align="center">
          <template slot-scope="scope">
            <el-button
              type="primary"
              icon="el-icon-plus"
              size="small"
              @click="ChoseSys(scope.row.Id)"
            >选择</el-button>
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
        <el-form-item label="描述" :label-width="formLabelWidth" prop="Description">
          <el-input v-model="editFrom.Description" placeholder="请输入描述" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="系统分类" :label-width="formLabelWidth" prop="Classify_id">
          <el-cascader v-model="selectedclass" style="width:500px;" :options="selectclasses" filterable :props="{label:'ClassName',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }" clearable @change="handleSelectClassChange" />
        </el-form-item>
        <el-form-item label="排序" :label-width="formLabelWidth" prop="SortCode">
          <el-input v-model.number="editFrom.SortCode" placeholder="请输入排序,默认为99" autocomplete="off" clearable />
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

    <el-dialog ref="dialogSitMainDbForm" title="选择主数据库" :visible.sync="dialogSitMainDbFormVisible" width="70%">
      <el-card>
        <el-cascader v-model="selectedsdclass" style="width:500px;" :options="selectsdclasses" filterable :props="{label:'ClassName',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }" clearable @change="handleSelectSdClassChange" />
      </el-card>
      <el-card>
        <el-table
          ref="gridMDbtable"
          v-loading="tableMDbloading"
          :data="tableMDbData"
          border
          stripe
          highlight-current-row
          style="width: 100%"
          :default-sort="{prop: 'SortMDbCode', order: 'ascending'}"
          @select="handleMDbSelectChange"
          @sort-change="handleMDbSortChange"
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
          <el-table-column label="是否启用" sortable="custom" width="120" prop="EnabledMark" align="center">
            <template slot-scope="scope">
              <el-tag :type="scope.row.EnabledMark === true ? 'success' : 'info'" disable-transitions>{{ scope.row.EnabledMark === true ? "启用" : "禁用" }}</el-tag>
            </template>
          </el-table-column>
        </el-table>
        <div class="pagination-container">
          <el-pagination
            background
            :current-page="MDbpagination.currentPage"
            :page-sizes="[5,10,20,50,100, 200, 300, 400]"
            :page-size="MDbpagination.pagesize"
            layout="total, sizes, prev, pager, next, jumper"
            :total="MDbpagination.pageTotal"
            @size-change="handleMDbSizeChange"
            @current-change="handleMDbCurrentChange"
          />
        </div>

      </el-card>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogSitMainDbFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="saveMDbForm()">确 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>

import {
  getSys_sysListWithPager, getSys_sysDetail, choseSys,
  saveSys_sys, setSys_sysEnable, deleteSoftSys_sys,
  deleteSys_sys, updateMDb
} from '@/api/dataprocess/sys_sys'
import { getAllClassifyTreeTable
} from '@/api/dataprocess/sys_classify'
import {
  getSd_sysdbListWithSysPager

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
        EnabledMark: '',
        SortCode: '',
        Syscode: '',
        Sysname: ''

      },
      rules: {

      },
      formLabelWidth: '80px',
      currentId: '', // 当前操作对象的ID值，主要用于修改
      currentSelected: [],

      tableMDbloading: false,
      tableMDbData: [],
      SortMDbCode: {
        order: 'desc',
        sort: 'CreatorTime'
      },
      MDbpagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      selectedsdclass: '',
      selectsdclasses: [],
      dialogSitMainDbFormVisible: false,
      currentMDbId: '',
      currentMDbSelected: []
    }
  },
  created() {
    this.pagination.currentPage = 1
    this.MDbpagination.currentPage = 1
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
      getAllClassifyTreeTable().then(res => {
        //this.tableData = res.ResData
        this.selectclasses = res.ResData
        //this.tableloading = false
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
        this.selectedclass = ''
        this.currentId = ''
        this.dialogEditFormVisible = true
      }
    },
    bindEditInfo: function() {
      getSys_sysDetail(this.currentId).then(res => {
        this.editFrom.Classify_id = res.ResData.Classify_id
        this.editFrom.Description = res.ResData.Description
        this.editFrom.EnabledMark = res.ResData.EnabledMark
        this.editFrom.SortCode = res.ResData.SortCode
        this.editFrom.Syscode = res.ResData.Syscode
        this.editFrom.Sysname = res.ResData.Sysname
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
            'SortCode': this.editFrom.SortCode,
            'Syscode': this.editFrom.Syscode,
            'Sysname': this.editFrom.Sysname,

            'Id': this.currentId
          }
          var url = 'Sys_sys/Insert'
          if (this.currentId !== '') {
            url = 'Sys_sys/Update?id=' + this.currentId
          }
          saveSys_sys(data, url).then(res => {
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
     * 选择指定系统
     * @param sysId 系统ID
     */
    ChoseSys: function(sysId) {
      choseSys(sysId).then(res => {
        if (res.Success) {
          this.$message({
            message: '恭喜你，操作成功',
            type: 'success'
          })
        }
      })
    },
    chosemaindb: function() {
      if (this.currentSelected.length > 0) {
        getAllSdClassifyTreeTable().then(res => {
          this.selectsdclasses = res.ResData
        })
        this.loadMDbTableData()
        this.dialogSitMainDbFormVisible = true
      } else {
        alert('请先选择一个系统')
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
    },

    /**
    * 加载页面table数据
    */
    loadMDbTableData: function() {
      this.tableMDbloading = true
      var seachdata = {
        CurrenetPageIndex: this.MDbpagination.currentPage,
        PageSize: this.MDbpagination.pagesize,
        Filter: {
          Classify_id: this.selectedsdclass,
          Sys_id: this.currentSelected[0].Id
        },
        Order: this.SortMDbCode.order,
        Sort: this.SortMDbCode.sort
      }

      getSd_sysdbListWithSysPager(seachdata).then(res => {
        this.tableMDbData = res.ResData.Items
        this.MDbpagination.pageTotal = res.ResData.TotalItems
        this.tableMDbloading = false
      })
    },

    /**
      *选择目标库分类
*/
    handleSelectSdClassChange: function() {
      this.loadMDbTableData()
    },
    /**
     * 当表格的排序条件发生变化的时候会触发该事件
     */
    handleMDbSortChange: function(column) {
      this.SortMDbCode.sort = column.prop
      if (column.order === 'ascending') {
        this.SortMDbCode.order = 'asc'
      } else {
        this.SortMDbCode.order = 'desc'
      }
      this.loadMDbTableData()
    },
    /**
 * 当用户手动勾选checkbox数据行事件
 */
    handleMDbSelectChange: function(selection, row) {
      this.currentMDbSelected = selection
    },
    /**
     * 选择每页显示数量
     */
    handleMDbSizeChange(val) {
      this.MDbpagination.pagesize = val
      this.MDbpagination.currentPage = 1
      this.loadMDbTableData()
    },
    /**
     * 选择当页面
     */
    handleMDbCurrentChange(val) {
      this.MDbpagination.currentPage = val
      this.MDbloadTableData()
    },
    saveMDbForm() {
      updateMDb(this.currentSelected[0].Id, this.currentMDbSelected[0].Id).then(res => {
        if (res.Success) {
          this.$message({
            message: '恭喜你，操作成功',
            type: 'success'
          })
          this.dialogSitMainDbFormVisible = false
          this.currentMDbSelected = ''
          this.selectedsdclass = ''
          this.loadTableData()
          this.InitDictItem()
        } else {
          this.$message({
            message: res.ErrMsg,
            type: 'error'
          })
        }
      })
    }
  }
}
</script>

<style>
</style>
