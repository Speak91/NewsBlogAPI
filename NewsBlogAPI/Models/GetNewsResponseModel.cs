namespace NewsBlogAPI.Models
{
    public class GetNewsResponseModel
    {
        /// <summary>
        /// Айди новости 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название новости
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание новости
        /// </summary>
        public string Desctiption { get; set; }

        /// <summary>
        /// Новость
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Дата публикации
        /// </summary>
        public DateTime Created_at { get; set; }
    }
}
