import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 数据输出模型最终查询语句分页查询
   * @param {查询条件} data
   */
export function getSys_outmodel_sqlListWithPager(data) {
  return http.request({
    url: 'Sys_outmodel_sql/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 数据输出模型
   * @param {查询条件} data
   */
export function getByOutModelId(data) {
  return http.request({
    url: 'Sys_outmodel_sql/GetByOutModelId',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 获取所有可用的数据输出模型最终查询语句
   */
export function getAllSys_outmodel_sqlList() {
  return http.request({
    url: 'Sys_outmodel_sql/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 新增或修改保存数据输出模型最终查询语句
   * @param data
   */
export function saveSys_outmodel_sql(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 获取数据输出模型最终查询语句详情
   * @param {Id} 数据输出模型最终查询语句Id
   */
export function getSys_outmodel_sqlDetail(id) {
  return http({
    url: 'Sys_outmodel_sql/GetById?id=' + id,
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量设置启用状态
   * @param {id集合} ids
   */
export function setSys_outmodel_sqlEnable(data) {
  return http({
    url: 'Sys_outmodel_sql/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量软删除
   * @param {id集合} ids
   */
export function deleteSoftSys_outmodel_sql(data) {
  return http({
    url: 'Sys_outmodel_sql/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}

/**
   * 批量删除
   * @param {id集合} ids
   */
export function deleteSys_outmodel_sql(data) {
  return http({
    url: 'Sys_outmodel_sql/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
