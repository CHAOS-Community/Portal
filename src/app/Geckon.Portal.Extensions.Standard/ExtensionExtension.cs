using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Geckon.Common.Extensions;
using Geckon.Portal.Core;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;

namespace Geckon.Portal.Extensions.Standard
{
    public class ExtensionExtension : AExtension
    {
        #region GET

        public void Get( string sessionID )
        {
            foreach( IExtensionLoader loader in PortalContext.LoadedExtensions.Values )
            {
                foreach( Type type in loader.Assembly.GetTypes() )
                {
                    if( !type.Implements<IExtension>( ) )
                        continue;
                    
                    foreach( MethodInfo methodInfo in type.GetMethods(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly) )
                    {
                        string parameters = "";

                        foreach( ParameterInfo parameterInfo in methodInfo.GetParameters() )
                        {
                            parameters += string.Format( "{0} {1},", parameterInfo.Name, parameterInfo.ParameterType.FullName );
                        }

                        CallContext.PortalResult.GetModule("Geckon.Portal").AddResult(new ExtensionMethod(loader.Extension.Map, methodInfo.Name, "(" + parameters.Substring(0, parameters.Length - 1) + ")"));
                    }
                }
            }
        }

        
        #endregion
    }
}
