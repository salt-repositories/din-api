namespace Din.Service.Dto
{
    public class ResultDto
    {
        public string Title { get; set; }
        public string TitleColor { get; set; }
        public string Message { get; set; }
    }

    public enum ResultDtoStatus
    {
        Unsuccessful,
        Successful
    }
}
