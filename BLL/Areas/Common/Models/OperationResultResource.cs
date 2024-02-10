namespace NetCoreApiBoilerplate.BLL.Areas.Common.Models
{
    public class OperationResultResource
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string OperationName { get; set; }
        public Exception Exception { get; set; }
    }
}
