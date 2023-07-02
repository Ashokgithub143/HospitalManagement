using HospitalManagement.Interfaces;
using HospitalManagement.Models.DTOs;
using HospitalManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        [HttpPost]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserResponse>> PatinetRegister(PatientDTO patinentDTO)
        {
            var userResponse = await _patientService.AddPatient(patinentDTO);
            if (userResponse == null)
            {
                return BadRequest("Unable to register");
            }
            return Created("Home", userResponse);
        }
        [HttpPost]
        [ProducesResponseType(typeof(ActionResult<Patient>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _patientService.GetPatient(id);
            if (patient == null)
            {
                return NotFound("No patient are available at the moment");
            }
            return Ok(patient);
        }
        [HttpGet]
        [ProducesResponseType(typeof(ActionResult<ICollection<Patient>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<Patient>>> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatients();
            if (patients == null)
            {
                return NotFound("No patients are available at the moment");
            }
            return Ok(patients);
        }
        [HttpPut]
        [ProducesResponseType(typeof(ActionResult<Patient>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Patient>> Update(Patient patient)
        {
            var result = await _patientService.UpdatePatient(patient);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Unable to update patient details");
        }
        [HttpDelete]
        [ProducesResponseType(typeof(ActionResult<Patient>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Patient>> Delete(int id)
        {
            var result = await _patientService.DeletePatient(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Unable to Delete patient details");
        }
    }
}
