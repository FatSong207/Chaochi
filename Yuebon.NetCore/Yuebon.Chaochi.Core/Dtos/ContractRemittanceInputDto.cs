using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 合約匯款帳號維護輸入對象模型
    /// </summary>
    [AutoMap(typeof(ContractRemittance))]
    [Serializable]
    public class ContractRemittanceInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// 設置或獲取類型(社宅、一般宅..)
        /// </summary>
        public string Type { get; set; }

        /// 設置或獲取戶名
        /// </summary>
        public string AccountName { get; set; }

        /// 設置或獲取使用單位
        /// </summary>
        public string UseCounty { get; set; }

        /// 設置或獲取銀行名稱
        /// </summary>
        public string BankName { get; set; }

        /// 設置或獲取銀行代碼
        /// </summary>
        public string BankNo { get; set; }

        /// 設置或獲取分行名稱
        /// </summary>
        public string BranchName { get; set; }

        /// 設置或獲取分行代碼
        /// </summary>
        public string BranchNo { get; set; }

    }
}