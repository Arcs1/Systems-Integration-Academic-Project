

namespace Api.Models.Enum
{
    public enum Response
    {
        Ok = 200, 
        BadRequest = 400,
        BadRequest_ContentEmpty = 401,
        BadRequest_ContentAlreadyExists = 402,
        InternalServerError = 500,
        BadRequest_NoSubscriptions = 403,
    }
}