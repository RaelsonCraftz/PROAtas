using SQLite;

namespace PROAtas.Core.Repository
{
    public interface IMinuteImageRepository : IBaseRepository<MinuteImage>
    {

    }

    public class MinuteImageRepository : BaseRepository<MinuteImage>, IMinuteImageRepository
    {
        public MinuteImageRepository(SQLiteConnection context) : base(context) { }

        #region Implementation



        #endregion
    }
}
