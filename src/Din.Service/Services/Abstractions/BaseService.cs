using Din.Service.Dto;

namespace Din.Service.Services.Abstractions
{
    public abstract class BaseService
    {
        public ResultDto GenerateResultDto(string title, string message, ResultDtoStatus status)
        {
            return status.Equals(ResultDtoStatus.Unsuccessful)
                ? new ResultDto
                {
                    Title = title,
                    Message = message,
                    TitleColor = "#b43232"
                }
                : new ResultDto
                {
                    Title = title,
                    Message = message,
                    TitleColor = "#00d77c"
                };
        }
    }
}
