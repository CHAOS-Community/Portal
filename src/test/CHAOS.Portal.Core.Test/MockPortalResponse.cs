﻿using System;
using CHAOS.Portal.Core.Standard;

namespace CHAOS.Portal.Core.Test
{
    public class MockPortalResponse : IPortalResponse
    {
        #region Properties

        public IPortalResult PortalResult { get; set; }

        #endregion
        #region Constructors

        public MockPortalResponse()
        {
            PortalResult = new PortalResult();
        }

        #endregion
    }
}
