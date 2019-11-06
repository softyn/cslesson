using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Interfaces
{
    interface INews
    {
        string Title { get; set; }
        string Date { get; set; }
        string Text { get; set; }
    }
}
