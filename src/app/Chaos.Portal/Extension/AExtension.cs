﻿using System.Collections.Generic;
using System.Reflection;
using CHAOS.Portal.Exception;
using Chaos.Portal.Data;
using Chaos.Portal.Data.Dto.Standard;
using Chaos.Portal.Response;

namespace Chaos.Portal.Extension
{
    public abstract class AExtension : IExtension
    {
        #region Properties

        protected IPortalRepository  PortalRepository { get { return PortalApplication.PortalRepository; } }
        protected IPortalApplication PortalApplication { get;set; }

        #endregion
        #region Initialization

        public abstract IExtension WithConfiguration(string configuration);

        public IExtension WithPortalApplication(IPortalApplication portalApplication)
        {
            PortalApplication = portalApplication;

            return this;
        }

        #endregion
        #region Business Logic

        /// <summary>
        /// Calls an action on the extension with the parameters based on the settings specified in the callContext
        /// </summary>
        /// <param name="callContext"></param>
        public virtual IPortalResponse CallAction(ICallContext callContext)
        {
            // REVIEW: Caching Methods could improve performance when getting the method
            var method     = GetType().GetMethod(callContext.Request.Action);
            var parameters = BindParameters(callContext, method.GetParameters());
            
            try
            {
                var result = method.Invoke(this, parameters);

                callContext.Response.WriteToResponse(result);
            }
            catch (TargetInvocationException e)
            {
                callContext.Response.Error.SetException(e.InnerException);
                
                PortalApplication.Log.Fatal("ProcessRequest() - Unhandeled exception occured during", e.InnerException);
            }

            return callContext.Response;
        }

        private static object[] BindParameters(ICallContext callContext, ICollection<ParameterInfo> parameters)
        {
            var boundParameters = new object[parameters.Count];

            foreach (var parameterInfo in parameters)
            {
                if (!callContext.Application.Bindings.ContainsKey(parameterInfo.ParameterType))
                    throw new ParameterBindingMissingException(string.Format("There is no binding for the type:{0}", parameterInfo.ParameterType.FullName));

                boundParameters[parameterInfo.Position] = callContext.Application.Bindings[parameterInfo.ParameterType].Bind(callContext, parameterInfo);
            }

            return boundParameters;
        }

        #endregion
    }
}
