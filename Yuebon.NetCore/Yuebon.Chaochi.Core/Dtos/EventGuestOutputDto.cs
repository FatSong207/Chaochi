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
    public class EventGuestOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string EventId { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string GuestName { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string GusetCell { get; set; }

    }
}
