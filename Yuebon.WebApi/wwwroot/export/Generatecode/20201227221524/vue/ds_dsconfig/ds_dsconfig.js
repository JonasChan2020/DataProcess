import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 数据源配置信息表分页查询
   * @param {查询条件} data
   */
export function getDs_dsconfigListWithPager(data) {
  return http.request({
    url: 'Ds_dsconfig/FindWithPagerAsync',
    method: 'get',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}/**
   * 获取所有可用的数据源配置信息表
   */
export function getAllDs_dsconfigList() {
  return http.request({
    url: 'Ds_dsconfig/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 新增或修改保存数据源配置信息表
   * @param data
   */
export function saveDs_dsconfig(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 获取数据源配置信息表详情
   * @param {Id} 数据源配置信息表Id
   */
export function getDs_dsconfigDetail(id) {
  return http({
    url: 'Ds_dsconfig/GetById?id=' + id,
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量设置启用状态
   * @param {id集合} ids
   */
export function setDs_dsconfigEnable(data) {
  return http({
    url: 'Ds_dsconfig/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量软删除
   * @param {id集合} ids
   */
export function deleteSoftDs_dsconfig(data) {
  return http({
    url: 'Ds_dsconfig/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}

/**
   * 批量删除
   * @param {id集合} ids
   */
export function deleteDs_dsconfig(data) {
  return http({
    url: 'Ds_dsconfig/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
