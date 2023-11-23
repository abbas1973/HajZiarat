namespace DTO.MyEntity
{
    /// <summary>
    /// مدل لازم برای لود داده از فایل اکسل
    /// </summary>
    public class MyEntityExcelDTO
    {
        public string Title { get; set; }


        /// <summary>
        /// تبدیل DTO به Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IEnumerable<Domain.Entities.MyEntity> ToEntity(List<MyEntityExcelDTO> model)
        {
            return model.Select(x => new Domain.Entities.MyEntity
            {
                Title = x.Title
            });
        }

    }
}
