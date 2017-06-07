using Resources;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PublishingHouse.Models
{
    public class ArticleViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Resource))]
        public string Name { get; set; }

        [Display(Name = "Authors", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Resource))]
        public string Authors { get; set; }

        [Display(Name = "Annotation", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Resource))]
        public string Annotation { get; set; }

        [Display(Name = "ArticlePath", ResourceType = typeof(Resource))]
        // [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Resource))]
        public HttpPostedFileBase File { get; set; }
    }
}