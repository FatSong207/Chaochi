﻿import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 獲取所有可用的
   */
export function GetUserListByDept(deptId) {
  return http.request({
    url: 'User/GetUserListByDept',
    method: 'get',
    params: { deptId: deptId },
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 取的當前使用者可觸摸到的userlist
   */
export function GetPermissionUserList() {
  return http.request({
    url: 'User/GetPermissionUserList',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 取的當前使用者可觸摸到的userlist
   */
export function GetStoreSales() {
  return http.request({
    url: 'User/GetStoreSales',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 用戶分頁查詢
   * @param {查詢條件} data
   */
export function getUserListWithPager(data) {
  return http.request({
    url: 'User/FindWithPagerSearchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存用戶
   * @param data
   */
export function saveUser(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取用戶詳情
   * @param {Id} 用戶Id
   */
export function getUserDetail(id) {
  return http({
    url: 'User/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取用戶詳情
   * @param {Id} 用戶Id
   */
export function getUserInfo(account) {
  return http({
    url: 'User/GetByUserId',
    method: 'get',
    params: { account: account },
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取用戶詳情
   * @param {userName} 用戶帳號
   */
export function getByUserName(userName) {
  return http({
    url: 'User/GetByUserName',
    method: 'get',
    params: { userName: userName },
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setUserEnable(data) {
  return http({
    url: 'User/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftUser(data) {
  return http({
    url: 'User/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteUser(data) {
  return http({
    url: 'User/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 重置密碼
   * @param {userId:用戶id} data
   */
export function resetPassword(data) {
  return http({
    url: 'User/ResetPassword',
    method: 'post',
    params: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 修改密碼
   * @param {password:新密碼,password2:重復新密碼} data
   */
export function modifyPassword(data) {
  return http({
    url: 'User/ModifyPassword',
    method: 'post',
    params: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 註冊新用戶
   * @param data
   */
export function registerUser(data, url) {
  return http.request({
    url: 'User/Register',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**              <el-option
                v-for="item in sales"
                :key="item.Account"
                :label="item.RealName"
                :value="item.Account"
              />
   * 保存用戶主題配置
   * @param data
   */
export function saveThemeConfig(data) {
  return http.request({
    url: 'User/SaveUserTheme',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取所有可用的
   */
export function getAllUserList() {
  return http.request({
    url: 'User/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取所有可用的
   */
export function getSales() {
  return http.request({
    url: 'User/GetSales',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取所有可用的
   */
export function getSalesByDepartmentId(departmentId) {
  return http.request({
    url: 'User/GetSalesByDepartmentId',
    method: 'get',
    params: { departmentId: departmentId },
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
