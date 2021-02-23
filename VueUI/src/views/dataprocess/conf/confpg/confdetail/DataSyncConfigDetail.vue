<template>
  <div>
    <el-card>
      <el-table ref="gridrighttable"
                v-loading="righttableloading"
                :data="righttableData"
                :height="700"
                border
                stripe
                highlight-current-row
                @row-click="handleTableClickRow"
                style="width: 100%">
        <el-table-column type="expand" label="详情" width="50">
          <template slot-scope="props">
            <el-form label-position="left" inline class="demo-table-expand">
              <el-form-item label="表顺序">
                <span>{{ props.row.TableLevelNum }}</span>
              </el-form-item>
              <el-form-item label="表名称">
                <span>{{ props.row.WriteTableName }}</span>
              </el-form-item>
              <el-form-item label="字段名称">
                <span>{{ props.row.WriteFieldName }}</span>
              </el-form-item>
              <el-form-item label="字段描述">
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
        <el-table-column prop="WriteDescription" label="字段描述" min-width='20%'></el-table-column>
        <el-table-column prop="ReadFieldName" label="对应表字段" min-width='20%'>
          <template slot-scope="scope">
            {{ scope.row.ReadFieldInfo!=undefined&&scope.row.ReadFieldInfo!=null ? scope.row.ReadFieldInfo.TableLevelNum+'^'+scope.row.ReadFieldInfo.WriteTableName+'^'+scope.row.ReadFieldInfo.FieldName : "" }}
            <el-button type="text" @click="ShowDialogReadFieldEditOrViewDialog('ReadField',scope.$index,scope.row)">配置</el-button>
          </template>
        </el-table-column>
        <el-table-column prop="DefaultValue" label="默认值" min-width='10%'></el-table-column>
        <el-table-column prop=" Is_DynamicSingle" label="动态表唯一判定字段" min-width='15%'></el-table-column>
        <el-table-column label="配置" min-width='15%' align="center">
          <template slot-scope="scope">
            <el-button type="text" @click="ShowDialogVerifyEditOrViewDialog('Verify',scope.$index)">配置</el-button>
            <el-tag :type="scope.row.SyncDataConfParamter!=undefined&&scope.row.SyncDataConfParamter!=null&&scope.row.SyncDataConfParamter.length>5 ? 'success' : 'info'" disable-transitions>{{ scope.row.SyncDataConfParamter!=undefined&&scope.row.SyncDataConfParamter!=null&&scope.row.SyncDataConfParamter.length>5 ? "(已配置)" : "(未配置)" }}</el-tag>
          </template>
        </el-table-column>
      </el-table>
    </el-card>
    <div slot="footer" class="dialog-footer">
      <el-button @click="cancleEditForm()">取 消</el-button>
      <el-button type="primary" @click="saveEditForm()">确 定</el-button>
    </div>
    <el-dialog ref="dialogReadFieldInfo"
               :close-on-click-modal="false"
               :show-close="false"
               :title="'对应列'"
               :visible.sync="dialogReadFieldFormVisible"
               append-to-body
               width="640px">
      <el-card>
        <el-table ref="gridreadfieldtable"
                  v-loading="readFieldtableloading"
                  :data="readFieldTableData"
                  :height="400"
                  border
                  stripe
                  highlight-current-row
                  @row-click="handleReadFieldClickRow"
                  style="width: 100%">
          <el-table-column type="expand" label="详情" width="50">
            <template slot-scope="props">
              <el-form label-position="left" inline class="demo-table-expand">
                <el-form-item label="表顺序">
                  <span>{{ props.row.TableLevelNum }}</span>
                </el-form-item>
                <el-form-item label="表名称">
                  <span>{{ props.row.WriteTableName }}</span>
                </el-form-item>
                <el-form-item label="字段名称">
                  <span>{{ props.row.WriteFieldName }}</span>
                </el-form-item>
                <el-form-item label="字段描述">
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
          <el-table-column prop="TableLevelNum" label="表顺序" min-width='10%'></el-table-column>
          <el-table-column prop="WriteTableName" label="表名称" min-width='15%'></el-table-column>
          <el-table-column prop="WriteFieldName" label="字段名称" min-width='15%'></el-table-column>
          <el-table-column prop=" Description" label="字段描述" min-width='25%'></el-table-column>
        </el-table>
      </el-card>
      <div slot="footer" class="dialog-footer">
        <el-button @click="cancleReadFieldEditForm()">取 消</el-button>
        <el-button type="primary" @click="saveReadFieldEditForm()">确 定</el-button>
      </div>
    </el-dialog>
    <el-dialog ref="dialogVerifyEditForm"
               :close-on-click-modal="false"
               :show-close="false"
               :title="dialogFormOpenTitle+'配置'"
               :visible.sync="dialogVerifyEditFormVisible"
               width="640px">
      <component v-if="showPlugConfig" v-bind:is="loadertpl" :SysId="SysId" :dbtype="dtype" :formData="formData" v-on:returndata="savePlugConfig" v-on:exit="closePlugConfig"></component>
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
      dtype:'',
      righttableloading: false,
      righttableData: [],
      rightcurrentSelectId: [],
      SysId: '',
      tpl: '',
      formData: [],
      showPlugConfig: false,
      PlugType: '', //调用插件编辑页面时区分是验证还是获取
      showType: 'edit', // 操作类型编辑、新增、查看
      dialogVerifyEditFormVisible: false,
      dialogFormOpenTitle: '',
      dialogReadFieldFormVisible: false,
      readFieldtableloading:false,
      readFieldTableData: [],
      readFieldSelected: [],


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
      this.loadRightTableData()
      this.loadBtnFunc = JSON.parse(localStorage.getItem('yueboncurrentfuns'))
  },
  methods: {

    /**
      * 加载页面左侧table数据
      */
    loadRightTableData: function () {
      this.righttableloading = true
      var seachdata = {
        Pkey: this.cid
      }
      getConfTbContent(seachdata).then(res => {
        this.righttableData = res.ResData.finFields
        this.readFieldTableData = res.ResData.sourceFields
        this.SysId = res.ResData.sysid
        this.righttableloading = false
      })
    },
    /**
    * 点击一条记录
    */
    handleTableClickRow(row) {
      this.rightcurrentSelectId = row
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
    cancleEditForm: function (data) {
      this.readFieldSelected = []
      this.dialogReadFieldFormVisible = false
    },
    /**
 * 新增、修改或查看验证明细信息     *
 */
    ShowDialogVerifyEditOrViewDialog: function (view, index) {
      this.dtype = view
      if (this.dtype == 'Verify') {
        this.dialogFormOpenTitle = "验证"
        if (this.righttableData[index].SyncDataConfParamter != undefined && this.righttableData[index].SyncDataConfParamter !== null && this.righttableData[index].SyncDataConfParamter.length > 0) {
          this.formData = this.righttableData[index].SyncDataConfParamter
        }
      } 
      this.verifyTableDataIndex = index
      this.tpl = "components/PlugListComponent.vue"
      this.showPlugConfig = true
      this.dialogVerifyEditFormVisible = true

    },
    savePlugConfig: function (data) {
      if (data != null) {
        if (this.dtype == 'Verify') {
          this.righttableData[this.verifyTableDataIndex].SyncDataConfParamter = data
        } 
      }
      this.closePlugConfig()
    },
    closePlugConfig: function (data) {
      this.showPlugConfig = false
      this.dialogVerifyEditFormVisible = false
    },

    /**
* 新增、修改或查看验证明细信息     *
*/
    ShowDialogReadFieldEditOrViewDialog: function (view, index,row) {
      this.verifyTableDataIndex = index
      this.rightcurrentSelectId=row
      if (this.rightcurrentSelectId.length === 0) {
        this.$alert('请先选择要操作的数据', '提示')
        return false
      }
      this.rightcurrentSelectId = row
      if (view == 'ReadField') {
        this.dialogReadFieldFormVisible = true
      }
    },
    /**
   * 点击一条记录
   */
    handleReadFieldClickRow(row) {
      this.readFieldSelected = row
    },
    saveReadFieldEditForm: function () {
      if (this.readFieldSelected.length === 0) {
        this.$alert('请先选择要操作的数据', '提示')
        return false
      }
      if (this.readFieldSelected != null) {
        this.righttableData[this.verifyTableDataIndex].ReadFieldInfo = this.readFieldSelected
      }
      this.dialogReadFieldFormVisible = false
      this.readFieldSelected=[]
    },
    cancleReadFieldEditForm: function(data) {
      this.readFieldSelected = []
      this.dialogReadFieldFormVisible = false
    },
  }
}
</script>

<style>
</style>
