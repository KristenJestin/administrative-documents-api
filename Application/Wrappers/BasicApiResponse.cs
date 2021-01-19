namespace Application.Wrappers
{
    public class BasicApiResponse<T>
    {
        public string Message { get; set; }
        public T Result { get; set; }


        public BasicApiResponse(T result = default)
        {
            Message = "Request successful.";
            Result = result;
        }

        public BasicApiResponse(T result = default, string message = "")
        {
            Message = message == string.Empty ? "Success" : message;
            Result = result;
        }
    }
}
