namespace TreeAutoSort.Exceptions;

public class NullOrEmptyInputException: Exception
{
    public NullOrEmptyInputException(string message) : base(message)
    {
    }
}