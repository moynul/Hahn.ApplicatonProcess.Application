using Hahn.ApplicatonProcess.December2020.Data.Interface;
using Hahn.ApplicatonProcess.December2020.Domain.Entities;
using Hahn.ApplicatonProcess.December2020.Web.helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ApplicantController : Controller
    {
        private readonly ILogger<ApplicantController> _logger;
        private readonly IApplicant _applicantService;
        private static readonly HttpClient _httpclient = new HttpClient();

        public ApplicantController(ILogger<ApplicantController> logger, IApplicant applicantService)
        {
            _logger = logger;
            _applicantService = applicantService;
        }

        /// <summary>
        /// Get all Applicant.
        /// </summary>     
        /// <response code="200">success</response>
        /// <response code="404">If User Not found</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       // [Route("GetApplicant")]
        public async Task<ActionResult<Applicant>> GetApplicant()
        {
            try
            {
                _logger.LogInformation("GetApplicantById method fired. ");
                var applicant =  _applicantService.GetApplicant();
                if (applicant != null)
                {
                    return Ok(applicant);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "User Not found");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(" GetApplicantById Error details :" + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get a specific Applicant.
        /// </summary>
        /// <param name="id"></param>        
        /// <response code="200">success</response>
        /// <response code="404">If User Not found</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      //  [Route("GetApplicantById/{applicantId:int}")]
        public async Task<ActionResult<Applicant>> GetApplicantById(int id)
        {
            try
            {
                _logger.LogInformation("GetApplicantById method fired. ");
                var applicant =  _applicantService.GetApplicantById(id);
                if (applicant != null)
                {
                    return Ok(applicant);
                }
                else
                {
                    return NotFound();
                    // return StatusCode(StatusCodes.Status404NotFound, "User Not found");
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(" GetApplicantById Error details :" + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }


        /// <summary>
        /// Creates Applicant.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///{
        ///  "name": "Md Moynul",
        ///  "familyName": "Biswas",
        ///  "address": "House:16/a Banasree, Dhaka ",
        ///  "countryOfOrigin": "Bangladesh",
        ///  "emailAdress": "bappyist@gmail.com",
        ///  "age": 25,
        ///  "hired": true
        ///}
        ///
        /// </remarks>
        /// <param name="applicantobject"></param>
        /// <returns>A newly created Applicant</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       // [Route("CreateApplicant")]
        public async Task<ActionResult<Applicant>> CreateApplicant([FromBody] Applicant applicantobject)
        {
            try
            {
                _logger.LogInformation("CreateApplicant method fired datetime.");
                string result = await Country.GetCountryById(applicantobject.CountryOfOrigin);
                if (result == "OK")
                {
                    var createdApplicant = await _applicantService.AddApplicant(applicantobject);
                    if (createdApplicant != null)
                    {
                        return Ok(StatusCode(201));
                    }
                    else
                    {
                        return BadRequest(StatusCode(400));
                    }

                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "CountryOfOrigin – must be a valid Country");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(" CreateApplicant Error details :" + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }


        /// <summary>
        /// Update a specific Applicant.
        /// </summary>
        /// <param name="id"></param>        
        /// <param name="applicantobject"></param>   
        /// <response code="200">success</response>
        /// <response code="404">If User Not found</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       // [Route("UpdateApplicant/{applicantId:int}")]
        public async Task<ActionResult> UpdateApplicant(int id, Applicant applicantobject)
        {
            try
            {
                _logger.LogInformation("UpdateApplicant method fired datetime.");
                string result = await Country.GetCountryById(applicantobject.CountryOfOrigin);
                if (result == "OK")
                {
                    var existingApplicant = _applicantService.GetApplicantById(id);
                    if (existingApplicant != null)
                    {
                        await _applicantService.Update(id, applicantobject);
                        return Ok(StatusCode(201));
                    }
                    else
                    {
                        return BadRequest(StatusCode(400));
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "CountryOfOrigin – must be a valid Country");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(" UpdateApplicant Error details :" + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }



        /// <summary>
        /// Deletes a specific Applicant.
        /// </summary>
        /// <param name="id"></param>        
        /// <response code="200">success</response>
        /// <response code="404">If User Not found</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       // [Route("DeleteApplicant/{applicantId:int}")]
        public async Task<ActionResult> DeleteApplicant(int id)
        {
            try
            {
                _logger.LogInformation("DeleteApplicant method fired datetime.");
                var existingApplicant = _applicantService.GetApplicantById(id);
                if (existingApplicant != null)
                {
                    await _applicantService.DeleteApplicant(id);
                    return StatusCode(StatusCodes.Status200OK, "User deleted");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "User Not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(" DeleteApplicant Error details :" + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get all country.
        /// </summary> 

        [HttpGet("[action]")]
        [Route("GetCountry")]
        public async Task<List<countrydomain>> GetCountry()
        {
                _logger.LogInformation("AllCountry method fired datetime.");
                var response = await _httpclient.GetAsync("https://restcountries.eu/rest/v2/all");
                string result = response.Content.ReadAsStringAsync().Result;
                List<countrydomain> countries = JsonConvert.DeserializeObject<List<countrydomain>>(result);
                return countries;

        }
    }
}
