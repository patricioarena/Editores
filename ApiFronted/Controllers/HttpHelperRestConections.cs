using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ApiFrontend.Controllers
{
    public class HttpHelperRestConections
    {
        WebClient client;
        private string apiDominio = ConfigurationManager.AppSettings["coreApiUrl"].ToString();
        public HttpHelperRestConections()
        {
            client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
        }

        public JObject restCallGet(string uri, ApiController api)
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

                if (api.Request.Headers.Contains("Authorization"))
                {
                    foreach (var value in api.Request.Headers.GetValues("Authorization"))
                    {
                        if (value.Contains("Basic"))
                        {
                            client.Headers["Authorization"] = value;
                            tieneAuthorizationBasic = true;
                        }

                    }
                }
                if (!tieneAuthorizationBasic)
                {
                    client.UseDefaultCredentials = true;
                }
                var text = client.DownloadString(apiDominio + uri);
                JObject jobject = JObject.Parse(text);
                jobject.Add("request headers", jsonHeades);
                return jobject;
            }
            catch (Exception e)
            {
                var errorObject = new JObject();
                errorObject.Add("error", e.Message);
                errorObject.Add("url", apiDominio + uri);
                errorObject.Add("headers", jsonHeades);
                return errorObject;
            }
        }


        public JObject restCallPost(string uri, object body, ApiController api)
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

                if (api.Request.Headers.Contains("Authorization"))
                {
                    foreach (var value in api.Request.Headers.GetValues("Authorization"))
                    {
                        if (value.Contains("Basic"))
                        {
                            client.Headers["Authorization"] = value;
                            tieneAuthorizationBasic = true;
                        }

                    }
                }
                if (!tieneAuthorizationBasic)
                {
                    client.UseDefaultCredentials = true;
                }

                var bodyRest = JObject.FromObject(body).ToString();
                var response = client.UploadString(apiDominio + uri, bodyRest);
                JObject jobject = JObject.Parse(response);

                jobject.Add("request headers", jsonHeades);
                return jobject;
            }
            catch (Exception e)
            {
                var errorObject = new JObject();
                errorObject.Add("error", e.Message);
                errorObject.Add("url", apiDominio + uri);
                errorObject.Add("headers", jsonHeades);
                return errorObject;
            }
        }


        public JObject restCallPut(string uri, object body, ApiController api)
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

                if (api.Request.Headers.Contains("Authorization"))
                {
                    foreach (var value in api.Request.Headers.GetValues("Authorization"))
                    {
                        if (value.Contains("Basic"))
                        {
                            client.Headers["Authorization"] = value;
                            tieneAuthorizationBasic = true;
                        }

                    }
                }
                if (!tieneAuthorizationBasic)
                {
                    client.UseDefaultCredentials = true;
                }

                var bodyRest = JObject.FromObject(body).ToString();
                var response = client.UploadString(apiDominio + uri + "/update", bodyRest);
                JObject jobject = JObject.Parse(response);

                jobject.Add("request headers", jsonHeades);
                return jobject;
            }
            catch (Exception e)
            {
                var errorObject = new JObject();
                errorObject.Add("error", e.Message);
                errorObject.Add("url", apiDominio + uri);
                errorObject.Add("headers", jsonHeades);
                return errorObject;
            }
        }

        public JObject restCallDelete(string uri, ApiController api)
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

                if (api.Request.Headers.Contains("Authorization"))
                {
                    foreach (var value in api.Request.Headers.GetValues("Authorization"))
                    {
                        if (value.Contains("Basic"))
                        {
                            client.Headers["Authorization"] = value;
                            tieneAuthorizationBasic = true;
                        }

                    }
                }
                if (!tieneAuthorizationBasic)
                {
                    client.UseDefaultCredentials = true;
                }

                var response = client.DownloadString(apiDominio + uri + "/delete");
                JObject jobject = JObject.Parse(response);

                jobject.Add("request headers", jsonHeades);
                return jobject;
            }
            catch (Exception e)
            {
                var errorObject = new JObject();
                errorObject.Add("error", e.Message);
                errorObject.Add("url", apiDominio + uri);
                errorObject.Add("headers", jsonHeades);
                return errorObject;
            }
        }
    }
}