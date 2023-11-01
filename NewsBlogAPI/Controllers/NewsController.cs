using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewsBlogAPI.Data;
using NewsBlogAPI.Data.Repository.Interfaces;
using NewsBlogAPI.Data.Services;
using NewsBlogAPI.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewsBlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _newsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NewsController(INewsRepository newsRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить токен
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetToken")]
        [SwaggerOperation("GetToken")]
        public IActionResult GetToken()
        {
            var token = GenerateJWTToken();
            return Ok(new { Token = token });
        }

        /// <summary>
        /// Создать новость
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CreateNews")]
        [SwaggerOperation("CreateNews")]
        [Authorize]
        public async Task<IActionResult> CreateNewsAsync(
        [FromBody] AddNewsRequestModel request)
        {
            _newsRepository.Create(_mapper.Map<News>(request));
            await _unitOfWork.SaveChangesAsync();
            return new JsonResult(Ok("Запись добавлена"));

        }

        /// <summary>
        /// Получить новость по ИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetNewsById")]
        [SwaggerOperation("GetNewsById")]
        public async Task<IActionResult> GetNewsByIdAsync(
        [FromQuery] Guid id)
        {
            var news = await _newsRepository.GetByIdAsync(id);
            if (news != null)
            {
                return Ok(_mapper.Map<GetNewsResponseModel>(news));
            }
            return NotFound($"Новость с Id:{id} не найдена");
        }

        /// <summary>
        /// Получить список всех новостей
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllNews")]
        [SwaggerOperation("GetAllNews")]
        public async Task<IActionResult> GetAllNewsAsync()
        {
            var news = await _newsRepository.GetAllAsync();
            if (news != null)
            {
                return Ok(_mapper.Map<IList<GetNewsResponseModel>>(news));
            }
            return NoContent();
        }

        /// <summary>
        /// Удалить новость
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteNews")]
        [SwaggerOperation("DeleteNews")]
        [Authorize]
        public async Task<IActionResult> DeleteNewsAsync(
        [FromQuery] Guid id)
        {
            var news = await _newsRepository.GetByIdAsync(id);
            if (news != null)
            {
                _newsRepository.Delete(news);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Запись удалена");
            }
            return NotFound($"Новость с Id:{id} не найдена");
        }

        /// <summary>
        /// Получить новости за период
        /// </summary>
        /// <param name="startDate">Начало периода</param>
        /// <param name="endDate">Конец периода</param>
        /// <returns></returns>
        [HttpGet("GetNewsByPeriod")]
        [SwaggerOperation("GetNewsByPeriod")]
        public async Task<IActionResult> GetNewsByPeriodAsync(
        [FromQuery] DateTime startDate, DateTime endDate)
        {
            var news = await _newsRepository.GetByTimePeriodListAsync(startDate, endDate);
            if (news != null)
            {
                return Ok(news);
            }
            return NotFound($"Новости за период с {startDate} по {endDate} не найдена");
        }

        /// <summary>
        /// Обновить новость
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [SwaggerOperation("UpdateNews")]
        [Authorize]
        public async Task<IActionResult> UpdateNewsAsync(Guid id, [FromBody] UpdateNewsRequestModel request)
        {
            var news = await _newsRepository.GetByIdAsync(id);
            if (news != null)
            {
                var updateNews = _mapper.Map<UpdateNewsRequestModel, News>(request);
                _newsRepository.Update(updateNews);
                await _unitOfWork.SaveChangesAsync();
                return Ok($"Новость с Id:{id} успешно обновлена");
            }
            return NotFound($"Новость с Id:{id} не найдена");
        }

        /// <summary>
        /// Метод генерации токена
        /// </summary>
        /// <returns></returns>
        private string GenerateJWTToken()
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes("kYp3s6v9y/B?E(H+");

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{}),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}

