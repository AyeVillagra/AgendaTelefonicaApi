using AgendaApi.Entities;
using AgendaApi.Data;
using AgendaApi.Data.Repository.Interfaces;
using AutoMapper;
using AgendaApi.Models.DTOs;
using AgendaApi.Models.Enum;

namespace AgendaApi.Data.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private AgendaContext _context;
        private readonly IMapper _mapper;
        public UserRepository(AgendaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper; 
        }


        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User? GetById(int userId)
        {
            return _context.Users.SingleOrDefault(u => u.Id == userId);
        }


        public void Create(CreateAndUpdateUserDto dto)
        {
            _context.Users.Add(_mapper.Map<User>(dto));
            _context.SaveChanges();
        }

        public User? ValidateUser(AuthenticationRequestBody authRequestBody)
        {
            return _context.Users.FirstOrDefault(p => p.UserName == authRequestBody.UserName && p.Password == authRequestBody.Password);
        }

        public void Delete(int id)
        {
            _context.Users.Remove(_context.Users.Single(u => u.Id == id));
            _context.SaveChanges();
        }

        public User Update(CreateAndUpdateUserDto dto)
        {
            //_context.Users.Update(_mapper.Map<User>(dto));
            User user = _mapper.Map<User>(dto);
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }

        public void Archive(int Id)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Id == Id);
            if (user != null)
            {
                user.state = State.Archived;
                _context.Update(user);
                _context.SaveChanges();
            }

        }

    }
}
