using Business.Exceptions;
using Business.Extension;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryConcrets;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrets
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IWebHostEnvironment _env;

        public ServiceService(IServiceRepository serviceRepository, IWebHostEnvironment env)
        {
            _serviceRepository = serviceRepository;
            _env = env;
        }

        public async Task AddServiceAsync(Service service)
        {
            if (service.ImageFile == null)
                throw new FileNullReferenceException("File bos ola bilmez!");

            service.ImageUrl = Helper.CreateFile(_env.WebRootPath, @"uploads\servicePic", service.ImageFile);

            await _serviceRepository.AddAsync(service);
            await _serviceRepository.CommitAsync();

        }

        public void DeleteService(int id)
        {
            var exist = _serviceRepository.Get(x=>x.Id == id);

            if (exist == null) throw new FileNullReferenceException("File tapilmadi!"); 

            Helper.DeleteFile(_env.WebRootPath, @"uploads\servicePic", exist.ImageUrl);

            _serviceRepository.Delete(exist);
            _serviceRepository.Commit();
        }

        public List<Service> GetAllService(Func<Service, bool>? func = null)
        {
            return _serviceRepository.GetAll(func);
        }

        public Service GetService(Func<Service, bool>? func = null)
        {
            return _serviceRepository.Get(func);
        }

        public void UpdateService(int id, Service service)
        {
            var old = _serviceRepository.Get(x=> x.Id == id);

            if (old == null) 
                throw new FileNullReferenceException("File tapilmadi");

            if (service.ImageFile != null)
            {
                Helper.DeleteFile(_env.WebRootPath, $@"uploads\servicePic", old.ImageUrl);
                old.ImageUrl = Helper.CreateFile(_env.WebRootPath, $@"uploads\servicePic", service.ImageFile);
            }
            old.Name = service.Name;
            old.Description = service.Description;

            _serviceRepository.Commit();
        }
    }
}
