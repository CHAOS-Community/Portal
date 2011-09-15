namespace Geckon.Portal.Core.Extension
{
    public enum ReturnFormat
    {
        XML_SIMPLE, // XML + No HTTP Errorcodes
        XML_ERRORCODE,
        JSON_SIMPLE,
        JSONP_SIMPLE,
        UNKNOWN
    }
}
