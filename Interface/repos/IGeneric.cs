using System.Linq.Expressions;

public interface IGeneric<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
    Task<int> CreateAsync(T entity);
    Task<int> UpdateAsync(T entity);
    Task<int> DeleteAsync(int id);

    // ------ الميثودز الجديدة المضافة ------

    // بترجع كل الصفوف اللي بتحقق شرط معين (زي WHERE في SQL)، مع إمكانية جلب الـ Includes معاها
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

    // بترجع صف واحد بس مطابق لشرط معين، وبترمي Error لو لقت أكتر من صف
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

    // بترجع IQueryable عشان تقدر تعمل Where/OrderBy/Skip/Take بنفسك في الـ Service (مفيدة جدًا في الـ Pagination والفلترة)
    IQueryable<T> Query();

    // بترجع true/false بس، هل فيه صف بيحقق الشرط ده
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

    // بترجع عدد الصفوف - كله لو من غير شرط، أو حسب شرط معين
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
}
