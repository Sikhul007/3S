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
    [Authorize(Roles = "Admin")] 
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            var existing = await _userService.GetByEmailAsync(dto.Email);
            if (existing != null)
                return BadRequest("Email is already registered.");

            using var sha = SHA256.Create();
            var passwordHash = Convert.ToBase64String(
                sha.ComputeHash(Encoding.UTF8.GetBytes(dto.Password))
            );

            var user = _mapper.Map<User>(dto);
            user.PasswordHash = passwordHash;

            var createdUser = await _userService.CreateAsync(user);

            var userDto = _mapper.Map<UserDto>(createdUser);
            return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
        }

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
