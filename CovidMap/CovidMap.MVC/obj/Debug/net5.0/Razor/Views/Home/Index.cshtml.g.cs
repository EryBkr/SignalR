#pragma checksum "C:\Users\Blackerback\OneDrive\Masaüstü\NetCoreSignalR\CovidMap\CovidMap.MVC\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fc0361a31a538e01485b733f31d38b6313df1500"
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
#line 1 "C:\Users\Blackerback\OneDrive\Masaüstü\NetCoreSignalR\CovidMap\CovidMap.MVC\Views\_ViewImports.cshtml"
using CovidMap.MVC;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fc0361a31a538e01485b733f31d38b6313df1500", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bf7ad07c3f59139ad7f3cb10c073b02e7ca8a10e", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Blackerback\OneDrive\Masaüstü\NetCoreSignalR\CovidMap\CovidMap.MVC\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container-fluid\">\r\n    <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n            <div id=\"myChart\" style=\"width: 100%; height: 700px\"></div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n\r\n");
                WriteLiteral("    <script type=\"text/javascript\" src=\"https://www.gstatic.com/charts/loader.js\"></script>\r\n\r\n");
                WriteLiteral(@"    <script src=""https://cdnjs.cloudflare.com/ajax/libs/aspnet-signalr/1.0.27/signalr.js"" integrity=""sha512-RnZfhh4QUtg97mSlY5mFfp/yqJpEUBmAUU6JI3xrOWshmw9CJB4ejsd7YTB15JmUWMHMB7s4NrqueF+zrskEtQ=="" crossorigin=""anonymous"" referrerpolicy=""no-referrer""></script>

    <script>
        $(document).ready(() => {

            var covidChartList = new Array();
            //ChartJS içerisinde ki dizimin ilk satırını ve label kısmını doldurdum
            covidChartList.push([""Tarih"", ""İstanul"", ""Ankara"", ""İzmir"", ""Canakkale"", ""Antalya""]);

            //Socket Connection
            var connection = new signalR.HubConnectionBuilder().withUrl(""https://localhost:44372/MyHub"").build();

            connection.start().then(() => {

                //Server daki metodu tetikledik
                connection.invoke(""GetCovidList"");

            }).catch((err) => {
                alert(`Hata Mesajı: ${err}`);
            });

            //Serverdaki GetCovidList metodunun tetiklenmesinden sonra tetikl");
                WriteLiteral(@"enecek metodumuz
            connection.on(""ReceiveCovidList"", (covidList) => {

                //Dizinin ilk datası hariç(label) hepsini sil.Baştan yüklendiği zaman üzerine eklemesin
                covidChartList = covidChartList.splice(0, 1);

                //Serverdan client tetiklemisiyle gelen datalarımdan bir dizi oluşturdum
                covidList.forEach((item) => {
                    covidChartList.push([item.date, item.totalIstanbulVariant, item.totalAnkaraVariant, item.totalIzmirVariant, item.totalCanakkaleVariant, item.totalAntalyaVariant]);
                });

                console.log(covidChartList);
                console.log(covidList);

                google.charts.load('current', { 'packages': ['corechart'] });
                google.charts.setOnLoadCallback(drawChart);
            });


           

            function drawChart() {
                //Oluşan diziyi parametre olarak verdim
                var data = google.visualization.arrayToDataTable(co");
                WriteLiteral(@"vidChartList);

                var options = {
                    title: 'Covid 19 Chart',
                    curveType: 'function',
                    legend: { position: 'bottom' }
                };

                var chart = new google.visualization.LineChart(document.getElementById('myChart'));

                chart.draw(data, options);
            }

        });
    </script>
");
            }
            );
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
