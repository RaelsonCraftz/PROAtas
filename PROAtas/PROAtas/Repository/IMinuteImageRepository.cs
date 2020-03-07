using PROAtas.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PROAtas.Repository
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
