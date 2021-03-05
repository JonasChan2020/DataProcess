import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 数据输出模型分页查询
   * @param {查询条件} data
   */
export function getSys_outmodelListWithPager(data) {
  return http.request({
    url: 'Sys_outmodel/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 获取所有可用的数据输出模型
   */
export function getAllSys_outmodelList() {
  return http.request({
    url: 'Sys_outmodel/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 新增或修改保存数据输出模型
   * @param data
   */
export function saveSys_outmodel(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 获取数据输出模型详情
   * @param {Id} 数据输出模型Id
   */
export function getSys_outmodelDetail(id) {
  return http({
    url: 'Sys_outmodel/GetById?id=' + id,
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量设置启用状态
   * @param {id集合} ids
   */
export function setSys_outmodelEnable(data) {
  return http({
    url: 'Sys_outmodel/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量软删除
   * @param {id集合} ids
   */
export function deleteSoftSys_outmodel(data) {
  return http({
    url: 'Sys_outmodel/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}

/**
   * 批量删除
   * @param {id集合} ids
   */
export function deleteSys_outmodel(data) {
  return http({
    url: 'Sys_outmodel/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
