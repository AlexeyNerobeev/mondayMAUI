using MauiApp4.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp4.Services
{
    public interface IApiService
    {
        Task<List<ContactDto>> GetContactsAsync(string search = null);
        Task<ContactDto> GetContactAsync(int id);
        Task<ContactDto> CreateContactAsync(CreateContactDto contact);
        Task UpdateContactAsync(int id, UpdateContactDto contact);
        Task DeleteContactAsync(int id);
    }
}
