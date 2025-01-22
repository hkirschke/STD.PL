namespace STD.PL.Exceptions;

public class DomainException : Exception
{
    public List<string> exceptionsMessages { get; set; }

    public void ThrowExceptionWithMessages(List<string> messages)
    {
        exceptionsMessages = messages;

        throw this;
    }
}