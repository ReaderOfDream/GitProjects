using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Month
    {
        public string Name { get; set; }
        public int Index { get; set; }
    }

    public class Months
    {
        public Month[] AllMonth = new Month[]
                                      {
                                          new Month {Index = 1, Name = "Январь"},
                                          new Month {Index = 2, Name = "Февраль"},
                                          new Month {Index = 3, Name = "Март"},
                                          new Month {Index = 4, Name = "Апрель"},
                                          new Month {Index = 5, Name = "Май"},
                                          new Month {Index = 6, Name = "Июнь"},
                                          new Month {Index = 7, Name = "Июль"},
                                          new Month {Index = 8, Name = "Август"},
                                          new Month {Index = 9, Name = "Сентябрь"},
                                          new Month {Index = 10, Name = "Октябрь"},
                                          new Month {Index = 11, Name = "Ноябрь"},
                                          new Month {Index = 12, Name = "Декабрь"}
                                      };
    }
}
