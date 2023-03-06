using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class BankinfoOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }

        /// 設置或獲取銀行名稱
        /// </summary>
        [MaxLength(100)]
        public string BankName { get; set; }

        /// 設置或獲取銀行代碼
        /// </summary>
        [MaxLength(100)]
        public string BankNo { get; set; }

        /// 設置或獲取分行名稱
        /// </summary>
        [MaxLength(100)]
        public string BranchName { get; set; }

        /// 設置或獲取分行代碼
        /// </summary>
        [MaxLength(100)]
        public string BranchNo { get; set; }

    }
}