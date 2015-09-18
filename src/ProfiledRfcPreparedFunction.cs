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

        public override RfcPreparedFunction Prepare()
        {
            foreach (var param in this.Parameters)
                this.prepared.AddParameter(param);

            return this.prepared.Prepare();
        }
        
        public override RfcResult Execute()
        {
            this.Prepare();

            using (this.profiler.CustomTiming("sap", this.ToString()))
            {
                return this.prepared.Execute();
            }
        }

        public override string ToString()
        {
            return this.prepared.ToString();
        }

    }
}
