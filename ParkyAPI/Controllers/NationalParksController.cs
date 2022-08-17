using AutoMapper;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ParkyOpenAPISpecNP")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public class NationalParksController : ControllerBase
    {
        private INationalParkRepository _npRepo;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository npRepo, IMapper mapper)
        {
            _npRepo = npRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of national parks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(NationalParkDto))]
        [ProducesResponseType(400)]
        public IActionResult GetNationalParks()
        {
            var objList = _npRepo.GetNationalParks();
            var objDto = new List<NationalParkDto>();


            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<NationalParkDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// get individual national park
        /// </summary>
        /// <param name="NationalParkId"> Id of the national park</param>
        /// <returns></returns>
        [HttpGet("{NationalParkId:int}," , Name = "GetNationalPark")]
        [ProducesResponseType(200, Type = typeof(NationalParkDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetNationalPark(int NationalParkId)
        {
            var obj = _npRepo.GetNationalPark(NationalParkId);
            if(obj == null) 
            {
                return NotFound();
            }
            var objDto = _mapper.Map<NationalParkDto>(obj);
            return Ok(objDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(NationalParkDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if(nationalParkDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_npRepo.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError(" ", "National Park Exists");
                return StatusCode(404, ModelState);
            }

            var objDto = _mapper.Map<NationalParkDto>(nationalParkDto);

            if (!_npRepo.CreateNationalPark(objDto))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500);
            }

            return CreatedAtRoute("GetNationalPark", new { nationalParkId = objDto.Id }, objDto);

        }

        [HttpPatch("{NationalParkId:int},", Name = "UpdateNationalPark")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateNationalPark(int NationalParkId, [FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null || NationalParkId != nationalParkDto.Id)
            {
                return BadRequest(ModelState);
            }

            var objDto = _mapper.Map<NationalParkDto>(nationalParkDto);

            if (!_npRepo.UpdateNationalPark(objDto))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{NationalParkId:int},", Name = "DeleteNationalPark")]
        [ProducesResponseType(201, Type = typeof(NationalParkDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult DeletNationalPark(int NationalParkId)
        {
            if (!_npRepo.NationalParkExists(NationalParkId))
            {
                return NotFound();
            }

            var obj = _npRepo.GetNationalPark(NationalParkId);
            if (!_npRepo.DeleteNationalPark(obj))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }




    }
}
