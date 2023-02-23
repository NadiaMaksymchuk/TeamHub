using CollectionsAndLinq.BL.Exeptions;
using CollectionsAndLinq.WebAPI.Enums;
using System.Net;

namespace CollectionsAndLinq.WebAPI.Extensions
{
    public static class ExceptionFilterExtensions
    {
        public static (HttpStatusCode statusCode, ErrorCode errorCode) ParseException(this Exception exception)
        {
            return exception switch
            {
                NotFoundException _ => (HttpStatusCode.NotFound, ErrorCode.NotFound),
                InvalidDataException _=> (HttpStatusCode.BadRequest, ErrorCode.InvalidData),
                _ => (HttpStatusCode.InternalServerError, ErrorCode.General),
            };
        }
    }
}
