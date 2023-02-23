using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.Common.Models.Tasks
{
    public record UpdateTaskDto(
    int Id,
    int ProjectId,
    int PerformerId,
    string Name,
    string Description,
    string State,
    DateTime? FinishedAt)
    {

    }
}
