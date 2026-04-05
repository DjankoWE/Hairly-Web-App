namespace Hairly.GCommon
{
    public class ApplicationConstants
    {
        public const string UserRoleName = "User";
        public const string HairdresserRoleName = "Hairdresser";
        public const string AdminRoleName = "Admin";

        public const string AdminAreaName = "Admin";

        public const string DefaultAdminEmail = "stylist@hairly.com";
        public const string DefaultAdminPassword = "Hairly123!";
        public const string DefaultAdminId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d";

        public const string DefaultHairdresserEmail = "hairdresser@hairly.com";
        public const string DefaultHairdresserPassword = "Hairdresser123!";

        public class ErrorMessages
        {
            public const string ErrorMessageKey = "ErrorMessage";

            public const string ClientCreateError = "An error occurred while creating the client.";
            public const string ClientUpdateError = "An error occurred while updating the client.";
            public const string ClientDeleteError = "An error occurred while deleting the client.";

            public const string ServiceCreateError = "An error occurred while creating the service.";
            public const string ServiceUpdateError = "An error occurred while updating the service.";
            public const string ServiceDeleteError = "An error occurred while deleting the service.";

            public const string AppointmentCreateError = "An error occurred while creating the appointment.";
            public const string AppointmentUpdateError = "An error occurred while updating the appointment.";
            public const string AppointmentDeleteError = "An error occurred while deleting the appointment.";

            public const string ProductCreateError = "An error occurred while creating the product.";
            public const string ProductUpdateError = "An error occurred while updating the product.";
            public const string ProductDeleteError = "An error occurred while deleting the product.";

            public const string ReviewCreateError = "An error occurred while creating the review.";
            public const string ReviewDeleteError = "An error occurred while deleting the review.";
            public const string ReviewNotAllowed = "You can only review completed appointments.";
        }

        public class SuccessMessages
        {
            public const string SuccessMessageKey = "SuccessMessage";

            public const string ClientCreatedSuccessfully = "Client created successfully.";
            public const string ClientUpdatedSuccessfully = "Client updated successfully.";
            public const string ClientDeletedSuccessfully = "Client deleted successfully.";

            public const string ServiceCreatedSuccessfully = "Service created successfully.";
            public const string ServiceUpdatedSuccessfully = "Service updated successfully.";
            public const string ServiceDeletedSuccessfully = "Service deleted successfully.";

            public const string AppointmentCreatedSuccessfully = "Appointment created successfully.";
            public const string AppointmentUpdatedSuccessfully = "Appointment updated successfully.";
            public const string AppointmentDeletedSuccessfully = "Appointment deleted successfully.";

            public const string ProductCreatedSuccessfully = "Product created successfully.";
            public const string ProductUpdatedSuccessfully = "Product updated successfully.";
            public const string ProductDeletedSuccessfully = "Product deleted successfully.";

            public const string ReviewCreatedSuccessfully = "Review created successfully.";
            public const string ReviewDeletedSuccessfully = "Review deleted successfully.";
        }
    }
}
