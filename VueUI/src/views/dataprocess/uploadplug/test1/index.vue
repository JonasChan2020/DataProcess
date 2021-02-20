<template>
  <div class="app-container">
    <div>
      <span>参数有三种，1:步骤内，2:自身,3:外传参数，1使用格式  $<1$_$1!T_FAC_BASE.ID>$;2使用格式  $<2$_$列编码$_$列Index>$;3通常是导入对应配置中的参数传递，故此以$<3$_$0>$，$<3$_$1>$顺延，默认最后一位是对应列</span>
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
