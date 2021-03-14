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
              <el-cascader v-model="selectedclass" :key="cascaderkey" style="width: 100%;" :options="selectclasses" filterable :props="{label:'ClassName',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }" clearable @change="handletoSelectClassChange" />
              <el-table ref="gridtotable"
                        v-loading="totableloading"
                        :data="totableData"
                        style="width: 100%;margin-bottom: 20px;"
                        row-key="Id"
                        border
                        size="mini"
                        max-height="850"
                        default-expand-all
                        highlight-current-row
                        :tree-props="{children: 'Children'}"
                        @row-click="handletoClickRow">
                <el-table-column prop="NodeName" label="名称" min-width="30%" />
                <el-table-column prop="Description" label="描述" min-width="70%" />
              </el-table>
            </el-card>
          </el-col>
        </el-row>
      </el-col>
      <el-col :span="14">
        <el-card>
          <div class="list-btn-container">
            <el-button-group>
              <el-button v-hasPermi="['Conf_conf/Add']"
                         type="primary"
                         icon="el-icon-plus"
                         size="small"
                         @click="ShowEditOrViewDialog()">新增</el-button>
              <el-button v-hasPermi="['Conf_conf/Delete']"
                         type="danger"
                         icon="el-icon-delete"
                         size="small"
                         @click="deletePhysics()">删除</el-button>
              <el-button v-hasPermi="['Conf_conf/Enable']"
                         type="info"
                         icon="el-icon-video-pause"
                         size="small"
                         @click="setEnable('0')">禁用</el-button>
              <el-button v-hasPermi="['Conf_conf/Enable']"
                         type="success"
                         icon="el-icon-video-play"
                         size="small"
                         @click="setEnable('1')">启用</el-button>
              <el-button type="default" icon="el-icon-refresh" size="small" @click="loadfromTableData()">刷新</el-button>
            </el-button-group>
          </div>
          <el-table ref="gridfromtable"
                    v-loading="fromtableloading"
                    :data="fromtableData"
                    :height="700"
                    border
                    stripe
                    highlight-current-row
                    style="width: 100%"
                    :default-sort="{prop: 'ConfFromType', order: 'ascending'}"
                    @row-click="handlefromClickRow"
                    @select="handlefromSelectChange"
                    @select-all="handlefromSelectAllChange"
                    @sort-change="handlefromSortChange">
            <el-table-column type="selection" min-width="5%" />
            <el-table-column prop="ConfFromType" label="类型" sortable="custom" min-width="10%">
              <template slot-scope="scope">
                <el-tag :type="scope.row.ConfFromType === 1 ? 'success' : 'info'" disable-transitions>{{ scope.row.ConfFromType === 1 ? "数据库" : "系统" }}</el-tag>
              </template>
            </el-table-column>
            <el-table-column prop="FromTbName" label="名称" sortable="custom" min-width="15%" />
            <el-table-column prop="FromDescription" label="描述" sortable="custom" min-width="15%" />
            <el-table-column prop="FromParentName" label="所属模型或库" sortable="custom" min-width="15%" />
            <el-table-column prop="FromParentDescription" label="描述" sortable="custom" min-width="15%" />
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
                           @click="HandlefromConfig(scope.row.Id,'DataSyncConfig')">同步配置</el-button>
              </template>
            </el-table-column>
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
  import {
    getConf_confListWithPager,
    saveConf_conf, setConf_confEnable,
    deleteConf_conf
  } from '@/api/dataprocess/conf_conf'

  import {
    getSysAndOutModelTree
  } from '@/api/dataprocess/sys_outmodel'

  export default {
    name: 'confcontrol',
    data() {
      return {
        selectedSysDb: '',
        selectSysDb: [{
          value: '0',
          label: '系统'
        }, {
          value: '1',
          label: '数据库'
        }],
        cascaderkey: 1,
        selectedclass: '',
        selectclasses: [],
        loadBtnFunc: [],
        tocurrentSelectId: '',
        tocurrentSelectParentId: '', //toParentId
        totableloading: false,
        totableHead: [],
        totableData: [],
        tosortableData: {
          order: 'desc',
          sort: 'CreatorTime'
        },
        fromcurrentId: '', // 当前操作对象的ID值，主要用于修改
        fromcurrentSelectId: '',
        fromcurrentSelected: [],
        fromtableloading: false,
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
        editFormTitle: '',
        dialogcurrentId: '', // 当前操作对象的ID值，主要用于修改
        dialogcurrentSelectId: '',
        dialogcurrentSelectParentId: '',//toParentId
        dialogcurrentSelected: [],
        dialogEditFormVisible: false,
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
        this.totableData = []
        this.cascaderkey++
        if (this.selectedSysDb == '0') {
          this.totableHead = [
            { column_name: "Syscode", column_comment: "编码", column_minWidth: "25%" },
            { column_name: "Sysname", column_comment: "名称", column_minWidth: "75%" }
          ]
          getAllClassifyTreeTable().then(res => {
            this.selectclasses = res.ResData
          })
        } else if (this.selectedSysDb == '1') {
          this.totableHead = [
            { column_name: "SdName", column_comment: "名称", column_minWidth: "95%" }
          ]
          getAllSdClassifyTreeTable().then(res => {
            this.selectclasses = res.ResData
          })
        }
        this.loadtoTableData()
      },
      /**
      *系统或数据库分类选择
      */
      handletoSelectClassChange: function (value) {
        this.selectedclass = value
        //this.loadtoTableData()
      },
      /**
      * 加载页面左侧table数据
      */
      loadtoTableData: function () {
        this.totableloading = true
        var seachdata = {
          Filter: {
            Classify_id: this.selectedclass,
          }
        }

        if (this.selectedSysDb == '0') {

          getSysAndModelTree(seachdata).then(res => {

            this.totableData = res.ResData
            this.totableloading = false
          })
        } else if (this.selectedSysDb == '1') {

          getSdAndTbTree(seachdata).then(res => {
            this.totableData = res.ResData
            this.totableloading = false
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
      handletoClickRow(row) {
        if (row.NodeType !== "tb") {
          this.tocurrentSelectId = ""
          this.tocurrentSelectParentId = ""
        } else {
          this.tocurrentSelectId = row.Id
          this.tocurrentSelectParentId = row.ParentId
          this.frompagination.currentPage = 1
          this.loadfromTableData()
        }
      },

      /**
    * 新增、修改或查看明细信息（绑定显示数据）     *
    */
      ShowEditOrViewDialog: function () {
        if (this.tocurrentSelectId.length === 0) {
          this.$alert('请先选择左侧列表中的表或模型', '提示')
        } else {
          this.editFormTitle = '新增'
          this.fromcurrentId = ''
          this.dialogEditFormVisible = true
        }
      },
      deletePhysics: function () {
        if (this.fromcurrentSelected.length === 0) {
          this.$alert('请先选择要操作的数据', '提示')
          return false
        } else {
          var fromcurrentIds = []
          this.fromcurrentSelected.forEach(element => {
            fromcurrentIds.push(element.Id)
          })
          const data = {
            Ids: fromcurrentIds
          }
          deleteConf_conf(data).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
              this.fromcurrentSelected = ''
              this.loadfromTableData()
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
     * 加载页面左侧table数据
     */
      loadfromTableData: function () {
        this.fromtableloading = true
        var seachdata = {
          CurrenetPageIndex: this.frompagination.currentPage,
          PageSize: this.frompagination.pagesize,
          Order: this.fromsortableData.order,
          Sort: this.fromsortableData.sort,
          Filter: {
            ToId: this.tocurrentSelectId
          }
        }
        getConf_confListWithPager(seachdata).then(res => {
          this.fromtableData = res.ResData.Items
          this.frompagination.pageTotal = res.ResData.TotalItems
          this.fromtableloading = false
        })
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
        this.loadfromTableData()
      },
      /**
      * 选择每页显示数量
      */
      handlefromSizeChange(val) {
        this.frompagination.pagesize = val
        this.frompagination.currentPage = 1
        this.loadfromTableData()
      },
      /**
       * 选择当页面
       */
      handlefromCurrentChange(val) {
        this.frompagination.currentPage = val
        this.loadfromTableData()
      },
      /**
      * 点击一条记录
      */
      handlefromClickRow(row) {
        this.fromcurrentSelectId = row.Id
      },
      /**
     * 当用户手动勾选checkbox数据行事件
     */
      handlefromSelectChange: function (selection, row) {
        this.fromcurrentSelected = selection
      },
      /**
       * 当用户手动勾选全选checkbox事件
       */
      handlefromSelectAllChange: function (selection) {
        this.fromcurrentSelected = selection
      },
      /**
       * 点击表格按钮跳转配置页面
       */
      HandlefromConfig: function (id, viewstr) {
        this.$router.push({ name: 'ConfigDetails', params: { id: id, viewstr: viewstr } })
      },
      /**
     * 新增/修改保存
     */
      saveEditForm() {
        if (this.dialogcurrentSelected.length > 1 || this.dialogcurrentSelected.length === 0) {
          this.$alert('请选择一条数据进行编辑/修改', '提示')
        } else {
          if (this.dialogcurrentSelected[0].NodeType !== "tb") {
            this.$alert('请选择表或数据模型', '提示')
          } else {
            this.dialogcurrentSelectId = this.dialogcurrentSelected[0].Id
            this.dialogcurrentSelectParentId = this.dialogcurrentSelected[0].ParentId
            const data = {
              'ToId': this.tocurrentSelectId,
              'ToParentId': this.tocurrentSelectParentId,
              'FromId': this.dialogcurrentSelectId,
              'FromParentId': this.dialogcurrentSelectParentId,
              'EnabledMark': true,
              'ConfToType': this.selectedSysDb,
              'ConfFromType': this.dialogselectedSysDb,
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
                this.loadfromTableData()

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

        }
      },
      /**
     * 关闭对话框
     */
      closeEditForm() {
        this.dialogEditFormVisible = false
        this.dialogcurrentSelected = ''
        this.dialogselectedSysDb = ''
        this.dialogselectedclass = ''
        this.dialogselectclasses = []
        this.dialogtableData = []

        this.dialogsortableData = {
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

          getSysAndOutModelTree(seachdata).then(res => {
            this.dialogtableData = res.ResData
            this.dialogtableloading = false
          })
        } else if (this.dialogselectedSysDb == '1') {

          getSdAndTbTree(seachdata).then(res => {
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
