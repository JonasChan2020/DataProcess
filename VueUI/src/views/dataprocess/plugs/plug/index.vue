<template>
  <div class="app-container">
    <div class="filter-container">
      <el-card>
        <el-form
          ref="searchform"
          :inline="true"
          :model="searchform"
          class="demo-form-inline"
          size="small"
        >
          <el-form-item label="名称：">
            <el-input v-model="searchform.keywords" clearable placeholder="名称" />
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="handleSearch()">查询</el-button>
          </el-form-item>
        </el-form>
      </el-card>
    </div>
    <el-card>
      <div class="list-btn-container">
        <el-button-group>
          <el-button
            v-hasPermi="['Plug_plug/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['Plug_plug/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >修改</el-button>
          <el-button
            v-hasPermi="['Plug_plug/Enable']"
            type="info"
            icon="el-icon-video-pause"
            size="small"
            @click="setEnable('0')"
          >禁用</el-button>
          <el-button
            v-hasPermi="['Plug_plug/Enable']"
            type="success"
            icon="el-icon-video-play"
            size="small"
            @click="setEnable('1')"
          >启用</el-button>
          <el-button
            v-hasPermi="['Plug_plug/DeleteSoft']"
            type="warning"
            icon="el-icon-delete"
            size="small"
            @click="deleteSoft('0')"
          >软删除</el-button>
          <el-button
            v-hasPermi="['Plug_plug/Delete']"
            type="danger"
            icon="el-icon-delete"
            size="small"
            @click="deletePhysics()"
          >删除</el-button>
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
        <el-table-column prop="Pcode" label="插件编码" sortable="custom" width="120" />
        <el-table-column prop="Pname" label="插件名称" sortable="custom" width="120" />
        <el-table-column prop="Ptag" label="标签" sortable="custom" width="120" />
        <el-table-column label="是否为公共" sortable="custom" width="120" prop="Is_public" align="center">
          <template slot-scope="scope">
            <el-tag :type="scope.row.Is_public === true ? 'success' : 'info'" disable-transitions>{{ scope.row.Is_public === true ? "是" : "否" }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="Ptype" label="插件类型" sortable="custom" width="260" align="center">
          <template slot-scope="scope">
            {{ scope.row.Classify_Name }}
          </template>
        </el-table-column>
        <el-table-column prop="Description" label="描述" sortable="custom" width="120" />
        <el-table-column prop="SortCode" label="排序字段" sortable="custom" width="90" align="center" />
        <el-table-column label="是否启用" sortable="custom" width="120" prop="EnabledMark" align="center">
          <template slot-scope="scope">
            <el-tag :type="scope.row.EnabledMark === true ? 'success' : 'info'" disable-transitions>{{ scope.row.EnabledMark === true ? "启用" : "禁用" }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="CreatorTime" label="创建时间" sortable />
        <el-table-column prop="LastModifyTime" label="更新时间" sortable />
      </el-table>
      <div class="pagination-container">
        <el-pagination
          background
          :current-page="pagination.currentPage"
          :page-sizes="[5,10,20,50,100, 200, 300, 400]"
          :page-size="pagination.pagesize"
          layout="total, sizes, prev, pager, next, jumper"
          :total="pagination.pageTotal"
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
        />
      </div>
    </el-card>
    <el-dialog
      ref="dialogEditForm"
      :close-on-click-modal="false"
      :show-close="true"
      :title="editFormTitle+'插件'"
      :visible.sync="dialogEditFormVisible"
      width="640px"
    >
      <el-form ref="editFrom" :model="editFrom" :rules="rules">
        <el-form-item label="插件编码" :label-width="formLabelWidth" prop="Pcode">
          <el-input v-model="editFrom.Pcode" placeholder="请输入插件编码" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="插件名称" :label-width="formLabelWidth" prop="Pname">
          <el-input v-model="editFrom.Pname" placeholder="请输入插件名称" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="标签" :label-width="formLabelWidth" prop="Ptag">
          <el-input v-model="editFrom.Ptag" placeholder="请输入标签" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="关联系统" :label-width="formLabelWidth" prop="Description">
          <el-input v-model="editFrom.Description" placeholder="请输入描述" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="描述" :label-width="formLabelWidth" prop="Description">
          <el-input v-model="editFrom.Description" placeholder="请输入描述" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="插件类型" :label-width="formLabelWidth" prop="Ptype">
          <el-cascader v-model="selectedclass" style="width:500px;" :options="selectclasses" filterable :props="{label:'Ptname',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }" clearable @change="handleSelectClassChange" />
        </el-form-item>
        <el-form-item label="上传插件" :label-width="formLabelWidth" prop="">
          <el-upload class="upload-demo"
                     ref="upload"
                     action=""
                     :accept="acceptFileType"
                     :limit="1"
                     @on-exceed="handleExceed"
                     :before-upload="beforeUpload"
                     @on-preview="handlePreview"
                    @on-remove="handleRemove"
                     :file-list="fileList"
                     :auto-upload="false">
            <el-button slot="trigger" size="small" type="primary">选取ZIP格式文件</el-button>
            <div slot="tip" class="el-upload_tip">只能上传.zip文件</div>
          </el-upload>
        </el-form-item>
        <el-form-item label="排序" :label-width="formLabelWidth" prop="SortCode">
          <el-input v-model.number="editFrom.SortCode" placeholder="请输入排序,默认为99" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="选项" :label-width="formLabelWidth" prop="">
          <el-checkbox v-model="editFrom.Is_public">是否为公共插件</el-checkbox>
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

import { getPlug_plugListWithPager, getPlug_plugDetail,
  savePlug_plug, setPlug_plugEnable, deleteSoftPlug_plug,
    deletePlug_plug, updateLoadplug
} from '@/api/dataprocess/plug_plug'
import {
  getAllClassifyTreeTable
} from '@/api/dataprocess/plug_type'

export default {
  data() {
    return {
      searchform: {
        keywords: ''
      },
      loadBtnFunc: [],
      tableData: [],
      tableloading: true,
      pagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      sortableData: {
        order: 'desc',
        sort: 'CreatorTime'
      },
      selectedclass: '',
      selectclasses: [],
      dialogEditFormVisible: false,
      editFormTitle: '',
      editFrom: {
        Description: '',
        EnabledMark: '',
        Is_public: '',
        Pcode: '',
        Pdesc: '',
        Pname: '',
        Ptag: '',
        Ptype: '',
        SortCode: ''

      },
      rules: {

      },
      formLabelWidth: '80px',
      currentId: '', // 当前操作对象的ID值，主要用于修改
      currentSelected: [],

      fileList: [],
      uploadLoading: false,
      acceptFileType: '.zip',
      downLoadLoading: ''
    }
  },
  created() {
    this.pagination.currentPage = 1
    this.InitDictItem()
    this.loadTableData()
    this.loadBtnFunc = JSON.parse(localStorage.getItem('yueboncurrentfuns'))
  },
  methods: {
    /**
     * 初始化数据
     */
    InitDictItem() {
    },
    /**
     * 加载页面table数据
     */
    loadTableData: function() {
      this.tableloading = true
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.keywords,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      }
      getPlug_plugListWithPager(seachdata).then(res => {
        this.tableData = res.ResData.Items
        this.pagination.pageTotal = res.ResData.TotalItems
        this.tableloading = false
      })
      getAllClassifyTreeTable().then(res => {
        this.tableData = res.ResData
        this.selectclasses = res.ResData
        this.tableloading = false
      })
    },
    /**
     * 点击查询
     */
    handleSearch: function() {
      this.pagination.currentPage = 1
      this.loadTableData()
    },

    /**
     * 新增、修改或查看明细信息（绑定显示数据）     *
     */
    ShowEditOrViewDialog: function(view) {
      if (view !== undefined) {
        if (this.currentSelected.length > 1 || this.currentSelected.length === 0) {
          this.$alert('请选择一条数据进行编辑/修改', '提示')
        } else {
          this.currentId = this.currentSelected[0].Id
          this.editFormTitle = '编辑'
          this.dialogEditFormVisible = true
          this.bindEditInfo()
        }
      } else {
        this.editFormTitle = '新增'
        this.currentId = ''
        this.selectedclass = ''
        this.dialogEditFormVisible = true
      }
    },
    bindEditInfo: function() {
      getPlug_plugDetail(this.currentId).then(res => {
        this.editFrom.Description = res.ResData.Description
        this.editFrom.EnabledMark = res.ResData.EnabledMark
        this.editFrom.Is_public = res.ResData.Is_public
        this.editFrom.Pcode = res.ResData.Pcode
        this.editFrom.Pdesc = res.ResData.Pdesc
        this.editFrom.Pname = res.ResData.Pname
        this.editFrom.Ptag = res.ResData.Ptag
        this.editFrom.Ptype = res.ResData.Ptype
        this.editFrom.SortCode = res.ResData.SortCode
        this.selectedclass = res.ResData.Ptype
      })
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          const data = {
            'Description': this.editFrom.Description,
            'EnabledMark': this.editFrom.EnabledMark,
            'Is_public': this.editFrom.Is_public,
            'Pcode': this.editFrom.Pcode,
            'Pdesc': this.editFrom.Pdesc,
            'Pname': this.editFrom.Pname,
            'Ptag': this.editFrom.Ptag,
            'Ptype': this.editFrom.Ptype,
            'SortCode': this.editFrom.SortCode,
            'Id': this.currentId
          }
          submitUpload();
          var url = 'Plug_plug/Insert'
          if (this.currentId !== '') {
            url = 'Plug_plug/Update?id=' + this.currentId
          }
          savePlug_plug(data, url).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
              this.dialogEditFormVisible = false
              this.currentSelected = ''
              this.selectedclass = ''
              this.$refs['editFrom'].resetFields()
              this.loadTableData()
              this.InitDictItem()
            } else {
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              })
            }
          })
        } else {
          return false
        }
      })
    },
    setEnable: function(val) {
      if (this.currentSelected.length === 0) {
        this.$alert('请先选择要操作的数据', '提示')
        return false
      } else {
        var currentIds = []
        this.currentSelected.forEach(element => {
          currentIds.push(element.Id)
        })
        const data = {
          Ids: currentIds,
          Flag: val
        }
        setPlug_plugEnable(data).then(res => {
          if (res.Success) {
            this.$message({
              message: '恭喜你，操作成功',
              type: 'success'
            })
            this.currentSelected = ''
            this.loadTableData()
          } else {
            this.$message({
              message: res.ErrMsg,
              type: 'error'
            })
          }
        })
      }
    },
    deleteSoft: function(val) {
      if (this.currentSelected.length === 0) {
        this.$alert('请先选择要操作的数据', '提示')
        return false
      } else {
        var currentIds = []
        this.currentSelected.forEach(element => {
          currentIds.push(element.Id)
        })
        const data = {
          Ids: currentIds,
          Flag: val
        }
        deleteSoftPlug_plug(data).then(res => {
          if (res.Success) {
            this.$message({
              message: '恭喜你，操作成功',
              type: 'success'
            })
            this.currentSelected = ''
            this.loadTableData()
          } else {
            this.$message({
              message: res.ErrMsg,
              type: 'error'
            })
          }
        })
      }
    },
    deletePhysics: function() {
      if (this.currentSelected.length === 0) {
        this.$alert('请先选择要操作的数据', '提示')
        return false
      } else {
        var currentIds = []
        this.currentSelected.forEach(element => {
          currentIds.push(element.Id)
        })
        const data = {
          Ids: currentIds
        }
        deletePlug_plug(data).then(res => {
          if (res.Success) {
            this.$message({
              message: '恭喜你，操作成功',
              type: 'success'
            })
            this.currentSelected = ''
            this.loadTableData()
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
     * 当表格的排序条件发生变化的时候会触发该事件
     */
    handleSortChange: function(column) {
      this.sortableData.sort = column.prop
      if (column.order === 'ascending') {
        this.sortableData.order = 'asc'
      } else {
        this.sortableData.order = 'desc'
      }
      this.loadTableData()
    },
    /**
*选择分类
*/
    handleSelectClassChange: function() {
      this.editFrom.Ptype = this.selectedclass
    },
    /**
     * 当用户手动勾选checkbox数据行事件
     */
    handleSelectChange: function(selection, row) {
      this.currentSelected = selection
    },
    /**
     * 当用户手动勾选全选checkbox事件
     */
    handleSelectAllChange: function(selection) {
      this.currentSelected = selection
    },
    /**
     * 选择每页显示数量
     */
    handleSizeChange(val) {
      this.pagination.pagesize = val
      this.pagination.currentPage = 1
      this.loadTableData()
    },
    /**
     * 选择当页面
     */
    handleCurrentChange(val) {
      this.pagination.currentPage = val
      this.loadTableData()
    },

    
    handleExceed(files, fileList) {
      this.$message.warning('只能选择1个文件!');
    },
    submitUpload() {
      this.uploadLoading = true;
      var that = this;
      setTimeout(function () {
        if (that.$refs.upload.$children[0].fileList.length == 1) {
          that.$refs.upload.submit();
        } else {
          that.uploadLoading = false;
          that.$message({
            type: 'error',
            showClose: true,
            duration: 3000,
            message: '请选择文件!'
          });
        };
      }, 100);
    },
    handleRemove(file, fileList) {
      //console.log(file,fileList);
    },
    handlePreview(file) {
      //console.log(file);
    },
    beforeUpload(file) {
      var that = this;
      //文件类型
      var fileName = file.name.substring(file.name.lastIndexOf('.') + 1);
      if (fileName != 'zip') {
        that.$message({
          type: 'error',
          showClose: true,
          duration: 3000,
          message: '文件类型不是.zip文件!'
        });
        return false;
      }
      //读取文件大小
      //var fileSize = file.size;
      //console.log(fileSize);
      //if (fileSize > 1048576) {
      //  that.uploadTemplateDialog = false;
      //  that.$message({
      //    type: 'error',
      //    showClose: true,
      //    duration: 3000,
      //    message: '文件大于1M!'
      //  });
      //  return false;
      //}
      that.downloadLoading = that.$loading({
        lock: true,
        text: '数据导入中...',
        spinner: 'el-icon-loading',
        background: 'rgba(0,0,0,0.7)'
      });
      let fd = new FormData();
      fd.append('file', file);
      updateLoadplug(fd).then(res => {
        that.downloadLoading.close();
        that.uploadLoading = false;
        let resp = res.data
        if (resp.resultCode == 200) {
          that.$message.success(resp.resultMsg);
          //that.queryData();//更新数据
        } else {
          that.$message({
            type: 'error',
            showClose: true,
            duration: 60000,
            message: resp.resultMsg
          });
        }
      })
      return false;
    }
  }
}
</script>

<style>
</style>
