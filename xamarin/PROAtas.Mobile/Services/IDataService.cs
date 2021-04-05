using PROAtas.Core.Model.Entities;
using PROAtas.Core.Repository;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

namespace PROAtas.Services
{
    public interface IDataService
    {
        IMinuteRepository MinuteRepository { get; set; }
        IMinuteImageRepository MinuteImageRepository { get; set; }
        ITopicRepository TopicRepository { get; set; }
        IInformationRepository InformationRepository { get; set; }
        IPersonRepository PersonRepository { get; set; }
    }

    public class DataService : IDataService
    {
        private readonly SQLiteConnection Database;
        private string databaseName = "CosmoAtas.db3";
        private string databasePath;

        public DataService()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);
                    break;
                case Device.iOS:
                    SQLitePCL.Batteries_V2.Init();
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", databaseName);
                    break;
                default:
                    break;
            }

            Database = new SQLiteConnection(databasePath);
            Database.CreateTable<Minute>();
            Database.CreateTable<MinuteImage>();
            Database.CreateTable<Topic>();
            Database.CreateTable<Information>();
            Database.CreateTable<Person>();

            MinuteRepository = new MinuteRepository(Database);
            MinuteImageRepository = new MinuteImageRepository(Database);
            TopicRepository = new TopicRepository(Database);
            InformationRepository = new InformationRepository(Database);
            PersonRepository = new PersonRepository(Database);
        }

        public IMinuteRepository MinuteRepository { get; set; }
        public IMinuteImageRepository MinuteImageRepository { get; set; }
        public ITopicRepository TopicRepository { get; set; }
        public IInformationRepository InformationRepository { get; set; }
        public IPersonRepository PersonRepository { get; set; }
    }
}
