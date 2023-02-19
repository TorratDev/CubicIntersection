namespace CubicIntersection.Application;

public interface IResponseCache
{
    bool TryGetValue<T>(int key, out T item);
    void Set<T>(int key, T item);
}