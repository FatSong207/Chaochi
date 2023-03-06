﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Models;
using Yuebon.Messages.Models;

namespace Yuebon.Messages.Dtos
{
    /// <summary>
    /// 輸入對象模型
    /// </summary>
    [AutoMap(typeof(MemberSubscribeMsg))]
    [Serializable]
    public class MemberSubscribeMsgInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取訂閱用戶
        /// </summary>
        public string SubscribeUserId { get; set; }

        /// <summary>
        /// 設置或獲取訂閱類型：SMS短信，WxApplet 微信小程序，InnerMessage站內消息 ，Email郵件通知
        /// </summary>
        public string SubscribeType { get; set; }

        /// <summary>
        /// 設置或獲取消息模板Id主鍵
        /// </summary>
        public string MessageTemplateId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string SubscribeTemplateId { get; set; }

        /// <summary>
        /// 設置或獲取訂閱狀態
        /// </summary>
        public string SubscribeStatus { get; set; }


    }
}
