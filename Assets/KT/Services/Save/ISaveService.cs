namespace KT
{
    public interface ISaveService
    {
        T Get<T>(string key);
        void Set<T>(string key, T obj);

        LiteDB.ILiteDatabase GetDatabase();
    }
}