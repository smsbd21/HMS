
namespace HMS.Entities
{
    public class PackagePicture
    {
        public int ID { get; set; }
        public int PictureId { get; set; }
        public virtual Picture Picture { get; set; }
        public int AccomodationPackageId { get; set; }
    }
}
