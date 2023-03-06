using System;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Repositories
{
    /// <summary>
    /// 業務工作日誌倉儲接口的實現
    /// </summary>
    public class SalesWorkLogRepository : BaseRepository<SalesWorkLog, string>, ISalesWorkLogRepository
    {
		public SalesWorkLogRepository()
        {
        }

        public SalesWorkLogRepository(IDbContextCore context) : base(context)
        {

        }

        #region Dapper 操作

        //DapperConn 用于讀寫操作
        //DapperConnRead 用于只讀操作

        #endregion

        
        #region EF 操作

        //DbContext 用于讀寫操作
        //DbContextRead 用于只讀操作

        #endregion
    }
}