using AutoMapper;
using Chat_Business.Repository.IRepository;
using Chat_DataAccess;
using Chat_DataAccess.Data;
using Chat_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Business.Repository
{
    public class UserChatNameRepository : IUserChatNameRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UserChatNameRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UserChatNameDTO> CreateChatName(UserChatNameDTO nameDTO)
        {
            var name = _mapper.Map<UserChatNameDTO, UserChatName>(nameDTO);
            _db.UserChatNames.Add(name);
            await _db.SaveChangesAsync();
            return _mapper.Map<UserChatName, UserChatNameDTO>(name);
        }

        public async Task<int> DeleteChatName(int id)
        {
            var chatName = _db.UserChatNames.FirstOrDefault(x => x.Id == id);
            if (chatName != null)
            {
                _db.UserChatNames.Remove(chatName);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<UserChatNameDTO>> GetChatNames(string userId)
        {
            return _mapper.Map<IEnumerable<UserChatName>, IEnumerable<UserChatNameDTO>>(_db.UserChatNames.Where(x => x.UserId.Equals(userId)));
        }

        public async Task<UserChatNameDTO> UpdateChatName(UserChatNameDTO nameDTO)
        {
            var name_db = _db.UserChatNames.FirstOrDefault(x => x.Id == nameDTO.Id);
            if (name_db != null)
            {
                name_db.Name = nameDTO.Name;
                _db.UserChatNames.Update(name_db);
                await _db.SaveChangesAsync();
                return _mapper.Map<UserChatName, UserChatNameDTO>(name_db);
            }
            return nameDTO;
        }
    }
}
