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
                <el-button type="text" @click="ShowDialogEditOrViewDialog()">配置</el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-card>
      </el-col>
    </el-row>
    <el-dialog ref="dialogEditForm"
               :close-on-click-modal="false"
               :title="'验证配置'"
               :visible.sync="dialogEditFormVisible"
               width="640px">
      <div class="list-btn-container">
        <el-button-group>
          <el-button
                     v-hasPermi="['Sd_sysdb/Add']"
                     type="primary"
                     icon="el-icon-plus"
                     size="small"
                     @click="OpenConfigPage()">新增</el-button>
          <el-button
                     v-hasPermi="['Sd_sysdb/Edit']"
                     type="primary"
                     icon="el-icon-edit"
                     class="el-button-modify"
                     size="small"
                     @click="OpenConfigPage()">修改</el-button>
          <el-button
                     v-hasPermi="['Sd_sysdb/Enable']"
                     type="info"
                     icon="el-icon-video-pause"
                     size="small"
                     @click="setDialogEnable('0')">禁用</el-button>
          <el-button
                     v-hasPermi="['Sd_sysdb/Enable']"
                     type="success"
                     icon="el-icon-video-play"
                     size="small"
                     @click="setDialogEnable('1')">启用</el-button>
          <el-button
                     v-hasPermi="['Sd_sysdb/Delete']"
                     type="danger"
                     icon="el-icon-delete"
                     size="small"
                     @click="deletePhysics()">删除</el-button>
          <el-button type="default" icon="el-icon-refresh" size="small" @click="loadDialogTableData()">刷新</el-button>
        </el-button-group>
      </div>
      <el-table ref="griddialogtable"
                v-loading="dialogtableloading"
                :data="dialogtableData"
                border
                stripe
                highlight-current-row
                style="width: 100%"
                @select="handleDialogSelectChange"
                @select-all="handleDialogSelectAllChange">
        <el-table-column type="selection" width="30" />
        <el-table-column prop="SdName" label="编码" sortable="custom" min-width='40%' />
        <el-table-column prop="SdName" label="名称" sortable="custom" min-width='60%' />
      </el-table>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogEditFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="saveDataSyncEditForm()">确 定</el-button>
      </div>
    </el-dialog>
    <el-dialog ref="dialogDataSyncEditForm"
               :close-on-click-modal="false"
               :show-close="false"
               :title="'插件'"
               :visible.sync="dialogDataSyncEditFormVisible"
               width="640px">
      <component v-if="showDataSyncDetailConfig" v-bind:is="loadertpl" :DataSyncConfig="DataSyncConfig" v-on:listenTochildEvent="saveDataSyncDetailConfig"></component>
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
      dialogEditFormVisible: false,
      dialogtableloading: false,
      dialogtableData: [],
      loadBtnFunc: [],
      currentDialogId: '', // 当前操作对象的ID值，主要用于修改
      currentDialogSelected: [],
      dialogDataSyncEditFormVisible: false,
      showDataSyncDetailConfig: false,
      DataSyncConfig: '', //所选记录中的获取值详细配置信息
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
    * 加载页面table数据
    */
    loadDialogTableData: function () {
      this.dialogtableloading = true
      var seachdata = {
        
      }
      //getSd_sysdbListWithPager(seachdata).then(res => {
      //  this.dialogtableData = res.ResData.Items
      //  this.dialogtableloading = false
      //})
    },
    /**
 * 新增、修改或查看验证明细信息     *
 */
    ShowDialogEditOrViewDialog: function () {
        this.dialogEditFormVisible = true
    },
    setDialogEnable: function (val) {
      if (this.currentDialogSelected.length === 0) {
        this.$alert('请先选择要操作的数据', '提示')
        return false
      } else {
        var currentDialogIds = []
        this.currentDialogSelected.forEach(element => {
          currentDialogIds.push(element.Id)
        })
        const data = {
          Ids: currentDialogIds,
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
    deleteDialogPhysics: function () {
      if (this.currentDialogSelected.length === 0) {
        this.$alert('请先选择要操作的数据', '提示')
        return false
      } else {
        var currentDialogIds = []
        this.currentDialogSelected.forEach(element => {
          currentDialogIds.push(element.Id)
        })
        const data = {
          Ids: currentDialogIds
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
    handleDialogSelectChange: function (selection, row) {
      this.currentDialogSelected = selection
    },
    /**
     * 当用户手动勾选全选checkbox事件
     */
    handleDialogSelectAllChange: function (selection) {
      this.currentDialogSelected = selection
    },

    /**
        * 打开配置页面
        */
    OpenConfigPage: function (type) {
      if (row.GetFunctionParamter.length > 0) {
        let Base64 = require('js-base64').Base64
        if (type = 'DataSync') {
          this.detailConfig = Base64.decode(row.DataSyncFunctionParamter)
        } else if (type = 'DataSync') {
          this.detailConfig = Base64.decode(row.GetFunctionParamter)
        }
        
      } else {
        this.detailConfig = ''
      }
      if (type = 'DataSync') {
        this.tpl = "uploadplug/DataSynctest/index.vue"
      } else if (type = 'DataSync') {
        this.tpl = "uploadplug/test1/index.vue"
      }
      this.currentOpenIndex = index
      this.showDetailConfig = true
      this.dialogEditFormVisible = true
    },

    saveDataSyncDetailConfig(data) {
      if (data !== null) {
        let Base64 = require('js-base64').Base64
        this.tableData[this.currentOpenIndex].GetFunctionParamter = Base64.encode(JSON.stringify(data))
      }
      this.tpl = ''
      this.currentOpenIndex = -1
      this.detailConfig = ''
      this.dialogEditFormVisible = false
      this.showDetailConfig = false
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
  }
}
</script>

<style>
</style>
