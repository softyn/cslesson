using System.Collections.Generic;
using GameAPI.DTO;

namespace GameAPI.DataAccess
{
    public interface INewsDataSource
    {
        IEnumerable<News> LoadNews(out bool success, out string errorMessage);
        IEnumerable<News> LoadNewsById(int id, out bool success);
        bool SaveNews(News news, out bool success);
        bool DeleteNews(int id, out bool success);
        bool EditNews(News news, out bool success);
    }
}