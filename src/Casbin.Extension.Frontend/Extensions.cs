using NetCasbin.Abstractions;

namespace Casbin.Extension.Frontend
{
    using Models;

    public static class Extensions
    {
        /// <summary>
        /// Get permission set for the user.
        /// </summary>
        /// <example>
        /// {
        ///     "read": [ "data1", "data2" ],
        ///     "write": [ "data1" ]
        /// }
        /// </example>
        /// <param name="enforcer">Application's enforcer</param>
        /// <param name="user">Name of user from a policy</param>
        /// <returns></returns>
        public static ISubjectPermissions CasbinJsGetPermissionForUser(this IEnforcer enforcer, string user)
        {
            var policy = enforcer.GetImplicitPermissionsForUser(user);
            var permissions = new SubjectPermissions(policy.Count);
            for (var i = 0; i < policy.Count; i++)
            {
                var obj = policy[i][1];
                var act = policy[i][2];

                if (!permissions.ContainsKey(act))
                {
                    permissions[act] = new SubjectObjects();
                }

                (permissions[act] as SubjectObjects).Add(obj);
            }

            return permissions;
        }
    }
}
