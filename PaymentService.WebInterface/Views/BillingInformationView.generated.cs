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

namespace PaymentService.WebInterface.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.5.4.0")]
    public partial class BillingInformationView : RazorGenerator.Templating.RazorTemplateBase
    {
#line hidden

        #line 3 "..\..\Views\BillingInformationView.cshtml"

	public Guid ReservationId{get;set;}

        #line default
        #line hidden

        public override void Execute()
        {


WriteLiteral("\r\n\r\n");


WriteLiteral("\r\n\r\n<div class=\"obtain_billing_information\">\r\n<script type=\"text/javascript\" src=" +
"\"/Scripts/Services/PaymentService/ObtainBillingInformation.js\"></script>\r\n<input" +
" type=\"hidden\" name=\"Payment.ReservationId\" value=\"");


            
            #line 9 "..\..\Views\BillingInformationView.cshtml"
                                                    Write(ReservationId);

            
            #line default
            #line hidden
WriteLiteral("\" />\r\nCredit card number: <input type=\"text\" name=\"Payment.CreditCardNumber\" />\r\n" +
"</div>");


        }
    }
}
#pragma warning restore 1591
