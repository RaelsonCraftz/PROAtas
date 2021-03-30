using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PROAtas.Core.Repository
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
