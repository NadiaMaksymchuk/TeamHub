using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.Common.Models.Users
{
    public record NewUserDto(
    int? TeamId,
    string FirstName,
    string LastName,
    string Email,
    DateTime BirthDay)
    {

    }
}
