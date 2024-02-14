namespace WebApi.Services.LoggerServices;

public interface IFileLoggerService
{
    void Log(string message, string endofline = "\n");
}