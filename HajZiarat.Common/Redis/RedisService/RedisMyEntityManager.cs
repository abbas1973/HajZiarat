using Redis.DTOs;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Services.RedisService
{
    /// <summary>
    /// ذخیره انتیتی ها در ردیس
    /// </summary>
    public static class RedisMyEntityManager
    {
        /// <summary>
        /// کلید پیشفرض 
        /// </summary>
        public static readonly string Key = "MyEntities";

        /// <summary>
        /// مدت زمان ماندگاری در ردیس
        /// </summary>
        //public static readonly int ExpMin = 1000000;


        #region گرفتن انتیتی از ردیس
        /// <summary>
        /// گرفتن انتیتی ها از ردیس به تعداد دلخواه
        /// </summary>
        /// <param name="db">دیتابیس ردیس</param>
        /// <param name="count">تعداد آیتم درخواستی</param>
        public static async Task<IEnumerable<MyEntityRedisDTO>> GetEntities(this IRedisDatabase db, long count = int.MaxValue)
        {
            try
            {
                var model = await db.SetPopAsync<MyEntityRedisDTO>(Key, count: count);
                return model ?? new List<MyEntityRedisDTO>();
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        #endregion



        #region افزودن پیامک به ردیس
        /// <summary>
        /// افزودن پیامک به ردیس
        /// </summary>
        /// <param name="db">دیتابیس ردیس</param>
        /// <returns></returns>
        public static async Task<bool> SetEntity(this IRedisDatabase db, MyEntityRedisDTO entity)
        {
            try
            {
                return await db.SetAddAsync(Key, entity);
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// افزودن پیامک ها به ردیس
        /// </summary>
        /// <param name="db">دیتابیس ردیس</param>
        /// <param name="sms">لیست پیامک ها</param>
        /// <returns></returns>
        public static async Task<long> SetEntities(this IRedisDatabase db, List<MyEntityRedisDTO> entities)
        {
            try
            {
                var count = await db.SetAddAllAsync<MyEntityRedisDTO>(Key, items: entities.ToArray());
                return count;
            }
            catch
            {
                return -1;
            }
        }
        #endregion



    }
}
