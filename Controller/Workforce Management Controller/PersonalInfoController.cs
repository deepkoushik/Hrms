using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Providers; // Replace with your namespace
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/personalinfo")]
[ApiController]
public class PersonalInfoController : ControllerBase
{
    private readonly IPersonalInfoProvider _personalInfoProvider;

    public PersonalInfoController(IPersonalInfoProvider personalInfoProvider)
    {
        _personalInfoProvider = personalInfoProvider;
    }

    [HttpGet]
    public async Task<ActionResult<List<PersonalInfo>>> GetAllPersonalInfoAsync()
    {
        try
        {
            var personalInfoList = await _personalInfoProvider.GetAllPersonalInfoAsync();
            return Ok(personalInfoList);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonalInfo>> GetPersonalInfoByIdAsync(int id)
    {
        try
        {
            var personalInfo = await _personalInfoProvider.GetPersonalInfoByIdAsync(id);

            if (personalInfo == null)
            {
                return NotFound($"PersonalInfo with ID {id} not found.");
            }

            return Ok(personalInfo);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> AddPersonalInfoAsync([FromBody] PersonalInfo personalInfo)
    {
        try
        {
            await _personalInfoProvider.AddPersonalInfoAsync(personalInfo);
            return CreatedAtAction(nameof(GetPersonalInfoByIdAsync), new { id = personalInfo.Id }, personalInfo);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePersonalInfoAsync(int id, [FromBody] PersonalInfo personalInfo)
    {
        try
        {
            var existingPersonalInfo = await _personalInfoProvider.GetPersonalInfoByIdAsync(id);

            if (existingPersonalInfo == null)
            {
                return NotFound($"PersonalInfo with ID {id} not found.");
            }

            personalInfo.Id = id; // Ensure the ID is set to the correct value
            await _personalInfoProvider.UpdatePersonalInfoAsync(personalInfo);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePersonalInfoAsync(int id)
    {
        try
        {
            var existingPersonalInfo = await _personalInfoProvider.GetPersonalInfoByIdAsync(id);

            if (existingPersonalInfo == null)
            {
                return NotFound($"PersonalInfo with ID {id} not found.");
            }

            await _personalInfoProvider.DeletePersonalInfoAsync(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
