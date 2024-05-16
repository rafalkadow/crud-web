namespace Domain.Modules.Communication.Generics
{
    public class ServiceResponse<T> : BaseResponse
    {
        public T Data { get; protected set; }

        public ServiceResponse()
        {
            Success = true;
        }

        public ServiceResponse(T result) : this()
        {
            Data = result;
        }
    }
}