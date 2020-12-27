import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 数据源分类表分页查询
   * @param {查询条件} data
   */
export function getDs_classifyListWithPager(data) {
  return http.request({
    url: 'Ds_classify/FindWithPagerAsync',
    method: 'get',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}/**
   * 获取所有可用的数据源分类表
   */
export function getAllDs_classifyList() {
  return http.request({
    url: 'Ds_classify/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 新增或修改保存数据源分类表
   * @param data
   */
export function saveDs_classify(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 获取数据源分类表详情
   * @param {Id} 数据源分类表Id
   */
export function getDs_classifyDetail(id) {
  return http({
    url: 'Ds_classify/GetById?id=' + id,
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量设置启用状态
   * @param {id集合} ids
   */
export function setDs_classifyEnable(data) {
  return http({
    url: 'Ds_classify/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量软删除
   * @param {id集合} ids
   */
export function deleteSoftDs_classify(data) {
  return http({
    url: 'Ds_classify/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}

/**
   * 批量删除
   * @param {id集合} ids
   */
export function deleteDs_classify(data) {
  return http({
    url: 'Ds_classify/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
