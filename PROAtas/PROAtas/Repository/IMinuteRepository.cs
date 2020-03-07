using PROAtas.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PROAtas.Repository
{
    public interface IMinuteRepository : IBaseRepository<Minute>
    {

    }

    public class MinuteRepository : BaseRepository<Minute>, IMinuteRepository
    {
        public MinuteRepository(SQLiteConnection context) : base(context) { }

        #region Implementation



        #endregion
    }
}
