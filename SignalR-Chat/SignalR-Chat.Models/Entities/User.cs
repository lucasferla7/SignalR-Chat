using SignalR_Chat.Models.Base;
using SignalR_Chat.Models.Entities;
using System;
using System.Collections.Generic;

namespace SignalR_Chat.Models
{
    public class User : BaseEntity
    { 
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get => CalculeAge(); }
        public int UserTypeId { get; set; }
        public IEnumerable<Message> SendedMessages { get; set; }
        public IEnumerable<Message> ReceivedMessages { get; set; }
        public UserType UserType { get; set; }

        private int CalculeAge()
        {
            DateTime today = DateTime.Now;
            int age = today.Year - BirthDate.Year;
            if (BirthDate > today.AddYears(-1))
                age--;

            return age;
        }
    }
}