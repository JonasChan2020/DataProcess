import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 数据输出模型分类表分页查询
   * @param {查询条件} data
   */
export function getSys_outmodel_classifyListWithPager(data) {
  return http.request({
    url: 'Sys_outmodel_classify/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}/**
   * 获取所有可用的数据输出模型分类表
   */
export function getAllSys_outmodel_classifyList() {
  return http.request({
    url: 'Sys_outmodel_classify/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 新增或修改保存数据输出模型分类表
   * @param data
   */
export function saveSys_outmodel_classify(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 获取数据输出模型分类表详情
   * @param {Id} 数据输出模型分类表Id
   */
export function getSys_outmodel_classifyDetail(id) {
  return http({
    url: 'Sys_outmodel_classify/GetById?id=' + id,
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量设置启用状态
   * @param {id集合} ids
   */
export function setSys_outmodel_classifyEnable(data) {
  return http({
    url: 'Sys_outmodel_classify/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量软删除
   * @param {id集合} ids
   */
export function deleteSoftSys_outmodel_classify(data) {
  return http({
    url: 'Sys_outmodel_classify/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}

/**
   * 批量删除
   * @param {id集合} ids
   */
export function deleteSys_outmodel_classify(data) {
  return http({
    url: 'Sys_outmodel_classify/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
