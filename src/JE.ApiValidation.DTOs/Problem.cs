namespace JE.ApiValidation.DTOs
{
    public class Problem
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public object AttemptedValue { get; set; }

        public object CustomState { get; set; }
    }
}