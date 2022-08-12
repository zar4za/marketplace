using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grecatech.Security.OpenID
{
    public class OpenIdRequestBuilder
    {
        private string _provider;

        public static OpenIdRequestBuilder Create() => new();

        public OpenIdRequestBuilder WithProvider(string provider)
        {
            _provider = provider;

            return this;
        }

    }
}
