using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatWithLikes
{
    class ChatService
    {
        private ChatRepository _chatRepository = new ChatRepository();

        public List<Message> GetAllHierarchicaly() //returns messages in tree structure
        {
            List<Message> messages = _chatRepository.GetAll().ToList();

            List<Message> roots = messages.Where(m => m.AnswerTo == null).ToList();
            foreach (var mes in roots)
                mes.Answers = GetChildren(mes, messages);

            return roots;
        }

        private List<Message> GetChildren(Message mes, List<Message> messages)
        {
            var answers = messages.Where(m => m.AnswerTo?.Id == mes.Id).ToList();

            if (answers.Count != 0)
                foreach (var m in answers)
                    m.Answers = GetChildren(m, messages);

            return answers;
        }
    }
}
