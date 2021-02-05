<template>
  <div>
    <el-row>
      <el-select v-model="Tbname" placeholder="请选择" @change="handleSelectTbChange(Tbname)">
        <el-option v-for="item in SelectTbnameList"
                   :key="item.TableName"
                   :label="item.TableName"
                   :value="item.TableName" />
      </el-select>
    </el-row>
    <el-table ref="gridtable"
              :data="tableData"
              row-key="FieldName"
              border
              stripe
              highlight-current-row
              style="width: 100%;margin-bottom: 20px;"
              :default-sort="{prop: 'SortCode', order: 'ascending'}">

      <el-table-column prop="FieldName" label="名称" sortable="custom" width="120" />
      <el-table-column prop="Description" label="描述" sortable="custom" width="200" />
      <el-table-column prop="DataGetType" label="获取方式" sortable="custom" width="200">
        <template slot-scope="scope">
          <el-select v-model="scope.row.DataGetType"
                     placeholder="请选择类型">
            <el-option v-for="item in SelectDataGetTypeList"
                       :key="item.Id"
                       :label="item.Pname"
                       :value="item.Id" />
          </el-select>
        </template>
      </el-table-column>
      <el-table-column label="操作" width="100">
        <template slot-scope="scope">
          <el-button type="text" @click="toogleExpand(scope.row)">查看详情</el-button>
        </template>
      </el-table-column>
      <el-table-column label="配置" sortable="custom" width="120" align="center">
        <template slot-scope="scope">
          <el-button type="primary"
                     icon="el-icon-plus"
                     size="small"
                     @click="OpenConfigPage()">配置</el-button>
        </template>
      </el-table-column>
      <el-table-column v-if="false" prop="Description" label="配置信息" />

      <el-table-column type="expand" width="1">
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
    </el-table>
    <div v-if="showType!=='show'" class="yuebon-page-footer">
      <el-button @click="reset">重置</el-button>
      <el-button v-preventReClick type="primary" @click="saveEditForm">保存</el-button>
    </div>
  </div>
</template>

<script>

  import {
    saveSys_conf_details, getTbNameList, getSys_conf_detailsDetail, GetColumnListsByDetailId,
    GetColumnListsBytbName, getDataGetTypeLists
  } from '@/api/dataprocess/sys_conf_details'

  export default {
    name: 'ConfDetails',
    data() {
      return {
        tableloading: true,
        tableData: [],
        Sys_conf_id: '',
        Tbname: '',
        Is_dynamic: '',
        Is_flag: '',
        configjson: '',
        SelectTbnameList: [],
        SelectedTbName: '',
        SelectedDataGetType: '',
        SelectDataGetTypeList: [],
        currentId: '', // 当前操作对象的ID值，主要用于修改
        showType: 'edit' // 操作类型编辑、新增、查看
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
        if (this.$route.params && this.$route.params.id && this.$route.params.id !== 'null') {
          this.currentId = this.$route.params.id
          this.showType = this.$route.params.showtype
          this.bindEditInfo()
        }
        this.Sys_conf_id = this.$route.params.Sys_conf_id

        getTbNameList().then(res => {
          this.SelectTbnameList = res.ResData
        })
        getDataGetTypeLists().then(res => {
          this.SelectDataGetTypeList = res.ResData
        })
      },
      reset() {

      },
      /**
           * 加载页面table数据
           */
      loadTableDataByDetailId: function (detailId) {
        GetColumnListsByDetailId(detailId).then(res => {
          this.tableData = res.ResData.Items
        })
      },
      /**
          * 加载页面table数据
          */
      loadTableDataByTbName: function (tb) {
        GetColumnListsBytbName(tb).then(res => {
          this.tableData = res.ResData
        })
      },
      /**
       * 读取详情
       */
      bindEditInfo: function () {
        getSys_conf_detailsDetail(this.currentId).then(res => {
          this.Tbname = res.ResData.Tbname
          this.Sys_conf_id = res.ResData.Sys_conf_id
          this.Is_dynamic = res.ResData.Is_dynamic
          this.Is_flag = res.ResData.Is_flag
          this.configjson = res.ResData.configjson
          this.loadTableDataByDetailId(this.currentId)
        })
      },
      /**
        * 打开配置页面
        */
      OpenConfigPage: function () {

      },
      /**
         * 新增/修改保存
         */
      saveEditForm() {

      },
      /**
           *选择表
           */
      handleSelectTbChange: function (value) {
        this.SelectedTbName = value
        this.Tbname = this.SelectedTbName
        this.loadTableDataByTbName(this.Tbname)
      },
      toogleExpand(row) {
        const $table = this.$refs.gridtable
        this.tableData.map((item) => {
          if (row.FieldName != item.FieldName) {
            $table.toggleRowExpansion(item, false)
          }
        })
        $table.toggleRowExpansion(row)
      }
    }
  }
</script>
