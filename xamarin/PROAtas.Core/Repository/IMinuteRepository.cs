using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PROAtas.Core.Repository
{
    public interface IMinuteRepository : IBaseRepository<Minute, string>
    {

    }

    public class MinuteRepository : BaseRepository<Minute, string>, IMinuteRepository
    {
        public MinuteRepository(SQLiteConnection context) : base(context) { }

        #region Implementation



        #endregion
    }
}
