<template>
  <div class="app-container">
    <div>
      <span>验证插件</span>
    </div>
    <el-form ref="editFrom" :model="editFrom" label-width="80px">
      <el-form-item label="sql语句" :label-width="formLabelWidth" prop="sqlstr">
        <el-input type="textarea" v-model="editFrom.sqlstr" placeholder="请输入类型编码" autocomplete="off" clearable />
      </el-form-item>
    </el-form>
    <div slot="footer" class="dialog-footer">
      <el-button @click="closeForm()">取 消</el-button>
      <el-button type="primary" @click="saveEditForm()">确 定</el-button>
    </div>
  </div>
</template>

<script>


  export default {
    props: ["detailConfig"],
    data() {
      return {
        editFrom: {
          sqlstr: '',
        },
        formLabelWidth: '80px',
      }
    },
    created() {
      this.bindEditInfo()
    },
    methods: {
      saveEditForm() {
        const data = {
          'sqlstr': this.editFrom.sqlstr
        }
        this.$emit("listenTochildEvent", data);
        this.$refs['editFrom'].resetFields()
      },
      closeForm() {
        const data = null
        this.$emit("listenTochildEvent", data);
        this.$refs['editFrom'].resetFields()
      },
      bindEditInfo: function () {
        if (this.detailConfig != null) {
          this.editFrom.sqlstr = this.detailConfig.sqlstr
        }
      },
    }
  }
</script>

<style>
</style>
