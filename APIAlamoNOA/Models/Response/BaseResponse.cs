namespace APIAlamoNOA.Models.Response
{
    public class BaseResponse<T>
    {
        public T response { get; set; }

        public BaseResponse(T response)
        {
            this.response = response;
        }


    }
}
