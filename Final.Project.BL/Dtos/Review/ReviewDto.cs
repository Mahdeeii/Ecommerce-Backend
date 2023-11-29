using Final.Project.DAL;

namespace Final.Project.BL;

public class ReviewDto
{
    public string Comment { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; } = DateTime.Now.Date;
    public int Rating { get; set; }
    public string FName { get; set; } = string.Empty;
    public string LName { get; set; } = string.Empty;

    public string FormattedCreationDate => CreationDate.ToString("dd-MM-yyyy");

}




