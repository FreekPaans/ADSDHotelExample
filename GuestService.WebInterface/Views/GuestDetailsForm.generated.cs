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

namespace GuestService.WebInterface.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.5.4.0")]
    public partial class GuestDetailsForm : RazorGenerator.Templating.RazorTemplateBase
    {
#line hidden

        #line 4 "..\..\Views\GuestDetailsForm.cshtml"

	public Guid  ReservationId{get;set;}

        #line default
        #line hidden

        public override void Execute()
        {


WriteLiteral("\r\n\r\n\r\n");


WriteLiteral("\r\n\r\n<div class=\"reservation_obtain_guest_details\">\t\r\n<script type=\"text/javascrip" +
"t\" src=\"/Scripts/Services/GuestService/ObtainGuestDetails.js\"></script>\r\n<input " +
"type=\"hidden\" name=\"GuestService.ReservationId\" value=\"");


            
            #line 10 "..\..\Views\GuestDetailsForm.cshtml"
                                                         Write(ReservationId);

            
            #line default
            #line hidden
WriteLiteral(@""" />
<p>
Firstname: <input type=""text"" name=""GuestService.Firstname"" class=""required"" title=""Please enter your first name"" />
</p>
<p>
Lastname: <input type=""text"" name=""GuestService.Lastname""  title=""Please enter your last name"" class=""required"" />
</p>
<p>
E-mail: <input type=""text"" name=""GuestService.Email"" title=""Please enter your email address"" class=""required email""/>
</p>
<p>
Phonenumber: <input type=""text"" name=""GuestService.Phonenumber"" title=""Please enter a phone number"" class=""required"" />
</p>
</div>");


        }
    }
}
#pragma warning restore 1591
