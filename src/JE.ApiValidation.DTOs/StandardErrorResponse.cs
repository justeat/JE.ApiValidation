using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JE.ApiValidation.DTOs
{
    [DataContract]
    public abstract class StandardErrorResponse
    {
        protected StandardErrorResponse(int code = (int)ErrorCodes.Unknown, string message = "Unknown error")
        {
            Code = code;
            Message = message;
            Errors = new Dictionary<string, IList<Problem>>();
        }

        [DataMember]
        public int Code { get; private set; }

        [DataMember]
        public string Message { get; private set; }

        [DataMember]
        public IDictionary<string, IList<Problem>> Errors { get; private set; }
    }
}
