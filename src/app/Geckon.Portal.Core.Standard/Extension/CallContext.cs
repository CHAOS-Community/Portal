﻿using System;
using System.Linq;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Data;

namespace Geckon.Portal.Core.Standard.Extension
{
    public class CallContext : ICallContext
    {
        #region Properties

        public ICache Cache { get; private set; }
        public ISolr  Solr { get; private set; }
        public string SessionID { get; set; }

        public Data.Dto.UserInfo User
        {
            get
            {
                Data.Dto.UserInfo userInfo = Cache.Get<Data.Dto.UserInfo>( string.Format( "[UserInfo:sid={0}]", SessionID ) );

                if (userInfo == null)
                {
                    using( PortalDataContext db = PortalDataContext.Default() )
                    {
                        userInfo = Data.Dto.UserInfo.Create( db.UserInfo_Get( null, Guid.Parse( SessionID ), null, null, null ).First() );

                        Cache.Put( string.Format("[UserInfo:sid={0}]", SessionID ),
                                   userInfo.ToXML().OuterXml,
                                   new TimeSpan(0, 1, 0));
                    }
                }

                return userInfo;
            }
        }

        #endregion
        #region Construction

        public CallContext( ICache cache, ISolr solr, string sessionID )
        {
            Cache     = cache;
            Solr      = solr;
            SessionID = sessionID;
        }

        #endregion
    }
}