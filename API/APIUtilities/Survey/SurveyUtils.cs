using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.IO;

namespace API.Utilities.Survey
{
    public class SurveyUtils
    {
        public static string TransformXsltToCustomerContent(XmlDocument answerXML, string xsltContent)
        {
            var customerContent = "";

            if (!String.IsNullOrWhiteSpace(xsltContent))
            {
                XslCompiledTransform myXslTransform = new XslCompiledTransform();
                XsltArgumentList argsList = new XsltArgumentList();
                StringReader xsltString = new StringReader(xsltContent);
                myXslTransform.Load(XmlReader.Create(xsltString));
                using (MemoryStream customerStream = new MemoryStream())
                {
                    argsList.AddParam("ModeName", "", "Customer");
                    System.Xml.Linq.XDocument xd = new System.Xml.Linq.XDocument();
                    myXslTransform.Transform(answerXML, argsList, customerStream);
                    customerStream.Position = 0;
                    XmlDocument xdResult = new XmlDocument();
                    xdResult.Load(customerStream);
                    customerContent = xdResult.DocumentElement.OuterXml;
                }
            }
            else
            {
                customerContent = "";
            }
            return customerContent;
        }

        public static string TransformXsltToAdminContent(XmlDocument answerXML, string xsltContent, string sHeaderData, string sReferrer)
        {
            var adminContent = "";
            if (!String.IsNullOrWhiteSpace(xsltContent))
            {
                XslCompiledTransform myXslTransform = new XslCompiledTransform();
                XsltArgumentList argsList = new XsltArgumentList();
                StringReader xsltString = new StringReader(xsltContent);
                myXslTransform.Load(XmlReader.Create(xsltString));
                using (MemoryStream adminStream = new MemoryStream())
                {
                    argsList.AddParam("ModeName", "", "Admin");
                    argsList.AddParam("HeaderData", "", sHeaderData);
                    argsList.AddParam("UrlReferrer", "", sReferrer);
                    myXslTransform.Transform(answerXML, argsList, adminStream);
                    adminStream.Position = 0;
                    XmlDocument xdResult = new XmlDocument();
                    xdResult.Load(adminStream);
                    adminContent = xdResult.DocumentElement.OuterXml;
                }
            }
            return adminContent;
        }

        public static XmlDocument GenerateAnswerXMLforXsltTransform(string[] questions, string[] answers, Guid surveyId, string emailAddress, string[] questionTitles)
        {
            int iAnswerCount = answers.Count();
            var sSettings = new XmlWriterSettings { Indent = false };
            XmlDocument answerXML = new XmlDocument();
            var oMemoryStream = new MemoryStream();
            using (var xwAnswer = XmlWriter.Create(oMemoryStream, sSettings))
            {
                xwAnswer.WriteStartDocument(true);
                xwAnswer.WriteStartElement("Answers");
                xwAnswer.WriteAttributeString("SurveyID", surveyId.ToString());
                xwAnswer.WriteAttributeString("EmailAddress", emailAddress);

                for (int i = 0; i < iAnswerCount; i++)
                {
                    xwAnswer.WriteStartElement("Answer");
                    var answervalue = Convert.ToString(answers[i]) ?? string.Empty;
                    xwAnswer.WriteStartElement("Question");
                    xwAnswer.WriteValue(questionTitles[i]);
                    xwAnswer.WriteEndElement();
                    xwAnswer.WriteStartElement("QuestionAnswer");
                    xwAnswer.WriteValue(answervalue);
                    xwAnswer.WriteEndElement();
                    xwAnswer.WriteEndElement();
                }
                xwAnswer.WriteEndElement();
                xwAnswer.WriteEndDocument();
            }
            oMemoryStream.Position = 0;
            answerXML.Load(oMemoryStream);
            return answerXML;
        }
    }
}
