<template>
  <div class="app-container">
    <el-row :gutter="24">
      <el-col :span="10">
        <el-row :gutter="24">
          <el-col :span="24">
            <el-card>
              <el-select v-model="selectedSysDb" placeholder="请选择系统或数据库" @change="handleSelectSysDbChange()">
                <el-option v-for="item in selectSysDb"
                           :key="item.value"
                           :label="item.label"
                           :value="item.value" />
              </el-select>
            </el-card>
          </el-col>
        </el-row>
        <el-row :gutter="24">
          <el-col :span="24">
            <el-card>
              <el-cascader v-model="selectedclass" :key="cascaderkey" style="width: 100%;" :options="selectclasses" filterable :props="{label:'ClassName',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }" clearable @change="handleFromSelectClassChange" />
              <el-table ref="gridfromtable"
                        v-loading="fromtableloading"
                        :data="fromtableData"
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
                <el-table-column prop="NodeName" label="名称"  min-width="30%" />
                <el-table-column prop="Description" label="描述"  min-width="70%" />
              </el-table>
            </el-card>
          </el-col>
        </el-row>
      </el-col>
      <el-col :span="14">
        <el-card>
          <div class="list-btn-container">
            <el-button-group>
              <el-button v-hasPermi="['Sd_sysdb/Add']"
                         type="primary"
                         icon="el-icon-plus"
                         size="small"
                         @click="ShowEditOrViewDialog()">新增</el-button>
              <el-button v-hasPermi="['Sd_sysdb/Delete']"
                         type="danger"
                         icon="el-icon-delete"
                         size="small"
                         @click="deletePhysics()">删除</el-button>
              <el-button v-hasPermi="['Sd_sysdb/Enable']"
                         type="info"
                         icon="el-icon-video-pause"
                         size="small"
                         @click="setEnable('0')">禁用</el-button>
              <el-button v-hasPermi="['Sd_sysdb/Enable']"
                         type="success"
                         icon="el-icon-video-play"
                         size="small"
                         @click="setEnable('1')">启用</el-button>
              <el-button type="default" icon="el-icon-refresh" size="small" @click="loadToTableData()">刷新</el-button>
            </el-button-group>
          </div>
          <el-table ref="gridtotable"
                    v-loading="totableloading"
                    :data="totableData"
                    :height="700"
                    border
                    stripe
                    highlight-current-row
                    style="width: 100%"
                    :default-sort="{prop: 'Conftype', order: 'ascending'}"
                    @row-click="handletoClickRow"
                    @select="handleToSelectChange"
                    @select-all="handleToSelectAllChange"
                    @sort-change="handletoSortChange">
            <el-table-column type="selection" min-width="5%" />
            <el-table-column prop="ConfToType" label="类型" sortable="custom" min-width="10%">
              <template slot-scope="scope">
                <el-tag :type="scope.row.ConfToType === 1 ? 'success' : 'info'" disable-transitions>{{ scope.row.ConfToType === 1 ? "数据库" : "系统" }}</el-tag>
              </template>
            </el-table-column>
            <el-table-column prop="ToTbName" label="名称" sortable="custom" min-width="15%" />
            <el-table-column prop="ToDescription" label="描述" sortable="custom" min-width="15%" />
            <el-table-column prop="ToParentName" label="所属模型或库" sortable="custom" min-width="15%" />
            <el-table-column prop="ToParentDescription" label="描述" sortable="custom" min-width="15%" />
            <el-table-column label="是否启用" sortable="custom" min-width="10%" prop="EnabledMark" align="center">
              <template slot-scope="scope">
                <el-tag :type="scope.row.EnabledMark === true ? 'success' : 'info'" disable-transitions>{{ scope.row.EnabledMark === true ? "启用" : "禁用" }}</el-tag>
              </template>
            </el-table-column>
            <el-table-column label="同步配置" sortable="custom" min-width="15%" align="center">
              <template slot-scope="scope">
                <el-button type="primary"
                           icon="el-icon-plus"
                           size="small"
                           @click="HandleToConfig(scope.row.Id,scope.row.ConfToType,'DataSyncConfig')">同步配置</el-button>
              </template>
            </el-table-column>
          </el-table>
            <div class="pagination-container">
              <el-pagination background
                             :current-page="topagination.currentPage"
                             :page-sizes="[5,10,20,50,100, 200, 300, 400]"
                             :page-size="topagination.pagesize"
                             layout="total, sizes, prev, pager, next, jumper"
                             :total="topagination.pageTotal"
                             @size-change="handletoSizeChange"
                             @current-change="handletoCurrentChange" />
            </div>
          

        </el-card>
      </el-col>
    </el-row>
    <el-dialog ref="dialogEditForm"
               :close-on-click-modal="false"
               :show-close="false"
               :title="editFormTitle+'关联'"
               :visible.sync="dialogEditFormVisible"
               width="640px">
      <el-row :gutter="24">
        <el-col :span="24">
          <el-card>
            <el-select v-model="dialogselectedSysDb" placeholder="请选择系统或数据库" @change="handleDialogSelectSysDbChange()">
              <el-option v-for="item in selectSysDb"
                         :key="item.value"
                         :label="item.label"
                         :value="item.value" />
            </el-select>
          </el-card>
        </el-col>
      </el-row>
      <el-row :gutter="24">
        <el-col :span="24">
          <el-card>
            <el-cascader v-model="dialogselectedclass" :key="cascaderkey" style="width:500px;" :options="dialogselectclasses" filterable :props="{label:'ClassName',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }" clearable @change="handleDialogSelectClassChange" />
            <el-table ref="griddialogtable"
                      v-loading="dialogtableloading"
                      :data="dialogtableData"
                      style="width: 100%;margin-bottom: 20px;"
                      row-key="Id"
                      border
                      size="mini"
                      max-height="850"
                      default-expand-all
                      highlight-current-row
                      :tree-props="{children: 'Children'}"
                      @select="handleDialogSelectChange"
                      @select-all="handleDialogSelectAllChange">
              <el-table-column type="selection" min-width="10%" />
              <el-table-column prop="NodeName" label="名称" min-width="30%" />
              <el-table-column prop="Description" label="描述" min-width="70%" />
            </el-table>
          </el-card>
        </el-col>
      </el-row>
      <div slot="footer" class="dialog-footer">
        <el-button @click="closeEditForm()">取 消</el-button>
        <el-button type="primary" @click="saveEditForm()">确 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
  import {
    getSysAndModelTree
  } from '@/api/dataprocess/sys_conf'
  import {
    getAllClassifyTreeTable
  } from '@/api/dataprocess/sys_classify'
  import {
     getSdAndTbTree
  } from '@/api/dataprocess/sd_sysdb'
  import {
    getAllSdClassifyTreeTable
  } from '@/api/dataprocess/sd_classify'
import { getConf_confListWithPager,
  saveConf_conf, setConf_confEnable,
  deleteConf_conf
} from '@/api/dataprocess/conf_conf'

  export default {
    name: 'confcontrol',
  data() {
    return {
      selectedSysDb:'',
      selectSysDb: [{
        value: '0',
        label: '系统'
      }, {
        value: '1',
        label: '数据库'
        }],
      cascaderkey:1,
      selectedclass: '',
      selectclasses: [],
      loadBtnFunc: [],
      fromcurrentSelectId:'',
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
      },
      dialogcurrentId: '', // 当前操作对象的ID值，主要用于修改
      dialogcurrentSelectId: '',
      dialogcurrentSelected: [],
      dialogEditFormVisible: false,
      editFormTitle: '',
      dialogselectedSysDb: '',
      dialogselectedclass: '',
      dialogselectclasses: [],
      dialogtableloading: false,
      dialogtableData: [],
      dialogsortableData: {
        order: 'desc',
        sort: 'CreatorTime'
      },
    }
  },
  created() {
    this.loadBtnFunc = JSON.parse(localStorage.getItem('yueboncurrentfuns'))
  },
    methods: {

      /**
      *系统或数据库选择
      */
      handleSelectSysDbChange: function () {
        this.selectclasses = []
        this.selectedclass = ''
        this.fromtableData = []
        this.cascaderkey++
        if (this.selectedSysDb == '0') {
          this.fromtableHead = [
            { column_name: "Syscode", column_comment: "编码", column_minWidth:"25%" },
            { column_name: "Sysname", column_comment: "名称", column_minWidth: "75%" }
          ]
          getAllClassifyTreeTable().then(res => {
            this.selectclasses = res.ResData
          })
        } else if (this.selectedSysDb == '1') {
          this.fromtableHead = [
            { column_name: "SdName", column_comment: "名称", column_minWidth: "95%" }
          ]
          getAllSdClassifyTreeTable().then(res => {
            this.selectclasses = res.ResData
          })
        }
        this.loadFromTableData()
      },
      /**
      *系统或数据库分类选择
      */
      handleFromSelectClassChange: function (value) {
        this.selectedclass = value
        this.loadFromTableData()
      },
      /**
      * 加载页面左侧table数据
      */
      loadFromTableData: function () {
        this.fromtableloading = true
        var seachdata = {
          Filter: {
            Classify_id: this.selectedclass,
          }
        }
        
        if (this.selectedSysDb == '0') {
          
          getSysAndModelTree(seachdata).then(res => {
            
            this.fromtableData = res.ResData
            this.fromtableloading = false
          })
        } else if (this.selectedSysDb == '1') {

          getSdAndTbTree(seachdata).then(res => {
            this.fromtableData = res.ResData
            this.fromtableloading = false
          })
        }
        //getSequenceListWithPager(seachdata).then(res => {
        //  this.tableData = res.ResData.Items
        //  this.pagination.pageTotal = res.ResData.TotalItems
        //  this.tableloading = false
        //})
      },
      /**
       * 点击一条记录
       */
      handlefromClickRow(row) {
        this.fromcurrentSelectId = row.Id
        this.topagination.currentPage = 1
        this.loadToTableData()
      },
      /**
      * 加载页面左侧table数据
      */
      loadToTableData: function () {
        this.totableloading = true
        var seachdata = {
          CurrenetPageIndex: this.topagination.currentPage,
          PageSize: this.topagination.pagesize,
          Order: this.tosortableData.order,
          Sort: this.tosortableData.sort,
          Filter: {
            FromId: this.fromcurrentSelectId
          }
        }
        getConf_confListWithPager(seachdata).then(res => {
          this.totableData = res.ResData.Items
          this.topagination.pageTotal = res.ResData.TotalItems
          this.totableloading = false
        })
      },
      /**
       * 当表格的排序条件发生变化的时候会触发该事件
       */
      handletoSortChange: function (column) {
        this.tosortableData.sort = column.prop
        if (column.order === 'ascending') {
          this.tosortableData.order = 'asc'
        } else {
          this.tosortableData.order = 'desc'
        }
        this.loadToTableData()
      },
      /**
      * 选择每页显示数量
      */
      handletoSizeChange(val) {
        this.topagination.pagesize = val
        this.topagination.currentPage = 1
        this.loadToTableData()
      },
      /**
       * 选择当页面
       */
      handletoCurrentChange(val) {
        this.topagination.currentPage = val
        this.loadToTableData()
      },
      /**
      * 点击一条记录
      */
      handletoClickRow(row) {
        this.tocurrentSelectId = row.Id
      },
      /**
     * 当用户手动勾选checkbox数据行事件
     */
      handleToSelectChange: function (selection, row) {
        this.tocurrentSelected = selection
      },
      /**
       * 当用户手动勾选全选checkbox事件
       */
      handleToSelectAllChange: function (selection) {
        this.tocurrentSelected = selection
      },
      /**
       * 点击表格按钮跳转配置页面
       */
      HandleToConfig: function (id, dbtype, viewstr) {
        this.$router.push({ name: 'ConfigDetails', params: { id: id, dtype: dbtype, viewstr: viewstr } })
      },
      /**
      * 新增、修改或查看明细信息（绑定显示数据）     *
      */
      ShowEditOrViewDialog: function () {
        if (this.fromcurrentSelectId.length === 0) {
          this.$alert('请先选择左侧列表中的一条数据', '提示')
        } else {
          this.editFormTitle = '新增'
          this.tocurrentId = ''
          this.dialogEditFormVisible = true
        }
      },
      setEnable: function (val) {
        if (this.tocurrentSelected.length === 0) {
          this.$alert('请先选择要操作的数据', '提示')
          return false
        } else {
          var tocurrentIds = []
          this.tocurrentSelected.forEach(element => {
            tocurrentIds.push(element.Id)
          })
          const data = {
            Ids: tocurrentIds,
            Flag: val
          }
          setConf_confEnable(data).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
              this.tocurrentSelected = ''
              this.loadToTableData()
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
        if (this.tocurrentSelected.length === 0) {
          this.$alert('请先选择要操作的数据', '提示')
          return false
        } else {
          var tocurrentIds = []
          this.tocurrentSelected.forEach(element => {
            tocurrentIds.push(element.Id)
          })
          const data = {
            Ids: tocurrentIds
          }
          deleteConf_conf(data).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
              this.tocurrentSelected = ''
              this.loadToTableData()
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
     * 新增/修改保存
     */
      saveEditForm() {
        if (this.dialogcurrentSelected.length > 1 || this.dialogcurrentSelected.length === 0) {
          this.$alert('请选择一条数据进行编辑/修改', '提示')
        } else {
          this.dialogcurrentSelectId = this.dialogcurrentSelected[0].Id
          alert(this.dialogselectedSysDb)
          const data = {
            'FromId': this.fromcurrentSelectId,
            'ToId': this.dialogcurrentSelectId,
            'EnabledMark': true,
            'ConfFromType': this.selectedSysDb,
            'ConfToType': this.dialogselectedSysDb,
            'Id': this.currentId
          }
          var url = 'Conf_conf/Insert'
          saveConf_conf(data, url).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
              this.dialogEditFormVisible = false
              this.dialogcurrentSelected = ''
              this.dialogselectedclass = ''
              this.dialogselectedsys = ''
              this.loadDialogTableData()
              this.loadToTableData()

            } else {
              if (res.CustomCode == "err80404") {
                this.dialogselectedSysDb = '0'
                getAllClassifyTreeTable().then(res => {
                  this.dialogselectclasses = res.ResData
                })
                this.loadDialogTableData(res.ResData)
              }
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              })
            }
          })
        }
      },
    /**
   * 关闭对话框
   */
      closeEditForm() {
        this.dialogEditFormVisible = false
        this.dialogcurrentSelected = ''
        this.dialogselectedSysDb= ''
        this.dialogselectedclass = ''
        this.dialogselectclasses = []
        this.dialogtableData=[]
         
        this.dialogsortableData={
          order: 'desc',
            sort: 'CreatorTime'
        }
      },

      /**
    *弹出框系统或数据库选择
    */
      handleDialogSelectSysDbChange: function () {
        this.dialogselectclasses = []
        this.dialogselectedclass = ''
        this.dialogtableData = []
        this.cascaderkey++
        if (this.dialogselectedSysDb == '0') {
          getAllClassifyTreeTable().then(res => {
            this.dialogselectclasses = res.ResData
          })
        } else if (this.dialogselectedSysDb == '1') {
          getAllSdClassifyTreeTable().then(res => {
            this.dialogselectclasses = res.ResData
          })
        }
        this.loadDialogTableData()
      },
      /**
    *系统或数据库分类选择
    */
      handleDialogSelectClassChange: function (value) {
        this.dialogselectedclass = value
        this.loadDialogTableData()
      },
      /**
      * 加载页面左侧table数据
      */
      loadDialogTableData: function (sysdbId) {
        this.dialogtableloading = true
        var seachdata = {
          Pkey: sysdbId,
          Filter: {
            Classify_id: this.dialogselectedclass,
          }
        }
        if (this.dialogselectedSysDb == '0') {

          getSysAndModelTree(seachdata).then(res => {
            this.dialogtableData = res.ResData
            this.dialogtableloading = false
          })
        } else if (this.dialogselectedSysDb == '1') {

          getAllClassifyTreeTable(seachdata).then(res => {
            this.dialogtableData = res.ResData
            this.dialogtableloading = false
          })
        }
      },
      
     
      /**
     * 当用户手动勾选checkbox数据行事件
     */
      handleDialogSelectChange: function (selection, row) {
        this.dialogcurrentSelected = selection
      },
      /**
       * 当用户手动勾选全选checkbox事件
       */
      handleDialogSelectAllChange: function (selection) {
        this.dialogcurrentSelected = selection
      },
    }
  }
</script>

<style lang="scss" scoped>
</style>
