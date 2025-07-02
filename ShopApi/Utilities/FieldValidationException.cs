namespace ShopApi.Utilities;

public class FieldValidationException : Exception
{
  public string Field { get; }

  public FieldValidationException(string field, string message) : base(message)
  {
    Field = field;
  }
}
