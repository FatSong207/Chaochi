﻿using System;
using System.Data.Common;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Repositories;
using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using System.Data;
using Yuebon.Chaochi.Dtos;
using System.Collections.Generic;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Mapping;
using System.Threading.Tasks;
using Yuebon.Commons.Extend;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Enums;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class TBNoB4Service : BaseService<TBNoB4, TBNoB4, string>, ITBNoB4Service
    {
        private readonly ITBNoB4Repository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userLogOnRepository"></param>
        /// <param name="logService"></param>
        /// <param name="roleService"></param>
        /// <param name="organizeService"></param>
        public TBNoB4Service(ITBNoB4Repository repository) : base(repository)
        {
            _repository = repository;
        }

    }
}