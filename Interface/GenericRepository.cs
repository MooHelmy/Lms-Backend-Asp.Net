using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

// التنفيذ العام لـ IGeneric<T> - أي Repository خاص هيرث من الكلاس ده
// ويضيف بس الميثودز الإضافية اللي خاصة بيه (زي CourseRepository في المثال تحت).
public class GenericRepository<T> : IGeneric<T> where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    // بترجع كل الصفوف، وبتطبق أي Includes اتبعتلها عشان تجيب الـ Navigation Properties
    // (زي Instructor, Category) من غير ما تكتب Include بره الـ Repository كل مرة.
    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        foreach (var include in includes)
            query = query.Include(include);

        return await query.ToListAsync();
    }

    // بترجع صف واحد بالـ Id، وبتطبق نفس فكرة الـ Includes لو محتاج تجيب علاقاته
    // (زي Sections بتاعت الكورس) في نفس الاستعلام.
    public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        foreach (var include in includes)
            query = query.Include(include);

        // EF.Property بتقرا اسم الـ Primary Key ديناميكيًا (افتراضه "Id")
        // فالميثود دي تشتغل مع أي Entity من غير ما تكون مربوطة بكلاس معين.
        return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
    }

    // بتضيف الصف وبتحفظ على طول (SaveChangesAsync جوه نفس الميثود)
    // وبترجع عدد الصفوف المتأثرة (المفروض تبقى 1 لو نجحت).
    public async Task<int> CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return await _context.SaveChangesAsync();
    }

    // بتعلّم الصف إنه اتعدل وبتحفظ على طول.
    public async Task<int> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return await _context.SaveChangesAsync();
    }

    // بتدور على الصف بالـ Id الأول، ولو لقته بتحذفه وتحفظ على طول.
    // لو مش موجود بترجع 0 (معنى إن مفيش صفوف اتأثرت).
    public async Task<int> DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is null)
            return 0;

        _dbSet.Remove(entity);
        return await _context.SaveChangesAsync();
    }

    // ------ الميثودز الجديدة المضافة ------

    // بترجع كل الصفوف اللي بتحقق شرط معين، ومعاها أي Includes اتبعتلها
    // مثال: FindAsync(c => c.CategoryId == 3, c => c.Instructor)
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        foreach (var include in includes)
            query = query.Include(include);

        return await query.Where(predicate).ToListAsync();
    }

    // بترجع صف واحد بس مطابق للشرط - بترمي Exception لو لقت أكتر من صف
    // استخدمها لما تكون متأكد إن الشرط بيرجع نتيجة فريدة (زي البحث بالإيميل)
    public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        foreach (var include in includes)
            query = query.Include(include);

        return await query.SingleOrDefaultAsync(predicate);
    }

    // بترجع IQueryable من غير ما تتنفذ - عشان تقدر تبني عليها Where/OrderBy/Skip/Take
    // بنفسك في الـ Service (زي الفلترة والـ Pagination في صفحة الكورسات)
    public IQueryable<T> Query()
    {
        return _dbSet.AsQueryable();
    }

    // بترجع true/false بس - أسرع من FindAsync لو مش محتاج البيانات نفسها
    // (مثال: هل الطالب Enrolled في الكورس ده قبل كده؟)
    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    // بترجع عدد الصفوف - كله لو من غير شرط، أو حسب شرط معين
    // بنستخدمها في حساب TotalCount/TotalPages وقت الـ Pagination
    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
    {
        return predicate is null
            ? await _dbSet.CountAsync()
            : await _dbSet.CountAsync(predicate);
    }
}
