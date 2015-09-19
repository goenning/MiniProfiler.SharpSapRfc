# MiniProfiler.SharpSapRfc

If you are building a Web Application, you should be using MiniProfiler. It's totally awesome!

This package is a custom provider for [MiniProfiler](http://miniprofiler.com) that automatically records timing of all calls made with [SharpSapRfc](https://github.com/goenning/SharpSapRfc).

## How to install

    PM> Install-Package MiniProfiler.SharpSapRfc

## How to configure

`MiniProfiler.SharpSapRfc` provides a new SAP Connection class called `ProfiledSapRfConnection`. You have to wrap your real SAP Connection with this one and you are good to go. It's simple as that!

#### Without profiling
```C#
using (SapRfcConnection sap = new SoapSapRfcConnection("SAP"))
{
    var result = conn.ExecuteFunction("Z_SSRT_GET_ALL_CUSTOMERS");
    var customers = result.GetTable<ZCustomer>("t_customers");
}
```
#### With profiling
```C#
using (SapRfcConnection sap = new ProfiledSapRfcConnection(new SoapSapRfcConnection("SAP"), MiniProfiler.Current))
{
    var result = conn.ExecuteFunction("Z_SSRT_GET_ALL_CUSTOMERS");
    var customers = result.GetTable<ZCustomer>("t_customers");
}
```

## What you should see

![Sample 1](https://raw.githubusercontent.com/goenning/MiniProfiler.SharpSapRfc/master/samples/profile-sap-1.PNG)
![Sample 2](https://raw.githubusercontent.com/goenning/MiniProfiler.SharpSapRfc/master/samples/profile-sap-2.PNG)
