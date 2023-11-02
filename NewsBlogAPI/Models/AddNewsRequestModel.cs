namespace NewsBlogAPI.Models
{
    public class AddNewsRequestModel
    {
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
    }
}
