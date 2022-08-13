namespace Web.Security.OpenID
{
    public class OpenIdRequestBuilder
    {
        private string _provider;

        private readonly Dictionary<string, string> _openIdMessage = new();

        public enum Spec
        {
            OpenId20
        }

        public enum Mode
        {
            CheckIdSetup
        }

        public static OpenIdRequestBuilder Create() => new();

        public OpenIdRequestBuilder WithProvider(string provider)
        {
            _provider = new Uri(provider).AbsoluteUri;

            return this;
        }

        public OpenIdRequestBuilder WithSpec(Spec spec)
        {
            _openIdMessage.Add(OpenIdConstants.SpecParam, "http://specs.openid.net/auth/2.0");
            return this;
        }

        public OpenIdRequestBuilder WithMode(Mode mode)
        {
            _openIdMessage.Add(OpenIdConstants.ModeParam, "checkid_setup");
            return this;
        }

        public OpenIdRequestBuilder WithIdentifierSelect()
        {
            _openIdMessage.Add(OpenIdConstants.ClaimedIdParam, OpenIdConstants.IdentifierSelect);
            _openIdMessage.Add(OpenIdConstants.IdentityParam, OpenIdConstants.IdentifierSelect);
            return this;
        }

        public OpenIdRequestBuilder WithReturn(string returnTo)
        {
            _openIdMessage.Add(OpenIdConstants.ReturnToParam, new Uri(returnTo).AbsoluteUri);
            return this;
        }

        public string Build()
        {
            var query = string.Join('&', _openIdMessage.Select(x => $"{x.Key}={x.Value}"));
            var uri = _provider + "?" + query;
            return uri;
        }
    }
}
