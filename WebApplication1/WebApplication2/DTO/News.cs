using System;
using GameAPI.Interfaces;

namespace GameAPI.DTO
{

    public class News : INews
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }
    }
}
