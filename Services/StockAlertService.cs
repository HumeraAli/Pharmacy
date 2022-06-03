using System;
using System.Collections.Generic;

namespace Services
{
    public class StockAlertService
    {
        public StockAlertService()
        {
        }

        public ServiceResponse SendNotification(string medicineName, int quantity)
        {
            var response = new ServiceResponse();
            //EmailService(OwnerEmail, AlertMessage)
            var OwnerEmail = "ali@gmail.com";
            var AlertMessage = $"Hey Administrator! Quantity of {medicineName} is {quantity}";
            if (EmailService.Send(OwnerEmail, AlertMessage))
            {
                response.IsSuccess = true;
            }
            else
            {
                List<string> errors = null;
                response.Errors = errors;
            }
            return response;    
        }
    }
}