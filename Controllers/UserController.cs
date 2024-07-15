using AgendaApi.Data.Repository.Interfaces;
using AgendaApi.Entities;
using AgendaApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace AgendaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        // private readonly: vinculado al encapsulamiento
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            //inicialización de dependencias en el constructor
            // el controlador tiene así acceso a las implementaciones del repository y el mapper
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            try
            {                
                var users = _userRepository.GetAll();
                var usersDTO = _mapper.Map<IEnumerable<UserDto>>(users);
                return Ok(usersDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetOneById(int Id)
        {
            User? user = _userRepository.GetById(Id);

            if (user != null)
            {
                var userDTO = _mapper.Map<UserDto>(user);
                try
                {
                    return Ok(userDTO);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound(); 
            }

        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateUser(CreateAndUpdateUserDto dto)
        {
            try
            {
                _userRepository.Create(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Created("Created", dto);
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateUser(CreateAndUpdateUserDto dto)
        {
            try
            {
                var user = _mapper.Map<User>(dto);
                _userRepository.Update(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("{Id}")]
        public IActionResult DeleteUser(int Id)
        {
            try
            {
                // Obtener del claim, el rol del usuario logueado
                var userRoleClaim = User.Claims.FirstOrDefault(c => c.Type == "role");
                if (userRoleClaim == null)
                {
                    return Forbid();
                }

                    if (!int.TryParse(userRoleClaim.Value, out int userRole))
                    {
                        return Forbid();
                    }

                    // Verificar si el usuario logueado tiene o no el rol de administrador (rol 0)
                    if (userRole != 0)
                    {
                        return Forbid();
                    }


                    var user = _userRepository.GetById(Id);

                    if (user != null)
                    {
                        _userRepository.Archive(Id);
                        return NoContent();
                    }
                    else
                    {
                        return NotFound();
                    }                
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("archive/{Id}")]
        public IActionResult ArchiveUser(int Id)
        {
            try
            {
                _userRepository.Archive(Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
