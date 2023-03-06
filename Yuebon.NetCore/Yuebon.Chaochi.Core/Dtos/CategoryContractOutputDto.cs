﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Core.Dtos
{
    /// <summary>
    /// 輸出契約分類模型
    /// </summary>
    [Serializable]
    public class CategoryContractOutputDto
    {
        /// <summary>
        /// 分類Id
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// 父層Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 層次
        /// </summary>
        public int? Layers { get; set; }

        /// <summary>
        /// 分類名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 歸類
        /// </summary>
        public virtual string ArchiveLTo { get; set; }


        /// <summary>
        /// 合約種類
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// 合約子類別
        /// </summary>
        [MaxLength(50)]
        public virtual string SubType { get; set; }

        /// <summary>
        /// 業務是否要簽核
        /// </summary>
        public virtual string NeedSalesSign { get; set; }

        /// <summary>
        /// 主管是否要簽核
        /// </summary>
        public virtual string NeedSupervisorSign { get; set; }

        /// <summary>
        /// 是否要線上簽名
        /// </summary>
        public virtual string NeedSignOnline { get; set; }

        /// <summary>
        /// 排序碼
        /// </summary>
        public int? SortOrder { get; set; }

        /// <summary>
        /// 刪除標誌
        /// </summary>
        public bool? DeleteMark { get; set; }

        /// <summary>
        /// 有效標誌
        /// </summary>
        public bool EnabledMark { get; set; }

        /// <summary>
        /// 創建日期
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 創建用戶主鍵
        /// </summary>
        [MaxLength(50)]
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 最後修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最後修改用戶
        /// </summary>
        [MaxLength(50)]
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 刪除時間
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 刪除用戶
        /// </summary>
        [MaxLength(50)]
        public string DeleteUserId { get; set; }

        private List<CategoryContractOutputDto> _children = null;

        /// <summary>
        /// 子層
        /// </summary>
        public List<CategoryContractOutputDto> Children
        {
            get
            {
                if (_children == null) {
                    return null;
                } else if (_children.Count == 0) {
                    return null;
                } else {
                    return _children;
                }
            }
            set
            {
                _children = value;
            }
        }
    }
}
