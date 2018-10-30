using System;
using System.Collections.Generic;
using System.Text;
using App;
using GeoGo.Model;

namespace GeoGo.Model
{
    public static class User
    {
        public static String nickname { get; set; }
        public static String name { get; set; }
        public static String picture { get; set; }




        private static IdentityModel.OidcClient.LoginResult loginResult;
        public static void SetLoginResult(IdentityModel.OidcClient.LoginResult loginResult2)
        {
            loginResult = loginResult2;
            var e = loginResult.User.Claims;
            foreach (var claim in loginResult.User.Claims)
            {
                if (claim.Type.Equals("nickname"))
                {
                    nickname = claim.Value;
                }
                else if (claim.Type.Equals("name"))
                {
                    name = claim.Value;
                }
                else if (claim.Type.Equals("picture"))
                {
                    picture = claim.Value;
                }


            }


        }

        public static void DebugResult()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"ID Token: {loginResult.IdentityToken}");
            sb.AppendLine($"Access Token: {loginResult.AccessToken}");
            sb.AppendLine($"Refresh Token: {loginResult.RefreshToken}");
            sb.AppendLine();
            sb.AppendLine("-- Claims --");
            foreach (var claim in loginResult.User.Claims)
            {
                sb.AppendLine($"{claim.Type} = {claim.Value}");
            }
            System.Diagnostics.Debug.WriteLine(sb.ToString());
        }


    }
}
