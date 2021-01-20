using BaseLibrary.Attributes;
using BaseLibrary.Tool.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Sample.Sql.Entities.Enums;
using Sample.Sql.Entities.GenreEntity;
using Sample.Sql.Entities.LocalizationNameEntity;
using Sample.Sql.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResponse]
    public class SqlController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SqlController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            try
            {
                // var result = await _unitOfWork.Genres.GetAllAsync();

                var localizationName = new List<GenreLocalizationName>
                {
                     new GenreLocalizationName
                    {
                        LanguageType = LanguageType.English,
                        Title = "Pop"
                    },
                      new GenreLocalizationName
                    {
                        LanguageType = LanguageType.Persian,
                        Title = "پاپ"
                    }
                };

                var genre = new Genre
                {
                    DateOfRegistration = DateTime.UtcNow,
                    LocalizationNames = localizationName
                };

                await _unitOfWork.Genres.AddAsync(genre);
                await _unitOfWork.SaveChangesTransactionAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException(ex);
            }
        }
    }
}
