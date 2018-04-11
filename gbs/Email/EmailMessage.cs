using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Email
{
    public class EmailMessage
    {
        //public EmailMessage()
        //{
        //    ToAddresses = new ;
        //    FromAddresses = EmailAddress;
        //}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string ToAddresses { get; set; }
        [DataType(DataType.EmailAddress)]
        public string FromAddresses { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        [DataType(DataType.PhoneNumber)]

        public string phoneNumber { get; set; }
    }
}
