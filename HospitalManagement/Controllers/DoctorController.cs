using HospitalManagement.Interfaces;
using HospitalManagement.Models.DTOs;
using HospitalManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpPost]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserResponse>> DoctorRegister(DoctorDTO doctorDTO)
        {
            var userResponse = await _doctorService.AddDcotor(doctorDTO);
            if (userResponse == null)
            {
                return BadRequest("Unable to register");
            }
            return Created("Home", userResponse);
        }
        [HttpPost]
        [ProducesResponseType(typeof(ActionResult<Doctor>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctor = await _doctorService.GetDcotor(id);
            if (doctor == null)
            {
                return NotFound("No doctor are available at the moment");
            }
            return Ok(doctor);
        }
        [HttpGet]
        [ProducesResponseType(typeof(ActionResult<ICollection<Doctor>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<Doctor>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllDcotors();
            if (doctors == null)
            {
                return NotFound("No doctors are available at the moment");
            }
            return Ok(doctors);
        }
        [HttpPut]
        [ProducesResponseType(typeof(ActionResult<Doctor>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Doctor>> Update(Doctor doctor)
        {
            var result = await _doctorService.UpdateDoctor(doctor);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Unable to update doctor details");
        }
        [HttpDelete]
        [ProducesResponseType(typeof(ActionResult<Doctor>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Doctor>> Delete(int id)
        {
            var result = await _doctorService.DeleteDcotor(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Unable to Delete doctor details");
        }
        [HttpPut]
        [ProducesResponseType(typeof(ActionResult<Doctor>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Doctor>> ChangeDoctorStatus(ChangeStatus changeStatus)
        {
            var result = await _doctorService.ChangeDoctorStatus(changeStatus);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Unable to Delete doctor details");
        }
    }
}

