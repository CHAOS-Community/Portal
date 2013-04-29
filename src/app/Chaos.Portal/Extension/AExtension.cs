using System.Collections.Generic;
using System.Reflection;
using Chaos.Portal.Core.Data;
using Chaos.Portal.Response;

namespace Chaos.Portal.Extension
{
    using Chaos.Portal.Core.Exceptions;

    public abstract class AExtension : IExtension
    {
        #region Properties

        protected IPortalResponse Response { get; set; }
        protected IPortalRepository  PortalRepository { get { return PortalApplication.PortalRepository; } }
        protected IDictionary<string, MethodInfo> MethodInfos { get; set; }

        public IPortalApplication PortalApplication { get;set; }

        #endregion
        #region Initialization

        protected AExtension()
        {
            MethodInfos = new Dictionary<string, MethodInfo>();
        }

        public abstract IExtension WithConfiguration(string configuration);

        public IExtension WithPortalApplication(IPortalApplication portalApplication)
        {
            PortalApplication = portalApplication;
            
            return this;
        }

        public IExtension WithPortalResponse(IPortalResponse response)
        {
            Response = response;

            return this;
        }

        #endregion
        #region Business Logic

        /// <summary>
        /// Calls an action on the extension with the parameters based on the settings specified in the inputParameters
        /// </summary>
        /// <param name="callContext"></param>
        public virtual IPortalResponse CallAction(ICallContext callContext)
        {
            if(!MethodInfos.ContainsKey(callContext.Request.Action))
                MethodInfos.Add(callContext.Request.Action, GetType().GetMethod(callContext.Request.Action));

            var method     = MethodInfos[callContext.Request.Action];
            var parameters = BindParameters(callContext.Request.Parameters, method.GetParameters());
            
            try
            {
                var result = method.Invoke(this, parameters);

                Response.WriteToResponse(result);
            }
            catch (TargetInvocationException e)
            {
                Response.Error.SetException(e.InnerException);
                
                PortalApplication.Log.Fatal("ProcessRequest() - Unhandeled exception occured during", e.InnerException);
            }

            return Response;
        }

        private object[] BindParameters(IDictionary<string, string> inputParameters, ICollection<ParameterInfo> parameters)
        {
            var boundParameters = new object[parameters.Count];

            foreach (var parameterInfo in parameters)
            {
                if (!PortalApplication.Bindings.ContainsKey(parameterInfo.ParameterType))
                    throw new ParameterBindingMissingException(string.Format("There is no binding for the type:{0}", parameterInfo.ParameterType.FullName));

                boundParameters[parameterInfo.Position] = PortalApplication.Bindings[parameterInfo.ParameterType].Bind(inputParameters, parameterInfo);
            }

            return boundParameters;
        }

        #endregion
    }
}
