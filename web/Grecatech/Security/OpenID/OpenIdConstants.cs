using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grecatech.Security.OpenID
{
    internal class OpenIdConstants
    {
        public const string SpecParam = "openid.ns";
        public const string ModeParam = "openid.mode";
        public const string ClaimedIdParam = "openid.claimed_id";
        public const string IdentityParam = "openid.identity";
        public const string AssocHadleParam = "openid.assoc_handle";
        public const string ReturnToParam = "openid.return_to";
        public const string RealmParam = "openid.realm";

        public const string IdentifierSelect = "http://specs.openid.net/auth/2.0/identifier_select";
    }
}
