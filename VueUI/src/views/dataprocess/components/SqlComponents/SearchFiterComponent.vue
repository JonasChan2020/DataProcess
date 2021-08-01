<template>
  <div>
    <el-form ref="editSubFrom" :model="editSubFrom" :rules="Subrules">
      <el-button v-if="editSubFrom.dynamicFiterForm==null||editSubFrom.dynamicFiterForm.length==0" type="text" @click.prevent="addStartDomain()">添加</el-button>
      <el-button v-if="editSubFrom.dynamicFiterForm==null||editSubFrom.dynamicFiterForm.length==0" type="text" @click.prevent="addStartDomainKh()">括号</el-button>
      <el-form-item
        v-for="(domain, index) in editSubFrom.dynamicFiterForm"
        :key="index"
      >
        <el-tag v-if="domain.KhType==0" effect="dark" :type="domain.type">(</el-tag>
        <el-tag v-if="domain.KhType==1" effect="dark" :type="domain.type">)</el-tag>
        <el-select v-if="(domain.KhType==-1)" v-model="domain.columnName" placeholder="请选择字段" style="width:15%">
          <el-option
            v-for="item in SelectColumnNameList"
            :key="item.FieldName"
            :label="item.FieldName"
            :value="item.FieldName"
          />
        </el-select>
        <el-select v-if="(domain.KhType==-1)" v-model="domain.rex" placeholder="请选择操作符" style="width:10%">
          <el-option
            v-for="item in SelectRexList"
            :key="item"
            :label="item"
            :value="item"
          />
        </el-select>
        <el-select
          v-if="(domain.KhType==-1)"
          v-model="domain.valuetype"
          placeholder="请选择值类型"
          style="width:15%"
        >
          <el-option
            v-for="item in SelectValueTypeList"
            :key="item.Key"
            :label="item.Value"
            :value="item.Key"
          />
        </el-select>
        <el-input v-if="(domain.KhType==-1&&domain.valuetype==2)" v-model="domain.value" style="width:20%" />
        <el-select
          v-if="(domain.KhType==-1&&domain.valuetype==1)"
          v-model="domain.tbName"
          placeholder="请选择表"
          style="width:15%"
        >
          <el-option
            v-for="item in tbList"
            :key="item"
            :label="item"
            :value="item"
          />
        </el-select>
        <el-select
          v-if="(domain.KhType==-1&&domain.valuetype==1)"
          v-model="domain.colName"
          placeholder="请选择字段"
          style="width:15%"
        >
          <el-option
            v-for="item in colList"
            :key="item"
            :label="item"
            :value="item"
          />
        </el-select>
        <el-select v-if="(domain.KhType!=0)" v-model="domain.aon" placeholder="请选择连接符" style="width:15%">
          <el-option
            v-for="item in SelectaonList"
            :key="item"
            :label="item"
            :value="item"
          />
        </el-select>
        <el-button type="text" @click.prevent="removeDomain(domain)">删除</el-button>
        <el-button v-if="domain.showBtn==true&&domain.KhType!=0" type="text" @click.prevent="addDomain(domain)">添加</el-button>
        <el-button v-if="domain.showBtn==true&&domain.KhType!=0" type="text" @click.prevent="addDomainKh(domain)">括号</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>

export default {
  name: 'Searchfitercom',
  props: ['beforeTables'], // 父页面传过来的配置ID
  data() {
    return {
      editSubFrom: {
        Tbname: '',
        dynamicFiterForm: []

      },
      Subrules: {

      },
      tbList: [],
      colList: [],
      SelectColumnNameList: [], // 字段下拉框数据
      SelectValueTypeList: [
        { Key: '1', Value: '上级表字段' },
        { Key: '2', Value: '自定义值' }
      ], // 连接符下拉框数据
      SelectRexList: [
        '=',
        '!=',
        '<',
        '<=',
        '>',
        '>=',
        '包含',
        '不包含',
        '开始以',
        '开始不是以',
        '结束以',
        '结束不是以',
        '是null',
        '不是null',
        '是空',
        '不是空',
        '介于',
        '不介于'
      ], // 操作符下拉框数据
      SelectaonList: [
        'and',
        'or'
      ], // 连接符下拉框数据
      tagType: [
        '',
        'success',
        'info',
        'danger',
        'warning'
      ]

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

    /**
      * 动态修改字段列表
      */
    changeCols(cols) {
      this.SelectColumnNameList = cols
    },

    /**
       * 返回最终Json
       */
    getResultJson() {
      return JSON.stringify(this.editSubFrom.dynamicFiterForm)
    },

    removeDomain(item) {
      var index = this.editSubFrom.dynamicFiterForm.indexOf(item)
      if (index !== -1) {
        if (item.KhType != -1) { // 如果是括号
          var startkh
          var endkh
          var children = this.editSubFrom.dynamicFiterForm.filter(tmp => tmp.InKhIndex == item.Kh && tmp.KhType == 0)
          children.forEach(tmp => {
            this.removeDomain(tmp) // 删掉整个括号
          })
          if (item.KhType == 0) {
            startkh = item
            endkh = this.editSubFrom.dynamicFiterForm.filter(tmp => tmp.Kh == item.Kh && tmp.KhType == 1)[0]
          } else {
            startkh = this.editSubFrom.dynamicFiterForm.filter(tmp => tmp.Kh == item.Kh && tmp.KhType == 0)[0]
            endkh = item
          }
          var startindex = this.editSubFrom.dynamicFiterForm.indexOf(startkh)
          var endindex = this.editSubFrom.dynamicFiterForm.indexOf(endkh)
          if (startindex != 0) {
            if (this.editSubFrom.dynamicFiterForm[startindex - 1].KhType == 0 && this.editSubFrom.dynamicFiterForm[endindex + 1].KhType == 1) { // 括号外层也是括号
              var upstartkh = this.editSubFrom.dynamicFiterForm[startindex - 1]
              this.editSubFrom.dynamicFiterForm = this.editSubFrom.dynamicFiterForm.filter(tmp => tmp.Kh != item.Kh)
              this.removeDomain(upstartkh) // 删掉上层括号
            } else {
              this.editSubFrom.dynamicFiterForm = this.editSubFrom.dynamicFiterForm.filter(tmp => tmp.Kh != item.Kh)
            }
          } else {
            this.editSubFrom.dynamicFiterForm = this.editSubFrom.dynamicFiterForm.filter(tmp => tmp.Kh != item.Kh)
          }
        } else {
          if (this.editSubFrom.dynamicFiterForm[index - 1].KhType == 0) { // 如果上一个是左括号，则删除整个括号，并更新括号序号
            if (this.editSubFrom.dynamicFiterForm[index + 1].KhType == 1) { // 如果下一个是右括号，则证明括号内无数据了。
              this.removeDomain(this.editSubFrom.dynamicFiterForm[index - 1]) // 删掉整个括号
            } else {
              this.editSubFrom.dynamicFiterForm.splice(index, 1)
            }
          } else {
            this.editSubFrom.dynamicFiterForm.splice(index, 1)
          }
        }
      }
    },
    addDomain(item) {
      var index = this.editSubFrom.dynamicFiterForm.indexOf(item)
      if (item.InKhIndex >= 0) { // 括号内，需要插入
        this.editSubFrom.dynamicFiterForm.splice(index + 1, 0, {
          columnName: '', // 字段名称
          rex: '', // 操作符
          valuetype: '', // 值类型
          tbName: '', // 表名
          colName: '', // 字段名
          value: '', // 值
          Kh: item.InKhIndex, // 括号序号 从1开始
          KhType: -1, // 括号类型，0是起始，1是结束
          InKhIndex: item.InKhIndex, // 属于第几个括号内
          NextKhIndex: item.NextKhIndex,
          type: '',
          showBtn: true // 是否显示按钮
        })
      } else { // 括号外，直接新增
        this.editSubFrom.dynamicFiterForm.push({
          columnName: '', // 字段名称
          rex: '', // 操作符
          valuetype: '', // 值类型
          tbName: '', // 表名
          colName: '', // 字段名
          value: '', // 值
          Kh: item.InKhIndex, // 括号序号 从1开始
          KhType: -1, // 括号类型，0是起始，1是结束
          InKhIndex: item.InKhIndex, // 属于第几个括号内
          NextKhIndex: item.NextKhIndex,
          type: '',
          showBtn: true // 是否显示按钮
        })
      }
    },
    addDomainKh(item) {
      var index = this.editSubFrom.dynamicFiterForm.indexOf(item)
      if (item.InKhIndex >= 0) { // 括号内，需要插入
        this.editSubFrom.dynamicFiterForm.splice(index + 1, 0, { // 加左括号
          columnName: '', // 字段名称
          rex: '', // 操作符
          valuetype: '', // 值类型
          tbName: '', // 表名
          colName: '', // 字段名
          value: '', // 值
          Kh: item.NextKhIndex, // 括号序号 从1开始
          KhType: 0, // 括号类型，0是起始，1是结束
          InKhIndex: item.InKhIndex, // 属于第几个括号内
          NextKhIndex: item.NextKhIndex + 1,
          type: this.tagType[item.NextKhIndex % 5],
          showBtn: false // 是否显示按钮
        })
        this.editSubFrom.dynamicFiterForm.splice(index + 2, 0, { // 加数据
          columnName: '', // 字段名称
          rex: '', // 操作符
          valuetype: '', // 值类型
          tbName: '', // 表名
          colName: '', // 字段名
          value: '', // 值
          Kh: item.NextKhIndex, // 括号序号 从1开始
          KhType: -1, // 括号类型，0是起始，1是结束
          InKhIndex: item.NextKhIndex, // 属于第几个括号内
          NextKhIndex: item.NextKhIndex + 1,
          type: '',
          showBtn: true // 是否显示按钮
        })
        this.editSubFrom.dynamicFiterForm.splice(index + 3, 0, { // 加右括号
          columnName: '', // 字段名称
          rex: '', // 操作符
          valuetype: '', // 值类型
          tbName: '', // 表名
          colName: '', // 字段名
          value: '', // 值
          Kh: item.NextKhIndex, // 括号序号 从1开始
          KhType: 1, // 括号类型，0是起始，1是结束
          InKhIndex: item.InKhIndex, // 属于第几个括号内
          NextKhIndex: item.NextKhIndex + 1,
          type: this.tagType[item.NextKhIndex % 5],
          showBtn: true // 是否显示按钮
        })
      } else { // 括号外，直接新增
        this.editSubFrom.dynamicFiterForm.push({ // 加左括号
          columnName: '', // 字段名称
          rex: '', // 操作符
          valuetype: '', // 值类型
          tbName: '', // 表名
          colName: '', // 字段名
          value: '', // 值
          Kh: item.NextKhIndex, // 括号序号 从1开始
          KhType: 0, // 括号类型，0是起始，1是结束
          InKhIndex: item.InKhIndex, // 属于第几个括号内
          NextKhIndex: item.NextKhIndex + 1,
          type: '',
          showBtn: false // 是否显示按钮
        })
        this.editSubFrom.dynamicFiterForm.push({ // 加数据
          columnName: '', // 字段名称
          rex: '', // 操作符
          valuetype: '', // 值类型
          tbName: '', // 表名
          colName: '', // 字段名
          value: '', // 值
          Kh: item.NextKhIndex, // 括号序号 从1开始
          KhType: -1, // 括号类型，0是起始，1是结束
          InKhIndex: item.NextKhIndex, // 属于第几个括号内
          NextKhIndex: item.NextKhIndex + 1,
          type: '',
          showBtn: true // 是否显示按钮
        })
        this.editSubFrom.dynamicFiterForm.push({ // 加右括号
          columnName: '', // 字段名称
          rex: '', // 操作符
          valuetype: '', // 值类型
          tbName: '', // 表名
          colName: '', // 字段名
          value: '', // 值
          Kh: item.NextKhIndex, // 括号序号 从1开始
          KhType: 1, // 括号类型，0是起始，1是结束
          InKhIndex: item.InKhIndex, // 属于第几个括号内
          NextKhIndex: item.NextKhIndex + 1,
          type: '',
          showBtn: true // 是否显示按钮
        })
      }
      // 更新下一个括号Index
      this.editSubFrom.dynamicFiterForm.forEach(tmp => {
        tmp.NextKhIndex = item.NextKhIndex + 1
      })
    },
    addStartDomain() {
      this.editSubFrom.dynamicFiterForm = [{
        columnName: '', // 字段名称
        rex: '', // 操作符
        valuetype: '', // 值类型
        tbName: '', // 表名
        colName: '', // 字段名
        value: '', // 值
        Kh: -1, // 括号序号 从1开始
        KhType: -1, // 括号类型，0是起始，1是结束
        InKhIndex: -1, // 属于第几个括号内
        NextKhIndex: 0,
        type: '',
        showBtn: true // 是否显示按钮
      }]
    },
    addStartDomainKh() {
      this.editSubFrom.dynamicFiterForm = [{ // 加左括号
        columnName: '', // 字段名称
        rex: '', // 操作符
        valuetype: '', // 值类型
        tbName: '', // 表名
        colName: '', // 字段名
        value: '', // 值
        Kh: 0, // 括号序号 从0开始
        KhType: 0, // 括号类型，0是起始，1是结束
        InKhIndex: -1, // 属于第几个括号内
        NextKhIndex: 1,
        type: '',
        showBtn: false // 是否显示按钮
      }]
      this.editSubFrom.dynamicFiterForm.push({ // 加数据
        columnName: '', // 字段名称
        rex: '', // 操作符
        valuetype: '', // 值类型
        tbName: '', // 表名
        colName: '', // 字段名
        value: '', // 值
        Kh: 0, // 括号序号 从1开始
        KhType: -1, // 括号类型，0是起始，1是结束
        InKhIndex: 0, // 属于第几个括号内
        NextKhIndex: 1,
        type: '',
        showBtn: true // 是否显示按钮
      })
      this.editSubFrom.dynamicFiterForm.push({ // 加右括号
        columnName: '', // 字段名称
        rex: '', // 操作符
        valuetype: '', // 值类型
        tbName: '', // 表名
        colName: '', // 字段名
        value: '', // 值
        Kh: 0, // 括号序号 从1开始
        KhType: 1, // 括号类型，0是起始，1是结束
        InKhIndex: -1, // 属于第几个括号内
        NextKhIndex: 1,
        type: '',
        showBtn: true // 是否显示按钮
      })
    },
    uuid() {
      var s = []
      var hexDigits = '0123456789abcdef'
      for (var i = 0; i < 36; i++) {
        s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1)
      }
      s[14] = '4'
      s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1)
      s[8] = s[13] = s[18] = s[23] = '-'

      this.uuidA = s.join('')
      console.log(s.join(''), 's.join("")')
      return this.uuidA
    }
  }
}
</script>

<style>
</style>
