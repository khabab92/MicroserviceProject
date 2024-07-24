using WebApp.Utility;
using static WebApp.Utility.SD;

namespace WebApp.Models.Dto
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.Get;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken {  get; set; }
    }
}
