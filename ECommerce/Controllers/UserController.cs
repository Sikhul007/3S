using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Only admins can access these endpoints
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET: api/user/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(userDtos);
        }

        // GET: api/user/getbyid/{id}
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(_mapper.Map<UserDto>(user));
        }

        // POST: api/user/createuser
        [HttpPost("createuser")]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            // Check if email already exists
            var existing = await _userService.GetByEmailAsync(dto.Email);
            if (existing != null)
                return BadRequest("Email is already registered.");

            // Hash password
            using var sha = SHA256.Create();
            var passwordHash = Convert.ToBase64String(
                sha.ComputeHash(Encoding.UTF8.GetBytes(dto.Password))
            );

            // Map DTO → User entity
            var user = _mapper.Map<User>(dto);
            user.PasswordHash = passwordHash;

            var createdUser = await _userService.CreateAsync(user);

            var userDto = _mapper.Map<UserDto>(createdUser);
            return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
        }

        // PUT: api/user/update/{id}
        [HttpPatch("update/{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserDto dto)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.Username))
                user.Username = dto.Username;

            if (!string.IsNullOrEmpty(dto.Email))
                user.Email = dto.Email;

            if (!string.IsNullOrEmpty(dto.Role))
                user.Role = dto.Role;

            var updatedUser = await _userService.UpdateAsync(user);
            return Ok(_mapper.Map<UserDto>(updatedUser));
        }


        // DELETE: api/user/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
