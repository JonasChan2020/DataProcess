import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 系统分类表分页查询
   * @param {查询条件} data
   */
export function getSys_classifyListWithPager(data) {
  return http.request({
    url: 'Sys_classify/FindWithPagerAsync',
    method: 'get',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}/**
   * 获取所有可用的系统分类表
   */
export function getAllSys_classifyList() {
  return http.request({
    url: 'Sys_classify/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 新增或修改保存系统分类表
   * @param data
   */
export function saveSys_classify(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 获取系统分类表详情
   * @param {Id} 系统分类表Id
   */
export function getSys_classifyDetail(id) {
  return http({
    url: 'Sys_classify/GetById?id=' + id,
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量设置启用状态
   * @param {id集合} ids
   */
export function setSys_classifyEnable(data) {
  return http({
    url: 'Sys_classify/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量软删除
   * @param {id集合} ids
   */
export function deleteSoftSys_classify(data) {
  return http({
    url: 'Sys_classify/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}

/**
   * 批量删除
   * @param {id集合} ids
   */
export function deleteSys_classify(data) {
  return http({
    url: 'Sys_classify/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
