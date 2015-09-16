using StackExchange.Profiling;
using System;
using System.Linq;

namespace SharpSapRfc.Profiling
{
    public class ProfiledRfcPreparedFunction : RfcPreparedFunction
    {
        private RfcPreparedFunction prepared;
        private MiniProfiler profiler;

        public ProfiledRfcPreparedFunction(RfcPreparedFunction prepared, MiniProfiler profiler)
            : base(prepared.FunctionName)
        {
            this.prepared = prepared;
            this.profiler = profiler;
        }
        
        public override RfcResult Execute()
        {
            string formattedParameters = " Empty";

            if (this.Parameters.Any())
            {
                string[] parameterList = new string[this.Parameters.Count()];
                for (int i = 0; i < parameterList.Length; i++)
                {
                    var parameter = this.Parameters.ElementAt(i);
                    this.prepared.AddParameter(parameter);
                    parameterList[i] = parameter.ToString().Replace("[ {", String.Concat("[",Environment.NewLine, "   {"))
                                                           .Replace("}, {", String.Concat("}, ", Environment.NewLine, "   {"))
                                                           .Replace("} ]", String.Concat("}", Environment.NewLine, "  ]"));
                }
                formattedParameters = string.Concat(Environment.NewLine, "- ", string.Join(string.Concat(Environment.NewLine, "- "), parameterList));
            }

            using (this.profiler.CustomTiming("sap", string.Format("Function: {0}{1}Parameters:{2}", this.FunctionName, Environment.NewLine, formattedParameters)))
            {
                return this.prepared.Execute();
            }
        }
    }
}
