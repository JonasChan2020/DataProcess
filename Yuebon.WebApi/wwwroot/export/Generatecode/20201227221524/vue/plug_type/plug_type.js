import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 插件类型表分页查询
   * @param {查询条件} data
   */
export function getPlug_typeListWithPager(data) {
  return http.request({
    url: 'Plug_type/FindWithPagerAsync',
    method: 'get',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}/**
   * 获取所有可用的插件类型表
   */
export function getAllPlug_typeList() {
  return http.request({
    url: 'Plug_type/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 新增或修改保存插件类型表
   * @param data
   */
export function savePlug_type(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 获取插件类型表详情
   * @param {Id} 插件类型表Id
   */
export function getPlug_typeDetail(id) {
  return http({
    url: 'Plug_type/GetById?id=' + id,
    method: 'get',
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量设置启用状态
   * @param {id集合} ids
   */
export function setPlug_typeEnable(data) {
  return http({
    url: 'Plug_type/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
/**
   * 批量软删除
   * @param {id集合} ids
   */
export function deleteSoftPlug_type(data) {
  return http({
    url: 'Plug_type/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}

/**
   * 批量删除
   * @param {id集合} ids
   */
export function deletePlug_type(data) {
  return http({
    url: 'Plug_type/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiDataProcessUrl // 直接通过覆盖的方式
  })
}
