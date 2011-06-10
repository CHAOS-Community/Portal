namespace Geckon.Portal.Core.Standard
{
    public class EventType : IEventType
    {
        #region Properties

        public string Type { get; protected set; }
        public string EventName { get; protected set; }

        #endregion
        #region Construction

        public EventType( string type, string eventName )
        {
            Type      = type;
            EventName = eventName;
        }

        #endregion
    }
}
