using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net;
using System.Text;
using System.Threading;
using System.Security.Principal;

namespace WebApiDemo.Models
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if(actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            else
            {
                string authenticationtoken = actionContext.Request.Headers.Authorization.Parameter;
                var decodedauthenticationtoken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationtoken));
                string[] usernamepasswordarray = decodedauthenticationtoken.Split(':');
                string username = usernamepasswordarray[0];
                string password = usernamepasswordarray[1];
                if (EmpolyeeSecurity.Login(username, password))
                {

                    Thread.CurrentPrincipal = new GenericPrincipal( new GenericIdentity(username),null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }

    }
}

//ADD COMMENT