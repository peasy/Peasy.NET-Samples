using System;
using System.Text.RegularExpressions;
using Peasy.DataProxy.Http;
using Peasy;

namespace Orders.com.DAL.Http
{
    public abstract class OrdersDotComHttpProxyBase<T, TKey> : HttpServiceProxyBase<T, TKey> where T : IDomainObject<TKey>
    {
        public OrdersDotComHttpProxyBase(string baseAddress)
        {
            BaseAddress = baseAddress; 
        }

        protected virtual string BaseAddress
        {
            get;set;
            //get
            //{
            //    string hostSettingName = "apiHostNameAddress";
            //    var baseAddress = System.Configuration.ConfigurationManager.AppSettings[hostSettingName];
            //    if (baseAddress == null) throw new Exception($"The setting '{hostSettingName}' in the AppSettings portion of the configuration file was not found.");
            //    return baseAddress;
            //}
        }

        protected override string OnFormatServerError(string message)
        {
            var msg = message.Split(new[] { ':' })[1];
            Regex rgx = new Regex("[\\{\\}\"]"); // get rid of the quotes and braces
            msg = rgx.Replace(msg, "").Trim();
            return msg;
        }
    }
}
