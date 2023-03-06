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
    public class RemittanceROutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }

        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string CustomerRId { get; set; }

        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string IDNo { get; set; }

        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RAName { get; set; }

        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RAID { get; set; }

        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RBankName { get; set; }

        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RBankNo { get; set; }

        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RBranchName { get; set; }

        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RBranchNo { get; set; }

        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RANo { get; set; }

        /// 設置或獲取 
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserId { get; set; }

        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string LastModifyUserId { get; set; }

    }
}