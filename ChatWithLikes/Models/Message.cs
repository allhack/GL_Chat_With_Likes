using System;
using System.Collections.Generic;
using System.Data;

namespace Models
{
    public class Message
    {
        public int           Id       { get; set; }
        public string        Author   { get; set; }
        public string        Content  { get; set; }
        public DateTime      Time     { get; set; }
        public Message       AnswerTo { get; set; } // by default null

        public List<Message> Answers  { get; set; }

        public int           Likes    { get; set; }
        public int           Dislikes { get; set; }

        public Message()
        {
            Time = DateTime.Now;
            Answers = new List<Message>();
        }

        public static Message FromReader(IDataReader reader)
        {
            return new Message
            {
                Id       = (int)      reader.GetValue(0),
                Author   = (string)   reader.GetValue(1),
                Content  = (string)   reader.GetValue(2),
                Time     = (DateTime) reader.GetValue(3),

                AnswerTo = reader.GetValue(4) is DBNull ? null : new Message { Id = (int)reader.GetValue(4) }
            };
        }
    }
}
