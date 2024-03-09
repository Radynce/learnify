using System.ComponentModel.DataAnnotations;
namespace learnify.Models;
public class ResourceCollection
{
    public Guid Id {get; set;}
    public Resource SelectedResource { get; set; }
    public IEnumerable<Resource> Resources { get; set; } // Collection of resources
}