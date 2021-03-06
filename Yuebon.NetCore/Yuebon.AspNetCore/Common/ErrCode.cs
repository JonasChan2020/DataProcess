﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yuebon.AspNetCore.Models
{
    /// <summary>
    /// 错误代码描述
    /// </summary>
    public static class ErrCode
    {

        /// <summary>
        /// 请求成功
        /// </summary>
        public static string err0 = "请求成功";

        /// <summary>
        /// 请求成功代码0
        /// </summary>
        public static string successCode = "0";

        /// <summary>
        /// 请求失败
        /// </summary>
        public static string err1 = "请求失败";

        /// <summary>
        /// 请求失败代码1
        /// </summary>
        public static string failCode = "1";

        /// <summary>
        /// 获取access_token时AppID或AppSecret错误。请开发者认真比对appid和AppSecret的正确性，或查看是否正在为恰当的应用调用接口
        /// </summary>
        public static string err40001 = "获取access_token时AppID或AppSecret错误。请开发者认真比对appid和AppSecret的正确性，或查看是否正在为恰当的应用调用接口";

        /// <summary>
        /// 调用接口的服务器URL地址不正确，请联系供应商进行设置
        /// </summary>
        public static string err40002 = "调用接口的服务器URL地址不正确，请联系供应商进行授权";

        /// <summary>
        /// 请确保grant_type字段值为client_credential
        /// </summary>
        public static string err40003 = "请确保grant_type字段值为client_credential";

        /// <summary>
        /// 不合法的凭证类型
        /// </summary>
        public static string err40004 = "不合法的凭证类型";

        /// <summary>
        /// 用户令牌accesstoken超时失效
        /// </summary>
        public static string err40005 = "用户令牌accesstoken超时失效";

        /// <summary>
        /// 您未被授权使用该功能，请重新登录试试或联系管理员进行处理
        /// </summary>
        public static string err40006 = "您未被授权使用该功能，请重新登录试试或联系系统管理员进行处理";

        /// <summary>
        /// 传递参数出现错误
        /// </summary>
        public static string err40007 = "传递参数出现错误";

        /// <summary>
        /// 用户未登录或超时
        /// </summary>
        public static string err40008 = "用户未登录或超时";
        /// <summary>
        /// 该系统未获得授权，请联系系统管理员
        /// </summary>
        public static string err40009 = "该系统未获得授权，请联系系统管理员";
        /// <summary>
        /// 程序异常
        /// </summary>
        public static string err40110 = "程序异常";

        /// <summary>
        /// 更新数据失败
        /// </summary>
        public static string err43001 = "新增数据失败";

        /// <summary>
        /// 更新数据失败
        /// </summary>
        public static string err43002 = "更新数据失败";

        /// <summary>
        /// 物理删除数据失败
        /// </summary>
        public static string err43003 = "删除数据失败";

        /// <summary>
        /// 该用户不存在
        /// </summary>
        public static string err50001 = "该用户不存在";

        /// <summary>
        /// 该用户已存在
        /// </summary>
        public static string err50002 = "用户已存在，请登录或重新注册！";

        /// <summary>
        /// 会员注册失败
        /// </summary>
        public static string err50003 = "会员注册失败";

        /// <summary>
        /// 查询数据不存在
        /// </summary>
        public static string err60001 = "查询数据不存在";

        /// <summary>
        /// 未选择系统
        /// </summary>
        public static string err80001 = "未选择系统，请在系统管理页面选择系统";

        /// <summary>
        /// 未找到指定系统
        /// </summary>
        public static string err80002 = "未找到指定系统";

        /// <summary>
        /// 未设置主库
        /// </summary>
        public static string err80010 = "未设置主库";

        /// <summary>
        /// 未找到指定表的字段信息
        /// </summary>
        public static string err80012 = "未找到指定表的字段信息";

        /// <summary>
        /// 未找到指定表的字段信息
        /// </summary>
        public static string err80021 = "上传文件错误";

        #region 数据源管理
        /// <summary>
        /// 不能连接数据源
        /// </summary>
        public static string err80301 = "不能连接数据源";

        #endregion

        #region 数据交互管理
        /// <summary>
        /// 系统未设置主数据库，请先设置主数据库
        /// </summary>
        public static string err80401 = "系统未设置主数据库，请先设置主数据库";
        /// <summary>
        /// 找不到所选系统
        /// </summary>
        public static string err80402 = "找不到所选系统";
        /// <summary>
        /// 找不到所选数据库
        /// </summary>
        public static string err80403 = "找不到所选数据库";
        /// <summary>
        /// 所选数据库已加入系统，请直接关联系统
        /// </summary>
        public static string err80404 = "所选数据库已加入系统，请直接关联系统";

        /// <summary>
        /// 所选数据库已加入系统，但找不到对应系统
        /// </summary>
        public static string err80405= "所选数据库已加入系统，但找不到对应系统";

        /// <summary>
        /// 未找到该数据库或该系统主数据库的表格信息
        /// </summary>
        public static string err80406 = "未找到该数据库或该系统主数据库的表格信息";

        /// <summary>
        /// 未找到该数据项
        /// </summary>
        public static string err80407 = "未找到该数据项";


        #endregion

    }
}
