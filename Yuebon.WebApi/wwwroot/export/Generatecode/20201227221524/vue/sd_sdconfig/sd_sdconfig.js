import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 目标库配置信息表分页查询
   * @param {查询条件} data
   */
export function getSd_sdconfigListWithPager(data) {
  return http.request({
    url: 'Sd_sdconfig/FindWithPagerAsync',
    method: 'get',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}/**
   * 获取所有可用的目标库配置信息表
   */
export function getAllSd_sdconfigList() {
  return http.request({
    url: 'Sd_sdconfig/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 新增或修改保存目标库配置信息表
   * @param data
   */
export function saveSd_sdconfig(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 获取目标库配置信息表详情
   * @param {Id} 目标库配置信息表Id
   */
export function getSd_sdconfigDetail(id) {
  return http({
    url: 'Sd_sdconfig/GetById?id=' + id,
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量设置启用状态
   * @param {id集合} ids
   */
export function setSd_sdconfigEnable(data) {
  return http({
    url: 'Sd_sdconfig/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量软删除
   * @param {id集合} ids
   */
export function deleteSoftSd_sdconfig(data) {
  return http({
    url: 'Sd_sdconfig/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}

/**
   * 批量删除
   * @param {id集合} ids
   */
export function deleteSd_sdconfig(data) {
  return http({
    url: 'Sd_sdconfig/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
