using NetCasbin;
using NetCasbin.Abstractions;
using System.Collections.Concurrent;

namespace Casbin.Extension.Frontend.Tests.Helpers
{
    internal class EnforcerBuilder
    {
        private const string MODEL_PATH_TEMPLATE = "Assets/{0}/model.conf";
        private const string POLICY_PATH_TEMPLATE = "Assets/{0}/policy.csv";
        private readonly ConcurrentDictionary<string, IEnforcer> _enforcers;

        internal EnforcerBuilder()
        {
            _enforcers = new ConcurrentDictionary<string, IEnforcer>();
        }


        internal IEnforcer GetEnforcer(string schema)
        {
            var modelPath = string.Format(MODEL_PATH_TEMPLATE, schema);
            var policyPath = string.Format(POLICY_PATH_TEMPLATE, schema);
            return _enforcers.GetOrAdd(schema, (_) => new Enforcer(modelPath, policyPath));
        }
    }
}
