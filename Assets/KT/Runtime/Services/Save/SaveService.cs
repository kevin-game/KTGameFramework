using LiteDB;

namespace KT
{
    public class SaveService : ISaveService
    {
        private static readonly string _defaultKvCollectionName = "__KTGameFramework$SaveService$kv__";
        private IConfigService _configService;

        public SaveService(IConfigService configService)
        {
            _configService = configService;
        }
        
        public T Get<T>(string key)
        {
            using var db = GetDatabase();
            var collection = db.GetCollection<KvRecord>(_defaultKvCollectionName);
            var record = collection.FindOne(x => x.Key == key);
            if (record is null)
                return default;
            
            return (T)record.Value;
        }

        public void Set<T>(string key, T obj)
        {
            using var db = GetDatabase();
            var collection = db.GetCollection<KvRecord>(_defaultKvCollectionName);
            var record = collection.FindOne(x => x.Key == key);
            if (record is not null)
            {
                record.Value = obj;
                collection.Update(record);
            }
            else
            {
                collection.Insert(new KvRecord()
                {
                    Key = key,
                    Value = obj
                });
            }
        }

        /// <summary>
        /// need to dispose, using statement is recomment 
        /// </summary>
        /// <returns></returns>
        public ILiteDatabase GetDatabase()
        {
            return new LiteDatabase(_configService.GameApplicationConfig.dbFileName);
        }
    }
}