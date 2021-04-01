using System;
using System.Collections.Generic;
using System.Text;

namespace PROAtas.Core.Model
{
    public class Moment
    {
        public Moment() { }

        public Moment(Moment model)
        {
            Date = model.Date;
            Start = model.Start;
            End = model.End;
        }

        public DateTime Date { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
    }
}
