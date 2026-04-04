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

            public const string ProductCreateError = "An error occurred while creating the product.";
            public const string ProductUpdateError = "An error occurred while updating the product.";
            public const string ProductDeleteError = "An error occurred while deleting the product.";
        }

        public class SuccessMessages
        {
            public const string SuccessMessageKey = "SuccessMessage";

            public const string ProductCreatedSuccessfully = "Product created successfully.";
            public const string ProductUpdatedSuccessfully = "Product updated successfully.";
            public const string ProductDeletedSuccessfully = "Product deleted successfully.";
        }
    }
}
