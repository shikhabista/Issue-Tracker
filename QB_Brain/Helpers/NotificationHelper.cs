using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace IT_Web.Helpers;

public interface INotificationHelper
{
    void SetSuccessMsg(string msg);
    void SetErrorMsg(string msg);
    void SetWarningMsg(string msg);
}

public class NotificationHelper : INotificationHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private HttpContext _httpContext;
    private ITempDataDictionaryFactory _factory;
    private ITempDataDictionary _tempData;
    

    public NotificationHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _httpContext = _httpContextAccessor.HttpContext;
        _factory =
            _httpContext.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as
                ITempDataDictionaryFactory;
        _tempData = _factory.GetTempData(_httpContext);
    }

    public void SetSuccessMsg(string msg)
    {
        _tempData["Msg"] = msg;
        _tempData["MsgType"] = "Success";
    }
    
    public void SetErrorMsg(string msg)
    {
        _tempData["Msg"] = msg;
        _tempData["MsgType"] = "Error";
    }
    
    public void SetWarningMsg(string msg)
    {
        _tempData["Msg"] = msg;
        _tempData["MsgType"] = "Warning";
    }
}