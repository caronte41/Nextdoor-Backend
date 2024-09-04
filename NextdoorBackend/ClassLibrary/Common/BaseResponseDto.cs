namespace NextDoorBackend.ClassLibrary.Common
{
    public class BaseResponseDto<T>
    {
        public T data { get; set; }
        public bool result { get; set; }
        public string exceptionDetail { get; set; }
        public static BaseResponseDto<T> Success(T data)
        {
            return new BaseResponseDto<T> { data = data, result = true };
        }
        public static BaseResponseDto<T> Success()
        {
            return new BaseResponseDto<T> { result = true };
        }
        public static BaseResponseDto<T> Fail(string error)
        {
            return new BaseResponseDto<T> { result = false, exceptionDetail = error };
        }
       
    }
}
