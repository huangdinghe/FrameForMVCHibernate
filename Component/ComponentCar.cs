using com.fxm.MVCHibernate.Domain;
using com.fxm.MVCHibernate.Manager;
using com.fxm.MVCHibernate.Service;

namespace com.fxm.MVCHibernate.Component
{
    public class ComponentCar : BaseComponent<Car, ManagerCar>, IServiceCar
    {
    }
}
