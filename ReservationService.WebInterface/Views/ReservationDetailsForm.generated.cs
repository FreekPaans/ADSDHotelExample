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

namespace ReservationService.WebInterface.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.5.4.0")]
    public partial class ReservationDetailsForm : RazorGenerator.Templating.RazorTemplateBase
    {
#line hidden

        #line 5 "..\..\Views\ReservationDetailsForm.cshtml"

	public Guid Model{get;set;}

        #line default
        #line hidden

        public override void Execute()
        {


WriteLiteral(" \r\n\r\n");



WriteLiteral("\r\n\r\n");


WriteLiteral("\r\n\r\n\r\n<div>\r\n<h2>Please enter your details</h2>\r\n<form action=\"/Rooms/Summary\" cl" +
"ass=\"reservation_details_form\">\r\n<input type=\"hidden\" name=\"Reservation.Reservat" +
"ionId\" value=\"");


            
            #line 13 "..\..\Views\ReservationDetailsForm.cshtml"
                                                        Write(Model);

            
            #line default
            #line hidden
WriteLiteral("\" />\r\n<input type=\"submit\" value=\"Go to confirm\" />\r\n<script src=\"/Scripts/Reserv" +
"ationService/ReservationDetailsForm.js\" type=\"text/javascript\"></script>\r\n</form" +
">\r\n</div>");


        }
    }
}
#pragma warning restore 1591
