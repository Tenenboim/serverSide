using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Data;
using System.Xml;
using System.Web.Script.Serialization;

namespace BLL

{
    public static class trygoogle

    {

        //    public static void GetDistance(string origin, string destination)
        //    {
        //        //http://maps.googleapis.com/maps/api/distancematrix/xml?origins=NEW+YORK+11535&destinations=WASHINGTON+20544&sensor=true

        //        string url = @"http://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin + "&destinations=" + destination + "&sensor=false";
        //        string time;
        //        string des;
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //        WebResponse response = request.GetResponse();
        //        Stream dataStream = response.GetResponseStream();
        //        StreamReader sreader = new StreamReader(dataStream);
        //        string responsereader = sreader.ReadToEnd();
        //        response.Close();

        //        DataSet ds = new DataSet();
        //        ds.ReadXml(new XmlTextReader(new StringReader(responsereader)));
        //        if (ds.Tables.Count > 0)
        //        {
        //            if (ds.Tables["element"].Rows[0]["status"].ToString() == "OK")
        //            {
        //                time = ds.Tables["duration"].Rows[0]["text"].ToString();
        //                des = ds.Tables["distance"].Rows[0]["text"].ToString();
        //            }
        //        }

        //    }
        //}
        public static GeographicData GetDistanceAndDuration(double fromX, double fromY, double toX, double toY)
        {
            var gr = new GeographicData();
            try
            {
                var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={fromX},{fromY}&destinations={toX},{toY}&key=AIzaSyB6XGmiIhsaoXzLTu611HLGNL74ZEWIaSE&language=he";
                var request = WebRequest.Create(url);
                request.ContentType = "application/json; charset=utf-8";
                var response = (HttpWebResponse)request.GetResponse();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    var res = sr.ReadToEnd();
                    JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                    dynamic result = json_serializer.DeserializeObject(res);
                    var details = result["rows"][0]["elements"][0];

                    gr.Distance = details["distance"]["text"];
                    gr.Duration = details["duration"]["text"];
                }
            }
            catch (Exception) { }
            return gr;
        }

        public class GeographicData {
            public string Distance { get; set; }

            public string Duration { get; set; }
        }
    }
}

