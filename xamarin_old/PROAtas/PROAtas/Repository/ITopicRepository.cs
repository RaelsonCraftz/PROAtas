using PROAtas.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

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
