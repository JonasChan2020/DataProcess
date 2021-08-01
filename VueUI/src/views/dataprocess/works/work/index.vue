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
            v-hasPermi="['Work_work/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['Work_work/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >修改</el-button>
          <el-button
            v-hasPermi="['Work_work/Enable']"
            type="info"
            icon="el-icon-video-pause"
            size="small"
            @click="setEnable('0')"
          >禁用</el-button>
          <el-button
            v-hasPermi="['Work_work/Enable']"
            type="success"
            icon="el-icon-video-play"
            size="small"
            @click="setEnable('1')"
          >启用</el-button>
          <el-button
            v-hasPermi="['Work_work/DeleteSoft']"
            type="warning"
            icon="el-icon-delete"
            size="small"
            @click="deleteSoft('0')"
          >软删除</el-button>
          <el-button
            v-hasPermi="['Work_work/Delete']"
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
        <el-table-column prop="Conf_conf_id" label="配置信息ID" sortable="custom" width="120" />
        <el-table-column prop="Conf_detail_ids" label="配置信息详情IDs" sortable="custom" width="120" />
        <el-table-column prop="CreatorTime" label="创建时间" sortable="custom" width="120" />
        <el-table-column prop="CreatorUserId" label="创建人" sortable="custom" width="120" />
        <el-table-column prop="Datapath" label="数据路径" sortable="custom" width="120" />
        <el-table-column prop="DeleteMark" label="删除标记" sortable="custom" width="120" />
        <el-table-column prop="DeleteTime" label="删除时间" sortable="custom" width="120" />
        <el-table-column prop="DeleteUserId" label="删除人" sortable="custom" width="120" />
        <el-table-column prop="Description" label="描述" sortable="custom" width="120" />
        <el-table-column prop="EnabledMark" label="启用标记" sortable="custom" width="120" />
        <el-table-column prop="LastModifyTime" label="最后修改时间" sortable="custom" width="120" />
        <el-table-column prop="LastModifyUserId" label="最后修改人" sortable="custom" width="120" />
        <el-table-column prop="Sdid" label="目标ID" sortable="custom" width="120" />
        <el-table-column prop="SortCode" label="排序字段" sortable="custom" width="120" />
        <el-table-column prop="State" label="状态" sortable="custom" width="120" />
        <el-table-column prop="Wcode" label="工作编码" sortable="custom" width="120" />

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
      width="1280px"
    >
      <el-form ref="editFrom" :model="editFrom" :rules="rules">
        <el-row :gutter="24">
          <el-col :span="12">
            <el-card>
              <el-select v-model="editFrom.selectedSysDb" placeholder="请选择系统或数据库" @change="handleSelectSysDbChange()">
                <el-option
                  v-for="item in editFrom.selectSysDb"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-card>
            <el-card>
              <el-cascader :key="editFrom.cascaderkey" v-model="editFrom.selectedclass" style="width: 100%;" :options="editFrom.selectclasses" filterable :props="{label:'ClassName',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }" clearable @change="handlefromSelectClassChange" />
              <el-table
                ref="editFrom.gridfromtable"
                v-loading="editFrom.fromtableloading"
                :data="editFrom.fromtableData"
                style="width: 100%;margin-bottom: 20px;"
                row-key="Id"
                border
                size="mini"
                max-height="850"
                default-expand-all
                highlight-current-row
                :tree-props="{children: 'Children'}"
                @row-click="handlefromClickRow"
              >
                <el-table-column prop="NodeName" label="名称" min-width="30%" />
                <el-table-column prop="Description" label="描述" min-width="70%" />
              </el-table>
            </el-card>
          </el-col>
          <el-col :span="12">
            <el-table
              ref="gridtotable"
              v-loading="editFrom.totableloading"
              :data="editFrom.totableData"
              :height="700"
              border
              stripe
              highlight-current-row
              style="width: 100%"
              :default-sort="{prop: 'ConfFromType', order: 'ascending'}"
              @row-click="handletoClickRow"
              @select="handletoSelectChange"
              @sort-change="handletoSortChange"
            >
              <el-table-column type="selection" min-width="5%" />
              <el-table-column prop="ConfFromType" label="类型" sortable="custom" min-width="10%">
                <template slot-scope="scope">
                  <el-tag :type="scope.row.ConfFromType === 1 ? 'success' : 'info'" disable-transitions>{{ scope.row.ConfFromType === 1 ? "数据库" : "系统" }}</el-tag>
                </template>
              </el-table-column>
              <el-table-column prop="FromTbName" label="名称" sortable="custom" min-width="15%" />
              <el-table-column prop="FromDescription" label="描述" sortable="custom" min-width="15%" />
              <el-table-column prop="FromParentName" label="所属模型或库" sortable="custom" min-width="15%" />
              <el-table-column prop="FromParentDescription" label="描述" sortable="custom" min-width="15%" />
              <el-table-column label="是否启用" sortable="custom" min-width="10%" prop="EnabledMark" align="center">
                <template slot-scope="scope">
                  <el-tag :type="scope.row.EnabledMark === true ? 'success' : 'info'" disable-transitions>{{ scope.row.EnabledMark === true ? "启用" : "禁用" }}</el-tag>
                </template>
              </el-table-column>
            </el-table>
            <div class="pagination-container">
              <el-pagination
                background
                :current-page="editFrom.topagination.currentPage"
                :page-sizes="[5,10,20,50,100, 200, 300, 400]"
                :page-size="editFrom.topagination.pagesize"
                layout="total, sizes, prev, pager, next, jumper"
                :total="editFrom.topagination.pageTotal"
                @size-change="handletoSizeChange"
                @current-change="handletoCurrentChange"
              />
            </div>
          </el-col>
        </el-row>

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogEditFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="saveEditForm()">确 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>

import { getWork_workListWithPager, getWork_workDetail,
  saveWork_work, setWork_workEnable, deleteSoftWork_work,
  deleteWork_work
} from '@/api/dataprocess/work_work'

import {
  getAllClassifyTreeTable
} from '@/api/dataprocess/sys_classify'

import {
  getAllSdClassifyTreeTable
} from '@/api/dataprocess/sd_classify'

import {
  getSysAndModelTree
} from '@/api/dataprocess/sys_conf'

import {
  getSdAndTbTree
} from '@/api/dataprocess/sd_sysdb'

import {
  getConf_confListWithPager
} from '@/api/dataprocess/conf_conf'

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
        selectedSysDb: '',
        selectSysDb: [{
          value: '0',
          label: '系统'
        }, {
          value: '1',
          label: '数据库'
        }],
        cascaderkey: 1,
        selectedclass: '',
        selectclasses: [],
        fromcurrentSelectId: '',
        fromcurrentSelectParentId: '', // toParentId
        fromtableloading: false,
        fromtableHead: [],
        fromtableData: [],
        fromsortableData: {
          order: 'desc',
          sort: 'CreatorTime'
        },
        tocurrentId: '', // 当前操作对象的ID值，主要用于修改
        tocurrentSelectId: '',
        tocurrentSelected: [],
        totableloading: false,
        totableData: [],
        topagination: {
          currentPage: 1,
          pagesize: 20,
          pageTotal: 0
        },
        tosortableData: {
          order: 'desc',
          sort: 'CreatorTime'
        }
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
      getWork_workListWithPager(seachdata).then(res => {
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

    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {

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
        setWork_workEnable(data).then(res => {
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
        deleteSoftWork_work(data).then(res => {
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
        deleteWork_work(data).then(res => {
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
    },

    /**
   * 子页面
   */

    /**
      *系统或数据库选择
      */
    handleSelectSysDbChange: function() {
      this.editFrom.selectclasses = []
      this.editFrom.selectedclass = ''
      this.editFrom.fromtableData = []
      this.editFrom.cascaderkey++
      if (this.editFrom.selectedSysDb == '0') {
        this.editFrom.fromtableHead = [
          { column_name: 'Syscode', column_comment: '编码', column_minWidth: '25%' },
          { column_name: 'Sysname', column_comment: '名称', column_minWidth: '75%' }
        ]
        getAllClassifyTreeTable().then(res => {
          this.editFrom.selectclasses = res.ResData
        })
      } else if (this.editFrom.selectedSysDb == '1') {
        this.editFrom.fromtableHead = [
          { column_name: 'SdName', column_comment: '名称', column_minWidth: '95%' }
        ]
        getAllSdClassifyTreeTable().then(res => {
          this.editFrom.selectclasses = res.ResData
        })
      }
      this.loadfromTableData()
    },
    /**
    * 加载页面左侧table数据
    */
    loadfromTableData: function() {
      this.editFrom.fromtableloading = true
      var seachdata = {
        Filter: {
          Classify_id: this.editFrom.selectedclass
        }
      }

      if (this.editFrom.selectedSysDb == '0') {
        getSysAndModelTree(seachdata).then(res => {
          this.editFrom.fromtableData = res.ResData
          this.editFrom.fromtableloading = false
        })
      } else if (this.editFrom.selectedSysDb == '1') {
        getSdAndTbTree(seachdata).then(res => {
          this.editFrom.fromtableData = res.ResData
          this.editFrom.fromtableloading = false
        })
      }
    },

    /**
*系统或数据库分类选择
*/
    handlefromSelectClassChange: function(value) {
      this.editFrom.selectedclass = value
    },

    /**
      * 点击一条记录
      */
    handlefromClickRow(row) {
      if (row.NodeType !== 'tb') {
        this.editFrom.fromcurrentSelectId = ''
        this.editFrom.fromcurrentSelectParentId = ''
      } else {
        this.editFrom.fromcurrentSelectId = row.Id
        this.editFrom.fromcurrentSelectParentId = row.ParentId
        this.editFrom.topagination.currentPage = 1
        this.loadtoTableData()
      }
    },

    /**
    * 加载页面左侧table数据
    */
    loadtoTableData: function() {
      this.editFrom.totableloading = true
      var seachdata = {
        CurrenetPageIndex: this.editFrom.topagination.currentPage,
        PageSize: this.editFrom.topagination.pagesize,
        Order: this.editFrom.tosortableData.order,
        Sort: this.editFrom.tosortableData.sort,
        Filter: {
          ToId: this.editFrom.tocurrentSelectId
        }
      }
      getConf_confListWithPager(seachdata).then(res => {
        this.editFrom.totableData = res.ResData.Items
        this.editFrom.topagination.pageTotal = res.ResData.TotalItems
        this.editFrom.totableloading = false
      })
    },
    /**
       * 当表格的排序条件发生变化的时候会触发该事件
       */
    handletoSortChange: function(column) {
      this.editFrom.tosortableData.sort = column.prop
      if (column.order === 'ascending') {
        this.editFrom.tosortableData.order = 'asc'
      } else {
        this.editFrom.tosortableData.order = 'desc'
      }
      this.loadtoTableData()
    },
    /**
      * 选择每页显示数量
      */
    handletoSizeChange(val) {
      this.editFrom.topagination.pagesize = val
      this.editFrom.topagination.currentPage = 1
      this.loadtoTableData()
    },
    /**
       * 选择当页面
       */
    handletoCurrentChange(val) {
      this.editFrom.topagination.currentPage = val
      this.loadtoTableData()
    },
    /**
      * 点击一条记录
      */
    handletoClickRow(row) {
      this.editFrom.tocurrentSelectId = row.Id
    },
    /**
     * 当用户手动勾选checkbox数据行事件
     */
    handletoSelectChange: function(selection, row) {
      this.editFrom.tocurrentSelected = selection
    },
    /**
       * 当用户手动勾选全选checkbox事件
       */
    handletoSelectAllChange: function(selection) {
      this.editFrom.tocurrentSelected = selection
    }

  }
}
</script>

<style>
</style>
