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
              <el-cascader v-model="selectedclass" :key="cascaderkey" style="width:500px;" :options="selectclasses" filterable :props="{label:'ClassName',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }" clearable @change="handleFromSelectClassChange" />
              <el-table ref="gridfromtable"
                        v-loading="fromtableloading"
                        :data="fromtableData"
                        :height="700"
                        border
                        stripe
                        highlight-current-row
                        style="width: 100%"
                        :default-sort="{prop: 'SortCode', order: 'ascending'}"
                        @row-click="handlefromClickRow"
                        @sort-change="handlefromSortChange">
                <template v-for="(item,index) in fromtableHead">
                  <el-table-column :prop="item.column_name" :label="item.column_comment" :key="index" sortable="custom" width="120"></el-table-column>
                </template>
              </el-table>
              <div class="pagination-container">
                <el-pagination background
                               :current-page="frompagination.currentPage"
                               :page-sizes="[5,10,20,50,100, 200, 300, 400]"
                               :page-size="frompagination.pagesize"
                               layout="total, sizes, prev, pager, next, jumper"
                               :total="frompagination.pageTotal"
                               @size-change="handlefromSizeChange"
                               @current-change="handlefromCurrentChange" />
              </div>
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
              <el-table-column type="selection" width="30" />
              <el-table-column prop="Conftype" label="类型" sortable="custom" width="120">
                <template slot-scope="scope">
                  <el-tag :type="scope.row.Conftype === 1 ? 'success' : 'info'" disable-transitions>{{ scope.row.Conftype === 1 ? "数据库" : "系统" }}</el-tag>
                </template>
              </el-table-column>
              <el-table-column prop="ToName" label="名称" sortable="custom" width="120" />
              <el-table-column label="是否启用" sortable="custom" width="120" prop="EnabledMark" align="center">
                <template slot-scope="scope">
                  <el-tag :type="scope.row.EnabledMark === true ? 'success' : 'info'" disable-transitions>{{ scope.row.EnabledMark === true ? "启用" : "禁用" }}</el-tag>
                </template>
              </el-table-column>
              <el-table-column label="验证配置" sortable="custom" width="120" align="center">
                <template slot-scope="scope">
                  <el-button type="primary"
                             icon="el-icon-plus"
                             size="small"
                             @click="HandleToConfig(scope.row.Id,'VerifyConfig')">验证配置</el-button>
                </template>
              </el-table-column>
              <el-table-column label="同步配置" sortable="custom" width="120" align="center">
                <template slot-scope="scope">
                  <el-button type="primary"
                             icon="el-icon-plus"
                             size="small"
                             @click="HandleToConfig(scope.row.Id,'DataSyncConfig')">同步配置</el-button>
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
          </div>

        </el-card>
      </el-col>
    </el-row>
    <el-dialog ref="dialogEditForm"
               :close-on-click-modal="false"
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
                      border
                      stripe
                      highlight-current-row
                      style="width: 100%"
                      :default-sort="{prop: 'SortCode', order: 'ascending'}"
                      @select="handleDialogSelectChange"
                      @select-all="handleDialogSelectAllChange"
                      @sort-change="handledialogSortChange">
              <el-table-column type="selection" width="30" />
              <template v-for="(item,index) in dialogtableHead">
                <el-table-column :prop="item.column_name" :label="item.column_comment" :key="index" sortable="custom" width="120"></el-table-column>
              </template>
            </el-table>
            <div class="pagination-container">
              <el-pagination background
                             :current-page="dialogpagination.currentPage"
                             :page-sizes="[5,10,20,50,100, 200, 300, 400]"
                             :page-size="dialogpagination.pagesize"
                             layout="total, sizes, prev, pager, next, jumper"
                             :total="dialogpagination.pageTotal"
                             @size-change="handledialogSizeChange"
                             @current-change="handledialogCurrentChange" />
            </div>
          </el-card>
        </el-col>
      </el-row>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogEditFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="saveEditForm()">确 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
  import {
    getSys_sysListWithPager
  } from '@/api/dataprocess/sys_sys'
  import {
    getAllClassifyTreeTable
  } from '@/api/dataprocess/sys_classify'
  import {
    getSd_sysdbListWithPager
  } from '@/api/dataprocess/sd_sysdb'
  import {
    getAllSdClassifyTreeTable
  } from '@/api/dataprocess/sd_classify'
import { getConf_confListWithPager,
  saveConf_conf, setConf_confEnable,
  deleteConf_conf
} from '@/api/dataprocess/conf_conf'

export default {
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
      frompagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
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
      dialogtableHead: [],
      dialogtableData: [],
      dialogpagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
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
            { column_name: "Syscode", column_comment: "编码" },
            { column_name: "Sysname", column_comment: "名称" }
          ]
          getAllClassifyTreeTable().then(res => {
            this.selectclasses = res.ResData
          })
        } else if (this.selectedSysDb == '1') {
          this.fromtableHead = [
            { column_name: "SdName", column_comment: "名称" }
          ]
          getAllSdClassifyTreeTable().then(res => {
            this.selectclasses = res.ResData
          })
        }
        this.frompagination.currentPage = 1
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
          CurrenetPageIndex: this.frompagination.currentPage,
          PageSize: this.frompagination.pagesize,
          Order: this.fromsortableData.order,
          Sort: this.fromsortableData.sort,
          Filter: {
            Classify_id: this.selectedclass,
          }
        }
        
        if (this.selectedSysDb == '0') {
          
          getSys_sysListWithPager(seachdata).then(res => {
            
            this.fromtableData = res.ResData.Items
            this.frompagination.pageTotal = res.ResData.TotalItems
            this.fromtableloading = false
          })
        } else if (this.selectedSysDb == '1') {

          getSd_sysdbListWithPager(seachdata).then(res => {
            this.fromtableData = res.ResData.Items
            this.frompagination.pageTotal = res.ResData.TotalItems
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
       * 当表格的排序条件发生变化的时候会触发该事件
       */
      handlefromSortChange: function (column) {
        this.fromsortableData.sort = column.prop
        if (column.order === 'ascending') {
          this.fromsortableData.order = 'asc'
        } else {
          this.fromsortableData.order = 'desc'
        }
        this.loadFromTableData()
      },
      /**
      * 选择每页显示数量
      */
      handlefromSizeChange(val) {
        this.frompagination.pagesize = val
        this.frompagination.currentPage = 1
        this.loadFromTableData()
      },
      /**
       * 选择当页面
       */
      handlefromCurrentChange(val) {
        this.frompagination.currentPage = val
        this.loadFromTableData()
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
      HandleToConfig: function (id, viewstr) {
        this.$router.push({ name: 'ConfigDetails', params: { id: id, viewstr: viewstr } })
      },
      /**
      * 新增、修改或查看明细信息（绑定显示数据）     *
      */
      ShowEditOrViewDialog: function () {
        if (this.fromcurrentSelectId.length === 0) {
          this.$alert('请先选择左侧列表中的一条数据', '提示')
        } else {
          this.dialogpagination.currentPage = 1
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
              alert(res.CustomCode)
              if (res.CustomCode == "err80404") {
                this.dialogselectedSysDb = '0'
                this.dialogtableHead = [
                  { column_name: "Syscode", column_comment: "编码" },
                  { column_name: "Sysname", column_comment: "名称" }
                ]
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
    *弹出框系统或数据库选择
    */
      handleDialogSelectSysDbChange: function () {
        this.dialogselectclasses = []
        this.dialogselectedclass = ''
        this.dialogtableData = []
        this.cascaderkey++
        if (this.dialogselectedSysDb == '0') {
          this.dialogtableHead = [
            { column_name: "Syscode", column_comment: "编码" },
            { column_name: "Sysname", column_comment: "名称" }
          ]
          getAllClassifyTreeTable().then(res => {
            this.dialogselectclasses = res.ResData
          })
        } else if (this.dialogselectedSysDb == '1') {
          this.dialogtableHead = [
            { column_name: "SdName", column_comment: "名称" }
          ]
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
          CurrenetPageIndex: this.dialogpagination.currentPage,
          PageSize: this.dialogpagination.pagesize,
          Order: this.dialogsortableData.order,
          Sort: this.dialogsortableData.sort,
          Pkey: sysdbId,
          Filter: {
            Classify_id: this.dialogselectedclass,
          }
        }
        if (this.dialogselectedSysDb == '0') {

          getSys_sysListWithPager(seachdata).then(res => {
            this.dialogtableData = res.ResData.Items
            this.dialogpagination.pageTotal = res.ResData.TotalItems
            this.dialogtableloading = false
          })
        } else if (this.dialogselectedSysDb == '1') {

          getSd_sysdbListWithPager(seachdata).then(res => {
            this.dialogtableData = res.ResData.Items
            this.dialogpagination.pageTotal = res.ResData.TotalItems
            this.dialogtableloading = false
          })
        }
      },
      /**
       * 当表格的排序条件发生变化的时候会触发该事件
       */
      handledialogSortChange: function (column) {
        this.dialogsortableData.sort = column.prop
        if (column.order === 'ascending') {
          this.dialogsortableData.order = 'asc'
        } else {
          this.dialogsortableData.order = 'desc'
        }
        this.loadDialogTableData('')
      },
      /**
      * 选择每页显示数量
      */
      handledialogSizeChange(val) {
        this.dialogpagination.pagesize = val
        this.dialogpagination.currentPage = 1
        this.loadDialogTableData('')
      },
      /**
       * 选择当页面
       */
      handledialogCurrentChange(val) {
        this.dialogpagination.currentPage = val
        this.loadDialogTableData('')
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
