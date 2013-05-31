using Infrastructure.Emails;
using ITOps.ReservationCustomerEmails.Commands;
using ITOps.ReservationCustomerEmails.Contracts;
using ITOps.ReservationCustomerEmails.Views;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace ITOps.ReservationCustomerEmails {
	public class Handlers:IHandleMessages<SendReservationAcceptedEmail>,IHandleMessages<SendReservationRejectedEmail> {
		readonly static MailAddress Sender = new MailAddress("reservations@adsdhotel","ADSD Hotel");

		readonly ISmtpClient _smtpClient;
		readonly IProvideRecipientDetails _recipientsDetailsProvider;
		readonly IProvideReservationDetails _reservationDetailsProvider;

		public Handlers(ISmtpClient smtpClient,IProvideRecipientDetails recipientsDetailsProvider, IProvideReservationDetails reservationDetailsProvider) {
			_smtpClient = smtpClient;
			_recipientsDetailsProvider  = recipientsDetailsProvider;
			_reservationDetailsProvider = reservationDetailsProvider;
		}



		public void Handle(SendReservationAcceptedEmail command) {
			var recipientDetails = _recipientsDetailsProvider.GetDetails(command.ReservationId);
			var reservationDetails = _reservationDetailsProvider.GetDetails(command.ReservationId);

			var mailMessage= new MailMessage(Sender,recipientDetails.MailAddress);

			var htmlContent = new ReservationAcceptedEmailView { 
				ReservationId = command.ReservationId, 
				Name = recipientDetails.Name, 
				ArrivalDate = reservationDetails.ArrivalDate,
				CheckoutDate = reservationDetails.CheckoutDate
			}.TransformText();
				
			mailMessage.Subject = "Your reservation has been confirmed";

			mailMessage.IsBodyHtml = true;
			mailMessage.Body = htmlContent;

			_smtpClient.Send(mailMessage);
		}

		public void Handle(SendReservationRejectedEmail message) {
			throw new NotImplementedException();
		}
	}
}
