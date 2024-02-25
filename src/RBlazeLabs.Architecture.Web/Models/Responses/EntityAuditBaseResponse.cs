﻿using RBlazeLabs.Architecture.Web.Models.Responses;
using RBlazeLabs.Common.Contracts.Dtos;
using RBlazeLabs.Common.Models;

namespace RSoft.Lib.Common.Web.Models.Response
{

    /// <summary>
    /// Abstract audit-response base object
    /// </summary>
    public abstract class EntityAuditBaseResponse : EntityBaseResponse, IAuditDto
    {

        #region Properties

        /// <summary>
        /// Created author data
        /// </summary>
        public required AuditAuthor CreatedBy { get; set; }

        /// <summary>
        /// Changed author data
        /// </summary>
        public AuditAuthor? ChangedBy { get; set; }


        #endregion

    }

}
