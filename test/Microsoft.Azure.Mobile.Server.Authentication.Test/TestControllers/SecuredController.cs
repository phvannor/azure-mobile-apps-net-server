﻿// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------------------- 

using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace Microsoft.Azure.Mobile.Server.TestControllers
{
    public class SecuredController : ApiController
    {
        [Route("api/secured/anonymous")]
        public IHttpActionResult GetAnonymous()
        {
            return this.GetUserDetails();
        }

        [Authorize]
        [Route("api/secured/application")]
        public HttpResponseMessage GetApplication()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Authorize]
        [Route("api/secured/user")]
        public HttpResponseMessage GetUser()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Authorize]
        [Route("api/secured/admin")]
        public HttpResponseMessage GetAdmin()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Route("api/secured/nothing")]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private IHttpActionResult GetUserDetails()
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;

            JObject details = null;
            Claim userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            string userId = null;

            if (userIdClaim != null)
            {
                userId = userIdClaim.Value;
            }

            details = new JObject
            {
                { "id", userId },
            };

            return this.Json(details);
        }
    }
}
