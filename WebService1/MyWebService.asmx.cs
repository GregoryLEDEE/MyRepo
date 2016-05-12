using System;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml;
using log4net;
using log4net.Config;
using Newtonsoft.Json;

namespace WebService1
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MyWebService : System.Web.Services.WebService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MyWebService));

        public MyWebService()
        {
            
           XmlConfigurator.Configure();
             Logger.Info("Starting MyWebService on localhost");
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Fibonacci(int n)
        {
            try
            {
                Logger.Info("Calling Fibonacci service for the value : " + n);
                if (0 <= n && n <= 100)
                {
                    long a = 0;
                    long b = 1;
                    for (int count = 0; count < n; count++)
                    {
                        long c = a;
                        a = b;
                        b += c;
                    }
                    Logger.Info("Result : " + a);
                    Context.Response.Write(JsonConvert.SerializeObject(a));
                    //return a;
                }
                else
                {
                    Logger.Error("The value does not respect 0<=n<=100");
                    Logger.Info("Result : -1");
                    Context.Response.Write(JsonConvert.SerializeObject(-1));
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal("This fatal error occurred :\n" + ex.StackTrace);
                Context.Response.Write(JsonConvert.SerializeObject("ERROR"));
            }
            //return -1;

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void XmlToJson(string xml)
        {
            try
            {
                Logger.Info("Calling XmlToJson service for the value : " + xml);
                string str = "";

                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    xmlDoc.LoadXml(xml);
                    str = JsonConvert.SerializeObject(xmlDoc);
                }
                catch (Exception ex)
                {

                    str = "Bad Xml format";
                    Logger.Error(str);
                }
                Context.Response.Write(str);
            }
            catch (Exception ex)
            {
                Logger.Fatal("This fatal error occurred :\n" + ex.StackTrace);
                Context.Response.Write(JsonConvert.SerializeObject("ERROR"));
            }
            //return str;
        }
    }

    //public class Service1 : System.Web.Services.WebService
    //{

    //    [WebMethod]
    //    public long Fibonacci(int n)
    //    {

    //        if (0 <= n && n <= 100)
    //        {
    //            long a = 0;
    //            long b = 1;
    //            for (int count = 0; count < n; count++)
    //            {
    //                long c = a;
    //                a = b;
    //                b += c;
    //            }

    //            return a;
    //        }

    //        return -1;

    //    }

    //    [WebMethod]
    //    public string XmlToJson(string xml)
    //    {
    //        string str = "";

    //        XmlDocument xmlDoc = new XmlDocument();
    //        try
    //        {
    //            xmlDoc.LoadXml(xml);
    //            str = JsonConvert.SerializeObject(xmlDoc);
    //        }
    //        catch (Exception ex)
    //        {
    //            str = "Bad Xml format";
    //        }

    //        return str;
    //    }
    //}
}
