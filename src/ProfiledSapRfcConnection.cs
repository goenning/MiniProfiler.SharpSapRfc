using SharpSapRfc;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpSapRfc.Profiling
{
    public class ProfiledSapRfcConnection : SapRfcConnection
    {
        private SapRfcConnection innerConnection;
        private MiniProfiler profiler;

        public ProfiledSapRfcConnection(SapRfcConnection connection, MiniProfiler profiler)
        {
            this.innerConnection = connection;
            this.profiler = profiler;
        }

        public override void Dispose()
        {
            this.innerConnection.Dispose();
        }

        public override RfcPreparedFunction PrepareFunction(string functionName)
        {
            return new ProfiledRfcPreparedFunction(this.innerConnection.PrepareFunction(functionName), this.profiler);
        }

        public override void SetTimeout(int timeout)
        {
            this.innerConnection.SetTimeout(timeout);
        }

        public override RfcStructureMapper GetStructureMapper()
        {
            return this.innerConnection.GetStructureMapper();
        }
    }
}
