import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 数据源表分页查询
   * @param {查询条件} data
   */
export function getDs_dsListWithPager(data) {
  return http.request({
    url: 'Ds_ds/FindWithPagerAsync',
    method: 'get',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}/**
   * 获取所有可用的数据源表
   */
export function getAllDs_dsList() {
  return http.request({
    url: 'Ds_ds/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 新增或修改保存数据源表
   * @param data
   */
export function saveDs_ds(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 获取数据源表详情
   * @param {Id} 数据源表Id
   */
export function getDs_dsDetail(id) {
  return http({
    url: 'Ds_ds/GetById?id=' + id,
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量设置启用状态
   * @param {id集合} ids
   */
export function setDs_dsEnable(data) {
  return http({
    url: 'Ds_ds/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量软删除
   * @param {id集合} ids
   */
export function deleteSoftDs_ds(data) {
  return http({
    url: 'Ds_ds/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}

/**
   * 批量删除
   * @param {id集合} ids
   */
export function deleteDs_ds(data) {
  return http({
    url: 'Ds_ds/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
