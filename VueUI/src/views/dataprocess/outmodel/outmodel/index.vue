<template>
  <div class="app-container">


    <el-row :gutter="24">
      <el-col :span="8">
        <div class="grid-content bg-purple">
          <div class="grid-content bg-purple">
            <el-card>
              <el-row :gutter="24">
                <el-col :span="7">
                  <el-button type="primary"
                             icon="el-icon-plus"
                             size="small"
                             @click="ClassifyManage()">分类管理</el-button>
                </el-col>
                <el-col :span="17">
                  <el-cascader v-model="outselectedclass" style="width:300px;" :options="selectclasses" filterable :props="{label:'ClassName',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }" clearable @change="handleOutSelectClassChange" />
                </el-col>
              </el-row>
            </el-card>
            <el-card>
              <div class="list-btn-container">
                <el-button-group>
                  <el-button v-hasPermi="['Sys_outmodel/Add']"
                             type="primary"
                             icon="el-icon-plus"
                             size="small"
                             @click="ShowEditOrViewDialog()">新增</el-button>
                  <el-button v-hasPermi="['Sys_outmodel/Edit']"
                             type="primary"
                             icon="el-icon-edit"
                             class="el-button-modify"
                             size="small"
                             @click="ShowEditOrViewDialog('edit')">修改</el-button>
                  <el-button v-hasPermi="['Sys_outmodel/Enable']"
                             type="info"
                             icon="el-icon-video-pause"
                             size="small"
                             @click="setEnable('0')">禁用</el-button>
                  <el-button v-hasPermi="['Sys_outmodel/Enable']"
                             type="success"
                             icon="el-icon-video-play"
                             size="small"
                             @click="setEnable('1')">启用</el-button>
                  <el-button v-hasPermi="['Sys_outmodel/Delete']"
                             type="danger"
                             icon="el-icon-delete"
                             size="small"
                             @click="deletePhysics()">删除</el-button>
                  <el-button type="default" icon="el-icon-refresh" size="small" @click="loadTableData()">刷新</el-button>
                </el-button-group>
              </div>
              <el-table ref="gridtable"
                        v-loading="tableloading"
                        :data="tableData"
                        row-key="Id"
                        :height="550"
                        border
                        stripe
                        highlight-current-row
                        style="width: 100%;margin-bottom: 20px;"
                        :default-sort="{prop: 'SortCode', order: 'ascending'}"
                        @row-click="handleClickRow"
                        @select="handleSelectChange"
                        @select-all="handleSelectAllChange"
                        @sort-change="handleSortChange">
                <el-table-column type="selection" width="50" />
                <el-table-column prop="Modelcode" label="编码" width="100" />
                <el-table-column prop="Modelname" label="名称"  width="100" />
                <el-table-column prop="Sys_Name" label="所属系统" width="100" />
                <el-table-column prop="Classify_id" label="分类"  width="100" align="center">
                  <template slot-scope="scope">
                    {{ scope.row.Classify_Name }}
                  </template>
                </el-table-column>
                <el-table-column label="是否启用" width="100" prop="EnabledMark" align="center">
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
      </el-col>
      <el-col :span="8">
        <el-card>
          <div class="grid-content bg-purple">
            <div class="list-btn-container">
              <el-button-group>
                <el-button v-hasPermi="['Sys_outmodel_details/Add']"
                           type="primary"
                           icon="el-icon-plus"
                           size="small"
                           @click="ShowMiddleDialog('add')">新增</el-button>
                <el-button v-hasPermi="['Sys_outmodel_details/Edit']"
                           type="primary"
                           icon="el-icon-edit"
                           class="el-button-modify"
                           size="small"
                           @click="ShowMiddleDialog('edit')">修改</el-button>
                <el-button v-hasPermi="['Sys_outmodel_details/DeleteSoft']"
                           type="warning"
                           icon="el-icon-delete"
                           size="small"
                           @click="deleteMiddlePhysics('0')">删除</el-button>
                <el-button type="default" icon="el-icon-refresh" size="small" @click="loadMiddleTableData()">刷新</el-button>
              </el-button-group>
            </div>
            <el-table ref="gridMiddletable"
                      v-loading="tableMiddleloading"
                      :data="tableMiddleData"
                      row-key="Id"
                      :height="660"
                      border
                      max-height="660"
                      stripe
                      highlight-current-row
                      style="width: 100%;margin-bottom: 20px;"
                      :default-sort="{prop: 'Levelnum', order: 'ascending'}"
                      @select="handleMiddleSelectChange"
                      @select-all="handleMiddleSelectAllChange">
              <el-table-column type="selection" width="30" />
              <el-table-column prop="Levelnum" label="顺序"  min-width="20%" />
              <el-table-column prop="Tbname" label="表名" min-width="80%" />
            </el-table>
          </div>
        </el-card>
      </el-col>
      <el-col :span="8">
        <el-card>
          <div class="grid-content bg-purple">
            <div class="list-btn-container">
              <el-button-group>
                <el-button v-hasPermi="['Sys_outmodel_details/Edit']"
                           type="primary"
                           icon="el-icon-edit"
                           class="el-button-modify"
                           size="small"
                           @click="ShowRightDialog()">修改</el-button>
                <el-button type="default" icon="el-icon-refresh" size="small" @click="loadRightTableData()">刷新</el-button>
              </el-button-group>
            </div>
            <el-table ref="gridRighttable"
                      v-loading="tableRightloading"
                      :data="tableRightData"
                      row-key="Id"
                      :height="660"
                      border
                      max-height="660"
                      stripe
                      highlight-current-row
                      style="width: 100%;margin-bottom: 20px;"
                      :default-sort="{prop: 'Levelnum', order: 'ascending'}">
              <el-table-column prop="ColumnName" label="模型字段" min-width="40%" />
              <el-table-column label="数据库字段" min-width="60%">
                <template slot-scope="scope">
                  {{ scope.row.Tbname+'.'+ scope.row.ColumnCode}}
                </template>
              </el-table-column>
            </el-table>
          </div>
        </el-card>
      </el-col>
    </el-row>
    <el-dialog ref="dialogEditForm"
               :close-on-click-modal="false"
               :show-close="false"
               :title="editFormTitle+'数据输出模型'"
               :visible.sync="dialogEditFormVisible"
               width="640px">
      <el-form ref="editFrom" :model="editFrom" :rules="rules">
        <el-form-item label="编码" :label-width="formLabelWidth" prop="Modelcode">
          <el-input v-model="editFrom.Modelcode" placeholder="请输入配置信息编码" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="名称" :label-width="formLabelWidth" prop="Modelname">
          <el-input v-model="editFrom.Modelname" placeholder="请输入配置信息名称" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="描述" :label-width="formLabelWidth" prop="Description">
          <el-input v-model="editFrom.Description" placeholder="请输入描述" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="所属分类" :label-width="formLabelWidth" prop="Classify_id">
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

    <el-dialog ref="dialogMiddleEditForm"
               :close-on-click-modal="false"
               :show-close="false"
               :title="editMiddleFormTitle+'数据输出模型'"
               :visible.sync="dialogMiddleEditFormVisible"
               width="640px">
      <el-form ref="editMiddleFrom" :model="editMiddleFrom" :rules="Middlerules">
        <el-form-item label="数据表" :label-width="formLabelWidth" prop="Tbname">
          <el-select v-model="editMiddleFrom.Tbname" placeholder="请选择" @change="handleSelectTbChange()">
            <el-option v-for="item in SelectTbnameList"
                       :key="item.TableName"
                       :label="item.TableName"
                       :value="item.TableName" />
          </el-select>
        </el-form-item>
        <el-button @click.prevent="addStartDomain()" type="text" v-if="editMiddleFrom.dynamicFiterForm==null||editMiddleFrom.dynamicFiterForm.length==0">添加</el-button>
        <el-button @click.prevent="addStartDomainKh()" type="text" v-if="editMiddleFrom.dynamicFiterForm==null||editMiddleFrom.dynamicFiterForm.length==0">括号</el-button>
        <el-form-item v-for="(domain, index) in editMiddleFrom.dynamicFiterForm"
                      :key="index">
          <el-tag v-if="domain.KhType==0" effect="dark" :type="domain.type">(</el-tag>
          <el-tag v-if="domain.KhType==1" effect="dark" :type="domain.type">)</el-tag>
          <el-select v-model="domain.columnName" placeholder="请选择字段" style="width:20%" v-if="(domain.KhType==-1)">
            <el-option v-for="item in SelectColumnNameList"
                       :key="item.FieldName"
                       :label="item.FieldName"
                       :value="item.FieldName" />
          </el-select>
          <el-select v-model="domain.rex" placeholder="请选择操作符" style="width:20%" v-if="(domain.KhType==-1)">
            <el-option v-for="item in SelectRexList"
                       :key="item"
                       :label="item"
                       :value="item" />
          </el-select>
          <el-input v-model="domain.value" style="width:20%" v-if="(domain.KhType==-1)"></el-input>
          <el-select v-model="domain.aon" placeholder="请选择连接符" style="width:15%" v-if="(domain.KhType!=0)">
            <el-option v-for="item in SelectaonList"
                       :key="item"
                       :label="item"
                       :value="item" />
          </el-select>
          <el-button @click.prevent="removeDomain(domain)" type="text">删除</el-button>
          <el-button @click.prevent="addDomain(domain)" type="text" v-if="domain.showBtn==true&&domain.KhType!=0">添加</el-button>
          <el-button @click.prevent="addDomainKh(domain)" type="text" v-if="domain.showBtn==true&&domain.KhType!=0">括号</el-button>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="closeMiddleEditForm">取 消</el-button>
        <el-button type="primary" @click="saveMiddleEditForm()">确 定</el-button>
      </div>
    </el-dialog>

    <el-dialog ref="dialogRightEditForm"
               :close-on-click-modal="false"
               :show-close="false"
               :title="editRighteFormTitle+'数据输出模型'"
               :visible.sync="dialogRightEditFormVisible"
               width="640px">
      <div class="grid-content bg-purple">
        <div class="list-btn-container">
          <el-button-group>
            <el-button 
                       type="primary"
                       icon="el-icon-plus"
                       size="small"
                       @click="AddRightDetail()">新增</el-button>
            <el-button
                       type="warning"
                       icon="el-icon-delete"
                       size="small"
                       @click="DelRightDetail()">删除</el-button>
            <el-button type="default" icon="el-icon-refresh" size="small" @click="loadRightDetailTableData()">刷新</el-button>
          </el-button-group>
        </div>
        <el-table ref="gridRightDetailtable"
                  v-loading="tableRightDetailloading"
                  :data="tableRightDetailData"
                  row-key="Id"
                  :height="400"
                  border
                  max-height="400"
                  stripe
                  highlight-current-row
                  style="width: 100%;margin-bottom: 20px;"
                  :default-sort="{prop: 'Levelnum', order: 'ascending'}"
                  @select="handleRightDetailSelectChange"
                  @select-all="handleRightDetailSelectAllChange">
          <el-table-column type="selection" min-width="5%" />
          <el-table-column prop="Tbname" label="数据集" min-width="25%">
            <template slot-scope="scope">
              <el-select v-model="scope.row.Tbname" @change="handleSelectRightTbChange(scope.row)" placeholder="请选择" filterable allow-create>
                <el-option v-for="item in SelectRightTbNameList " :key="item.Tbname" :label="item.Tbname" :value="item.Tbname">
                </el-option>
              </el-select>
            </template>
          </el-table-column>
          <el-table-column prop="ColumnCode" label="字段编码" min-width="20%">
            <template slot-scope="scope">
              <el-select v-model="scope.row.ColumnCode" @change="handleSelectRightColumnChange(scope.row)" placeholder="请选择" filterable allow-create>
                <el-option v-for="item in scope.row.columnList " :key="item.FieldName" :label="item.FieldName" :value="item.FieldName">
                </el-option>
              </el-select>
            </template>
          </el-table-column>
          <el-table-column prop="ColumnDescription" label="字段描述" min-width="20%" />
          <el-table-column prop="ColumnName" label="字段名称" min-width="20%">
            <template slot-scope="scope">
              <input type="text" v-model="scope.row.ColumnName" style="width:100%" />
            </template>
          </el-table-column>
        </el-table>
      </div>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogRightEditFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="saveRightEditForm()">确 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>

import {
  getSys_outmodelListWithPager, getSys_outmodelDetail,
  saveSys_outmodel, setSys_outmodelEnable,
  deleteSys_outmodel
  } from '@/api/dataprocess/sys_outmodel'
import {
  getAlloutModelClassifyTreeTable
  } from '@/api/dataprocess/sys_outmodel_classify'
import {
    getSys_outmodel_detailsDetail, saveSys_outmodel_details, getAllEnableByConfId, getOutTbNameList
  } from '@/api/dataprocess/sys_outmodel_details'
  import {
    saveSys_outmodel_sql, getByOutModelId
  } from '@/api/dataprocess/sys_outmodel_sql'

  export default {
    name: 'sysconfcontrol',
    props: ['sid'], //父页面传过来的配置ID
  data() {
    return {
      searchform: {
        keywords: ''
      },
      loadBtnFunc: [],
      tableData: [],
      tableloading:false,
      pagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      sortableData: {
        order: 'desc',
        sort: 'CreatorTime'
      },
      cascaderkey: 1,
      outselectedclass: '', //配置首页上的筛选器分类
      selectedclass: '',//模型编辑内的分类
      selectclasses: [],
      dialogEditFormVisible: false,
      editFormTitle: '',
      editFrom: {
        Classify_id: '',
        Modelcode: '',
        Modelname: '',
        Description: '',
        EnabledMark: '',
        SortCode: ''

      },
      rules: {

      },
      formLabelWidth: '80px',
      currentId: '', // 当前操作对象的ID值，主要用于修改
      currentSelected: [],
      tableMiddleData: [],
      tableMiddleloading:false,
      dialogMiddleEditFormVisible: false,
      editMiddleFormTitle: '',
      editMiddleFrom: {
        Tbname: '',
        dynamicFiterForm: [],

      },
      Middlerules: {

      },
      formMiddleLabelWidth: '80px',
      currentMiddleId: '', // 当前操作对象的ID值，主要用于修改
      currentMiddleSelected: [],
      SelectTbnameList: [],
      SelectColumnNameList: [], //字段下拉框数据
      SelectRexList: [
        '=',
        '!=',
        '<',
        '<=',
        '>',
        '>=',
        '包含',
        '不包含',
        '开始以',
        '开始不是以',
        '结束以',
        '结束不是以',
        '是null',
        '不是null',
        '是空',
        '不是空',
        '介于',
        '不介于',
      ], //操作符下拉框数据
      SelectaonList: [
        'and',
        'or',
      ], //连接符下拉框数据
      tagType: [
        '',
        'success',
        'info',
        'danger',
        'warning',
      ],
      tableRightData: [],
      tableRightloading:false,
      dialogRightEditFormVisible: false,
      editRighteFormTitle: '',
      tableRightDetailData: [],
      tableRightDetailloading: false,
      currentRightDetailId: '', // 当前操作对象的ID值，主要用于修改
      currentRightDetailSelected: [],
      SelectRightTbNameList:[],
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
      
      var seachdata = {
        Filter: {
          Sysid: this.sid,
        }
      }
      getAlloutModelClassifyTreeTable(seachdata).then(res => {
        this.selectclasses = res.ResData
      })
    },
    /**
       * 加载页面table数据
       */
    loadTableData: function () {
      this.tableloading=true
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.keywords,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort,
        Filter: {
          Sysid: this.sid,
          Classify_id: this.outselectedclass,
        }
      }
      getSys_outmodelListWithPager(seachdata).then(res => {
        this.tableData = res.ResData.Items
        this.pagination.pageTotal = res.ResData.TotalItems
        this.tableloading=false
      })
      
    },

    ClassifyManage: function () {
      this.$router.push({ name: 'outModelClassify', params: { sysid: this.sid } })
    },
    /**
*选择分类
*/
    handleOutSelectClassChange: function () {
      this.loadTableData()
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
      this.loadMiddleTableData()
      this.loadRightTableData()
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
      getSys_outmodelDetail(this.currentId).then(res => {
        this.editFrom.Classify_id = res.ResData.Classify_id
        this.editFrom.Modelcode = res.ResData.Modelcode
        this.editFrom.Modelname = res.ResData.Modelname
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
            'Sysid': this.sid,
            'Modelcode': this.editFrom.Modelcode,
            'Modelname': this.editFrom.Modelname,
            'Description': this.editFrom.Description,
            'EnabledMark': this.editFrom.EnabledMark,
            'SortCode': this.editFrom.SortCode,
            'Id': this.currentId
          }
          var url = 'Sys_outmodel/Insert'
          if (this.currentId !== '') {
            url = 'Sys_outmodel/Update?id=' + this.currentId
          }
          saveSys_outmodel(data, url).then(res => {
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
        setSys_outmodelEnable(data).then(res => {
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
        deleteSys_outmodel(data).then(res => {
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
   * 加载详情页面table数据
   */
    loadMiddleTableData: function () {
      this.tableMiddleloading = true
      var seachdata = {
        Filter: {
          Sys_outmodel_id: this.currentId,
        },
      }
      getAllEnableByConfId(seachdata).then(res => {
        this.tableMiddleData = res.ResData
        this.tableMiddleloading = false
      })
    },

    /**
      * 新增、修改或查看中间列表项（绑定显示数据）     *
      */
    ShowMiddleDialog: function (view) {
      if (this.currentId !== undefined && this.currentId !== null && this.currentId.length > 0) {
        getOutTbNameList({ SysId: this.sid, outModelId:'' }).then(res => {
          let SelectTbnameListInfo = [];
          for (let i in res.ResData) {
            SelectTbnameListInfo.push(res.ResData[i]);
          }
          this.SelectTbnameList = SelectTbnameListInfo
        })
        if (view == 'edit') {
          if (this.currentMiddleSelected.length > 1 || this.currentMiddleSelected.length === 0) {
            this.$alert('请选择一条数据进行编辑/修改', '提示')
          } else {
            this.currentMiddleId = this.currentMiddleSelected[0].Id
            this.editMiddleFormTitle = '编辑'
            this.dialogMiddleEditFormVisible = true
            this.bindMiddleEditInfo()
          }
        } else {
          this.editMiddleFormTitle = '新增'
          this.currentMiddleId = ''
          this.editMiddleFrom.dynamicFiterForm = null
          this.dialogMiddleEditFormVisible = true
        }
      } else {
        this.$alert('请先在左侧列表中选择一个模型', '提示')
      }
    },
    bindMiddleEditInfo: function () {
      getSys_outmodel_detailsDetail(this.currentMiddleId).then(res => {
        this.editMiddleFrom.Tbname = res.ResData.Tbname
        this.editMiddleFrom.dynamicFiterForm = JSON.parse(res.ResData.Fiterstr)
      })
    },
    /**
   * 新增/修改保存
   */
    saveMiddleEditForm() {
      this.$refs['editMiddleFrom'].validate((valid) => {
        if (valid) {
          const data = {
            'Tbname': this.editMiddleFrom.Tbname,
            'Fiterstr': JSON.stringify(this.editMiddleFrom.dynamicFiterForm),
            'Sys_outmodel_id': this.currentId,
            'Id': this.currentMiddleId,
          }

          var url = 'Sys_outmodel_details/Insert'
          if (this.currentMiddleId !== '') {
            url = 'Sys_outmodel_details/Update?id=' + this.currentMiddleId
          }
          saveSys_outmodel_details(data, url).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
              this.dialogMiddleEditFormVisible = false
              this.$refs['editMiddleFrom'].resetFields()
              this.loadMiddleTableData()
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
    closeMiddleEditForm() {
      this.dialogMiddleEditFormVisible = false
      this.$refs['editMiddleFrom'].resetFields()
      //this.editMiddleFrom.dynamicFiterForm= []
    },
    deleteMiddlePhysics: function () {
      
    },
    /**
      * 当用户手动勾选checkbox数据行事件
      */
    handleMiddleSelectChange: function (selection, row) {
      this.currentMiddleSelected = selection
    },
    /**
       * 当用户手动勾选全选checkbox事件
       */
    handleMiddleSelectAllChange: function (selection) {
      this.currentMiddleSelected = selection
    },
    /**
          *选择表
          */
    handleSelectTbChange: function () {
      var res = this.SelectTbnameList.filter(item => {
        if (item.TableName.includes(this.editMiddleFrom.Tbname)) {
          return item
        }
      })
      this.SelectColumnNameList = res[0].Fileds
    },
    removeDomain(item) {
      var index = this.editMiddleFrom.dynamicFiterForm.indexOf(item)
      if (index !== -1) {
        if (item.KhType != -1) { //如果是括号
          var startkh;
          var endkh;
          var children = this.editMiddleFrom.dynamicFiterForm.filter(tmp => tmp.InKhIndex == item.Kh && tmp.KhType == 0)
          children.forEach(tmp => {
            this.removeDomain(tmp) //删掉整个括号
          })
          if (item.KhType == 0) {
            startkh = item
            endkh = this.editMiddleFrom.dynamicFiterForm.filter(tmp => tmp.Kh == item.Kh && tmp.KhType == 1)[0]
          } else {
            startkh = this.editMiddleFrom.dynamicFiterForm.filter(tmp => tmp.Kh == item.Kh && tmp.KhType == 0)[0]
            endkh = item
          }
          var startindex = this.editMiddleFrom.dynamicFiterForm.indexOf(startkh)
          var endindex = this.editMiddleFrom.dynamicFiterForm.indexOf(endkh)
          if (startindex != 0) {
            if (this.editMiddleFrom.dynamicFiterForm[startindex - 1].KhType == 0 && this.editMiddleFrom.dynamicFiterForm[endindex + 1].KhType == 1) { //括号外层也是括号
              var upstartkh = this.editMiddleFrom.dynamicFiterForm[startindex - 1]
              this.editMiddleFrom.dynamicFiterForm = this.editMiddleFrom.dynamicFiterForm.filter(tmp => tmp.Kh != item.Kh)
              this.removeDomain(upstartkh) //删掉上层括号
            } else {
              this.editMiddleFrom.dynamicFiterForm = this.editMiddleFrom.dynamicFiterForm.filter(tmp => tmp.Kh != item.Kh)
            }
          } else {
            this.editMiddleFrom.dynamicFiterForm = this.editMiddleFrom.dynamicFiterForm.filter(tmp => tmp.Kh != item.Kh)
          }


        } else {
          if (this.editMiddleFrom.dynamicFiterForm[index - 1].KhType == 0) { //如果上一个是左括号，则删除整个括号，并更新括号序号
            if (this.editMiddleFrom.dynamicFiterForm[index + 1].KhType == 1) { //如果下一个是右括号，则证明括号内无数据了。
              this.removeDomain(this.editMiddleFrom.dynamicFiterForm[index - 1]) //删掉整个括号
            } else {
              this.editMiddleFrom.dynamicFiterForm.splice(index, 1)
            }
          } else {
            this.editMiddleFrom.dynamicFiterForm.splice(index, 1)
          }
        }
      }
    },
    addDomain(item) {
      var index = this.editMiddleFrom.dynamicFiterForm.indexOf(item)
      if (item.InKhIndex >= 0) { //括号内，需要插入
        this.editMiddleFrom.dynamicFiterForm.splice(index+1,0,{
          columnName: '', //字段名称
          rex: '', //操作符
          value: '', //值
          Kh: item.InKhIndex, //括号序号 从1开始
          KhType: -1, //括号类型，0是起始，1是结束
          InKhIndex: item.InKhIndex, //属于第几个括号内
          NextKhIndex: item.NextKhIndex,
          type: '',
          showBtn: true, //是否显示按钮
        })
      } else { //括号外，直接新增
        this.editMiddleFrom.dynamicFiterForm.push({
          columnName: '', //字段名称
          rex: '', //操作符
          value: '', //值
          Kh: item.InKhIndex, //括号序号 从1开始
          KhType: -1, //括号类型，0是起始，1是结束
          InKhIndex: item.InKhIndex, //属于第几个括号内
          NextKhIndex: item.NextKhIndex,
          type: '',
          showBtn: true, //是否显示按钮
        })
      }
      
    },
    addDomainKh(item) {
      var index = this.editMiddleFrom.dynamicFiterForm.indexOf(item)
      if (item.InKhIndex >= 0) { //括号内，需要插入
        this.editMiddleFrom.dynamicFiterForm.splice(index + 1, 0, { //加左括号
          columnName: '', //字段名称
          rex: '', //操作符
          value: '', //值
          Kh: item.NextKhIndex, //括号序号 从1开始
          KhType: 0, //括号类型，0是起始，1是结束
          InKhIndex: item.InKhIndex, //属于第几个括号内
          NextKhIndex: item.NextKhIndex + 1,
          type: this.tagType[item.NextKhIndex %5],
          showBtn: false, //是否显示按钮
        })
        this.editMiddleFrom.dynamicFiterForm.splice(index + 2, 0, { //加数据
          columnName: '', //字段名称
          rex: '', //操作符
          value: '', //值
          Kh: item.NextKhIndex, //括号序号 从1开始
          KhType: -1, //括号类型，0是起始，1是结束
          InKhIndex: item.NextKhIndex, //属于第几个括号内
          NextKhIndex: item.NextKhIndex + 1,
          type: '',
          showBtn: true, //是否显示按钮
        })
        this.editMiddleFrom.dynamicFiterForm.splice(index + 3, 0, { //加右括号
          columnName: '', //字段名称
          rex: '', //操作符
          value: '', //值
          Kh: item.NextKhIndex, //括号序号 从1开始
          KhType: 1, //括号类型，0是起始，1是结束
          InKhIndex: item.InKhIndex, //属于第几个括号内
          NextKhIndex: item.NextKhIndex + 1,
          type: this.tagType[item.NextKhIndex % 5],
          showBtn: true, //是否显示按钮
        })
      } else { //括号外，直接新增
        this.editMiddleFrom.dynamicFiterForm.push({ //加左括号
          columnName: '', //字段名称
          rex: '', //操作符
          value: '', //值
          Kh: item.NextKhIndex, //括号序号 从1开始
          KhType: 0, //括号类型，0是起始，1是结束
          InKhIndex: item.InKhIndex, //属于第几个括号内
          NextKhIndex: item.NextKhIndex + 1,
          type: '',
          showBtn: false, //是否显示按钮
        })
        this.editMiddleFrom.dynamicFiterForm.push({ //加数据
          columnName: '', //字段名称
          rex: '', //操作符
          value: '', //值
          Kh: item.NextKhIndex, //括号序号 从1开始
          KhType: -1, //括号类型，0是起始，1是结束
          InKhIndex: item.NextKhIndex, //属于第几个括号内
          NextKhIndex: item.NextKhIndex + 1,
          type: '',
          showBtn: true, //是否显示按钮
        })
        this.editMiddleFrom.dynamicFiterForm.push({ //加右括号
          columnName: '', //字段名称
          rex: '', //操作符
          value: '', //值
          Kh: item.NextKhIndex, //括号序号 从1开始
          KhType: 1, //括号类型，0是起始，1是结束
          InKhIndex: item.InKhIndex, //属于第几个括号内
          NextKhIndex: item.NextKhIndex + 1,
          type: '',
          showBtn: true, //是否显示按钮
        })
      }
      //更新下一个括号Index
      this.editMiddleFrom.dynamicFiterForm.forEach(tmp => {
        tmp.NextKhIndex = item.NextKhIndex + 1
      })

    },
    addStartDomain() {
      this.editMiddleFrom.dynamicFiterForm=[{
        columnName: '', //字段名称
        rex: '', //操作符
        value: '', //值
        Kh: -1, //括号序号 从1开始
        KhType: -1, //括号类型，0是起始，1是结束
        InKhIndex: -1, //属于第几个括号内
        NextKhIndex: 0,
        type: '',
        showBtn: true, //是否显示按钮
      }]

    },
    addStartDomainKh() {
      this.editMiddleFrom.dynamicFiterForm=[{ //加左括号
        columnName: '', //字段名称
        rex: '', //操作符
        value: '', //值
        Kh: 0, //括号序号 从0开始
        KhType: 0, //括号类型，0是起始，1是结束
        InKhIndex: -1, //属于第几个括号内
        NextKhIndex: 1,
        type: '',
        showBtn: false, //是否显示按钮
      }]
      this.editMiddleFrom.dynamicFiterForm.push({ //加数据
        columnName: '', //字段名称
        rex: '', //操作符
        value: '', //值
        Kh: 0, //括号序号 从1开始
        KhType: -1, //括号类型，0是起始，1是结束
        InKhIndex: 0, //属于第几个括号内
        NextKhIndex: 1,
        type: '',
        showBtn: true, //是否显示按钮
      })
      this.editMiddleFrom.dynamicFiterForm.push({ //加右括号
        columnName: '', //字段名称
        rex: '', //操作符
        value: '', //值
        Kh: 0, //括号序号 从1开始
        KhType: 1, //括号类型，0是起始，1是结束
        InKhIndex: -1, //属于第几个括号内
        NextKhIndex: 1,
        type: '',
        showBtn: true, //是否显示按钮
      })

    },

    /**
 * 加载详情页面table数据
 */
    loadRightTableData: function () {
      this.tableRightloading = true
      var seachdata = {
        Filter: {
          Sys_outmodel_id: this.currentId,
        },
      }
      getByOutModelId(seachdata).then(res => {
        if (res.ResData != undefined && res.ResData != null && res.ResData.Sqlstr != undefined && res.ResData.Sqlstr != null) {
          this.tableRightData = JSON.parse(res.ResData.Sqlstr)
          this.currentRightDetailId = res.ResData.Id
        } else {
          this.tableRightData =[]
        }
        
        this.tableRightloading = false
      })
      
    },

    /**
      * 新增、修改或查看中间列表项（绑定显示数据）     *
      */
    ShowRightDialog: function () {
      if (this.currentId !== undefined && this.currentId !== null && this.currentId.length > 0) {
        getOutTbNameList({ SysId: this.sid, outModelId: this.currentId }).then(res => {
          let SelectTbnameListInfo = [];
          for (let i in res.ResData) {
            SelectTbnameListInfo.push(res.ResData[i]);
          }
          this.SelectRightTbNameList = SelectTbnameListInfo
          this.dialogRightEditFormVisible = true
          this.loadRightDetailTableData()
        })
      } else {
        this.$alert('请先在左侧列表中选择一个模型', '提示')
      }
    },
    /**
   * 新增/修改保存
   */
    saveRightEditForm() {
      var url = 'Sys_outmodel_sql/Insert'
      if (this.currentRightDetailId !== '') {
        url = 'Sys_outmodel_sql/Update?id=' + this.currentRightDetailId
      }
      this.tableRightDetailData.forEach(item => {
        if (item.columnList != undefined && item.columnList != null) {
          item.columnList=[]
        }
      })
      var data = {
        Id: this.currentRightDetailId,
        Sys_outmodel_id: this.currentId,
        Sqlstr: JSON.stringify(this.tableRightDetailData),
      }
      saveSys_outmodel_sql(data, url).then(res => {
        if (res.Success) {
          this.$message({
            message: '恭喜你，操作成功',
            type: 'success'
          })
          this.dialogRightEditFormVisible = false
          this.loadRightTableData()
        } else {
          this.$message({
            message: res.ErrMsg,
            type: 'error'
          })
        }
      })
    },
    /**
* 加载详情页面table数据
*/
    loadRightDetailTableData: function () {
      this.tableRightDetailloading=true
      this.tableRightDetailData = this.tableRightData
      this.tableRightDetailData.forEach(item => {
        if (item.columnList != undefined && item.columnList != null && item.columnList.length>0) {

        } else {
          var res = this.SelectRightTbNameList.filter(TbNameItem => TbNameItem.Tbname.includes(item.Tbname))
          item.columnList = res[0].Fileds
        }
      })
      this.tableRightDetailloading = false
    },
    /**
      * 当用户手动勾选checkbox数据行事件
      */
    handleRightDetailSelectChange: function (selection, row) {
      this.currentRightDetailSelected = selection
    },
    /**
       * 当用户手动勾选全选checkbox事件
       */
    handleRightDetailSelectAllChange: function (selection) {
      this.currentRightDetailSelected = selection
    },
    AddRightDetail() {
      if (this.tableRightDetailData != null && this.tableRightDetailData != undefined && this.tableRightDetailData.length > 0) {
        this.tableRightDetailData.push({
          id:this.uuid(),
          Tbname: '',
          ColumnCode: '',
          ColumnDescription: '',
          columnList: [],
        })
      } else {
        this.tableRightDetailData = [{
          id: this.uuid(),
          Tbname: '',
          ColumnCode: '',
          ColumnDescription: '',
          columnList: [],
        }]
      }
    },
    DelRightDetail() {
      if (this.currentRightDetailSelected.length === 0) {
        this.$alert('请选择一条数据进行删除', '提示')
      } else {
        this.currentRightDetailSelected.forEach(item => {
          var tmpIndex = this.tableRightDetailData.indexOf(item)
          this.tableRightDetailData.splice(tmpIndex, 1)
        })
        this.$refs.gridRightDetailtable.clearSelection()
      }
    },
    /**
         *选择表
         */
    handleSelectRightTbChange: function (row) {
      var res = this.SelectRightTbNameList.filter(item => {
        if (item.Tbname.includes(row.Tbname)) {
          return item
        }
      })
      row.columnList = res[0].Fileds
    },
    /**
         *选择表
         */
    handleSelectRightColumnChange: function (row) {
      var res = row.columnList.filter(item => {
        if (item.FieldName.includes(row.ColumnCode)) {
          return item
        }
      })
      row.ColumnDescription = res[0].Description
      row.ColumnName = res[0].Description
    },
    uuid() {
      var s = [];
      var hexDigits = '0123456789abcdef';
      for (var i = 0; i < 36; i++) {
        s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
      }
      s[14] = '4';
      s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1);
      s[8] = s[13] = s[18] = s[23] = '-';

      this.uuidA = s.join('');
      console.log(s.join(''), 's.join("")');
      return this.uuidA;
    },
  }
}
</script>

<style>

</style>
