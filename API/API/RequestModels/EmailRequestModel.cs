using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.RequestModels
{
    /// <summary>
    /// Email Request Model
    /// </summary>
    public class EmailRequestModel
    {
        /// <summary>
        /// From Email Address
        /// </summary>
        public string fromEmail { get; set; }

        /// <summary>
        /// ReplyTo Email Address
        /// </summary>
        public string replyToEmail { get; set; }

        /// <summary>
        /// End User Email Address
        /// </summary>
        public string endUserEmailId { get; set; }

        /// <summary>
        /// CC
        /// </summary>
        public List<string> ccEmailAddresses { get; set; }

        /// <summary>
        /// BCC
        /// </summary>
        public List<string> bccEmailAddresses { get; set; }

        /// <summary>
        /// Email Body
        /// </summary>
        public string emailBody { get; set; }

        /// <summary>
        /// Is Customer
        /// </summary>
        public bool isCustomer { get; set; }
    }
}