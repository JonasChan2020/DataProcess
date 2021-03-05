import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 数据输出模型详情表分页查询
   * @param {查询条件} data
   */
export function getSys_outmodel_detailsListWithPager(data) {
  return http.request({
    url: 'Sys_outmodel_details/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}/**
   * 获取所有可用的数据输出模型详情表
   */
export function getAllSys_outmodel_detailsList() {
  return http.request({
    url: 'Sys_outmodel_details/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 新增或修改保存数据输出模型详情表
   * @param data
   */
export function saveSys_outmodel_details(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 获取数据输出模型详情表详情
   * @param {Id} 数据输出模型详情表Id
   */
export function getSys_outmodel_detailsDetail(id) {
  return http({
    url: 'Sys_outmodel_details/GetById?id=' + id,
    method: 'get',
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
   * 批量设置启用状态
   * @param {id集合} ids
   */
export function setSys_outmodel_detailsEnable(data) {
  return http({
    url: 'Sys_outmodel_details/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量软删除
   * @param {id集合} ids
   */
export function deleteSoftSys_outmodel_details(data) {
  return http({
    url: 'Sys_outmodel_details/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}

/**
   * 批量删除
   * @param {id集合} ids
   */
export function deleteSys_outmodel_details(data) {
  return http({
    url: 'Sys_outmodel_details/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
