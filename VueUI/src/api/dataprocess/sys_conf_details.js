import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 分页查询
   * @param {查询条件} data
   */
export function getSys_conf_detailsListWithPager(data) {
  return http.request({
    url: 'Sys_conf_details/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
 * 获取所有表
 */
export function getTbNameList() {
  return http.request({
    url: 'Sys_conf_details/GetTbNameList',
    method: 'post',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
  * 获取指定记录的表字段
  */
export function GetColumnListsByDetailId(data) {
  return http.request({
    url: 'Sys_conf_details/GetColumnListsByDetailId',
    data: data,
    method: 'post',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
  * 获取指定表名的表字段
  */
export function GetColumnListsBytbName(data) {
  return http.request({
    url: 'Sys_conf_details/GetColumnListsBytbName',
    data: data,
    method: 'post',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
  * 获取数据获取方式下拉框数据集
  */
export function getDataGetTypeLists() {
  return http.request({
    url: 'Sys_conf_details/GetDataGetTypeLists',
    method: 'post',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 获取所有可用的
   */
export function getAllEnableByConfId(data) {
  return http.request({
    url: 'Sys_conf_details/GetAllEnableByConfId',
    data: data,
    method: 'post',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 新增或修改保存
   * @param data
   */
export function saveSys_conf_details (data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 获取详情
   * @param {Id} Id
   */
export function getSys_conf_detailsDetail (id) {
  return http({
    url: 'Sys_conf_details/GetById?id=' + id,
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量设置启用状态
   * @param {id集合} ids
   */
export function setSys_conf_detailsEnable (data) {
  return http({
    url: 'Sys_conf_details/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量软删除
   * @param {id集合} ids
   */
export function deleteSoftSys_conf_details (data) {
  return http({
    url: 'Sys_conf_details/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}

/**
   * 批量删除
   * @param {id集合} ids
   */
export function deleteSys_conf_details (data) {
  return http({
    url: 'Sys_conf_details/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
