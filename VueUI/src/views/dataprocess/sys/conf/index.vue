<template>
  <div class="app-container">

    
      <el-row :gutter="24">
        <el-col :span="10">
          <el-card>
            <div class="grid-content bg-purple">
              <div class="grid-content bg-purple">
                <el-card>
                  <el-cascader v-model="sysselectedclass"placeholder="请选择系统分类" :key="cascaderkey" style="width:500px;" :options="sysselectclasses" filterable :props="{label:'ClassName',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }" clearable @change="handleSysSelectClassChange" />
                  <el-select v-model="selectedSys" placeholder="请选择系统" @change="handleSelectSysChange()">
                    <el-option v-for="item in selectSyses"
                               :key="item.Id"
                               :label="item.Sysname"
                               :value="item.Id" />
                  </el-select>
                </el-card>
                <el-card>
                  <div class="list-btn-container">
                    <el-button-group>
                      <el-button v-hasPermi="['Sys_conf/Add']"
                                 type="primary"
                                 icon="el-icon-plus"
                                 size="small"
                                 @click="ShowEditOrViewDialog()">新增</el-button>
                      <el-button v-hasPermi="['Sys_conf/Edit']"
                                 type="primary"
                                 icon="el-icon-edit"
                                 class="el-button-modify"
                                 size="small"
                                 @click="ShowEditOrViewDialog('edit')">修改</el-button>
                      <el-button v-hasPermi="['Sys_conf/Enable']"
                                 type="info"
                                 icon="el-icon-video-pause"
                                 size="small"
                                 @click="setEnable('0')">禁用</el-button>
                      <el-button v-hasPermi="['Sys_conf/Enable']"
                                 type="success"
                                 icon="el-icon-video-play"
                                 size="small"
                                 @click="setEnable('1')">启用</el-button>
                      <el-button v-hasPermi="['Sys_conf/DeleteSoft']"
                                 type="warning"
                                 icon="el-icon-delete"
                                 size="small"
                                 @click="deleteSoft('0')">软删除</el-button>
                      <el-button type="default" icon="el-icon-refresh" size="small" @click="loadTableData()">刷新</el-button>
                    </el-button-group>
                  </div>
                  <el-table ref="gridtable"
                            :data="tableData"
                            row-key="Id"
                            :height="500"
                            border
                            stripe
                            highlight-current-row
                            style="width: 100%;margin-bottom: 20px;"
                            :default-sort="{prop: 'SortCode', order: 'ascending'}"
                            @row-click="handleClickRow"
                            @select="handleSelectChange"
                            @select-all="handleSelectAllChange"
                            @sort-change="handleSortChange">
                    <el-table-column type="selection" width="30" />
                    <el-table-column prop="Confcode" label="编码" sortable="custom" width="120" />
                    <el-table-column prop="Confname" label="名称" sortable="custom" width="120" />
                    <el-table-column prop="Sys_Name" label="所属系统" sortable="custom" width="120" />
                    <el-table-column prop="Classify_id" label="分类" sortable="custom" width="260" align="center">
                      <template slot-scope="scope">
                        {{ scope.row.Classify_Name }}
                      </template>
                    </el-table-column>
                    <el-table-column label="是否启用" sortable="custom" width="120" prop="EnabledMark" align="center">
                      <template slot-scope="scope">
                        <el-tag :type="scope.row.EnabledMark === true ? 'success' : 'info'" disable-transitions>{{ scope.row.EnabledMark === true ? "启用" : "禁用" }}</el-tag>
                      </template>
                    </el-table-column>
                  </el-table>
                  <div class="pagination-container">
                    <el-pagination background
                                   :current-page="pagination.currentPage"
                                   :page-sizes="[5,10,20,50,100, 200, 300, 400]"
                                   :page-size="pagination.pagesize"
                                   layout="total, sizes, prev, pager, next, jumper"
                                   :total="pagination.pageTotal"
                                   @size-change="handleSizeChange"
                                   @current-change="handleCurrentChange" />
                  </div>
                </el-card>
              </div>
            </div>
          </el-card>
            </el-col>
        <el-col :span="14">
          <el-card>
            <div class="grid-content bg-purple">
              <div class="list-btn-container">
                <el-button-group>
                  <el-button v-hasPermi="['Sys_conf_details/Add']"
                             type="primary"
                             icon="el-icon-plus"
                             size="small"
                             @click="ShowEditOrViewDetailDialog('add')">新增</el-button>
                  <el-button v-hasPermi="['Sys_conf_details/Edit']"
                             type="primary"
                             icon="el-icon-edit"
                             class="el-button-modify"
                             size="small"
                             @click="ShowEditOrViewDetailDialog('edit')">修改</el-button>
                  <el-button v-hasPermi="['Sys_conf_details/DeleteSoft']"
                             type="warning"
                             icon="el-icon-delete"
                             size="small"
                             @click="deleteDetailSoft('0')">删除</el-button>
                  <el-button type="default" icon="el-icon-refresh" size="small" @click="loadTableDetailData()">刷新</el-button>
                  <el-button v-hasPermi="['Sys_conf_details/Edit']"
                             type="primary"
                             icon="el-icon-edit"
                             size="small"
                             @click="changeLevelNum('up')">上移</el-button>
                  <el-button v-hasPermi="['Sys_conf_details/Edit']"
                             type="primary"
                             icon="el-icon-edit"
                             size="small"
                             @click="changeLevelNum('down')">下移</el-button>
                  <el-button v-hasPermi="['Sys_conf_details/Edit']"
                             type="primary"
                             icon="el-icon-edit"
                             size="small"
                             @click="changeLevelNum('top')">置顶</el-button>
                  <el-button v-hasPermi="['Sys_conf_details/Edit']"
                             type="primary"
                             icon="el-icon-edit"
                             size="small"
                             @click="changeLevelNum('buttom')">置底</el-button>
                </el-button-group>
              </div>
              <el-table ref="gridtable"
                        :data="tableDetailData"
                        row-key="Id"
                        :height="700"
                        border
                        max-height="850"
                        stripe
                        highlight-current-row
                        style="width: 100%;margin-bottom: 20px;"
                        :default-sort="{prop: 'Levelnum', order: 'ascending'}"
                        @select="handleDetailSelectChange"
                        @select-all="handleDetailSelectAllChange">
                <el-table-column type="selection" width="30" />
                <el-table-column prop="Levelnum" label="顺序" sortable="custom" width="80" />
                <el-table-column prop="Tbname" label="表名" sortable="custom" width="120" />
                <el-table-column label="动态表" sortable="custom" width="100" prop="Is_dynamic" align="center">
                  <template slot-scope="scope">
                    <el-tag :type="scope.row.Is_dynamic === true ? 'success' : 'info'" disable-transitions>{{ scope.row.Is_dynamic === true ? "是" : "否" }}</el-tag>
                  </template>
                </el-table-column>
                <el-table-column label="标识表" sortable="custom" width="100" prop="Is_flag" align="center">
                  <template slot-scope="scope">
                    <el-tag :type="scope.row.Is_flag === true ? 'success' : 'info'" disable-transitions>{{ scope.row.Is_flag === true ? "是" : "否" }}</el-tag>
                  </template>
                </el-table-column>
                <el-table-column label="启用" sortable="custom" width="100" prop="EnabledMark" align="center">
                  <template slot-scope="scope">
                    <el-tag :type="scope.row.EnabledMark === true ? 'success' : 'info'" disable-transitions>{{ scope.row.EnabledMark === true ? "启用" : "禁用" }}</el-tag>
                  </template>
                </el-table-column>

              </el-table>
            </div>
          </el-card>
        </el-col>
      </el-row>
    

    <el-dialog
      ref="dialogEditForm"
      :close-on-click-modal="false"
      :show-close="true"
      :title="editFormTitle+'数据模型'"
      :visible.sync="dialogEditFormVisible"
      width="640px"
    >
      <el-form ref="editFrom" :model="editFrom" :rules="rules">
        <el-form-item label="编码" :label-width="formLabelWidth" prop="Confcode">
          <el-input v-model="editFrom.Confcode" placeholder="请输入配置信息编码" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="名称" :label-width="formLabelWidth" prop="Confname">
          <el-input v-model="editFrom.Confname" placeholder="请输入配置信息名称" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="描述" :label-width="formLabelWidth" prop="Description">
          <el-input v-model="editFrom.Description" placeholder="请输入描述" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="所属分类" :label-width="formLabelWidth" prop="Classify_id">
          <el-cascader v-model="selectedclass" style="width:500px;" :options="selectclasses" filterable :props="{label:'ClassName',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }" clearable @change="handleSelectClassChange" />
        </el-form-item>
        <el-form-item label="所属系统" :label-width="formLabelWidth" prop="Sys_id">
          <el-cascader v-model="formselectedsys" style="width:500px;" :options="formselectsyses" filterable :props="{label:'Sysname',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }" clearable @change="handleFromSelectSysChange" />
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
  </div>
</template>

<script>

import {
  getSys_confListWithPager, getSys_confDetail,
  saveSys_conf, setSys_confEnable, deleteSoftSys_conf,
  deleteSys_conf
} from '@/api/dataprocess/sys_conf'
import {
  getAllSysConfClassifyTreeTable
} from '@/api/dataprocess/sys_conf_classify'
import {
    getAllEnableByConfId, deleteSys_conf_details,
   changeLevelNumAsync
  } from '@/api/dataprocess/sys_conf_details'
  import {
    getAllClassifyTreeTable
  } from '@/api/dataprocess/sys_classify'
  import {
    getSys_sysListWithPager
  } from '@/api/dataprocess/sys_sys'

  export default {
    name:'sysconfcontrol',
  data() {
    return {
      searchform: {
        keywords: ''
      },
      loadBtnFunc: [],
      tableData: [],
      pagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      sortableData: {
        order: 'desc',
        sort: 'CreatorTime'
      },
      formselectedsys: '',
      formselectsyses: [],
      selectedSys: '',
      selectSyses: [],
      cascaderkey: 1,
      sysselectedclass: '',
      sysselectclasses: [],
      selectedclass: '',
      selectclasses: [],
      dialogEditFormVisible: false,
      editFormTitle: '',
      editFrom: {
        Classify_id: '',
        Sysid:'',
        Confcode: '',
        Confdes: '',
        Confname: '',
        Description: '',
        EnabledMark: '',
        SortCode: ''

      },
      rules: {

      },
      formLabelWidth: '80px',
      currentId: '', // 当前操作对象的ID值，主要用于修改
      currentSysId: '',
      currentSelected: [],

      sortableDetailData: {
        order: 'desc',
        sort: 'CreatorTime'
      },
      tableDetailData: [],
      currentDetailId: '',
      currentDetailSelected: []
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
      
      getAllClassifyTreeTable().then(res => {
        this.sysselectclasses = res.ResData
      })

      var formsysseachdata = {
        CurrenetPageIndex: 1,
        PageSize: 999999999,
      }
      getSys_sysListWithPager(formsysseachdata).then(res => {
        this.formselectsyses = res.ResData.Items
      })
    },
    /**
       * 加载页面table数据
       */
    loadTableData: function() {
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.keywords,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort,
        Filter: {
          Sysid: this.selectedSys
        }
      }
      getSys_confListWithPager(seachdata).then(res => {
        this.tableData = res.ResData.Items
        this.pagination.pageTotal = res.ResData.TotalItems
      })
      getAllSysConfClassifyTreeTable(seachdata).then(res => {
        this.selectclasses = res.ResData
      })
    },
    /**
       * 加载详情页面table数据
       */
    loadTableDetailData: function() {
      var seachdata = {
        Filter: {
          Sys_conf_id: this.currentId
        }
      }
      getAllEnableByConfId(seachdata).then(res => {
        this.tableDetailData = res.ResData
      })
    },
    /**
       * 点击查询
       */
    handleSearch: function() {
      this.pagination.currentPage = 1
      this.loadTableData()
    },
    handleClickRow(row) {
      this.currentId = row.Id
      this.currentSysId = row.Sysid
      this.loadTableDetailData()
    },
    /**
      *系统分类选择 
      */
    handleSysSelectClassChange: function (value) {
      this.sysselectedclass = value
      var seachdata = {
        CurrenetPageIndex: 1,
        PageSize: 999999999,
        Filter: {
          Classify_id: this.sysselectedclass,
        }
      }
      getSys_sysListWithPager(seachdata).then(res => {
        this.selectSyses = res.ResData.Items
      })
      
    },
    /**
     *系统选择 
     */
    handleSelectSysChange: function () {
      this.loadTableData()
    },
    /**
    *表单内系统选择 
    */
    handleFromSelectSysChange: function () {
      this.editFrom.Sysid = this.formselectedsys
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
      getSys_confDetail(this.currentId).then(res => {
        this.editFrom.Classify_id = res.ResData.Classify_id
        this.editFrom.Sysid = res.ResData.Sysid
        this.editFrom.Confcode = res.ResData.Confcode
        this.editFrom.Confdes = res.ResData.Confdes
        this.editFrom.Confname = res.ResData.Confname
        this.editFrom.Description = res.ResData.Description
        this.editFrom.EnabledMark = res.ResData.EnabledMark
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
            'Sysid': this.editFrom.Sysid,
            'Confcode': this.editFrom.Confcode,
            'Confdes': this.editFrom.Confdes,
            'Confname': this.editFrom.Confname,
            'Description': this.editFrom.Description,
            'EnabledMark': this.editFrom.EnabledMark,
            'SortCode': this.editFrom.SortCode,
            'Id': this.currentId
          }
          var url = 'Sys_conf/Insert'
          if (this.currentId !== '') {
            url = 'Sys_conf/Update?id=' + this.currentId
          }
          saveSys_conf(data, url).then(res => {
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
        setSys_confEnable(data).then(res => {
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
        deleteSoftSys_conf(data).then(res => {
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
        deleteSys_conf(data).then(res => {
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
      * 新增、修改或查看详情明细信息（绑定显示数据）     *
      */
    ShowEditOrViewDetailDialog: function(view) {
      if (view !== undefined && view !== 'add') {
        if (this.currentDetailSelected.length > 1 || this.currentDetailSelected.length === 0) {
          this.$alert('请选择一条数据进行编辑/修改', '提示')
        } else {
          this.currentDetailId = this.currentDetailSelected[0].Id
          this.$router.push({ name: 'EditConfDetail', params: { id: this.currentDetailId, showtype: view, Sys_conf_id: this.currentId, SysId: this.currentSysId }})
        }
      } else {
        this.currentDetailId = ''
        this.selectedDetailclass = ''
        this.$router.push({ name: 'EditConfDetail', params: { id: this.currentDetailId, showtype: view, Sys_conf_id: this.currentId, SysId: this.currentSysId }})
      }
    },
    deleteDetailSoft: function(val) {
      if (this.currentDetailSelected.length === 0) {
        this.$alert('请先选择要操作的数据', '提示')
        return false
      } else {
        var currentDetailIds = []
        this.currentDetailSelected.forEach(element => {
          currentDetailIds.push(element.Id)
        })
        const data = {
          Ids: currentDetailIds,
          Flag: val
        }
        deleteSys_conf_details(data).then(res => {
          if (res.Success) {
            this.$message({
              message: '恭喜你，操作成功',
              type: 'success'
            })
            this.currentDetailSelected = ''
            this.loadTableDetailData()
          } else {
            this.$message({
              message: res.ErrMsg,
              type: 'error'
            })
          }
        })
      }
    },
    changeLevelNum: function(actionStr) {
      if (this.currentDetailSelected.length > 1 || this.currentDetailSelected.length === 0) {
        this.$alert('请选择一条数据进行编辑/修改', '提示')
      } else {
        this.currentDetailId = this.currentDetailSelected[0].Id
        const data = {
          Id: this.currentDetailId,
          actionStr: actionStr
        }
        changeLevelNumAsync(data).then(res => {
          if (res.Success) {
            this.$message({
              message: '恭喜你，操作成功',
              type: 'success'
            })
            this.loadTableDetailData()
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
       * 当用户手动勾选详情checkbox数据行事件
       */
    handleDetailSelectChange: function(selection, row) {
      this.currentDetailSelected = selection
    },
    /**
       * 当用户手动勾选详情全选checkbox事件
       */
    handleDetailSelectAllChange: function(selection) {
      this.currentDetailSelected = selection
    }
  }
}
</script>

<style>
</style>
