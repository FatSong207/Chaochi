using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;
using System.Diagnostics.CodeAnalysis;
using NPOI.OpenXmlFormats.Dml;

namespace Yuebon.Security.Models
{
    /// <summary>
    /// 用戶，數據實體對象
    /// </summary>
    [Table("Sys_UserD")]
    [Serializable]
    public class UserD:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Account { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string RealName { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string NickName { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string HeadIcon { get; set; }

        /// 設置或獲取 
        /// </summary>
        public int? Gender { get; set; }

        /// 設置或獲取 
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string MobilePhone { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string Email { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string WeChat { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string ManagerId { get; set; }

        /// 設置或獲取 
        /// </summary>
        public int? SecurityLevel { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string Signature { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string Country { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string Province { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string City { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string District { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string OrganizeId { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string DepartmentId { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string RoleId { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string OrganizeIdSec { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string DepartmentIdSec { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string RoleIdSec { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string OrganizeIdThird { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string DepartmentIdThird { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string RoleIdThird { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string DutyId { get; set; }

        /// 設置或獲取 
        /// </summary>
        public bool? IsAdministrator { get; set; }

        /// 設置或獲取 
        /// </summary>
        public bool? IsMember { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string MemberGradeId { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string ReferralUserId { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string UnionId { get; set; }

        /// 設置或獲取 
        /// </summary>
        public int? SortCode { get; set; }

        /// 設置或獲取 
        /// </summary>
        public bool? DeleteMark { get; set; }

        /// 設置或獲取 
        /// </summary>
        public bool? EnabledMark { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string Description { get; set; }

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

        /// 設置或獲取 
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// 設置或獲取 
        /// </summary>
        public string DeleteUserId { get; set; }
    }
}