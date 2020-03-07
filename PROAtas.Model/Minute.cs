using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PROAtas.Model
{
    public class Minute
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public bool Active { get; set; }

        [Ignore]
        public int PeopleQuantity { get; set; }
    }
}
