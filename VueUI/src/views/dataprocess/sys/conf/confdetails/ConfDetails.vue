<template>
  <div>
    <el-row>
      <el-col :span="10">
        需配置的数据表：<el-select v-model="Tbname" placeholder="请选择" @change="handleSelectTbChange()">

          <el-option v-for="item in SelectTbnameList"
                     :key="item.TableName"
                     :label="item.TableName"
                     :value="item.TableName" />
        </el-select>
      </el-col>
      <el-col :span="2">
        <el-checkbox v-model="Is_flag">标识表</el-checkbox>
      </el-col>
      <el-col :span="2">
        <el-checkbox v-model="Is_dynamic">动态表</el-checkbox>
      </el-col>
      <el-col :span="10">
      </el-col>
    </el-row>
    <el-table ref="gridtable"
              :data="tableData"
              row-key="FieldName"
              height="500"
              border
              stripe
              highlight-current-row
              style="width: 100%;margin-bottom: 20px;"
              :default-sort="{prop: 'SortCode',order: 'ascending'}">
      <el-table-column type="expand" width="50">
        <template slot-scope="props">
          <el-form label-position="left" inline class="demo-table-expand">
            <el-form-item label="名称">
              <span>{{ props.row.FieldName }}</span>
            </el-form-item>
            <el-form-item label="描述">
              <span>{{ props.row.Description }}</span>
            </el-form-item>
            <el-form-item label="数据类型">
              <span>{{ props.row.DataType }}</span>
            </el-form-item>
            <el-form-item label="小数位精度">
              <span>{{ props.row.FieldScale }}</span>
            </el-form-item>
            <el-form-item label="字段长度">
              <span>{{ props.row.FieldMaxLength }}</span>
            </el-form-item>
            <el-form-item label="默认值">
              <span>{{ props.row.FieldDefaultValue }}</span>
            </el-form-item>
            <el-form-item label="是否可空" sortable="custom" width="120" prop="IsNullable" align="center">
              <template slot-scope="scope">
                <el-tag :type="scope.row.IsNullable === true ? 'success' : 'info'" disable-transitions>{{ scope.row.IsNullable === true ? "是" : "否" }}</el-tag>
              </template>
            </el-form-item>
            <el-form-item label="是否主键" sortable="custom" width="120" prop="IsIdentity" align="center">
              <template slot-scope="scope">
                <el-tag :type="scope.row.IsIdentity === true ? 'success' : 'info'" disable-transitions>{{ scope.row.IsIdentity === true ? "是" : "否" }}</el-tag>
              </template>
            </el-form-item>
            <el-form-item label="是否自增" sortable="custom" width="120" prop="Increment" align="center">
              <template slot-scope="scope">
                <el-tag :type="scope.row.Increment === true ? 'success' : 'info'" disable-transitions>{{ scope.row.Increment === true ? "是" : "否" }}</el-tag>
              </template>
            </el-form-item>
          </el-form>
        </template>
      </el-table-column>
      <el-table-column prop="FieldName" label="名称" sortable="custom" width="120" align="center" />
      <el-table-column prop="Description" label="描述" sortable="custom" width="200" align="center" />
      <el-table-column label="唯一判定项" sortable="custom" width="120" prop="IsNullable" align="center">
        <template slot-scope="scope">
          <el-switch v-model="scope.row.Is_SingleDataKey === true"
                     active-color="#13ce66"
                     inactive-color="#ff4949"
                     @change='switchchange(scope.$index,"Is_SingleDataKey",$event)'>
          </el-switch>
        </template>
      </el-table-column>
      <el-table-column label="配置中显示" sortable="custom" width="120" prop="IsNullable" align="center">
        <template slot-scope="scope">
          <el-switch v-model="scope.row.Is_Visible === true"
                     active-color="#13ce66"
                     inactive-color="#ff4949"
                     @change='switchchange(scope.$index,"Is_Visible",$event)'>
          </el-switch>
        </template>
      </el-table-column>
      <el-table-column label="是否为ID字段" sortable="custom" width="150" prop="IsNullable" align="center">
        <template slot-scope="scope">
          <el-switch v-model="scope.row.Is_KeyColumn === true"
                     active-color="#13ce66"
                     inactive-color="#ff4949"
                     @change='switchchange(scope.$index,"Is_KeyColumn",$event)'>
          </el-switch>
        </template>
      </el-table-column>
      <el-table-column label="是否不为空" sortable="custom" width="120" prop="IsNullable" align="center">
        <template slot-scope="scope">
          <el-switch v-model="scope.row.Is_NotNull === true"
                     active-color="#13ce66"
                     inactive-color="#ff4949"
                     @change='switchchange(scope.$index,"Is_NotNull",$event)'>
          </el-switch>
        </template>
      </el-table-column>
      <el-table-column label="查询不到是否为空" sortable="custom" width="170" prop="IsNullable" align="center">
        <template slot-scope="scope">
          <el-switch v-model="scope.row.Is_ChangeWhite === true"
                     active-color="#13ce66"
                     inactive-color="#ff4949"
                     @change='switchchange(scope.$index,"Is_ChangeWhite",$event)'>
          </el-switch>
        </template>
      </el-table-column>
      <el-table-column label="更新时保留" sortable="custom" width="120" prop="IsNullable" align="center">
        <template slot-scope="scope">
          <el-switch v-model="scope.row.Is_NoUpdate === true"
                     active-color="#13ce66"
                     inactive-color="#ff4949"
                     @change='switchchange(scope.$index,"Is_NoUpdate",$event)'>
          </el-switch>
        </template>
      </el-table-column>
      <el-table-column label="验证配置" width="100" align="center">
        <template slot-scope="scope">
          <el-button type="text" @click="ShowDialogVerifyEditOrViewDialog(scope.row,scope.$index)">验证配置</el-button>
        </template>
      </el-table-column>
      <el-table-column prop="DataGetType" label="获取方式" sortable="custom" width="200" align="center">
        <template slot-scope="scope">
          <el-select v-model="scope.row.DataGetType"
                     placeholder="请选择类型"
                     @change="handleDataGetTypeChange">
            <el-option v-for="item in SelectDataGetTypeList"
                       :key="item.Id"
                       :label="item.Pname"
                       :value="{value:item.Id,btnvisib:item.HasPage,index:scope.$index,configuri:item.ConfigUri}" />
          </el-select>
        </template>
      </el-table-column>
      <el-table-column label="获取配置" sortable="custom" width="120" align="center">
        <template slot-scope="scope">
          <el-button type="primary"
                     icon="el-icon-plus"
                     size="small"
                     :disabled="scope.row.HasPage==false"
                     @click="GetDataOpenParamPage(scope.row,scope.$index,'getdata')">获取配置</el-button>
        </template>
      </el-table-column>


    </el-table>
    <div v-if="showType!=='show'" class="yuebon-page-footer">
      <el-button @click="reset">重置</el-button>
      <el-button v-preventReClick type="primary" @click="saveEditForm">保存</el-button>
    </div>
    <el-dialog ref="dialogPlugEditForm"
               :close-on-click-modal="false"
               :show-close="false"
               :title="'插件'"
               :visible.sync="dialogPlugEditFormVisible"
               width="640px">
      <component v-if="showDetailConfig" v-bind:is="loadertpl" :detailConfig="detailConfig" v-on:listenTochildEvent="saveDetailConfig"></component>
    </el-dialog>
    <el-dialog ref="dialogVerifyEditForm"
               :close-on-click-modal="false"
               :title="'验证配置'"
               :visible.sync="dialogVerifyEditFormVisible"
               width="640px">
      <div class="list-btn-container">
        <el-button-group>
          <el-button v-hasPermi="['Sd_sysdb/Add']"
                     type="primary"
                     icon="el-icon-plus"
                     size="small"
                     @click="GetDataOpenParamPage('','','verify')">新增</el-button>
          <el-button v-hasPermi="['Sd_sysdb/Edit']"
                     type="primary"
                     icon="el-icon-edit"
                     class="el-button-modify"
                     size="small"
                     @click="GetDataOpenParamPage('','','verify')">修改</el-button>
          <el-button v-hasPermi="['Sd_sysdb/Enable']"
                     type="info"
                     icon="el-icon-video-pause"
                     size="small"
                     @click="setVerifyDialogEnable('0')">禁用</el-button>
          <el-button v-hasPermi="['Sd_sysdb/Enable']"
                     type="success"
                     icon="el-icon-video-play"
                     size="small"
                     @click="setVerifyDialogEnable('1')">启用</el-button>
          <el-button v-hasPermi="['Sd_sysdb/Delete']"
                     type="danger"
                     icon="el-icon-delete"
                     size="small"
                     @click="deleteVerifyPhysics()">删除</el-button>
          <el-button type="default" icon="el-icon-refresh" size="small" @click="loadVerifyDialogTableData()">刷新</el-button>
        </el-button-group>
      </div>
      <el-table ref="griddialogtable"
                v-loading="dialogVerifytableloading"
                :data="dialogVerifytableData"
                border
                stripe
                highlight-current-row
                style="width: 100%"
                @select="handleVerifyDialogSelectChange"
                @select-all="handleVerifyDialogSelectAllChange">
        <el-table-column type="selection" width="30" />
        <el-table-column prop="SdName" label="编码" sortable="custom" min-width='40%' />
        <el-table-column prop="SdName" label="名称" sortable="custom" min-width='60%' />
      </el-table>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogVerifyEditFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="saveVerifyEditForm()">确 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>
<script>
import {
  saveSys_conf_details,getTbNameList, getSys_conf_detailsDetail
  } from '@/api/dataprocess/sys_conf_details'
  import {
    getDataGetTypeLists
  } from '@/api/dataprocess/plug_plug'
  export default {
    name: 'ConfDetails',
    props: ['cpname'],
    data() {
      return {
        tpl: '',
        tableloading: true,
        tableData: [],
        Sys_conf_id: '',
        SysId:'',
        Tbname: '',
        Is_dynamic: false,
        Is_flag: false,
        configjson: [],
        dialogPlugEditFormVisible: false,
        showDetailConfig: false,
        PlugType:'', //调用插件编辑页面时区分是验证还是获取
        SelectTbnameList: [],
        SelectedTbName: '',
        SelectedDataGetType: '',
        SelectDataGetTypeList: [],
        currentId: '', // 当前操作对象的ID值，主要用于修改
        showType: 'edit', // 操作类型编辑、新增、查看
        customplugpage: '',
        detailConfig: '', //所选记录中的获取值详细配置信息
        currentOpenIndex: '', //记录当前开启获取方式详细配置的记录号
        dialogVerifyEditFormVisible: false,
        dialogVerifytableloading: false,
        dialogVerifytableData: [],
        SelectVerifyGetTypeList:[],
        loadBtnFunc: [],
        currentVerifyDialogId: '', // 当前操作对象的ID值，主要用于修改
        currentVerifyDialogSelected: [],
        paramPageRowInfo: [],
        paramPageIndexInfo: '',
        verifyTableDataIndex:'', //用于记录当打开验证配置列表时字段表的行号
      }
    },
    computed: {
      loadertpl() {
        const self = this;
        if (!self.tpl) return "";

        return function (resolve) {
          require([`@/views/dataprocess/${self.tpl}`], resolve)
        };
      }
    },
    created() {
      this.InitDictItem()
      this.reset()
    },
    methods: {
      /**
         * 初始化数据
         */
      InitDictItem() {
        if (this.$route.params && this.$route.params.id && this.$route.params.id !== 'null') {
          this.currentId = this.$route.params.id
          this.showType = this.$route.params.showtype
          //this.bindEditInfo()
        }
        this.Sys_conf_id = this.$route.params.Sys_conf_id
        this.SysId = this.$route.params.SysId
        getTbNameList({ SysId:this.SysId }).then(res => {
          let SelectTbnameListInfo = [];
          for (let i in res.ResData) {
            SelectTbnameListInfo.push(res.ResData[i]);
          }
          this.SelectTbnameList = SelectTbnameListInfo
        })
        getDataGetTypeLists({ SysId: this.SysId, ptype:'DataModelPlug' }).then(res => {
          this.SelectDataGetTypeList = res.ResData
        })
        getDataGetTypeLists({ SysId: this.SysId, ptype: 'DataModelPlug' }).then(res => {
          this.SelectVerifyGetTypeList = res.ResData
        })
      },
      /**
          * 加载页面table数据
          */
      loadTableDataByTbName: function (tb) {
        var res = this.SelectTbnameList.filter(item => {
          if (item.TableName.includes(tb)) {
            return item
          }
        })
        this.tableData = res
      },
      /**
       * 读取详情
       */
      bindEditInfo: function () {
        getSys_conf_detailsDetail({ SysId: this.SysId, id: this.currentId }).then(res => {
          this.Tbname = res.ResData.Tbname
          this.Sys_conf_id = res.ResData.Sys_conf_id
          this.Is_dynamic = res.ResData.Is_dynamic
          this.Is_flag = res.ResData.Is_flag
          this.configjson = res.ResData.configjson
          this.tableData = JSON.parse(res.ResData.configjson)
        })
      },

      GetDataOpenParamPage: function (row, index, type) {
        this.PlugType = type
        if (this.PlugType == 'verify') {
          this.OpenConfigPage()
        } else if (this.PlugType == 'getdata') {
          this.paramPageRowInfo = row
          this.paramPageIndexInfo = index
          this.OpenConfigPage()
        }
      },
      /**
        * 打开配置页面
        */
      OpenConfigPage: function () {
        let Base64 = require('js-base64').Base64
        if (this.PlugType = 'verify') {
          if (this.paramPageRowInfo.OperaParamers.length > 0) {
            this.detailConfig = this.paramPageRowInfo.OperaParamers
          } else {
            this.detailConfig = ''
          }
        } else if (this.PlugType = 'getdata') {
          if (this.paramPageRowInfo.GetFunctionParamter.length > 0) {
            this.detailConfig = Base64.decode(this.paramPageRowInfo.GetFunctionParamter)
          } else {
            this.detailConfig = ''
          }
          
        }
        if (this.PlugType = 'verify') {
          this.tpl = "uploadplug/verifytest/index.vue"
        } else if (this.PlugType = 'getdata') {
          this.tpl = "uploadplug/test1/index.vue"
        }
        this.currentOpenIndex = this.paramPageIndexInfo
        this.showDetailConfig = true
        this.dialogPlugEditFormVisible = true
      },
      saveDetailConfig(data) {
        if (data !== null) {
          let Base64 = require('js-base64').Base64
          if (this.PlugType = 'verify') {
            this.dialogVerifytableData[this.currentOpenIndex].OperaParamers = data
          } else if (this.PlugType = 'getdata') {
            this.tableData[this.currentOpenIndex].GetFunctionParamter = Base64.encode(JSON.stringify(data))
          }
        }
        this.tpl = ''
        this.currentOpenIndex = -1
        this.detailConfig = ''
        this.PlugType = ''
        this.paramPageRowInfo = []
        this.paramPageIndexInfo = ''
        this.dialogPlugEditFormVisible = false
        this.showDetailConfig=false
      },
      /**
         * 新增/修改保存
         */
      saveEditForm() {
        this.configjson = this.tableData
        const data = {
          'Sys_conf_id': this.Sys_conf_id,
          'Tbname': this.Tbname,
          'Is_dynamic': this.Is_dynamic,
          'Is_flag': this.Is_flag,
          'configjson': JSON.stringify(this.tableData),
          'Id': this.currentId
        }
        var url = 'Sys_conf_details/Insert'
        if (this.currentId !== '') {
          url = 'Sys_conf_details/Update?id=' + this.currentId
        }
        saveSys_conf_details(data, url).then(res => {
          if (res.Success) {
            this.$message({
              message: '恭喜你，操作成功',
              type: 'success'
            })
            this.reset()
            this.InitDictItem()
          } else {
            this.$message({
              message: res.ErrMsg,
              type: 'error'
            })
          }
        })
      },
      reset() {
        if (!this.currentId) {
          this.Tbname = ''
          this.tableData=[]
          this.Is_dynamic = false
          this.Is_flag = false
          this.configjson = []
        } else {
          this.bindEditInfo()
        }
      },
    /**
       * 表格内开关变换后改变表格值
       */
      switchchange(index, propname, $event) {
        if (propname === 'Is_KeyColumn') {
          var i
          for (i = 0; i < this.tableData.length; i++) {
            this.tableData[i][propname] = false
          }
        } 
        this.tableData[index][propname] = $event
       
        this.tableData = JSON.parse(JSON.stringify(this.tableData));
      },
      /**
           *选择表
           */
      handleSelectTbChange: function () {
        this.loadTableDataByTbName(this.Tbname)
      },
    /**
         *展开详细表
         */
      toogleExpand(row) {
        const $table = this.$refs.gridtable
        this.tableData.map((item) => {
          if (row.FieldName !== item.FieldName) {
            $table.toggleRowExpansion(item, false)
          }
        })
        $table.toggleRowExpansion(row)
      },
    /**
         *当获取方式下拉框发生变化时
         */
      handleDataGetTypeChange: function (params) {
        const { value, btnvisib, index, configuri } = params
        this.tableData[index].GetFunctionParamter=''
        this.tableData[index].HasPage = btnvisib
        this.tableData[index].ConfigUri = configuri
      },
      /**
    * 加载页面table数据
    */
      loadVerifyDialogTableData: function () {
        this.dialogVerifytableloading = true
        let Base64 = require('js-base64').Base64
        this.dialogVerifytableData = Base64.decode(this.tableData[this.verifyTableDataIndex].VerifyFunctionParamter) 
        this.dialogVerifytableloading = false
      },
      /**
   * 新增、修改或查看验证明细信息     *
   */
      ShowDialogVerifyEditOrViewDialog: function ( index) {
        this.verifyTableDataIndex = index
        this.dialogVerifyEditFormVisible = true
        this.loadVerifyDialogTableData()
      },
      saveVerifyEditForm() {
        let Base64 = require('js-base64').Base64
        this.tableData[this.verifyTableDataIndex].VerifyFunctionParamter = Base64.encode(JSON.stringify(this.dialogVerifytableData))
        this.dialogVerifyEditFormVisible = false
        this.dialogVerifytableData = []
        this.verifyTableDataIndex = ''
      },
      setVerifyDialogEnable: function (val) {
        if (this.currentVerifyDialogSelected.length === 0) {
          this.$alert('请先选择要操作的数据', '提示')
          return false
        } else {
          var currentVerifyDialogIds = []
          this.currentVerifyDialogSelected.forEach(element => {
            currentVerifyDialogIds.push(element.Id)
          })
          const data = {
            Ids: currentVerifyDialogIds,
            Flag: val
          }
          //setSd_sysdbEnable(data).then(res => {
          //  if (res.Success) {
          //    this.$message({
          //      message: '恭喜你，操作成功',
          //      type: 'success'
          //    })
          //    this.currentDialogSelected = ''
          //    this.loadDialogTableData()
          //  } else {
          //    this.$message({
          //      message: res.ErrMsg,
          //      type: 'error'
          //    })
          //  }
          //})
        }
      },
      deleteVerifyPhysics: function () {
        if (this.currentVerifyDialogSelected.length === 0) {
          this.$alert('请先选择要操作的数据', '提示')
          return false
        } else {
          var currentVerifyDialogIds = []
          this.currentVerifyDialogSelected.forEach(element => {
            currentVerifyDialogIds.push(element.Id)
          })
          const data = {
            Ids: currentVerifyDialogIds
          }
          //deleteSd_sysdb(data).then(res => {
          //  if (res.Success) {
          //    this.$message({
          //      message: '恭喜你，操作成功',
          //      type: 'success'
          //    })
          //    this.currentDialogSelected = ''
          //    this.loadDialogTableData()
          //  } else {
          //    this.$message({
          //      message: res.ErrMsg,
          //      type: 'error'
          //    })
          //  }
          //})
        }
      },
      /**
       * 当用户手动勾选checkbox数据行事件
       */
      handleVerifyDialogSelectChange: function (selection, row) {
        this.currentVerifyDialogSelected = selection
        this.paramPageRowInfo = row
        this.dialogVerifytableData.forEach((v, i) => {
          if (row.Id == v.id) {
            this.paramPageIndexInfo = i;
          }
        })

      },
      /**
       * 当用户手动勾选全选checkbox事件
       */
      handleVerifyDialogSelectAllChange: function (selection) {
        this.currentVerifyDialogSelected = selection
      },
    },
  }
</script>
