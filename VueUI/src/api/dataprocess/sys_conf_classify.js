import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 分页查询
   * @param {查询条件} data
   */
export function getSys_conf_classifyListWithPager (data) {
  return http.request({
    url: 'Sys_conf_classify/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}/**
   * 获取所有可用的
   */
export function getAllSys_conf_classifyList () {
  return http.request({
    url: 'Sys_conf_classify/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}

/**
 * 获取树形分类
*/
export function getAllSysConfClassifyTreeTable (data) {
  return http.request({
    url: 'Sys_conf_classify/GetAllClassifyTreeTable',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 新增或修改保存
   * @param data
   */
export function saveSys_conf_classify (data, url) {
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
export function getSys_conf_classifyDetail (id) {
  return http({
    url: 'Sys_conf_classify/GetById?id=' + id,
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量设置启用状态
   * @param {id集合} ids
   */
export function setSys_conf_classifyEnable (data) {
  return http({
    url: 'Sys_conf_classify/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量软删除
   * @param {id集合} ids
   */
export function deleteSoftSys_conf_classify (data) {
  return http({
    url: 'Sys_conf_classify/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}

/**
   * 批量删除
   * @param {id集合} ids
   */
export function deleteSys_conf_classify (data) {
  return http({
    url: 'Sys_conf_classify/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
