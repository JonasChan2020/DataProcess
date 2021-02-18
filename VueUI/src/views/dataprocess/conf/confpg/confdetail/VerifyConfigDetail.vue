<template>
  <div >
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
            <el-table-column prop="TableName" label="表名" sortable="custom" width="150"></el-table-column>
            <el-table-column prop="Description" label="描述" sortable="custom" width="204"></el-table-column>
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
                <el-button type="text" >配置</el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>

  import {
    getConfTbContent
} from '@/api/dataprocess/conf_detail'

  export default {
    name: 'VerifyConfigDetail',
    props: ['cid'], //父页面传过来的配置ID
  data() {
    return {
      lefttableloading: false,
      lefttableData: [],
      leftcurrentSelectId:'',
      righttableloading: false,
      righttableData: [],
      rightcurrentSelectId: '',
      
    }
  },
  created() {
    this.loadLeftTableData()
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
      this.loadRightTableData(row.Fileds)
    },

    /**
      * 加载页面左侧table数据
      */
    loadRightTableData: function (data) {
      this.righttableloading = true
      this.righttableData = data
      this.righttableloading = false
    },

  }
}
</script>

<style>
</style>
