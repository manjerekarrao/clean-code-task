using System;

namespace Naming.Task2
{
    public class User
    {
        private readonly DateTime _dob = DateTime.Now;

        private readonly string _name;

        private readonly bool _blnAdmin= false;

        private readonly User[] _subordinates = null;

        private readonly int _rating = 0;

        public User(string name)
        {
            this._name = name;
        }

        public override string ToString()
        {
            return "User [dBirth=" + _dob + ", sName=" + _name + ", bAdmin=" + _blnAdmin + ", subordinateArray="
                   + string.Join<User>(", ", _subordinates) + ", iRating=" + _rating + "]";
        }
    }
}
