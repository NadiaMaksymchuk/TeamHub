using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.Common.Models.Projects
{
    public record NewProjectDto(
     int AuthorId,
     int TeamId,
     string Name,
     string Description,
     DateTime Deadline)
    {

    }
}
