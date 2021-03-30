using PROAtas.Model;
using SQLite;

namespace PROAtas.Repository
{
    public interface ITopicRepository : IBaseRepository<Topic>
    {

    }

    public class TopicRepository : BaseRepository<Topic>, ITopicRepository
    {
        public TopicRepository(SQLiteConnection context) : base(context) { }

        #region Implementation



        #endregion
    }
}
