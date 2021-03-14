<template>
  <div class="app-container">
    <el-card>
      <DataSyncConfigDetailtab v-if="DataSyncFormVisible"  :cid="confId"/>
    </el-card>
  </div>
</template>

<script>
  import DataSyncConfigDetail from './confdetail/DataSyncConfigDetail.vue'
export default {
    name: 'ConfigDetails',
    components: {
      name:'DataSyncConfigDetail',
      DataSyncConfigDetailtab: DataSyncConfigDetail,
  },
  data() {
    return {
      confId: '', //系统ID或数据库ID
      viewstr: '', //跳转页面类型
      DataSyncFormVisible: false,
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
          this.confId = this.$route.params.id
          this.viewstr = this.$route.params.viewstr
          if (this.viewstr == "VerifyConfig") {

          } else if (this.viewstr == "DataSyncConfig") {
            this.DataSyncFormVisible = true
          } else {
            this.VerifyFormVisible = false
            this.DataSyncFormVisible = false
          }
        }
      },
  }
}
</script>

<style>
</style>
