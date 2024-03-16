using AgendaApi.Data.Repository.Interfaces;
using AgendaApi.Entities;
using AgendaApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AgendaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUserRepository _userRepository;

        public ContactController(IContactRepository contactRepository, IUserRepository userRepository)
        {
            _contactRepository = contactRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            return Ok(_contactRepository.GetAllByUser(userId));
            
            
        }

        [HttpGet]
        [Route("{Id}")]        
        public IActionResult GetOne(int Id)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            return Ok(_contactRepository.GetContactById(Id, userId)); // Pasar userId al método GetContactById
        }


        [HttpPost]
        public IActionResult CreateContact(CreateAndUpdateContactDto createContactDto)
        {
            try
            {
                int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
                Contact c = _contactRepository.Create(createContactDto, userId);
                return Created("Created", c);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut]
        [Route("{Id}")]
        public IActionResult UpdateContact(CreateAndUpdateContactDto dto, int Id)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
            if (_contactRepository.ContactBelongsToUser(Id, userId))
            {
                _contactRepository.Update(dto, userId);

                // Después de actualizar el contacto, obtener el contacto actualizado de la base de datos
                Contact updatedContact = _contactRepository.GetContactById(Id, userId);
                
                if (updatedContact != null)
                {
                    // Devolver el contacto actualizado como parte de la respuesta
                    return Ok(updatedContact);
                }
                else
                {                    
                    return NotFound("Updated contact not found");
                }
            }
            else
            {
                return BadRequest("Contact does not belong to the current user");
            }
        }

        [HttpDelete]
        [Route("{Id}")]
        public IActionResult DeleteContactById(int Id)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
            // Verifica si el contacto pertenece al usuario antes de eliminar
            if (_contactRepository.ContactBelongsToUser(Id, userId))
            {
                _contactRepository.Delete(Id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
