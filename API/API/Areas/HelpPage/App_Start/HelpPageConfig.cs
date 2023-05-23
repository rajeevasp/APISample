using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI;
using API.Domain.Blog;


using API.Infrastructure.Extensions;

namespace API.Areas.HelpPage
{
    /// <summary>
    /// Use this class to customize the Help Page.
    /// For example you can set a custom <see cref="System.Web.Http.Description.IDocumentationProvider"/> to supply the documentation
    /// or you can provide the samples for the requests/responses.
    /// </summary>
    public static class HelpPageConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //// Uncomment the following to use the documentation from XML documentation file.
            config.SetDocumentationProvider(new XmlDocumentationProvider(HttpContext.Current.Server.MapPath("~/App_Data/APIDocumentation.xml")));

            #region Appointment Documentation

            
            config.SetActualResponseType(typeof(string), "Appointment", "SaveAppointment", "apt");

            #endregion

            #region Blog Documentation

            config.SetActualResponseType(typeof(Blog), "Blog", "Get", "id");
            config.SetActualResponseType(typeof(Blog), "Blog", "Get", "blog");
            config.SetActualResponseType(typeof(List<Blog>), "Blog", "GetAll");
            config.SetActualResponseType(typeof(string), "Blog", "Save", "blog");
            config.SetActualResponseType(typeof(string), "Blog", "Delete", "id");
            config.SetActualResponseType(typeof(string), "Blog", "Update", "blog");

            #endregion

            #region BlogRollItem Documentation

           
            config.SetActualResponseType(typeof(string), "BlogRollItem", "Save", "blogRollItem");
            config.SetActualResponseType(typeof(string), "BlogRollItem", "Delete", "id");
            config.SetActualResponseType(typeof(string), "BlogRollItem", "Update", "blogRollItem");

            #endregion

            #region Catalog Documentation

            // Product
            
            config.SetActualResponseType(typeof(string), "Catalog", "SaveProduct", "product");
            config.SetActualResponseType(typeof(string), "Catalog", "DeleteProduct", "id");
            config.SetActualResponseType(typeof(string), "Catalog", "UpdateProduct", "product");

            // Product Comments
            config.SetActualResponseType(typeof(string), "Catalog", "SaveProductComment", "productComment");
            config.SetActualResponseType(typeof(string), "Catalog", "UpdateProductComment", "productComment");
            config.SetActualResponseType(typeof(string), "Catalog", "DeleteProductComment", "commentId");
          

            // Department
            
        
            config.SetActualResponseType(typeof(string), "Catalog", "SaveDepartment", "department");
            config.SetActualResponseType(typeof(string), "Catalog", "DeleteDepartment", "id");
            config.SetActualResponseType(typeof(string), "Catalog", "UpdateDepartment", "department");

            //Department Comments
            config.SetActualResponseType(typeof(string), "Catalog", "SaveDepartmentComment", "departmentComment");
            config.SetActualResponseType(typeof(string), "Catalog", "UpdateDepartmentComment", "departmentComment");
            config.SetActualResponseType(typeof(string), "Catalog", "DeleteDepartmentComment", "commentId");
            

            // Manufacturer
            
            config.SetActualResponseType(typeof(string), "Catalog", "SaveManufacturer", "manufacturer");
            config.SetActualResponseType(typeof(string), "Catalog", "DeleteManufacturer", "id");
            config.SetActualResponseType(typeof(string), "Catalog", "UpdateManufacturer", "manufacturer");

            // Product Attribute
            
            config.SetActualResponseType(typeof(string), "Catalog", "SaveProductAttribute", "productAttribute");
            config.SetActualResponseType(typeof(string), "Catalog", "DeleteProductAttribute", "id");
            config.SetActualResponseType(typeof(string), "Catalog", "UpdateProductAttribute", "productAttribute");

            // Product Attribute Value
            
            config.SetActualResponseType(typeof(string), "Catalog", "SaveProductAttributeValue", "productAttributeValue");
            config.SetActualResponseType(typeof(string), "Catalog", "DeleteProductAttributeValue", "id");
            config.SetActualResponseType(typeof(string), "Catalog", "UpdateProductAttributeValue", "productAttributeValue");

            // Product Template
           
            config.SetActualResponseType(typeof(string), "Catalog", "SaveProductTemplate", "productTemplate");
            config.SetActualResponseType(typeof(string), "Catalog", "DeleteProductTemplate", "id");
            config.SetActualResponseType(typeof(string), "Catalog", "UpdateProductTemplate", "productTemplate");

            // Product Variant
            
            config.SetActualResponseType(typeof(string), "Catalog", "SaveProductVariant", "productVariant");
            config.SetActualResponseType(typeof(string), "Catalog", "DeleteProductVariant", "id");
            config.SetActualResponseType(typeof(string), "Catalog", "UpdateProductVariant", "productVariant");

            // Product Variant Comments
            config.SetActualResponseType(typeof(string), "Catalog", "SaveProductVariantComment", "productVariantComment");
            config.SetActualResponseType(typeof(string), "Catalog", "UpdateProductVariantComment", "productVariantComment");
            config.SetActualResponseType(typeof(string), "Catalog", "DeleteProductVariantComment", "commentId");
           
            // Manufacturer Template
            
            config.SetActualResponseType(typeof(string), "Catalog", "SaveManufacturerTemplate", "manufacturerTemplate");
            config.SetActualResponseType(typeof(string), "Catalog", "DeleteManufacturerTemplate", "id");
            config.SetActualResponseType(typeof(string), "Catalog", "UpdateManufacturerTemplate", "manufacturerTemplate");

            // Department Template
            
            config.SetActualResponseType(typeof(string), "Catalog", "SaveDepartmentTemplate", "departmentTemplate");
            config.SetActualResponseType(typeof(string), "Catalog", "DeleteDepartmentTemplate", "id");
            config.SetActualResponseType(typeof(string), "Catalog", "UpdateDepartmentTemplate", "departmentTemplate");

            #endregion

            #region Contact Documentation

           
            config.SetActualResponseType(typeof(string), "Contact", "Save", "contact");
            config.SetActualResponseType(typeof(string), "Contact", "Delete", "id");
            config.SetActualResponseType(typeof(string), "Contact", "Update", "contact");

            #endregion

            #region Entity Documentation

          
            config.SetActualResponseType(typeof(string), "Entity", "Save", "entity");
            config.SetActualResponseType(typeof(string), "Entity", "Delete", "id");
            config.SetActualResponseType(typeof(string), "Entity", "Update", "entity");

            #endregion

            #region Location Documentation

            
            config.SetActualResponseType(typeof(string), "Location", "Save", "location");
            config.SetActualResponseType(typeof(string), "Location", "Delete", "id");
            config.SetActualResponseType(typeof(string), "Location", "Update", "location");

            #endregion

            #region Page Documentation

            config.SetActualResponseType(typeof(Page), "Page", "Get", "id", "blogId", "fullyload");
            config.SetActualResponseType(typeof(List<Page>), "Page", "GetAll", "blogId", "fullyload");
            config.SetActualResponseType(typeof(string), "Page", "GetPageBreadCrumb", "id", "blogId");
            config.SetActualResponseType(typeof(List<Page>), "Page", "GetAllByPublishState", "published", "fullyload");
            config.SetActualResponseType(typeof(string), "Page", "Save", "page");
            config.SetActualResponseType(typeof(string), "Page", "Delete", "id", "blogId");
            config.SetActualResponseType(typeof(string), "Page", "Update", "page", "blogId");

            #endregion

            #region Person Documentation

            
            config.SetActualResponseType(typeof(string), "Person", "Save", "person");
            config.SetActualResponseType(typeof(string), "Person", "Delete", "id");
            config.SetActualResponseType(typeof(string), "Person", "Update", "person");

            #endregion

            #region Picture Documentation

            
            config.SetActualResponseType(typeof(string), "Picture", "Save", "picture");
            config.SetActualResponseType(typeof(string), "Picture", "Delete", "id");
            config.SetActualResponseType(typeof(string), "Picture", "Update", "picture");

            #endregion

            #region Post Documentation

           
            

            #endregion

            #region Survey Documentation

            // Survey
           

            // Answer
            
           
           
            config.SetActualResponseType(typeof(string), "Survey", "SaveAnswer", "answerModel");
            config.SetActualResponseType(typeof(string), "Survey", "DeleteAnswer", "id");
            config.SetActualResponseType(typeof(string), "Survey", "UpdateAnswer", "answer");

            // Question
           
            config.SetActualResponseType(typeof(string), "Survey", "SaveQuestion", "question");
            config.SetActualResponseType(typeof(string), "Survey", "DeleteQuestion", "id");
            config.SetActualResponseType(typeof(string), "Survey", "UpdateQuestion", "Question");

            // Question Option
           
            config.SetActualResponseType(typeof(string), "Survey", "SaveQuestionOption", "questionOption");
            config.SetActualResponseType(typeof(string), "Survey", "DeleteQuestionOption", "id");
            config.SetActualResponseType(typeof(string), "Survey", "UpdateQuestionOption", "questionOption");

            // Survey Comments
            config.SetActualResponseType(typeof(string), "Survey", "SaveComment", "surveyComment");
            config.SetActualResponseType(typeof(string), "Survey", "UpdateComment", "surveyComment");
            config.SetActualResponseType(typeof(string), "Survey", "DeleteComment", "commentId");
            

            #endregion

            //// Uncomment the following to use "1234" directly as the request sample for media type "text/plain" on the controller named "Values"
            //// and action named "Put".
            //config.SetSampleRequest("1234", new MediaTypeHeaderValue("text/plain"), "Values", "Put");
            //// Uncomment the following to use the image on "../images/aspNetHome.png" directly as the response sample for media type "image/png"
            //// on the controller named "Values" and action named "Get" with parameter "id".
            //config.SetSampleResponse(new ImageSample("../images/aspNetHome.png"), new MediaTypeHeaderValue("image/png"), "Values", "Get", "id");
            //// Uncomment the following to correct the sample request when the action expects an HttpRequestMessage with ObjectContent<string>.
            //// The sample will be generated as if the controller named "Values" and action named "Get" were having string as the body parameter.
            //config.SetActualRequestType(typeof(string), "Values", "Get");
        }
    }
}