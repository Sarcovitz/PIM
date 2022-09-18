using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PimModels.RequestModels
{
    public class UpdateProductImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string? ContentType { get; set; }
    }
}
