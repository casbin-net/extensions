using System.Collections.Generic;

namespace Casbin.Extension.Frontend.Models
{
    public interface ISubjectObjects : IReadOnlyList<string>
    {
    }

    internal class SubjectObjects : List<string>, ISubjectObjects
    {
    }

    /// <summary>
    /// [act] = [] { obj }
    /// </summary>
    public interface ISubjectPermissions : IReadOnlyDictionary<string, ISubjectObjects>
    {
    }

    internal class SubjectPermissions : Dictionary<string, ISubjectObjects>, ISubjectPermissions
    {
        internal SubjectPermissions(int capacity)
            : base(capacity)
        {
        }
    }
}
