﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PricingService.WebInterface.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.5.4.0")]
    public partial class RoomPricing : RazorGenerator.Templating.RazorTemplateBase
    {
#line hidden

        #line 3 "..\..\Views\RoomPricing.cshtml"

	public ICollection<Tuple<Guid,decimal>> PricesForRoom{get;set;}

        #line default
        #line hidden

        public override void Execute()
        {


WriteLiteral("\r\n\r\n");


WriteLiteral("\r\n\r\n");


            
            #line 7 "..\..\Views\RoomPricing.cshtml"
 foreach(var price in PricesForRoom) {

            
            #line default
            #line hidden
WriteLiteral("\t<div class=\"price_block\" roomtypeid=\"");


            
            #line 8 "..\..\Views\RoomPricing.cshtml"
                                 Write(price.Item1);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n\t\t<div>Starting at</div>\r\n\t\t<div class=\"price\">$");


            
            #line 10 "..\..\Views\RoomPricing.cshtml"
                 Write(price.Item2);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n\t</div>\r\n");


            
            #line 12 "..\..\Views\RoomPricing.cshtml"
}
            
            #line default
            #line hidden

        }
    }
}
#pragma warning restore 1591
