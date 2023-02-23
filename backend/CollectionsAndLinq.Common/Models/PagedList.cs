namespace CollectionsAndLinq.Common.Models;

public record PagedList<T>(
    ICollection<T> Items,
    int TotalCount)
{

}
