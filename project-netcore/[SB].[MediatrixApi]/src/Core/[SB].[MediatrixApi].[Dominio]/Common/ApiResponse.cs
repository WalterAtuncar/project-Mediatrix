namespace _SB_._MediatrixApi_._Dominio_.Common
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }
        public bool IsSuccess { get; set; }

        public static ApiResponse<T> Success(T data)
        {
            return new ApiResponse<T>
            {
                StatusCode = 200,
                Data = data,
                Error = null,
                IsSuccess = true
            };
        }

        public static ApiResponse<T> Failed(string error, int statusCode = 500)
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                Data = default,
                Error = error,
                IsSuccess = false
            };
        }
    }
} 