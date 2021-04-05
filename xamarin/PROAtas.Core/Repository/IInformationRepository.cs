using PROAtas.Core.Model.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PROAtas.Core.Repository
{
    public interface IInformationRepository : IBaseRepository<Information>
    {

    }

    public class InformationRepository : BaseRepository<Information>, IInformationRepository
    {
        public InformationRepository(SQLiteConnection context) : base(context) { }

        #region Implementation



        #endregion
    }
}
