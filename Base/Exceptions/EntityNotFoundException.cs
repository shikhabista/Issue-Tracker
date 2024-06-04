namespace Base.Exceptions;

public class EntityNotFoundException(string entityName, string msg = null) : Exception(GetMessage(entityName, msg))
{
    private static string GetMessage(string entityName, string msg = null)
    {
        if (msg != null) return msg;
        return entityName + " not found";
    }
}