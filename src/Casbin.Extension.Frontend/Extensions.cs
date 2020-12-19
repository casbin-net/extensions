using NetCasbin.Abstractions;
using NetCasbin.Model;
using System.Collections.Generic;
using System.Text;

namespace Casbin.Extension.Frontend
{
    using Providers;

    public static class Extensions
    {
        private static readonly string[] _policyKeys = new[] { "r", "p", "g", "e", "m" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string CasbinJsGetPermissionForUser(this IEnforcer enforcer, string user)
        {
            var model = enforcer.GetModel().ToKeyDictionary();
            var sb = new StringBuilder();
            sb.AppendLine("[request_definition]")
              .AppendLine(model["r"])
              .AppendLine("[policy_definition]")
              .AppendLine(model["p"]);
            if (model["g"] != null)
            {
                sb.AppendLine("[role_definition]")
                  .AppendLine(model["g"]);
            }
            sb.AppendLine("[policy_effect]")
              .AppendLine(model["e"])
              .AppendLine("[matchers]")
              .AppendLine(model["m"]);

            var jb = new JsonBuilder()
                .WriteOpenObject()
                .WriteKey("m")
                .WriteValue(sb.ToString());
            sb.Clear();

            var policy = enforcer.GetPolicy();
            jb.WriteKey("p");
            foreach(var p in policy)
            {
                jb.WriteValue(p);
            }

            jb.WriteCloseObject();
            return jb.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static IReadOnlyDictionary<string, string> ToKeyDictionary(this Model model)
        {
            var dict = model.Model;
            var result = new Dictionary<string, string>(dict.Keys.Count);
            foreach (var key in _policyKeys)
            {
                string value = null;
                if (dict.ContainsKey(key) && dict[key].ContainsKey(key))
                {
                    var assertion = dict[key][key];
                    value = assertion.Value?.Replace("_", ".");
                }
                result.Add(key, value);
            }
            return result;
        }
    }
}
