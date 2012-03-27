﻿using System.IO;
using System.Reflection;

namespace Geckon.Portal.Core.Standard.Extension
{
    public class ExtensionLoader : IExtensionLoader
    {
        #region Properties

        public Assembly                        Assembly { get; protected set; }
        public CHAOS.Portal.Data.EF.Extension Extension { get; protected set; }

        #endregion
        #region Construction

        public ExtensionLoader( CHAOS.Portal.Data.EF.Extension extension, APortalApplication application )
        {
            Extension = extension;
            Assembly  = Assembly.LoadFile( Path.Combine( application.ServiceDirectoryPath, "Extensions", extension.Path ) );
        }

        #endregion
        #region Business Logic

        

        #endregion
    }
}
