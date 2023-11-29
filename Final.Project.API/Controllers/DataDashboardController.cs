using Final.Project.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataDashboardController : ControllerBase
    {
        private readonly IDataDashboardManager _dashboardManager;

        public DataDashboardController(IDataDashboardManager dashboardManager)
        {
            _dashboardManager = dashboardManager;
        }

        [HttpGet]
        [Route("Dashboard/GetDataDashboard")]
        public ActionResult<DataReadDto> GetData()
        {
            DataReadDto data = _dashboardManager.GetDataDashboard();

            return data;
        }
    }
}
