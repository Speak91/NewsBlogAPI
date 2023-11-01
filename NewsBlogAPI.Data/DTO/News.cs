using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsBlogAPI.Data
{
    [Table("News")]
    public class News
    {
        /// <summary>
        /// Айди новости 
        /// </summary>
        [Required()]
        public Guid Id { get; set; }

        /// <summary>
        /// Название новости
        /// </summary>
        [Column(TypeName = "nvarchar(255)")]
        public string Title { get; set; }

        /// <summary>
        /// Описание новости
        /// </summary>
        [Column(TypeName = "nvarchar(500)")]
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