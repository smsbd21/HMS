
namespace HMS.Entities
{
    public class AccomPicture
    {
        public int ID { get; set; }
        public int PictureId { get; set; }
        public int AccomodationId { get; set; }
        public virtual Picture Picture { get; set; }
    }
}
