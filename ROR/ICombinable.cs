namespace ROR.Net;

public interface ICombinable<T>
{
    public T Combine(T other);
}