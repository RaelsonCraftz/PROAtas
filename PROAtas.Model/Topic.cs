using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PROAtas.Model
{
    public class Topic
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Order { get; set; }
        public string Text { get; set; }

        public int IdMinute { get; set; }
    }
}
