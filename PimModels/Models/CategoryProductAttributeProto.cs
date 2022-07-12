namespace PimModels.Models
{
    public class CategoryProductAttributeProto
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ProductAttributeProtoId { get; set; }
        public ProductAttributeProto ProductAttributeProto { get; set; }
    }
}
