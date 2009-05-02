using System;
using System.Collections.Generic;
using System.Text;

namespace TreeDataGridTestApp
{
    public class CMessage
    {
        private string _message;
        private DateTime _dateTime;
        private Guid _id;
        private Guid _idParent;

        public CMessage(string Message, DateTime DateTime)
        {
            _id = _idParent = Guid.Empty;
            this._message = Message;
            this._dateTime = DateTime;
        }
        public CMessage(Guid Id, Guid IdParent, string Message, DateTime DateTime):this(Message, DateTime)
        {
            _id = Id;
            _idParent = IdParent;
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        public DateTime DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Guid IdParent
        {
            get { return _idParent; }
            set { _idParent = value; }
        }

    }
}
