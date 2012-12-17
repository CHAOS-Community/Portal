using System;
using System.Collections.Generic;
using CHAOS;
using CHAOS.Index;
using CHAOS.Portal.Exception;
using Chaos.Portal.Bindings;
using Chaos.Portal.Bindings.Standard;
using Chaos.Portal.Cache;
using Chaos.Portal.Data;
using Chaos.Portal.Data.Dto.Standard;
using Chaos.Portal.Extension;
using Chaos.Portal.Logging;
using Chaos.Portal.Request;
using Chaos.Portal.Response;

namespace Chaos.Portal.Standard
{
    public class PortalApplication : IPortalApplication
    {
        #region Properties

        public IDictionary<Type, IParameterBinding> Bindings { get; set; }
        public IDictionary<string, IExtension>      LoadedExtensions { get; set; }
        public ICache                               Cache { get; protected set; }
        public IIndexManager                        IndexManager { get; protected set; }
        public ILog                                 Log { get; protected set; }
        public IPortalRepository                    PortalRepository { get; set; }

        #endregion
        #region Constructors

        public PortalApplication( ICache cache, IIndexManager indexManager, IPortalRepository portalRepository, ILog log )
        {
            Bindings         = new Dictionary<Type, IParameterBinding>();
            LoadedExtensions = new Dictionary<string, IExtension>();
            Cache            = cache; 
            IndexManager     = indexManager;
            PortalRepository = portalRepository;
            Log              = log;
            
            // Load bindings
            Bindings.Add( typeof(string), new StringParameterBinding() );
            Bindings.Add( typeof(ICallContext), new CallContextParameterBinding() );
            Bindings.Add( typeof(CallContext), new CallContextParameterBinding() );
            Bindings.Add( typeof(long), new ConvertableParameterBinding<long>() );
            Bindings.Add( typeof(int), new ConvertableParameterBinding<int>() );
            Bindings.Add( typeof(short), new ConvertableParameterBinding<short>() );
            Bindings.Add( typeof(ulong), new ConvertableParameterBinding<ulong>() );
            Bindings.Add( typeof(uint), new ConvertableParameterBinding<uint>() );
            Bindings.Add( typeof(ushort), new ConvertableParameterBinding<ushort>() );
            Bindings.Add( typeof(double), new ConvertableParameterBinding<double>() );
            Bindings.Add( typeof(float), new ConvertableParameterBinding<float>() );
            Bindings.Add( typeof(bool), new ConvertableParameterBinding<bool>() );
            Bindings.Add( typeof(DateTime), new DateTimeParameterBinding());
            Bindings.Add( typeof(long?), new ConvertableParameterBinding<long>() );
            Bindings.Add( typeof(int?), new ConvertableParameterBinding<int>() );
            Bindings.Add( typeof(short?), new ConvertableParameterBinding<short>() );
            Bindings.Add( typeof(ulong?), new ConvertableParameterBinding<ulong>() );
            Bindings.Add( typeof(uint?), new ConvertableParameterBinding<uint>() );
            Bindings.Add( typeof(ushort?), new ConvertableParameterBinding<ushort>() );
            Bindings.Add( typeof(double?), new ConvertableParameterBinding<double>() );
            Bindings.Add( typeof(float?), new ConvertableParameterBinding<float>() );
            Bindings.Add( typeof(bool?), new ConvertableParameterBinding<bool>() );
            Bindings.Add( typeof(DateTime?), new DateTimeParameterBinding());
            Bindings.Add( typeof(UUID), new UUIDParameterBinding() );
            Bindings.Add( typeof(Guid), new UUIDParameterBinding() );
            Bindings.Add( typeof(IQuery), new QueryParameterBinding() );

            // load portal extensions
            LoadedExtensions.Add("ClientSettings", new Extension.Standard.ClientSettings().WithPortalApplication(this));
            LoadedExtensions.Add("Group", new Extension.Standard.Group().WithPortalApplication(this));
            LoadedExtensions.Add("Session", new Extension.Standard.Session().WithPortalApplication(this));
            LoadedExtensions.Add("Subscription", new Extension.Standard.Subscription().WithPortalApplication(this));
            LoadedExtensions.Add("User", new Extension.Standard.User().WithPortalApplication(this));
            LoadedExtensions.Add("UserSettings", new Extension.Standard.UserSettings().WithPortalApplication(this));
        }

        #endregion
        #region Business Logic

		/// <summary>
		/// Process a request to portal. Any underlying extensions or modules will be called based on the callContext parameter
		/// </summary>
        /// <param name="request">contains information about what extension and action to call</param>
        public IPortalResponse ProcessRequest( IPortalRequest request )
        {
            if (!LoadedExtensions.ContainsKey(request.Extension))
                throw new ExtensionMissingException(string.Format("Extension named '{0}' not found", request.Extension));

            var callContext = new CallContext(this, request, new PortalResponse(new PortalHeader(request.Stopwatch), new PortalResult(), new PortalError()));
            Log.Info("Processing Request");

			try
			{
			    LoadedExtensions[request.Extension].CallAction(callContext);
			}
			catch (Exception e)
			{
			    callContext.Response.Error.SetException(e);
			    Log.Fatal("ProcessRequest() - Unhandeled exception occured during", e);
			}
			finally
			{
                Log.Info("Done Processing Request");
			}

		    return callContext.Response;
        }

        #endregion
    }
}
