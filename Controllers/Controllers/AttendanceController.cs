using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Providers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Kryptos.Hrms.API.Controllers;
//{
    [ApiController]
[Route("api/[controller]")]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceProvider _attendanceProvider;

    public AttendanceController(IAttendanceProvider attendanceProvider)
    {
        _attendanceProvider = attendanceProvider;
    }

    [HttpPost("checkin/{employeeId}")]
    public IActionResult CheckIn(int employeeId)
    {
        try
        {
            var attendance = _attendanceProvider.CheckIn(employeeId);
            return Ok(attendance);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("checkout/{employeeId}")]
    public IActionResult CheckOut(int employeeId)
    {
        try
        {
            var attendance = _attendanceProvider.CheckOut(employeeId);
            return Ok(attendance);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("attendanceDetails/{employeeId}")]
    public IActionResult GetAttendanceDetails(int employeeId)
    {
        try
        {
            var attendanceDetails = _attendanceProvider.GetAttendanceDetails(employeeId);
            return Ok(attendanceDetails);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

}

