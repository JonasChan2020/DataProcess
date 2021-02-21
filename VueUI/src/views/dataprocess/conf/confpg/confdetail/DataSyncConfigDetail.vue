<template>
  <div>
    <el-row :gutter="24">
      <el-col :span="6">
        <el-card>
          <el-table ref="gridlefttable"
                    v-loading="lefttableloading"
                    :data="lefttableData"
                    :height="700"
                    border
                    stripe
                    highlight-current-row
                    style="width: 100%"
                    @row-click="handleLeftClickRow">
            <template v-for="(item,index) in lefttableHead">
              <el-table-column :prop="item.column_name" :label="item.column_comment" :key="index" sortable="custom" min-width="item.column_minWidth"></el-table-column>
            </template>
          </el-table>
        </el-card>
      </el-col>
      <el-col :span="18">
        <el-card>
          <el-table ref="gridrighttable"
                    v-loading="righttableloading"
                    :data="righttableData"
                    :height="700"
                    border
                    stripe
                    highlight-current-row
                    style="width: 100%">
            <el-table-column type="expand" label="详情" width="50">
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
                  <el-form-item label="是否可空">
                    {{ props.row.IsNullable === true ? "是" : "否" }}
                  </el-form-item>
                  <el-form-item label="是否主键">
                    {{ props.row.IsIdentity === true ? "是" : "否" }}
                  </el-form-item>
                  <el-form-item label="是否自增">
                    {{ props.row.Increment === true ? "是" : "否" }}
                  </el-form-item>
                </el-form>
              </template>
            </el-table-column>
            <el-table-column prop="FieldName" label="字段名称" sortable="custom" min-width='25%'></el-table-column>
            <el-table-column prop="Description" label="描述" sortable="custom" min-width='25%'></el-table-column>
            <el-table-column label="是否配置" sortable="custom" min-width='25%'></el-table-column>
            <el-table-column label="配置" min-width='25%' align="center">
              <template slot-scope="scope">
                <el-button type="text" @click="ShowDialogVerifyEditOrViewDialog('Verify',scope.$index)">配置</el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-card>
      </el-col>
    </el-row>
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
    getConfTbContent
  } from '@/api/dataprocess/conf_detail'

  export default {
    name:  'DataSyncConfigDetail ',
    props: ['cid','dbtype'], //父页面传过来的配置ID
  data() {
    return {
      lefttableloading: false,
      lefttableData: [],
      lefttableHead:[],
      leftcurrentSelectId: '',
      righttableloading: false,
      righttableData: [],
      rightcurrentSelectId: '',
      SysId: '',
      tpl: '',
      dbtype: '',
      formData: [],
      showPlugConfig: false,
      PlugType: '', //调用插件编辑页面时区分是验证还是获取
      showType: 'edit', // 操作类型编辑、新增、查看
      dialogVerifyEditFormVisible: false,
      dialogFormOpenTitle: '',
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
    this.loadLeftTableData()
    this.loadBtnFunc = JSON.parse(localStorage.getItem('yueboncurrentfuns'))
  },
  methods: {
    /**
      * 加载页面左侧table数据
      */
    loadLeftTableData: function () {
      this.lefttableloading = true
      var seachdata = {
        Pkey: this.cid
      }
      if (this.dbtype == '0') { //系统
        this.lefttableHead = [
          { column_name: "Confcode", column_comment: "模型编码", column_minWidth: "25%" },
          { column_name: "Confname", column_comment: "模型名称", column_minWidth: "25%" },
          { column_name: "Description", column_comment: "描述", column_minWidth: "50%" }
        ]
      } else if (this.dbtype == '1') { //数据库
        this.lefttableHead = [
          { column_name: "TableName", column_comment: "表名", column_minWidth: "25%" },
          { column_name: "Description", column_comment: "描述", column_minWidth: "75%" }
        ]
      }
      getConfTbContent(seachdata).then(res => {
        this.lefttableData = res.ResData
        this.lefttableloading = false
      })
      
    },
    /**
    * 点击一条记录
    */
    handleLeftClickRow(row) {
      this.leftcurrentSelectId = row.Id
      if (this.dbtype == '0') { //系统
       
      } else if (this.dbtype == '1') { //数据库
        this.loadRightTableData(row.Fileds)
      }
    },

    /**
      * 加载页面左侧table数据
      */
    loadRightTableData: function (data) {
      this.righttableloading = true
      this.righttableData = data
      this.righttableloading = false
    },
    /**
       * 新增/修改保存
       */
    saveDataSyncEditForm() {
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
          this.righttableData[this.verifyTableDataIndex].VerifyFunctionParamter = data
        } else if (this.dbtype == 'DataSync') {
          this.righttableData[this.verifyTableDataIndex].GetFunctionParamter = data
        }
      }
      this.closePlugConfig()
    },
    closePlugConfig: function (data) {
      this.showPlugConfig = false
      this.dialogVerifyEditFormVisible = false
    },
  }
}
</script>

<style>
</style>
