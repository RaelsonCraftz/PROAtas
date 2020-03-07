using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PROAtas.Model
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }

        public int IdMinute { get; set; }
    }
}
