import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 分页查询
   * @param {查询条件} data
   */
export function getSys_sysListWithPager (data) {
  return http.request({
    url: 'Sys_sys/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}/**
   * 获取所有可用的
   */
export function getAllSys_sysList () {
  return http.request({
    url: 'Sys_sys/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
export function choseSys (sysId) {
  return http.request({
    url: 'Sys_sys/ChoseSys',
    data: sysId,
    method: 'post',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 新增或修改保存
   * @param data
   */
export function saveSys_sys (data, url) {
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
export function getSys_sysDetail (id) {
  return http({
    url: 'Sys_sys/GetById?id=' + id,
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量设置启用状态
   * @param {id集合} ids
   */
export function setSys_sysEnable (data) {
  return http({
    url: 'Sys_sys/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量软删除
   * @param {id集合} ids
   */
export function deleteSoftSys_sys (data) {
  return http({
    url: 'Sys_sys/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}

/**
   * 批量删除
   * @param {id集合} ids
   */
export function deleteSys_sys (data) {
  return http({
    url: 'Sys_sys/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
