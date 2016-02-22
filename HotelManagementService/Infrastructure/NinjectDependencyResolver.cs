using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelManagementService.Models.EF;
using HotelManagementService.Models.Interfaces;
using HotelManagementService.Models.Service;
using Ninject;

namespace HotelManagementService.Infrastructure
{
  public class NinjectDependencyResolver : IDependencyResolver
  {
    private readonly IKernel _kernel;

    public NinjectDependencyResolver()
    {
      _kernel = new StandardKernel();
      AddBindings();
    }

    private void AddBindings()
    {
      _kernel.Bind<IDataModelEF>().To<DataModelEF>();
      _kernel.Bind<IClientsService>().To<ClientsService>();
      _kernel.Bind<IEmployeesService>().To<EmployeesSerivce>();
    }
    public object GetService(Type serviceType)
    {
      return _kernel.TryGet(serviceType);
    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
      return _kernel.GetAll(serviceType);
    }
  }
}