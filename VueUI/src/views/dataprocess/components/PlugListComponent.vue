<template>
  <div>
    <div class="list-btn-container">
      <el-button-group>
        <el-button v-hasPermi="['Sys_conf_details/Add']"
                   type="primary"
                   icon="el-icon-plus"
                   size="small"
                   @click="AddEditPlugConf()">新增</el-button>
        <el-button v-hasPermi="['Sys_conf_details/Enable']"
                   type="info"
                   icon="el-icon-video-pause"
                   size="small"
                   @click="setVerifyDialogEnable('0')">禁用</el-button>
        <el-button v-hasPermi="['Sys_conf_details/Enable']"
                   type="success"
                   icon="el-icon-video-play"
                   size="small"
                   @click="setVerifyDialogEnable('1')">启用</el-button>
        <el-button v-hasPermi="['Sys_conf_details/Delete']"
                   type="danger"
                   icon="el-icon-delete"
                   size="small"
                   @click="deleteEditPlugConf()">删除</el-button>
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
    <el-table ref="griddialogtable"
              v-loading="dialogVerifytableloading"
              :data="dialogVerifytableData"
              :height="300"
              border
              stripe
              highlight-current-row
              style="width: 100%"
              :default-sort="{prop: 'levelNum', order: 'ascending'}"
              @select="handleVerifyDialogSelectChange"
              @select-all="handleVerifyDialogSelectAllChange">
      <el-table-column type="selection" width="30" />
      <el-table-column prop="levelNum" label="执行顺序" sortable="custom" width="200" align="center"></el-table-column>
      <el-table-column prop="PlugType" label="组件方式" sortable="custom" width="200" align="center">
        <template slot-scope="scope">
          <el-select v-model="scope.row.PlugType"
                     placeholder="请选择类型"
                     @change="handleDataGetTypeChange">
            <el-option v-for="item in SelectDataGetTypeList"
                       :key="item.Id"
                       :label="item.Pname"
                       :value="{value:item.Id,btnvisib:item.HasPage,index:scope.$index,configuri:item.ConfigUri}" />
          </el-select>
        </template>
      </el-table-column>
      <el-table-column label="配置" sortable="custom" width="120" align="center">
        <template slot-scope="scope">
          <el-button type="primary"
                     icon="el-icon-plus"
                     size="small"
                     :disabled="!scope.row.PlugType||scope.row.HasPage==false"
                     @click="GetDataOpenParamPage(scope.$index)">配置</el-button>
        </template>
      </el-table-column>
      <el-table-column label="是否启用" sortable="custom" width="120" prop="EnableMark" align="center">
        <template slot-scope="scope">
          <el-tag :type="scope.row.EnableMark === true ? 'success' : 'info'" disable-transitions>{{ scope.row.EnableMark === true ? "启用" : "禁用" }}</el-tag>
        </template>
      </el-table-column>
    </el-table>
    <div slot="footer" class="dialog-footer">
      <el-button @click="cancleVerifyEditForm()">取 消</el-button>
      <el-button type="primary" @click="saveVerifyEditForm()">确 定</el-button>
    </div>
    <el-dialog ref="dialogPlugEditForm"
               :close-on-click-modal="false"
               :show-close="false"
               :title="'插件'"
               :visible.sync="dialogPlugEditFormVisible"
               append-to-body
               width="640px">
      <component v-if="showDetailConfig" v-bind:is="loadertpl" :detailConfig="detailConfig" v-on:listenTochildEvent="saveDetailConfig"></component>
    </el-dialog>
  </div>
</template>

<script>
  /**
   * 需求数据：SysId:系统ID；dbtype:插件类型；formData:插件配置列表json对象
   * 方法：returndata：返回插件配置列表json对象；exit：关闭组件
   */
  import {
    getDataGetTypeLists
  } from '@/api/dataprocess/plug_plug'
  export default {
    name: 'PlugListComponent ',
    props: ['SysId', 'dbtype', 'formData'],
    data() {
      return {
        tpl: '',
        dialogPlugEditFormVisible: false,
        showDetailConfig: false,
        PlugType: '', //调用插件编辑页面时区分是验证还是获取
        SelectDataGetTypeList: [],
        detailConfig: '', //所选记录中的获取值详细配置信息
        dialogVerifytableloading: false,
        dialogVerifytableData: [],
        currentVerifyDialogSelected: [],
        paramPageIndexInfo: -1,
        verifyTableDataIndex: '', //用于记录当打开验证配置列表时字段表的行号
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
      this.InitDictItem()
    },
    methods: {
      /**
         * 初始化数据
         */
      InitDictItem() {
        getDataGetTypeLists({ SysId: this.SysId, ptype: this.dbtype }).then(res => {
          this.SelectDataGetTypeList = res.ResData
        })
        if (this.formData && this.formData !== null && this.formData.length > 0) {
          let Base64 = require('js-base64').Base64
          this.dialogVerifytableData = JSON.parse(Base64.decode(this.formData))
        }
      },
      GetDataOpenParamPage: function (index) {
        this.paramPageIndexInfo = index
        this.OpenConfigPage()
      },
      /**
        * 打开配置页面
        */
      OpenConfigPage: function () {
        if (this.paramPageIndexInfo > -1) {
          let Base64 = require('js-base64').Base64
          if (this.dialogVerifytableData[this.paramPageIndexInfo].confJson != null && this.dialogVerifytableData[this.paramPageIndexInfo].confJson.length > 0) {
            this.detailConfig = JSON.parse(Base64.decode(this.dialogVerifytableData[this.paramPageIndexInfo].confJson))
          }
        } else {
          this.detailConfig = ''
        }
        //this.tpl = this.dialogVerifytableData[this.paramPageIndexInfo].ConfigUri
        this.tpl = "uploadplug/verifytest/index.vue"
        this.showDetailConfig = true
        this.dialogPlugEditFormVisible = true
      },
      LoadTablePlugList: function () {
        this.dialogVerifytableloading = true
        let array = [];
        for (let i = 0; i < this.dialogVerifytableData.length; i++) {
          let obj = this.dialogVerifytableData[i];
          array.push(obj);
        }
        this.dialogVerifytableData = [];
        this.dialogVerifytableData = this.sortByKey(array, 'levelNum')
        this.dialogVerifytableloading = false
      },
      // 数组对象方法排序：
      sortByKey: function (array, key) {
        return array.sort(function (a, b) {
          var x = a[key];
          var y = b[key];
          return ((x < y) ? -1 : ((x < y) ? 1 : 0));
        });
      },
     
      /**
         *当获取方式下拉框发生变化时
         */
      handleDataGetTypeChange: function (params) {
        const { value, btnvisib, index, configuri } = params
        this.dialogVerifytableData[index].confJson = ''
        this.dialogVerifytableData[index].HasPage = btnvisib
        this.dialogVerifytableData[index].ConfigUri = configuri
      },
      AddEditPlugConf: function () {
        var objins = {
          'levelNum': this.dialogVerifytableData.length + 1,
          'PlugType': '',
          'confJson': '',
          'EnableMark': true,
        }
        this.dialogVerifytableData.push(objins)
      },
      deleteEditPlugConf: function () {
        if (this.currentVerifyDialogSelected.length === 0) {
          this.$alert('请先选择要操作的数据', '提示')
          return false
        } else {
          var arr = []
          this.currentVerifyDialogSelected.forEach(item => {
            this.dialogVerifytableData.forEach((tmp, i) => {
              if (item.levelNum == tmp.levelNum) {
                arr.push(i)
              }
            })
          })
          arr.forEach(item => {
            this.dialogVerifytableData.splice(item, 1)
          })
          this.LoadTablePlugList()
          this.dialogVerifytableData.forEach((item, i) => {
            item.levelNum = i + 1
          })
          this.LoadTablePlugList()
        }
      },
      setVerifyDialogEnable: function (val) {
        if (this.currentVerifyDialogSelected.length === 0) {
          this.$alert('请先选择要操作的数据', '提示')
          return false
        } else {
          this.currentVerifyDialogSelected.forEach(item => {
            this.dialogVerifytableData.forEach(tmp => {
              if (item.levelNum == tmp.levelNum) {
                tmp.EnableMark = val == '0' ? false : true
              }
            })
          })
        }
      },
      changeLevelNum: function (actionStr) {
        if (this.currentVerifyDialogSelected.length > 1 || this.currentVerifyDialogSelected.length === 0) {
          this.$alert('请选择一条数据进行编辑/修改', '提示')
        } else {
          var selectedLevelNum = this.currentVerifyDialogSelected[0].levelNum //没变更之前的数字
          var selectedIndex = -1; //所选的序号
          this.dialogVerifytableData.forEach((v, i) => {
            if (this.currentVerifyDialogSelected[0].levelNum == v.levelNum) {
              selectedIndex = i;
            }
          })
          if (actionStr == 'up') {
            if (selectedLevelNum == 1) {
              this.$alert('已经是第一个，无法移动', '提示')
            } else {
              this.dialogVerifytableData[selectedIndex].levelNum--
              this.dialogVerifytableData[selectedIndex - 1].levelNum++
            }
          } else if (actionStr == 'down') {
            if (selectedLevelNum == this.dialogVerifytableData.length) {
              this.$alert('已经是最后一个，无法移动', '提示')
            } else {
              this.dialogVerifytableData[selectedIndex].levelNum++
              this.dialogVerifytableData[selectedIndex + 1].levelNum--
            }
          }
          else if (actionStr == 'top') {
            if (selectedLevelNum == 1) {
              this.$alert('已经是第一个，无法移动', '提示')
            } else {
              this.dialogVerifytableData.forEach(item => {
                if (item.levelNum != selectedLevelNum && item.levelNum < selectedLevelNum) {
                  item.levelNum++
                }
              })
              this.dialogVerifytableData[selectedIndex].levelNum = 1
            }
          }
          else if (actionStr == 'buttom') {
            if (selectedLevelNum == this.dialogVerifytableData.length) {
              this.$alert('已经是最后一个，无法移动', '提示')
            } else {
              this.dialogVerifytableData.forEach(item => {
                if (item.levelNum != selectedLevelNum && item.levelNum > selectedLevelNum) {
                  item.levelNum--
                }
              })
              this.dialogVerifytableData[selectedIndex].levelNum = this.dialogVerifytableData.length
            }
          }

          this.LoadTablePlugList()
        }
      },
      saveDetailConfig(data) {
        let Base64 = require('js-base64').Base64
        if (data !== null) {
          this.dialogVerifytableData[this.paramPageIndexInfo].confJson = Base64.encode(JSON.stringify(data))
        }
        this.tpl = ''
        this.detailConfig = ''
        this.paramPageIndexInfo = -1
        this.dialogPlugEditFormVisible = false
        this.showDetailConfig = false
      },
      saveVerifyEditForm() {
        let Base64 = require('js-base64').Base64
        this.$emit("returndata", JSON.stringify(Base64.encode(JSON.stringify(this.dialogVerifytableData))))
        this.PlugType = ''
        this.dialogVerifytableData = []
        this.verifyTableDataIndex = ''
        this.$emit('exit', true)
      },
      cancleVerifyEditForm() {
        this.dialogVerifytableData = []
        this.PlugType = ''
        this.verifyTableDataIndex = ''
        this.$emit('exit', true)
      },

      /**
       * 当用户手动勾选checkbox数据行事件
       */
      handleVerifyDialogSelectChange: function (selection, row) {
        this.currentVerifyDialogSelected = selection
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

<style>
</style>
