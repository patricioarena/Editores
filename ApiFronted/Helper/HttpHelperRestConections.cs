using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ApiFronted.Helper
{
    public interface IHttpHelperRestConections
    {
        JObject restCallDelete(string uri, Controller api);
        JObject restCallGet(string uri, Controller api);
        JObject restCallPost(string uri, object body, Controller api);
        JObject restCallPut(string uri, object body, Controller api);
    }

    public class HttpHelperRestConections : IHttpHelperRestConections
    {
        private string _UrlBackend { get; set; }
        private string _UrlProxy { get; set; }

        private ILogger<HttpHelperRestConections> _Logger;

        public HttpHelperRestConections(IConfiguration configuration, ILogger<HttpHelperRestConections> logger)
        {
            _Logger = logger;
            _UrlBackend = configuration.GetSection("BackendeUrl").GetSection("url").Value;
            _UrlProxy = configuration.GetSection("BackendeUrl").GetSection("proxy").Value;
        }
        public JObject restCallGet(string uri, Controller api)
        {
            JObject jsonHeades = new JObject();
            bool tieneAuthorizationBasic = false;
            try
            {
                using (var client = new WebClient())
                {
                    if (!_UrlProxy.Equals(""))
                    {
                        WebProxy wp = new WebProxy();
                        wp.GetProxy(new System.Uri(_UrlProxy));
                        client.Proxy = wp;
                    }

                    client.Encoding = System.Text.Encoding.UTF8;
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
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
                    var text = client.DownloadString(_UrlBackend + uri);
                    JObject jobject = JObject.Parse(text);
                    jobject.Add("request headers", jsonHeades);
                    return jobject;
                }
            }
            catch (Exception e)
            {
                var errorObject = new JObject();
                errorObject.Add("error", e.Message);
                errorObject.Add("url", _UrlBackend + uri);
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
                using (var client = new WebClient())
                {
                    if (!_UrlProxy.Equals(""))
                    {
                        WebProxy wp = new WebProxy();
                        wp.GetProxy(new System.Uri(_UrlProxy));
                        client.Proxy = wp;
                    }

                    client.Encoding = System.Text.Encoding.UTF8;
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";

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
                    var response = client.UploadString(_UrlBackend + uri, bodyRest);
                    JObject jobject = JObject.Parse(response);

                    jobject.Add("request headers", jsonHeades);
                    return jobject;
                }

            }
            catch (Exception e)
            {
                var errorObject = new JObject();
                errorObject.Add("error", e.Message);
                errorObject.Add("url", _UrlBackend + uri);
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
                using (var client = new WebClient())
                {
                    if (!_UrlProxy.Equals(""))
                    {
                        WebProxy wp = new WebProxy();
                        wp.GetProxy(new System.Uri(_UrlProxy));
                        client.Proxy = wp;
                    }

                    client.Encoding = System.Text.Encoding.UTF8;
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";

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
                    var response = client.UploadString(_UrlBackend + uri + "/update", bodyRest);
                    JObject jobject = JObject.Parse(response);

                    jobject.Add("request headers", jsonHeades);
                    return jobject;
                }
            }
            catch (Exception e)
            {
                var errorObject = new JObject();
                errorObject.Add("error", e.Message);
                errorObject.Add("url", _UrlBackend + uri);
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
                using (var client = new WebClient())
                {
                    if (!_UrlProxy.Equals(""))
                    {
                        WebProxy wp = new WebProxy();
                        wp.GetProxy(new System.Uri(_UrlProxy));
                        client.Proxy = wp;
                    }

                    client.Encoding = System.Text.Encoding.UTF8;
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";

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

                    var response = client.DownloadString(_UrlBackend + uri + "/delete");
                    JObject jobject = JObject.Parse(response);

                    jobject.Add("request headers", jsonHeades);
                    return jobject;
                }
            }
            catch (Exception e)
            {
                var errorObject = new JObject();
                errorObject.Add("error", e.Message);
                errorObject.Add("url", _UrlBackend + uri);
                errorObject.Add("headers", jsonHeades);
                return errorObject;
            }
        }
    }
}

