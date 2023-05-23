using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.RequestModels
{
    /// <summary>
    /// Answer Request Model
    /// </summary>
    public class AnswerRequestModel
    {
        /// <summary>
        /// Question IDs
        /// </summary>
        public Guid[] questionid { get; set; }

        /// <summary>
        /// Answers
        /// </summary>
        public string[] answer { get; set; }

        /// <summary>
        /// SurveyId
        /// </summary>
        public Guid surveyId { get; set; }
        
        /// <summary>
        /// From Email Address
        /// </summary>
        public string fromEmail { get; set; }

        /// <summary>
        /// ReplyTo Email Address
        /// </summary>
        public string replyToEmail { get; set; }

        /// <summary>
        /// EmailId
        /// </summary>
        public string emailid { get; set; }

        /// <summary>
        /// Referrer
        /// </summary>
        public string referrer { get; set; }

        /// <summary>
        /// CC
        /// </summary>
        public string contactrequestreceiver { get; set; }

        /// <summary>
        /// BCC
        /// </summary>
        public string bcccontactrequestreceiver { get; set; }
    }
}