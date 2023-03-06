using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 內部表單，數據實體對象
    /// </summary>
    [Table("Chaochi_SecurityForm")]
    [Serializable]
    public class SecurityForm:BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string FormId { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string Vno { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string FormName { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string TBNO { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string Dept { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string Type { get; set; }

        /// 設置或獲取 
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string CreatorUserId { get; set; }

        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string LastModifyUserId { get; set; }

    }
}