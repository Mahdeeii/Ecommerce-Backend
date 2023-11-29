using Final.Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public class ContactUsManager:IContactUsManager
{
    private readonly IUnitOfWork _unitOfWork;

    public ContactUsManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public bool AddMessage(ContactUsSendDto message)
    {
        ContactUs contactUsMessage = new ContactUs
        {
            Name = message.Name,
            Email = message.Email,
            Message = message.Message,
        };
        _unitOfWork.ContactUsRepo.Add(contactUsMessage);
        return _unitOfWork.Savechanges() > 0;
    }

    public IEnumerable<ContactUsGetAllDto> GetAllMessages()
    {
        IEnumerable<ContactUs> messagesFromDB= _unitOfWork.ContactUsRepo.GetAll();
        IEnumerable<ContactUsGetAllDto> messages = messagesFromDB.Select(m => new ContactUsGetAllDto
        {
            id = m.Id,
            Email = m.Email,
            Message = m.Message,
            Name = m.Name,
            Status = m.Status,
            Subject = m.Subject,
        });

        return messages;
    }
}
