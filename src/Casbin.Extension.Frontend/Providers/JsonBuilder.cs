using System.Collections.Generic;
using System.Text;

namespace Casbin.Extension.Frontend.Providers
{
    /// <summary>
    /// Simple json builder.
    /// </summary>
    /// <remarks>Own implementation 'cauze System.Text.Json supports .Net >= 4.6</remarks>
    internal class JsonBuilder
    {
        private readonly StringBuilder _buffer;
        private bool _thereIsKey;

        internal JsonBuilder()
        {
            _buffer = new StringBuilder();
            _thereIsKey = false;
        }

        public JsonBuilder WriteOpenObject()
        {
            _buffer.Append("{");
            return this;
        }

        public JsonBuilder WriteCloseObject()
        {
            _buffer.Append("}");
            _thereIsKey = false;
            return this;
        }

        public JsonBuilder WriteKey(string key)
        {
            if (_thereIsKey)
            {
                _buffer.Append(",");
            }

            _buffer.Append("\"").Append(key).Append("\":");
            _thereIsKey = true;
            return this;
        }

        public JsonBuilder WriteValue(string value)
        {
            _buffer.Append("\"").Append(value).Append("\"");
            return this;
        }

        public JsonBuilder WriteValue(ICollection<string> value)
        {
            _buffer.Append("[");
            var isValue = false;
            foreach (var v in value)
            {
                if (isValue)
                {
                    _buffer.Append(",");
                }
                WriteValue(v);
                isValue = true;
            }
            _buffer.Append("]");
            return this;
        }


        public override string ToString()
        {
            return _buffer.ToString();
        }
    }
}
