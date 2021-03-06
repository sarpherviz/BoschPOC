#pragma checksum "/Users/sarp.herviz/Projects/BoschPOC/Bosch.WebUI/Views/Home/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8720539f9ad8ce31ef1c4bdae57249842263bc94"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/sarp.herviz/Projects/BoschPOC/Bosch.WebUI/Views/_ViewImports.cshtml"
using Bosch.WebUI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/sarp.herviz/Projects/BoschPOC/Bosch.WebUI/Views/_ViewImports.cshtml"
using Bosch.WebUI.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8720539f9ad8ce31ef1c4bdae57249842263bc94", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"46c3d0c8ab55ad230204f573bd08b02c4fe91efc", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/Users/sarp.herviz/Projects/BoschPOC/Bosch.WebUI/Views/Home/Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"


<div class=""text-center"">
    <h1 class=""display-4"">Welcome</h1>
</div >

    <div class = ""container"" >
    <br />
    <div style = ""width:90%; margin:0 auto;"" >
    <table id=""example"" class=""table table-striped table-bordered dt-responsive nowrap"" width=""100%"" cellspacing=""0"">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>MachineId</th>
                        <th>Product Barcode</th>
                        <th>Receive Date</th>
                    </tr>
                </thead>
            </table>
            </div>
            </div>

    <script>

    $(document).ready(function() {
        $(""#example"").DataTable({
            ""paging"": false,
            ""searching"": false,
            ""ordering"": false,
            ""processing"": true, // for show progress bar
            ""serverSide"": true, // for process server side
            ""filter"": false, // this is for disable filter (search box)
            ""orderMulti"": false, // for dis");
            WriteLiteral(@"able multiple column at once
            ""ajax"": {
                ""url"": ""/Home/GetProducts"",
                ""type"": ""POST"",
                ""datatype"": ""json""
            },
            ""columns"": [
                { ""data"": ""id"", ""name"": ""Id"", ""autoWidth"": true },
                { ""data"": ""machineId"", ""name"": ""MachineId"", ""autoWidth"": true },
                { ""data"": ""productBarcode"", ""name"": ""ProductBarcode"", ""autoWidth"": true },
                { ""data"": ""receiveDate"", ""name"": ""ReceiveDate"", ""autoWidth"": true }
            ]

        });
    });

</script> ");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
