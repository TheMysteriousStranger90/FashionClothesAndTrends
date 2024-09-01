using FashionClothesAndTrends.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionClothesAndTrends.WebAPI.Controllers;

[Authorize]
public class AdminController : BaseApiController
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("users-with-roles")]
    public async Task<ActionResult> GetUsersWithRoles()
    {
        try
        {
            var users = await _adminService.GetUsersWithRoles();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("roles")]
    public async Task<ActionResult> GetRoles()
    {
        try
        {
            var roles = await _adminService.GetRoles();
            return Ok(roles);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("add-role")]
    public async Task<ActionResult> AddRole(string roleName)
    {
        try
        {
            var result = await _adminService.AddRole(roleName);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("edit-roles/{username}")]
    public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
    {
        try
        {
            var result = await _adminService.EditRoles(username, roles);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpDelete("delete-role")]
    public async Task<ActionResult> DeleteRole(string roleName)
    {
        try
        {
            var result = await _adminService.DeleteRole(roleName);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}