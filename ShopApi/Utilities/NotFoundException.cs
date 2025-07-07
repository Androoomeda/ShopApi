namespace ShopApi.Utilities;

public class NotFoundException(string message) : Exception(message)
{
}
