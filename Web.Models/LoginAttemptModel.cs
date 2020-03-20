using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models
{
    public class LoginAttemptModel
    {
        public string InputEmailAddress { get; set; }


        public string InputPassword { get; set; }


        public bool RememberPasswordCheck { get; set; }
    }
}
