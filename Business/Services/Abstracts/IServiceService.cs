using Core.Models;
using Data.RepositoryConcrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface IServiceService
    {
        Task AddServiceAsync(Service service);
        void DeleteService(int id);
        void UpdateService(int id, Service service);
        Service GetService(Func<Service, bool>? func=null);
        List<Service> GetAllService(Func<Service, bool>? func=null);

    }
}
