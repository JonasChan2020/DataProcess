<template>
  <div class="app-container">
    <el-card>
      <VerifyConfigDetailtab v-if="VerifyFormVisible" :cid="confId" />
      <DataSyncConfigDetailtab v-if="DataSyncFormVisible" />
    </el-card>
  </div>
</template>

<script>
  import VerifyConfigDetail from './confdetail/VerifyConfigDetail.vue'
  import DataSyncConfigDetail from './confdetail/DataSyncConfigDetail.vue'
export default {
    name: 'ConfigDetails',
    components: {
      VerifyConfigDetailtab:VerifyConfigDetail,
      DataSyncConfigDetailtab:DataSyncConfigDetail
  },
  data() {
    return {
      confId: '',
      viewstr: '',
      VerifyFormVisible: false,
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

            this.VerifyFormVisible = true
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
