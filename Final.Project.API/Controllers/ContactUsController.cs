using Final.Project.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsManager _contactUsManager;

        public ContactUsController(IContactUsManager contactUsManager)
        {
            _contactUsManager = contactUsManager;
        }

        #region Send Message

        [HttpPost]
        public ActionResult SendMessage(ContactUsSendDto message)
        {
            bool status;
           status= _contactUsManager.AddMessage(message);
            if (status)
            {
                return Ok(new
                {
                    status = status,
                    message = "Message Send Successfully , we will contact you a"
                });
            }
            else
            {
                return Ok(new
                {
                    status = status,
                    message = "Failed to send message , try again"
                });

            }
           
        }

        #endregion

        #region Get All Messages

        [HttpGet]
        public ActionResult GetAllMessages()
        {
            IEnumerable<ContactUsGetAllDto> messages= _contactUsManager.GetAllMessages();
            return Ok(messages);
        }
        #endregion

    }
}
