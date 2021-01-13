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
        <el-table-column prop="CreatorTime" label="创建时间" sortable="custom" width="120" />
        <el-table-column prop="CreatorUserId" label="创建人" sortable="custom" width="120" />
        <el-table-column prop="DeleteMark" label="删除标记" sortable="custom" width="120" />
        <el-table-column prop="DeleteTime" label="删除时间" sortable="custom" width="120" />
        <el-table-column prop="DeleteUserId" label="删除人" sortable="custom" width="120" />
        <el-table-column prop="Description" label="描述" sortable="custom" width="120" />
        <el-table-column prop="Dtcode" label="类型编码" sortable="custom" width="120" />
        <el-table-column prop="Dtname" label="类型名称" sortable="custom" width="120" />
        <el-table-column prop="EnabledMark" label="启用标记" sortable="custom" width="120" />
        <el-table-column prop="LastModifyTime" label="最后修改时间" sortable="custom" width="120" />
        <el-table-column prop="LastModifyUserId" label="最后修改人" sortable="custom" width="120" />
        <el-table-column prop="Levelpath" label="层级路径" sortable="custom" width="120" />
        <el-table-column prop="Parentid" label="父ID" sortable="custom" width="120" />
        <el-table-column prop="SortCode" label="排序字段" sortable="custom" width="120" />
        <el-table-column prop="State" label="状态" sortable="custom" width="120" />

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
        <el-form-item label="创建时间" :label-width="formLabelWidth" prop="CreatorTime">
          <el-input v-model="editFrom.CreatorTime" placeholder="请输入创建时间" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="创建人" :label-width="formLabelWidth" prop="CreatorUserId">
          <el-input v-model="editFrom.CreatorUserId" placeholder="请输入创建人" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="删除标记" :label-width="formLabelWidth" prop="DeleteMark">
          <el-input v-model="editFrom.DeleteMark" placeholder="请输入删除标记" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="删除时间" :label-width="formLabelWidth" prop="DeleteTime">
          <el-input v-model="editFrom.DeleteTime" placeholder="请输入删除时间" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="删除人" :label-width="formLabelWidth" prop="DeleteUserId">
          <el-input v-model="editFrom.DeleteUserId" placeholder="请输入删除人" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="描述" :label-width="formLabelWidth" prop="Description">
          <el-input v-model="editFrom.Description" placeholder="请输入描述" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="类型编码" :label-width="formLabelWidth" prop="Dtcode">
          <el-input v-model="editFrom.Dtcode" placeholder="请输入类型编码" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="类型名称" :label-width="formLabelWidth" prop="Dtname">
          <el-input v-model="editFrom.Dtname" placeholder="请输入类型名称" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="启用标记" :label-width="formLabelWidth" prop="EnabledMark">
          <el-input v-model="editFrom.EnabledMark" placeholder="请输入启用标记" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="最后修改时间" :label-width="formLabelWidth" prop="LastModifyTime">
          <el-input v-model="editFrom.LastModifyTime" placeholder="请输入最后修改时间" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="最后修改人" :label-width="formLabelWidth" prop="LastModifyUserId">
          <el-input v-model="editFrom.LastModifyUserId" placeholder="请输入最后修改人" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="层级路径" :label-width="formLabelWidth" prop="Levelpath">
          <el-input v-model="editFrom.Levelpath" placeholder="请输入层级路径" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="父ID" :label-width="formLabelWidth" prop="Parentid">
          <el-input v-model="editFrom.Parentid" placeholder="请输入父ID" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="排序字段" :label-width="formLabelWidth" prop="SortCode">
          <el-input v-model="editFrom.SortCode" placeholder="请输入排序字段" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="状态" :label-width="formLabelWidth" prop="State">
          <el-input v-model="editFrom.State" placeholder="请输入状态" autocomplete="off" clearable />
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

import { getDs_classifyListWithPager, getDs_classifyDetail,
  saveDs_classify, setDs_classifyEnable, deleteSoftDs_classify,
  deleteDs_classify
} from '@/api/dataprocess/ds_classify'

export default {
  data () {
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
        CreatorTime: '',
        CreatorUserId: '',
        DeleteMark: '',
        DeleteTime: '',
        DeleteUserId: '',
        Description: '',
        Dtcode: '',
        Dtname: '',
        EnabledMark: '',
        LastModifyTime: '',
        LastModifyUserId: '',
        Levelpath: '',
        Parentid: '',
        SortCode: '',
        State: ''

      },
      rules: {

      },
      formLabelWidth: '80px',
      currentId: '', // 当前操作对象的ID值，主要用于修改
      currentSelected: []
    }
  },
  created () {
    this.pagination.currentPage = 1
    this.InitDictItem()
    this.loadTableData()
    this.loadBtnFunc = JSON.parse(localStorage.getItem('yueboncurrentfuns'))
  },
  methods: {
    /**
     * 初始化数据
     */
    InitDictItem () {
    },
    /**
     * 加载页面table数据
     */
    loadTableData: function () {
      this.tableloading = true
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.keywords,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      }
      getDs_classifyListWithPager(seachdata).then(res => {
        this.tableData = res.ResData.Items
        this.pagination.pageTotal = res.ResData.TotalItems
        this.tableloading = false
      })
    },
    /**
     * 点击查询
     */
    handleSearch: function () {
      this.pagination.currentPage = 1
      this.loadTableData()
    },

    /**
     * 新增、修改或查看明细信息（绑定显示数据）     *
     */
    ShowEditOrViewDialog: function (view) {
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
    bindEditInfo: function () {
      getDs_classifyDetail(this.currentId).then(res => {
        this.editFrom.CreatorTime = res.ResData.CreatorTime
        this.editFrom.CreatorUserId = res.ResData.CreatorUserId
        this.editFrom.DeleteMark = res.ResData.DeleteMark
        this.editFrom.DeleteTime = res.ResData.DeleteTime
        this.editFrom.DeleteUserId = res.ResData.DeleteUserId
        this.editFrom.Description = res.ResData.Description
        this.editFrom.Dtcode = res.ResData.Dtcode
        this.editFrom.Dtname = res.ResData.Dtname
        this.editFrom.EnabledMark = res.ResData.EnabledMark
        this.editFrom.LastModifyTime = res.ResData.LastModifyTime
        this.editFrom.LastModifyUserId = res.ResData.LastModifyUserId
        this.editFrom.Levelpath = res.ResData.Levelpath
        this.editFrom.Parentid = res.ResData.Parentid
        this.editFrom.SortCode = res.ResData.SortCode
        this.editFrom.State = res.ResData.State
      })
    },
    /**
     * 新增/修改保存
     */
    saveEditForm () {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          const data = {
            'CreatorTime': this.editFrom.CreatorTime,
            'CreatorUserId': this.editFrom.CreatorUserId,
            'DeleteMark': this.editFrom.DeleteMark,
            'DeleteTime': this.editFrom.DeleteTime,
            'DeleteUserId': this.editFrom.DeleteUserId,
            'Description': this.editFrom.Description,
            'Dtcode': this.editFrom.Dtcode,
            'Dtname': this.editFrom.Dtname,
            'EnabledMark': this.editFrom.EnabledMark,
            'LastModifyTime': this.editFrom.LastModifyTime,
            'LastModifyUserId': this.editFrom.LastModifyUserId,
            'Levelpath': this.editFrom.Levelpath,
            'Parentid': this.editFrom.Parentid,
            'SortCode': this.editFrom.SortCode,
            'State': this.editFrom.State,

            'Id': this.currentId
          }
          saveDs_classify(data).then(res => {
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
    setEnable: function (val) {
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
        setDs_classifyEnable(data).then(res => {
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
    deleteSoft: function (val) {
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
        deleteSoftDs_classify(data).then(res => {
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
    deletePhysics: function () {
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
        deleteDs_classify(data).then(res => {
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
    handleSortChange: function (column) {
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
    handleSelectChange: function (selection, row) {
      this.currentSelected = selection
    },
    /**
     * 当用户手动勾选全选checkbox事件
     */
    handleSelectAllChange: function (selection) {
      this.currentSelected = selection
    },
    /**
     * 选择每页显示数量
     */
    handleSizeChange (val) {
      this.pagination.pagesize = val
      this.pagination.currentPage = 1
      this.loadTableData()
    },
    /**
     * 选择当页面
     */
    handleCurrentChange (val) {
      this.pagination.currentPage = val
      this.loadTableData()
    }
  }
}
</script>

<style>
</style>
