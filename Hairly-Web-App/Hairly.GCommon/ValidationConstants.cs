namespace Hairly.GCommon
{
    public static class ValidationConstants
    {
        public const int ClientFirstNameMinLength = 2;
        public const int ClientFirstNameMaxLength = 100;
        public const int ClientLastNameMinLength = 2;
        public const int ClientLastNameMaxLength = 100;
        public const int ClientPhoneNumberMaxLength = 15;
        public const string ClientPhoneNumberRegex = @"^(?:\+359|0)8[7-9]\d{7}$"; 
        public const int ClientEmailMaxLength = 100;
        public const int ClientNotesMaxLength = 1000;

        public const int ServiceNameMinLength = 2;
        public const int ServiceNameMaxLength = 100;
        public const int ServiceDescriptionMinLength = 5;
        public const int ServiceDescriptionMaxLength = 1000;
        public const string ServicePriceMinValue = "5";
        public const string ServicePriceMaxValue = "1000";
        public const int ServiceDurationInMinutesMinValue = 5; 
        public const int ServiceDurationInMinutesMaxValue = 480;

        public const int AppointmentNotesMaxLength = 500;

        public const int ProductNameMinLength = 2;
        public const int ProductNameMaxLength = 100;
        public const int ProductDescriptionMaxLength = 1000;
        public const string ProductPriceMinValue = "0.01";
        public const string ProductPriceMaxValue = "500.00";
        public const int ProductQuantityMin = 0;
        public const int ProductQuantityMax = 1000;
        public const int ProductImageUrlMaxLength = 2048;

        public const int ReviewRatingMin = 1;
        public const int ReviewRatingMax = 5;
        public const int ReviewCommentMaxLength = 1000;
    }
}
