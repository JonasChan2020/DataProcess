<template>
  <div class="app-container">
    <el-card>
      <InModelConfigtab v-if="InModelConfigVisible" :sid="sysid" />
      <OutModelConfigtab v-if="OutModelConfigVisible" :sid="sysid" />
    </el-card>
  </div>
</template>

<script>
  import InModelConfig from '@/views/dataprocess/inmodel/inmodel/index.vue'
  import OutModelConfig from '@/views/dataprocess/outmodel/outmodel/index.vue'
export default {
    name: 'ModelConfig',
    components: {
      InModelConfigtab: InModelConfig,
      OutModelConfigtab: OutModelConfig
    },
  data() {
    return {
      sysid: '', //系统ID或数据库ID
      viewstr: '', //跳转页面类型
      InModelConfigVisible: false,
      OutModelConfigVisible: false,
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
          this.sysid = this.$route.params.id
          this.viewstr = this.$route.params.viewstr
          if (this.viewstr == "input") {
            this.OutModelConfigVisible = false
            this.InModelConfigVisible = true
          } else if (this.viewstr == "output") {
            this.InModelConfigVisible = false
            this.OutModelConfigVisible = true
          } else {
            this.InModelConfigVisible = false
            this.OutModelConfigVisible = false
          }
        }
      },
  }
}
</script>

<style>
</style>
