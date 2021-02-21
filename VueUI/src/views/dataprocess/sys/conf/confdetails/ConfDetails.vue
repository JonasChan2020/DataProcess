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
               v-loading="tableloading"
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
      <el-table-column label="验证配置" sortable="custom" width="120" prop="EnableMark" align="center">
        <template slot-scope="scope">
          <el-tag :type="scope.row.VerifyFunctionParamter!=undefined&&scope.row.VerifyFunctionParamter!=null&&scope.row.VerifyFunctionParamter.length>5 ? 'success' : 'info'" disable-transitions>{{ scope.row.VerifyFunctionParamter!=undefined&&scope.row.VerifyFunctionParamter!=null&&scope.row.VerifyFunctionParamter.length>5 ? "是" : "否" }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="数据获取配置" sortable="custom" width="120" prop="EnableMark" align="center">
        <template slot-scope="scope">
          <el-tag :type="scope.row.GetFunctionParamter!=undefined&&scope.row.GetFunctionParamter!=null&&scope.row.GetFunctionParamter.length>5 ? 'success' : 'info'" disable-transitions>{{ scope.row.GetFunctionParamter!=undefined&&scope.row.GetFunctionParamter!=null&&scope.row.GetFunctionParamter.length>5 ? "是" : "否" }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="" width="100" align="center">
        <template slot-scope="scope">
          <el-button type="text" @click="ShowDialogVerifyEditOrViewDialog('Verify',scope.$index)">验证配置</el-button>
        </template>
      </el-table-column>
      <el-table-column label="" width="100" align="center">
        <template slot-scope="scope">
          <el-button type="text" @click="ShowDialogVerifyEditOrViewDialog('DataSync',scope.$index)">数据获取配置</el-button>
        </template>
      </el-table-column>


    </el-table>
    <div v-if="showType!=='show'" class="yuebon-page-footer">
      <el-button @click="reset">重置</el-button>
      <el-button v-preventReClick type="primary" @click="saveEditForm">保存</el-button>
    </div>
    <el-dialog ref="dialogVerifyEditForm"
               :close-on-click-modal="false"
               :title="dialogFormOpenTitle+'配置'"
               :visible.sync="dialogVerifyEditFormVisible"
               width="640px">
      <component v-if="showPlugConfig" v-bind:is="loadertpl" :SysId="SysId" :dbtype="dbtype" :formData="formData" v-on:returndata="savePlugConfig" v-on:exit="closePlugConfig"></component>
    </el-dialog>
  </div>
</template>
<script>
import {
  saveSys_conf_details,getTbNameList, getSys_conf_detailsDetail
  } from '@/api/dataprocess/sys_conf_details'
  export default {
    name: 'ConfDetails',
    props: ['cpname'],
    data() {
      return {
        tableloading: false,
        tableData: [],
        Sys_conf_id: '',
        Tbname: '',
        Is_dynamic: false,
        Is_flag: false,
        configjson: [],
        SelectTbnameList: [],
        currentId: '', // 当前操作对象的ID值，主要用于修改
        SysId: '',
        tpl: '',
        dbtype: '',
        formData: [],
        showPlugConfig: false,
        PlugType:'', //调用插件编辑页面时区分是验证还是获取
        showType: 'edit', // 操作类型编辑、新增、查看
        dialogVerifyEditFormVisible: false,
        dialogFormOpenTitle:'',
      }
    },
    created() {
      this.InitDictItem()
      this.reset()
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
    methods: {
      /**
         * 初始化数据
         */
      InitDictItem() {
        if (this.$route.params && this.$route.params.id && this.$route.params.id !== 'null') {
          this.currentId = this.$route.params.id
          this.showType = this.$route.params.showtype
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
        
      },
      /**
          * 加载页面table数据
          */
      loadTableDataByTbName: function (tb) {
        this.tableloading=true
        var res = this.SelectTbnameList.filter(item => {
          if (item.TableName.includes(tb)) {
            return item
          }
        })
        this.tableData = res[0].Fileds
        this.tableloading = false
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
   * 新增、修改或查看验证明细信息     *
   */
      ShowDialogVerifyEditOrViewDialog: function (view, index) {
        this.dbtype = view
        if (this.dbtype == 'Verify') {
          this.dialogFormOpenTitle = "验证"
          if (this.tableData[index].VerifyFunctionParamter && this.tableData[index].VerifyFunctionParamter !== null && this.tableData[index].VerifyFunctionParamter.length > 0) {
            this.formData = this.tableData[index].VerifyFunctionParamter
          }
        } else if (this.dbtype == 'DataSync') {
          this.dialogFormOpenTitle = "数据获取"
          if (this.tableData[index].GetFunctionParamter && this.tableData[index].GetFunctionParamter !== null && this.tableData[index].GetFunctionParamter.length > 0) {
            this.formData = this.tableData[index].GetFunctionParamter
          }
        }
        this.verifyTableDataIndex = index
        this.tpl = "components/PlugListComponent.vue"
        this.showPlugConfig = true
        this.dialogVerifyEditFormVisible = true
        
      },
      savePlugConfig: function (data) {
        if (data != null) {
          if (this.dbtype == 'Verify') {
            this.tableData[this.verifyTableDataIndex].VerifyFunctionParamter = data
          } else if (this.dbtype == 'DataSync') {
            this.tableData[this.verifyTableDataIndex].GetFunctionParamter = data
          }
        }
        this.closePlugConfig()
      },
      closePlugConfig: function (data) {
        this.showPlugConfig = false
        this.dialogVerifyEditFormVisible = false
      },
    },
  }
</script>
