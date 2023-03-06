using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸入對象模型
    /// </summary>
    [AutoMap(typeof(SLMA))]
    [Serializable]
    public class SLMAInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取統一編號
        /// </summary>
        public string SLMAID { get; set; }
        /// <summary>
        /// 設置或獲取公司名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 設置或獲取負責人姓名
        /// </summary>
        public string Rep { get; set; }
        /// <summary>
        /// 設置或獲取許可字號/登記證字號
        /// </summary>
        public string LRNo { get; set; }
        /// <summary>
        /// 設置或獲取營業地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 設置或獲取聯絡電話
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 設置或獲取傳真號碼
        /// </summary>
        public string Fax { get; set; }        /// <summary>
        /// 設置或獲取電子郵件信箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 設置或獲取租賃住宅管理人員
        /// </summary>
        public string SIName { get; set; }
        /// <summary>
        /// 設置或獲取租賃住宅管理人員-證書字號
        /// </summary>
        public string SILRNo { get; set; }
        /// <summary>
        /// 設置或獲取租賃住宅管理人員-通訊地址
        /// </summary>
        public string SIAdd { get; set; }
        /// <summary>
        /// 設置或獲取租賃住宅管理人員-聯絡電話
        /// </summary>
        public string SITel { get; set; }
        /// <summary>
        /// 設置或獲取租賃住宅管理人員-電子郵件信箱
        /// </summary>
        public string SIEmail { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人-統一編號(身分證明文件編號)
        /// </summary>
        public string AGID { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人
        /// </summary>
        public string AGName { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人-證書字號
        /// </summary>
        public string AGLRNo { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人-通訊地址
        /// </summary>
        public string AGAdd { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人-聯絡電話
        /// </summary>
        public string AGTel { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人-電子郵件信箱
        /// </summary>
        public string AGEmail { get; set; }

    }
}
