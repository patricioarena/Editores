using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiFronted.Helper
{
    public class HttpHelperRestConections
    {
        WebClient client;
        public string urlBackend;

        public HttpHelperRestConections(string backendUrl)
        {
            this.urlBackend = backendUrl;
            client = new WebClient();

#if !DEBUG // <-- Sacar el ! para Trabajar en Fiscalia
            WebProxy wp = new WebProxy("proxy1.fepba.gov.ar:8080");
            client.Proxy = wp;
#else
            client.Proxy = null;
#endif
            client.Encoding = System.Text.Encoding.UTF8;
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
        }

        public JObject restCallGet(string uri, Controller api)
        {
            JObject jsonHeades = new JObject();
            bool tieneAuthorizationBasic = false;
            try
            {
                // armo un json con los headers
                foreach (var oneHeader in api.Request.Headers)
                {
                    var header = oneHeader.Key;
                    var value = oneHeader.Value.FirstOrDefault();
                    jsonHeades.Add(header, value);
                }

                if (api.Request.Headers.ContainsKey("Authorization"))
                {
                    StringValues userpas = "";
                    api.Request.Headers.TryGetValue(("Authorization"), out userpas);
                    string user =  userpas[0].ToString();
                    if (user != null)
                    {
                        client.Headers["Authorization"] = user;
                        client.UseDefaultCredentials = true;
                    }
                }
                if (!tieneAuthorizationBasic)
                {
                    client.UseDefaultCredentials = true;
                }
                var text = client.DownloadString(urlBackend + uri);
                JObject jobject = JObject.Parse(text);
                jobject.Add("request headers", jsonHeades);
                return jobject;
            }
            catch (Exception e)
            {
                var errorObject = new JObject();
                errorObject.Add("error", e.Message);
                errorObject.Add("url", urlBackend + uri);
                errorObject.Add("headers", jsonHeades);
                return errorObject;
            }
        }


        public JObject restCallPost(string uri, object body, Controller api)
        {
            JObject jsonHeades = new JObject();
            bool tieneAuthorizationBasic = false;
            try
            {
                // armo un json con los headers
                foreach (var oneHeader in api.Request.Headers)
                {
                    var header = oneHeader.Key;
                    var value = oneHeader.Value.FirstOrDefault();
                    jsonHeades.Add(header, value);
                }

                if (api.Request.Headers.ContainsKey("Authorization"))
                {
                    StringValues userpas = "";
                    api.Request.Headers.TryGetValue(("Authorization"), out userpas);
                    string user = userpas[0].ToString();
                    if (user != null)
                    {
                        client.Headers["Authorization"] = user;
                        client.UseDefaultCredentials = true;
                    }
                }
                if (!tieneAuthorizationBasic)
                {
                    client.UseDefaultCredentials = true;
                }

                var bodyRest = JObject.FromObject(body).ToString();
                var response = client.UploadString(urlBackend + uri, bodyRest);
                JObject jobject = JObject.Parse(response);

                jobject.Add("request headers", jsonHeades);
                return jobject;
            }
            catch (Exception e)
            {
                var errorObject = new JObject();
                errorObject.Add("error", e.Message);
                errorObject.Add("url", urlBackend + uri);
                errorObject.Add("headers", jsonHeades);
                return errorObject;
            }
        }


        public JObject restCallPut(string uri, object body, Controller api)
        {
            JObject jsonHeades = new JObject();
            bool tieneAuthorizationBasic = false;
            try
            {
                // armo un json con los headers
                foreach (var oneHeader in api.Request.Headers)
                {
                    var header = oneHeader.Key;
                    var value = oneHeader.Value.FirstOrDefault();
                    jsonHeades.Add(header, value);
                }
                if (api.Request.Headers.ContainsKey("Authorization"))
                {
                    StringValues userpas = "";
                    api.Request.Headers.TryGetValue(("Authorization"), out userpas);
                    string user = userpas[0].ToString();
                    if (user != null)
                    {
                        client.Headers["Authorization"] = user;
                        client.UseDefaultCredentials = true;
                    }
                }
                if (!tieneAuthorizationBasic)
                {
                    client.UseDefaultCredentials = true;
                }

                var bodyRest = JObject.FromObject(body).ToString();
                var response = client.UploadString(urlBackend + uri + "/update", bodyRest);
                JObject jobject = JObject.Parse(response);

                jobject.Add("request headers", jsonHeades);
                return jobject;
            }
            catch (Exception e)
            {
                var errorObject = new JObject();
                errorObject.Add("error", e.Message);
                errorObject.Add("url", urlBackend + uri);
                errorObject.Add("headers", jsonHeades);
                return errorObject;
            }
        }


        public JObject restCallDelete(string uri, Controller api)
        {
            JObject jsonHeades = new JObject();
            bool tieneAuthorizationBasic = false;
            try
            {
                // armo un json con los headers
                foreach (var oneHeader in api.Request.Headers)
                {
                    var header = oneHeader.Key;
                    var value = oneHeader.Value.FirstOrDefault();
                    jsonHeades.Add(header, value);
                }
                if (api.Request.Headers.ContainsKey("Authorization"))
                {
                    StringValues userpas = "";
                    api.Request.Headers.TryGetValue(("Authorization"), out userpas);
                    string user = userpas[0].ToString();
                    if (user != null)
                    {
                        client.Headers["Authorization"] = user;
                        client.UseDefaultCredentials = true;
                    }
                }
                if (!tieneAuthorizationBasic)
                {
                    client.UseDefaultCredentials = true;
                }

                var response = client.DownloadString(urlBackend + uri + "/delete");
                JObject jobject = JObject.Parse(response);

                jobject.Add("request headers", jsonHeades);
                return jobject;
            }
            catch (Exception e)
            {
                var errorObject = new JObject();
                errorObject.Add("error", e.Message);
                errorObject.Add("url", urlBackend + uri);
                errorObject.Add("headers", jsonHeades);
                return errorObject;
            }
        }
    }
}
