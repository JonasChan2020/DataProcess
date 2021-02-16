<template>
  <div class="app-container">
    <el-row :gutter="24">
      <el-col :span="10">
        <el-row :gutter="24">
          <el-col :span="24">
            <el-card>
              <el-select v-model="Tbname" placeholder="请选择系统或数据库" @change="handleSelectSysDbChange()">
                <el-option v-for="item in SelectTbnameList"
                           :key="item.TableName"
                           :label="item.TableName"
                           :value="item.TableName" />
              </el-select>
            </el-card>
          </el-col>
        </el-row>
        <el-row :gutter="24">
          <el-col :span="24">
            <el-card>
              <el-cascader v-model="selectedclass" style="width:500px;" :options="selectclasses" filterable :props="{label:'ClassName',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }" clearable @change="handleSelectClassChange" />
              <el-table ref="gridtable"
                        v-loading="tableloading"
                        :data="tableData"
                        border
                        stripe
                        highlight-current-row
                        style="width: 100%"
                        :default-sort="{prop: 'SortCode', order: 'ascending'}"
                        @select="handleSelectChange"
                        @select-all="handleSelectAllChange"
                        @sort-change="handleSortChange">
                <el-table-column type="selection" width="30" />
                <el-table-column prop="SdName" label="编码" sortable="custom" width="120" />
                <el-table-column prop="Sdtype" label="名称" sortable="custom" width="120" />
                <el-table-column label="操作" sortable="custom" width="120" align="center">
                  <template slot-scope="scope">
                    <el-button type="primary"
                               icon="el-icon-plus"
                               size="small"
                               @click="UpdateDbContents(scope.row.Id)">更新结构信息</el-button>
                  </template>
                </el-table-column>
              </el-table>
              <div class="pagination-container">
                <el-pagination background
                               :current-page="pagination.currentPage"
                               :page-sizes="[5,10,20,50,100, 200, 300, 400]"
                               :page-size="pagination.pagesize"
                               layout="total, sizes, prev, pager, next, jumper"
                               :total="pagination.pageTotal"
                               @size-change="handleSizeChange"
                               @current-change="handleCurrentChange" />
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
              <el-button v-hasPermi="['Sd_sysdb/Edit']"
                         type="primary"
                         icon="el-icon-edit"
                         class="el-button-modify"
                         size="small"
                         @click="ShowEditOrViewDialog('edit')">修改</el-button>
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
              <el-button v-hasPermi="['Sd_sysdb/DeleteSoft']"
                         type="warning"
                         icon="el-icon-delete"
                         size="small"
                         @click="deleteSoft('0')">软删除</el-button>
              <el-button v-hasPermi="['Sd_sysdb/Delete']"
                         type="danger"
                         icon="el-icon-delete"
                         size="small"
                         @click="deletePhysics()">删除</el-button>
              <el-button type="default" icon="el-icon-refresh" size="small" @click="loadTableData()">刷新</el-button>
            </el-button-group>
          </div>
          <el-table ref="gridtable"
                    v-loading="tableloading"
                    :data="tableData"
                    border
                    stripe
                    highlight-current-row
                    style="width: 100%"
                    :default-sort="{prop: 'SortCode', order: 'ascending'}"
                    @select="handleSelectChange"
                    @select-all="handleSelectAllChange"
                    @sort-change="handleSortChange">
            <el-table-column type="selection" width="30" />
            <el-table-column prop="SdName" label="编码" sortable="custom" width="120" />
            <el-table-column prop="Sdtype" label="名称" sortable="custom" width="120" />
            <el-table-column label="操作" sortable="custom" width="120" align="center">
              <template slot-scope="scope">
                <el-button type="primary"
                           icon="el-icon-plus"
                           size="small"
                           @click="UpdateDbContents(scope.row.Id)">更新结构信息</el-button>
              </template>
            </el-table-column>
          </el-table>
          <div class="pagination-container">
            <el-pagination background
                           :current-page="pagination.currentPage"
                           :page-sizes="[5,10,20,50,100, 200, 300, 400]"
                           :page-size="pagination.pagesize"
                           layout="total, sizes, prev, pager, next, jumper"
                           :total="pagination.pageTotal"
                           @size-change="handleSizeChange"
                           @current-change="handleCurrentChange" />
          </div>
        </el-card>
      </el-col>
    </el-row>
    <el-dialog ref="dialogEditForm"
               :title="editFormTitle+'{TableNameDesc}'"
               :visible.sync="dialogEditFormVisible"
               width="640px">
      <el-form ref="editFrom" :model="editFrom" :rules="rules">
        <el-form-item label="配置编码" :label-width="formLabelWidth" prop="Confcode">
          <el-input v-model="editFrom.Confcode" placeholder="请输入配置编码" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="配置名称" :label-width="formLabelWidth" prop="Confname">
          <el-input v-model="editFrom.Confname" placeholder="请输入配置名称" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="数据源ID" :label-width="formLabelWidth" prop="Dsid">
          <el-input v-model="editFrom.Dsid" placeholder="请输入数据源ID" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="描述" :label-width="formLabelWidth" prop="Description">
          <el-input v-model="editFrom.Description" placeholder="请输入描述" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="配置分类" :label-width="formLabelWidth" prop="Classify_id">
          <el-cascader v-model="selectedclass" style="width:500px;" :options="selectclasses" filterable :props="{label:'Cname',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }" clearable @change="handleSelectClassChange" />
        </el-form-item>
        <el-form-item label="排序" :label-width="formLabelWidth" prop="SortCode">
          <el-input v-model.number="editFrom.SortCode" placeholder="请输入排序,默认为99" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="选项" :label-width="formLabelWidth" prop="">
          <el-checkbox v-model="editFrom.EnabledMark">启用</el-checkbox>
        </el-form-item>

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogEditFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="saveEditForm()">确 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>

import { getConf_confListWithPager, getConf_confDetail,
  saveConf_conf, setConf_confEnable, deleteSoftConf_conf,
  deleteConf_conf
} from '@/api/dataprocess/conf_conf'

export default {
  data() {
    return {
      
    }
  },
  created() {
    
  },
  methods: {
    /**
    *系统或数据库选择
    */
    handleSelectTbChange: function (value) {
      
    },
  }
}
</script>

<style>
</style>
