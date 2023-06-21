using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/Gender")]
    public class GenderController : ControllerBase
    {
        private readonly GenderService _genderService;

        public GenderController(GenderService genderService)
        {
            _genderService = genderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Gender>>> GetAllGender()
        {
            var gender = await _genderService.GetGenderAsync();
            return Ok(gender);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Gender>> GetGenderById(int id)
        {
            var gender = await _genderService.GetGenderByIdAsync(id);
            if (gender == null)
                return NotFound();

            return Ok(gender);
        }

        [HttpPost]
        public async Task<ActionResult<Gender>> CreateGender(Gender gender)
        {
            await _genderService.AddGenderAsync(gender);
            return CreatedAtAction(nameof(GetGenderById), new { id = gender.GenderId }, gender);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Gender>> UpdateGender(int id, Gender gender)
        {
            if (id != gender.GenderId)
                return BadRequest();

            await _genderService.UpdateGenderAsync(gender);
            return NoContent();
        }
        ///
        ///////////////////////////////////////////////////
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGender(int id)
        {
            var gender = await _genderService.GetGenderByIdAsync(id);
            if (gender == null)
                return NotFound();

            await _genderService.DeleteGenderAsync(gender);
            return NoContent();
        }
    }
}
