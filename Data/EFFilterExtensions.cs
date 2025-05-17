using System.Linq.Expressions;
using WebApp1.models;

namespace WebApp1.Data
{
    public static class EFFilterExtensions
    {
        public static bool GetIsDeleted(BaseEntity entity)
        {
            return entity.IsDeleted;
        }
    }
}