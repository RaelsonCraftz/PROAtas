using PROAtas.Core.Model.Entities;
using SQLite;

namespace PROAtas.Core.Repository
{
    public interface IPersonRepository : IBaseRepository<Person>
    {

    }

    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(SQLiteConnection context) : base(context) { }

        #region Implementation



        #endregion
    }
}
