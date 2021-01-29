<template>
  <div>
    <el-form ref="editFrom" :model="editFrom" :rules="rules">
      <el-form-item label="创建时间" :label-width="formLabelWidth" prop="CreatorTime">
        <el-input v-model="editFrom.CreatorTime" placeholder="请输入创建时间" autocomplete="off" clearable />
      </el-form-item>
      <el-form-item label="创建人" :label-width="formLabelWidth" prop="CreatorUserId">
        <el-input v-model="editFrom.CreatorUserId" placeholder="请输入创建人" autocomplete="off" clearable />
      </el-form-item>
      <el-form-item label="删除标记" :label-width="formLabelWidth" prop="DeleteMark">
        <el-input v-model="editFrom.DeleteMark" placeholder="请输入删除标记" autocomplete="off" clearable />
      </el-form-item>
      <el-form-item label="删除时间" :label-width="formLabelWidth" prop="DeleteTime">
        <el-input v-model="editFrom.DeleteTime" placeholder="请输入删除时间" autocomplete="off" clearable />
      </el-form-item>
      <el-form-item label="删除人" :label-width="formLabelWidth" prop="DeleteUserId">
        <el-input v-model="editFrom.DeleteUserId" placeholder="请输入删除人" autocomplete="off" clearable />
      </el-form-item>
      <el-form-item label="描述" :label-width="formLabelWidth" prop="Description">
        <el-input v-model="editFrom.Description" placeholder="请输入描述" autocomplete="off" clearable />
      </el-form-item>
      <el-form-item label="启用标记" :label-width="formLabelWidth" prop="EnabledMark">
        <el-input v-model="editFrom.EnabledMark" placeholder="请输入启用标记" autocomplete="off" clearable />
      </el-form-item>
      <el-form-item label="是否为动态表" :label-width="formLabelWidth" prop="Is_dynamic">
        <el-input v-model="editFrom.Is_dynamic" placeholder="请输入是否为动态表" autocomplete="off" clearable />
      </el-form-item>
      <el-form-item label="是否为标识表" :label-width="formLabelWidth" prop="Is_flag">
        <el-input v-model="editFrom.Is_flag" placeholder="请输入是否为标识表" autocomplete="off" clearable />
      </el-form-item>
      <el-form-item label="最后修改时间" :label-width="formLabelWidth" prop="LastModifyTime">
        <el-input v-model="editFrom.LastModifyTime" placeholder="请输入最后修改时间" autocomplete="off" clearable />
      </el-form-item>
      <el-form-item label="最后修改人" :label-width="formLabelWidth" prop="LastModifyUserId">
        <el-input v-model="editFrom.LastModifyUserId" placeholder="请输入最后修改人" autocomplete="off" clearable />
      </el-form-item>
      <el-form-item label="执行顺序" :label-width="formLabelWidth" prop="Levelnum">
        <el-input v-model="editFrom.Levelnum" placeholder="请输入执行顺序" autocomplete="off" clearable />
      </el-form-item>
      <el-form-item label="排序字段" :label-width="formLabelWidth" prop="SortCode">
        <el-input v-model="editFrom.SortCode" placeholder="请输入排序字段" autocomplete="off" clearable />
      </el-form-item>
      <el-form-item label="状态" :label-width="formLabelWidth" prop="State">
        <el-input v-model="editFrom.State" placeholder="请输入状态" autocomplete="off" clearable />
      </el-form-item>
      <el-form-item label="配置ID" :label-width="formLabelWidth" prop="Sys_conf_id">
        <el-input v-model="editFrom.Sys_conf_id" placeholder="请输入配置ID" autocomplete="off" clearable />
      </el-form-item>
      <el-form-item label="表名" :label-width="formLabelWidth" prop="Tbname">
        <el-input v-model="editFrom.Tbname" placeholder="请输入表名" autocomplete="off" clearable />
      </el-form-item>

    </el-form>
    <div v-if="showType!=='show'" class="yuebon-page-footer">
      <el-button @click="reset">重置</el-button>
      <el-button v-preventReClick type="primary" @click="saveEditForm">保存</el-button>
    </div>
  </div>
</template>

<script>

import { saveSys_conf_details } from '@/api/dataprocess/sys_conf_details'

export default {
  name: 'ConfDetails',
  data() {
    return {
      editFormTitle: '',
      editFrom: {
        CreatorTime: '',
        CreatorUserId: '',
        DeleteMark: '',
        DeleteTime: '',
        DeleteUserId: '',
        Description: '',
        EnabledMark: '',
        Is_dynamic: '',
        Is_flag: '',
        LastModifyTime: '',
        LastModifyUserId: '',
        Levelnum: '',
        SortCode: '',
        State: '',
        Sys_conf_id: '',
        Tbname: ''

      },
      rules: {

      },
      formLabelWidth: '80px',
      currentId: '', // 当前操作对象的ID值，主要用于修改
      showType: 'edit' // 操作类型编辑、新增、查看
    }
  },
  created() {

  },
  methods: {
    /**
     * 初始化数据
     */
    InitDictItem() {

    },
    reset() {

    },
    bindEditInfo: function() {

    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          const data = {
            'CreatorTime': this.editFrom.CreatorTime,
            'CreatorUserId': this.editFrom.CreatorUserId,
            'DeleteMark': this.editFrom.DeleteMark,
            'DeleteTime': this.editFrom.DeleteTime,
            'DeleteUserId': this.editFrom.DeleteUserId,
            'Description': this.editFrom.Description,
            'EnabledMark': this.editFrom.EnabledMark,
            'Is_dynamic': this.editFrom.Is_dynamic,
            'Is_flag': this.editFrom.Is_flag,
            'LastModifyTime': this.editFrom.LastModifyTime,
            'LastModifyUserId': this.editFrom.LastModifyUserId,
            'Levelnum': this.editFrom.Levelnum,
            'SortCode': this.editFrom.SortCode,
            'State': this.editFrom.State,
            'Sys_conf_id': this.editFrom.Sys_conf_id,
            'Tbname': this.editFrom.Tbname,

            'Id': this.currentId
          }
          saveSys_conf_details(data).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
              this.dialogEditFormVisible = false
              this.currentSelected = ''
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
    }

  }
}
</script>
