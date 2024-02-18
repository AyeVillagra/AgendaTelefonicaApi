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
            return Ok(_contactRepository.GetContactById(Id));
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
        public IActionResult UpdateContact(CreateAndUpdateContactDto dto)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
            _contactRepository.Update(dto, userId);
            return NoContent();
        }

        [HttpDelete]
        [Route("{Id}")]
        public IActionResult DeleteContactById(int Id)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
            _contactRepository.Delete(Id);
            return Ok();
        }
    }
}
