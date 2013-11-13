namespace Chaos.Portal.Core.Extension
{
    using System.Collections.Generic;
    using System.Reflection;

    using Cache;
    using Data;
    using Exceptions;
    using Indexing.View;
    using Logging;
    using Request;
    using Response;

    public abstract class AExtension : IExtension
    {
        #region Fields



        public IPortalRequest Request { get; private set; }

        #endregion
        #region Initialization

        protected AExtension(IPortalApplication portalApplication)
        {
            PortalApplication = portalApplication;
            MethodInfos       = new Dictionary<string, MethodInfo>();
        }

        public IExtension WithPortalResponse(IPortalResponse response)
        {
            Response = response;

            return this;
        }

        public IExtension WithPortalRequest(IPortalRequest request)
        {
            Request = request;

            return this;
        }

        #endregion
        #region Properties

        protected ILog Log { get; private set; }
        protected ICache Cache { get { return PortalApplication.Cache; } }
        protected IViewManager ViewManager { get { return PortalApplication.ViewManager; } }
        protected IPortalResponse Response { get; set; }
        protected IPortalRepository PortalRepository { get { return PortalApplication.PortalRepository; } }
        protected IDictionary<string, MethodInfo> MethodInfos { get; set; }

        public IPortalApplication PortalApplication { get; set; }

        /// <summary>
        /// Returns the current user, the user is cached and will not be updated during the callContexts life.
        /// </summary>
        

        #endregion
        #region Business Logic

        /// <summary>
        /// Calls an action on the extension with the parameters based on the settings specified in the inputParameters
        /// </summary>
        public virtual IPortalResponse CallAction(IPortalRequest request)
        {
            Request = request;

            if(!MethodInfos.ContainsKey(Request.Action))
                MethodInfos.Add(Request.Action, GetType().GetMethod(Request.Action));

            var method = MethodInfos[Request.Action];

            if (method == null) throw new ActionMissingException(string.Format("No action ({0}) in extenion ({1})", Request.Extension, Request.Action));

            var parameters = BindParameters(Request.Parameters, method.GetParameters());
            
            try
            {
                var result = method.Invoke(this, parameters);

                WriteToOutput(result);
            }
            catch (TargetInvocationException e)
            {
                WriteToOutput(e.InnerException);
                
                PortalApplication.Log.Fatal("ProcessRequest() - Unhandeled exception occured during", e.InnerException);
            }

            return Response;
        }

        protected virtual void WriteToOutput(object result)
        {
            Response.WriteToOutput(result);
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
