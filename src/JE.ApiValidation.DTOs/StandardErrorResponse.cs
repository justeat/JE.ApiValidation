using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JE.ApiValidation.DTOs
{
    [DataContract]
    public class StandardErrorResponse
    {
        public StandardErrorResponse()
        {
            Errors = new Dictionary<string, IList<Problem>>();
        }

        public StandardErrorResponse(int code = (int)ErrorCodes.Unknown, string message = "Unknown error") : this()
        {
            Code = code;
            Message = message;
        }

        [DataMember]
        public int Code { get; private set; }

        [DataMember]
        public string Message { get; private set; }

        [DataMember]
        public IDictionary<string, IList<Problem>> Errors { get; private set; }
    }
}
