using System.Security.AccessControl;

namespace Clean.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
    }
}