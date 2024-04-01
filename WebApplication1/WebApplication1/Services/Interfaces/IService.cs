using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Services.Interfaces
{
    public interface IService
    {

        Planepath Create(Field model);

    }
}
