using PROAtas.Model;
using SQLite;

namespace PROAtas.Repository
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
