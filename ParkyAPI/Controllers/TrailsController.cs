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
    [Route("api/Trails")]
    [ApiController] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public class TrailsController : ControllerBase
    {
        private ITrailRepository _trailRepo;
        private readonly IMapper _mapper;

        public TrailsController(ITrailRepository trailRepo, IMapper mapper)
        {
            _trailRepo = trailRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of trails
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(TrailDto))]
        [ProducesResponseType(400)]
        public IActionResult GetTrails()
        {
            var objList = _trailRepo.GetTrail();
            var objDto = new List<TrailDto>();


            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<TrailDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// get individual trail
        /// </summary>
        /// <param name="TrailId"> Id of the trail</param>
        /// <returns></returns>
        [HttpGet("{TrailId:int}," , Name = "GetTrail")]
        [ProducesResponseType(200, Type = typeof(TrailDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrail(int TrailId)
        {
            var obj = _trailRepo.GetTrail(TrailId);
            if(obj == null) 
            {
                return NotFound();
            }
            var objDto = _mapper.Map<TrailDto>(obj);
            return Ok(objDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TrailDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreateTrail([FromBody] TrailUpsertDto TrailDto)
        {
            if(TrailDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_trailRepo.trailExists(TrailDto.Name))
            {
                ModelState.AddModelError(" ", "Trail Exists");
                return StatusCode(404, ModelState);
            }

            var objDto = _mapper.Map<TrailDto>(TrailDto);

            if (!_trailRepo.CreateTrail(objDto))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500);
            }

            return CreatedAtRoute("GetTrail", new { nationalParkId = objDto.Id }, objDto);

        }

        [HttpPatch("{TrailId:int},", Name = "UpdateTrail")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrail(int TrailId, [FromBody] TrailUpsertDto trailDto)
        {
            if (trailDto == null || TrailId != trailDto.Id)
            {
                return BadRequest(ModelState);
            }

            var objDto = _mapper.Map<TrailDto>(trailDto);

            if (!_trailRepo.UpdateTrail(objDto))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{TrailId:int},", Name = "DeleteTrail")]
        [ProducesResponseType(201, Type = typeof(TrailDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult DeletTrail(int TrailId)
        {
            if (!_trailRepo.TrailExists(TrailId))
            {
                return NotFound();
            }

            var obj = _trailRepo.GetTrail(TrailId);
            if (!_trailRepo.Deletetrail(obj))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }




    }
}
