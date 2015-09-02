using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Dynamic;

namespace AppSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string json = "{" +
                          "\"0592\" : \"厦门市\"," +
                          "\"0351\" : \"太原市\"," +
                          "\"0411\" : \"大连市\"," +
                          "\"0459\" : \"大庆市\"" +
                          "}";
            JsonObjectReader rd = new JsonObjectReader(json);

            dynamic res = rd.GetObject();
            IDictionary<string, object> d = (IDictionary<string, object>) res;
            foreach (var item in d)
            {
                Console.WriteLine("{0} = {1}", item.Key, item.Value);
            }
            Console.WriteLine("\n\n");
            //===============================================================================
            json = "{\"Name\":\"小明\", \"Age\":25, \"Email\":\"abcd@dog.cc\"}";
            JsonObjectReader rd2 = new JsonObjectReader(json);
            dynamic res2 = rd2.GetObject();
            Console.WriteLine("姓名：{0}", res2.Name);
            Console.WriteLine("年龄：{0}", res2.Age);
            Console.WriteLine("电邮：{0}", res2.Email);

            Console.Read();
        }
    }


    public sealed class JsonObjectReader
    {
        private string innerJson = null;

        public JsonObjectReader(string json)
        {
            innerJson = json;
        }

        public dynamic GetObject()
        {
            dynamic d = new ExpandoObject();
            // 将JSON字符串反序列化
            JavaScriptSerializer s = new JavaScriptSerializer();
            object resobj = s.DeserializeObject(this.innerJson);
            // 拷贝数据
            IDictionary<string, object> dic = (IDictionary<string, object>) resobj;
            IDictionary<string, object> dicdyn = (IDictionary<string, object>) d;
            foreach (var item in dic)
            {
                dicdyn.Add(item.Key, item.Value);
            }
            return d;
        }
    }
}