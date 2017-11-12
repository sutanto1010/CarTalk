namespace CarTalk
{
    public interface IPlatformUtils
    {
        void Save<T>(T item, string fileName);
        T Load<T>(string fileName);
        void Notify(string message, bool isLongDuration=false);
    }
}
