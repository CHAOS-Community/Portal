using System.Collections.Generic;
using System.Reflection;
using CHAOS.Portal.Core.Extension;
using CHAOS.Portal.Core.Request;
using CHAOS.Portal.Exception;

namespace CHAOS.Portal.Core
{
    public class PortalApplication
    {
        #region Properties

        public ParameterBindings                     Bindings { get; set; }
        public IDictionary<string, IExtensionLoader> LoadedExtensions { get; set; }

        #endregion
        #region Constructors

        public PortalApplication()
        {
            Bindings = new ParameterBindings();
            LoadedExtensions = new Dictionary<string, IExtensionLoader>();
        }

        #endregion
        #region Business Logic

        public void ProcessRequest( ICallContext callContext )
        {
            if( !LoadedExtensions.ContainsKey( callContext.PortalRequest.Extension ) )
                throw new ExtensionMissingException( "The requested Extension wasn't found in any loaded assembly" );

            IExtension extension  = GetExtension( callContext );
            MethodInfo action     = GetAction( callContext, extension );
            object[]   parameters = BindParameters( callContext, action.GetParameters() );
            
            action.Invoke( extension, parameters );
        }

        private object[] BindParameters( ICallContext callContext, ParameterInfo[] parameters )
        {
            object[] boundParameters = new object[ parameters.Length ];

            foreach( ParameterInfo parameterInfo in parameters )
            {
                if( !Bindings.Bindings.ContainsKey( parameterInfo.ParameterType ) )
                    throw new ParameterBindingMissingException( string.Format( "There is no binding for the type:{0}", parameterInfo.ParameterType.FullName ) );
                
                boundParameters[ parameterInfo.Position ] = Bindings.Bindings[ parameterInfo.ParameterType ].Bind( callContext, parameterInfo );
            }

            return boundParameters;
        }

        protected virtual IExtension GetExtension( ICallContext callContext )
        {
            if( !LoadedExtensions.ContainsKey( callContext.PortalRequest.Extension ) )
                throw new ExtensionMissingException( "Extension is not one of the available extensions" );

            IExtensionLoader loader = LoadedExtensions[callContext.PortalRequest.Extension];

            return loader.CreateInstance();
        }

        protected virtual MethodInfo GetAction( ICallContext callContext, IExtension extension )
        {
            return extension.GetType().GetMethod( callContext.PortalRequest.Action );
        }

        #endregion
    }
}
