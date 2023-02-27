using CollectionsAndLinq.Common.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.Common.Models
{
    public class GetRequest
    {
        public PageModel? PageModel { get; set; }
        public FilterModel? FilterModel { get; set; }
        public SortingModel? SortingModel { get; set; }
    }
}
